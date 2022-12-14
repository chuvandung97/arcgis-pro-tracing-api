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
    public class FeederTraceService : TraceService
    {
        public class FeederTraceTempGlobalIdComparer : IEqualityComparer<FeederTraceTemp>
        {
            public bool Equals(FeederTraceTemp x, FeederTraceTemp y)
            {
                return x.EGID.Equals(y.EGID, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(FeederTraceTemp node)
            {
                return node.EGID.GetHashCode();
            }
        }

        public override async Task<Dictionary<string, object>> RunTrace(List<NetworkInfo> parameters)
        {
            var results = new Dictionary<string, object>();

            //List<NetworkInfo> networkInfos = JsonConvert.DeserializeObject<List<NetworkInfo>>(parameters);
            List<FeatureServiceInfo> featureServices = await Transform(parameters);
            foreach (FeatureServiceInfo featureService in featureServices)
            {
                try
                {
                    HashSet<FeederTraceTemp> tempResults = new HashSet<FeederTraceTemp>(new FeederTraceTempGlobalIdComparer());
                    HashSet<FeederTraceResult> transformResults = new HashSet<FeederTraceResult>();

                    JObject items = await GetConnectivityItemsAndElementItems(featureService, parameters);
                    HashSet<ConnectivityItem> connectivityItems = items["ConnectivityItems"].ToObject<HashSet<ConnectivityItem>>();
                    HashSet<ElementItem> elementItems = items["ElementItems"].ToObject<HashSet<ElementItem>>();

                    Node rootNode = new Node()
                    {
                        ObjectId = ConvertObjectIdToNetworkId(featureService.ObjectId, featureService.GlobalId, elementItems),
                        GlobalId = featureService.GlobalId,
                    };
                    HashSet<Node> graphs = CreateGraph(
                        rootNode,
                        connectivityItems,
                        elementItems
                    );
                    int firstRank = graphs.ToList()[0].Rank;
                    int lastRank = graphs.ToList()[graphs.Count - 1].Rank;

                    for (int i = firstRank; i <= lastRank; i++)
                    {
                        List<Node> nodes = graphs.Where(x => x.Rank == i).ToList();
                        foreach (Node node in nodes)
                        {
                            FeederTraceTemp tempResult1 = tempResults.FirstOrDefault(item => item.EGID == node.Parent.ViaGlobalId);
                            FeederTraceTemp tempResult2 = tempResults.FirstOrDefault(item => item.EGID == node.Parent.GlobalId);
                            //Add line
                            if (!string.IsNullOrEmpty(node.ViaGlobalId))
                            {
                                tempResults.Add(new FeederTraceTemp()
                                {
                                    Rank1 = node.Parent.Rank,
                                    Rank2 = node.Parent.Rank * 2,
                                    EID = node.ViaObjectId,
                                    EGID = node.ViaGlobalId,
                                    FIDs = new List<long?> { node.Parent.ObjectId },
                                    TIDs = graphs.Where(x => x.ViaGlobalId == node.ViaGlobalId).Select(x => x.ObjectId).ToList(),
                                    Path1 = tempResult1 != null ? tempResult1.Path1.Concat(new List<long?>() { node.ViaObjectId }).ToList() : new List<long?>() { node.ViaObjectId },
                                    Path2 = tempResult2 != null ? tempResult2.Path2.Concat(new List<long?>() { node.ViaObjectId }).ToList() : new List<long?>() { node.ViaObjectId }
                                });
                            }
                            //Add node
                            tempResults.Add(new FeederTraceTemp()
                            {
                                Rank1 = null,
                                Rank2 = node.Parent == null ? node.Rank : node.Rank + node.Parent.Rank,
                                EID = node.ObjectId,
                                EGID = node.GlobalId,
                                FIDs = new List<long?> { node.ViaObjectId },
                                TIDs = graphs.Where(x => x.GlobalId != node.GlobalId).Where(x => x.Parent != null && x.Parent.GlobalId == node.GlobalId).Select(x => x.ViaObjectId).ToList(),
                                Path1 = new List<long?>(),
                                Path2 = tempResult2 != null ? tempResult2.Path2.Concat(new List<long?>() { node.ViaObjectId, node.ObjectId }).ToList() : new List<long?>() { node.ViaObjectId, node.ObjectId },
                            });
                        }
                    }
                    foreach (FeederTraceTemp tempResult in tempResults)
                    {
                        transformResults.Add(new FeederTraceResult()
                        {
                            Rank1 = tempResult.Rank1,
                            Rank2 = tempResult.Rank2,
                            EID = tempResult.EID,
                            EGID = tempResult.EGID,
                            FIDs = string.Join(",", tempResult.FIDs.Distinct()),
                            TIDs = string.Join(",", tempResult.TIDs.Distinct()),
                            EndFlag = tempResult.TIDs.Any() ? false : true,
                            Path1 = string.Join(",", tempResult.Path1).Trim(','),
                            Path2 = string.Join(",", tempResult.Path2).Trim(',')

                        });
                    }
                    results.Add($"{featureService.ObjectId}{featureService.AssetGroup}{featureService.AssetType}", transformResults.OrderBy(x => x.Rank2).ToList());
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
            return $"{(int)Constant.Layers.ELECTRIC_DEVICE}:{whereClause};{(int)Constant.Layers.ELECTRIC_LINE}:{whereClause};{(int)Constant.Layers.ELECTRIC_JUNCION}:{whereClause}";
        }
        public override Dictionary<string, string> GetTraceConfig(FeatureServiceInfo featureService, List<NetworkInfo> parameters)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("f", "json");
            dict.Add("traceType", "downstream");
            dict.Add("traceLocations", JsonConvert.SerializeObject(new List<object>()
            {
                new {
                    traceLocationType = "startingPoint",
                    globalId = featureService.GlobalId,
                    terminalId = featureService.TerminalId,
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
                domainNetworkName = Constant.DOMAIN_NETWORK_NAME,
                tierName = featureService.Tier,
                targetTierName = featureService.TargetTier,
                subnetworkName = "",
                diagramTemplateName = "",
                shortestPathNetworkAttributeName = "",
                filterBitsetNetworkAttributeName = "",
                traversabilityScope = "junctionsAndEdges",
                conditionBarriers = new List<object>()
                {
                    new
                    {
                        name = "Operational Device Status",
                        type = "networkAttribute",
                        @operator = "equal",
                        value = 1,
                        combineUsingOr = false,
                        isSpecificValue = true
                    },
                    /*new
                    {
                        name = "LifecycleStatus",
                        type = "networkAttribute",
                        @operator = "notEqual",
                        value = 4,
                        combineUsingOr = false,
                        isSpecificValue = true
                    }*/
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
