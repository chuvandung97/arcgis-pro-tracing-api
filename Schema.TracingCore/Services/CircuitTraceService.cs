using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.TracingCore.Models;
using Schema.TracingCore.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.TracingCore.Services
{
    public class CircuitTraceService : TraceService
    {
        public override async Task<Dictionary<string, object>> RunTrace(List<NetworkInfo> parameters)
        {
            //List<NetworkInfo> networkInfos = JsonConvert.DeserializeObject<List<NetworkInfo>>(parameters.NetworkInfos);
            var results = new Dictionary<string, object>();

            List<FeatureServiceInfo> featureServices = await Transform(parameters);
            foreach (FeatureServiceInfo featureService in featureServices)
            {
                try
                {
                    if (Convert.ToInt16(featureService.AssetGroup) == Constant.BUS_BAR_ASSET_GROUP) continue;
                    List<SequenceCircuitTraceResult> transformResults = new List<SequenceCircuitTraceResult>();

                    JObject items = await GetConnectivityItemsAndElementItems(featureService, parameters);
                    HashSet<ConnectivityItem> connectivityItems = items["ConnectivityItems"].ToObject<HashSet<ConnectivityItem>>();
                    HashSet<ElementItem> elementItems = items["ElementItems"].ToObject<HashSet<ElementItem>>();

                    var genesisConnectivityItem = connectivityItems.FirstOrDefault(item => item.ViaGlobalId == featureService.GlobalId);

                    Node fromRootNode = new Node()
                    {
                        ObjectId = ConvertObjectIdToNetworkId(genesisConnectivityItem.FromObjectId, genesisConnectivityItem.FromGlobalId, elementItems),
                        GlobalId = genesisConnectivityItem.FromGlobalId,
                        NetworkSourceId = genesisConnectivityItem.FromNetworkSourceId
                    };
                    Node toRootNode = new Node()
                    {
                        ObjectId = ConvertObjectIdToNetworkId(genesisConnectivityItem.ToObjectId, genesisConnectivityItem.ToGlobalId, elementItems),
                        GlobalId = genesisConnectivityItem.ToGlobalId,
                        NetworkSourceId = genesisConnectivityItem.ToNetworkSourceId
                    };
                    connectivityItems.Remove(genesisConnectivityItem);

                    HashSet<Node> fromGraphs = CreateGraph(
                        fromRootNode,
                        connectivityItems,
                        elementItems
                    );
                    HashSet<Node> toGraphs = CreateGraph(
                        toRootNode,
                        connectivityItems,
                        elementItems
                    );

                    HashSet<Node> graphs = new HashSet<Node>(fromGraphs.Concat(toGraphs));

                    List<string> parentGlobalIds = graphs.Where(x => x.Parent != null).Select(x => x.Parent.GlobalId).ToList();
                    List<Node> endFlagNodes = graphs.Where(x => !parentGlobalIds.Contains(x.GlobalId)).ToList();
                    List<Node> endFlagNodesCopy = new List<Node>(endFlagNodes);

                    int traceId = 1;
                    foreach (Node source in endFlagNodes)
                    {
                        var newConnectivityItems = items["ConnectivityItems"].ToObject<HashSet<ConnectivityItem>>();
                        Node rootSource = new Node()
                        {
                            ObjectId = source.ObjectId,
                            GlobalId = source.GlobalId,
                            NetworkSourceId = source.NetworkSourceId,
                        };
                        HashSet<Node> startNodeGraphs = CreateGraph(rootSource, newConnectivityItems, elementItems);
                        foreach (Node target in endFlagNodesCopy)
                        {
                            Node endNode = startNodeGraphs.FirstOrDefault(x => x.GlobalId == target.GlobalId);
                            if (target.GlobalId == source.GlobalId || endNode == null) continue;

                            List<SequenceCircuitTraceTemp> tempResults = new List<SequenceCircuitTraceTemp>();

                            // Add root
                            tempResults.Add(new SequenceCircuitTraceTemp()
                            {
                                Rank = 1,
                                EID = endNode.ObjectId,
                                EGID = endNode.GlobalId,
                                FIDs = new List<long?>(),
                                TIDs = new List<long?>() { endNode.ViaObjectId },
                                Path = new List<long?>() { endNode.ObjectId }
                            });

                            while (endNode.Parent != null)
                            {
                                //Add line
                                tempResults.Add(new SequenceCircuitTraceTemp()
                                {
                                    Rank = tempResults[tempResults.Count - 1].Rank + 1,
                                    EID = endNode.ViaObjectId,
                                    EGID = endNode.ViaGlobalId,
                                    FIDs = new List<long?>() { endNode.ObjectId },
                                    TIDs = new List<long?>() { endNode.Parent.ObjectId },
                                    Path = tempResults[tempResults.Count - 1].Path.Concat(new List<long?> { endNode.ViaObjectId }).ToList()
                                });

                                // Add node
                                tempResults.Add(new SequenceCircuitTraceTemp()
                                {
                                    Rank = tempResults[tempResults.Count - 1].Rank + 1,
                                    EID =endNode.Parent.ObjectId,
                                    EGID = endNode.Parent.GlobalId,
                                    FIDs = new List<long?> { endNode.ViaObjectId },
                                    TIDs = Convert.ToBoolean(endNode.Parent.ViaObjectId) ? new List<long?> { endNode.Parent.ViaObjectId } : new List<long?>(),
                                    Path = tempResults[tempResults.Count - 1].Path.Concat(new List<long?> { endNode.Parent.ObjectId }).ToList()
                                });
                                endNode = endNode.Parent;
                            }

                            foreach (var tempResult in tempResults)
                            {
                                transformResults.Add(new SequenceCircuitTraceResult()
                                {
                                    TraceId = traceId,
                                    Rank = tempResult.Rank,
                                    EID = tempResult.EID,
                                    EGID = tempResult.EGID,
                                    FIDs = string.Join(",", tempResult.FIDs.Distinct()).Trim(','),
                                    TIDs = string.Join(",", tempResult.TIDs.Distinct()).Trim(','),
                                    EndFlag = (tempResult.FIDs.Any() && tempResult.TIDs.Any()) ? false : true,
                                    Path = string.Join(",", tempResult.Path).Trim(',')
                                });

                            }
                            traceId++;
                        }
                        endFlagNodesCopy.Remove(source);
                    }
                    results.Add($"{featureService.ObjectId}{featureService.AssetGroup}{featureService.AssetType}", transformResults);
                }
                catch
                {
                    continue;
                }
            }
            return results;
        }
        public override string GetLayerDefs(FeatureServiceInfo featureServiceInfo)
        {
            string whereClause = $"OBJECTID={featureServiceInfo.ObjectId} AND ASSETTYPE={featureServiceInfo.AssetType} AND ASSETGROUP={featureServiceInfo.AssetGroup}";
            return $"{(int)Constant.Layers.ELECTRIC_LINE}:{whereClause}";
        }

        public override Dictionary<string, string> GetTraceConfig(FeatureServiceInfo featureService, List<NetworkInfo> parameters)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("f", "json");
            dict.Add("traceType", "connected");
            dict.Add("traceLocations", JsonConvert.SerializeObject(new List<object>()
            {
                new {
                    traceLocationType = "startingPoint",
                    globalId = featureService.GlobalId,
                    percentAlong = featureService.PercentAlong
                }
            }));
            dict.Add("traceConfiguration", JsonConvert.SerializeObject(new
            {
                includeContainers = true,
                includeContent = false,
                includeStructures = true,
                includeBarriers = true,
                validateConsistency = true,
                validateLocatability = false,
                includeIsolated = false,
                ignoreBarriersAtStartingPoints = false,
                includeUpToFirstSpatialContainer = false,
                allowIndeterminateFlow = true,
                domainNetworkName = "",
                tierName = "",
                targetTierName = "",
                subnetworkName = "",
                diagramTemplateName = "",
                shortestPathNetworkAttributeName = "",
                filterBitsetNetworkAttributeName = "",
                traversabilityScope = "junctionsAndEdges",
                conditionBarriers = new List<object>()
                {
                    new
                    {
                        name = "Device Asset Group",
                        type = "networkAttribute",
                        @operator = "greaterThanEqual",
                        value = 0,
                        combineUsingOr = false,
                        isSpecificValue = true
                    },
                    new
                    {
                        name = "Device Asset Group",
                        type = "networkAttribute",
                        @operator = "notEqual",
                        value = Constant.SERVICE_POINT_ASSET_GROUP,
                        combineUsingOr = false,
                        isSpecificValue = true
                    }
                },
                functionBarriers = new List<string>(),
                arcadeExpressionBarrier = "",
                filterBarriers = new List<string>(),
                filterFunctionBarriers = new List<string>(),
                filterScope = "junctionsAndEdges",
                functions = new List<string>(),
                nearestNeighbor = "",
                outputFilters = new List<string>(),
                outputConditions = new List<string>(),
                propagators = new List<string>()
            }));
            dict.Add("resultTypes", JsonConvert.SerializeObject(new List<object>()
            {
                new
                {
                    type = "connectivity",
                    includeGeometry = false,
                    includePropagatedValues = false,
                    includeDomainDescriptions = false,
                    networkAttributeNames = new List<string>(),
                    diagramTemplateName = "",
                    resultTypeFields = new List<string>()
                },
                new
                {
                    type = "elements",
                    includeGeometry = false,
                    includePropagatedValues = false,
                    includeDomainDescriptions = false,
                    networkAttributeNames = new List<string>(),
                    diagramTemplateName = "",
                    resultTypeFields = new List<string>()
                }
            }));
            dict.Add("async", "false");

            return dict;
        }
    }
}
