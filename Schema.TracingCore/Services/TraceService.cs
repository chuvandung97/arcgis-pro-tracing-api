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
    public abstract class TraceService : ITraceService
    {
        /*protected ApiService _apiService;
        public TraceService(ApiService apiService)
        {
            _apiService = apiService;
        }*/
        public class Node
        {
            public int Rank;
            public long? ObjectId;
            public string GlobalId = string.Empty;
            public int NetworkSourceId;
            public Node Parent;
            public long? ViaObjectId;
            public string ViaGlobalId = string.Empty;
            public int? ViaNetworkSourceId;
        }

        public class NodeGlobalIdComparer : IEqualityComparer<Node>
        {
            public bool Equals(Node x, Node y)
            {
                return x.GlobalId.Equals(y.GlobalId, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(Node node)
            {
                return node.GlobalId.GetHashCode();
            }
        }

        private HashSet<ConnectivityItem> _connectivityItems;
        private HashSet<ElementItem> _elementItems;

        public HashSet<ConnectivityItem> ConnectivityItems
        {
            get { return _connectivityItems; }
            set { _connectivityItems = value; }
        }

        public HashSet<ElementItem> ElementItems
        {
            get { return _elementItems; }
            set { _elementItems = value; }
        }

        public FeatureServiceInfo GetFeatureServiceInfo(string networkId)
        {
            return new FeatureServiceInfo()
            {
                ObjectId = (long)Convert.ToDouble(networkId.Substring(0, networkId.Length - 7)),
                AssetType = networkId.Substring(networkId.Length - 4, 4),
                AssetGroup = networkId.Substring(networkId.Length - 7, 3)
            };
        }

        public long ConvertObjectIdToNetworkId(long objectId, string globalId, HashSet<ElementItem> elementItems)
        {
            ElementItem element = elementItems.FirstOrDefault(x => x.GlobalId == globalId);
            if (element == null) return objectId;
            return long.Parse($"{objectId}{element.AssetGroupCode.ToString("000")}{element.AssetTypeCode.ToString("0000")}");
        }

        public async Task<List<FeatureServiceInfo>> Transform(List<NetworkInfo> networkInfos)
        {
            var features = new List<FeatureServiceInfo>();
            foreach (NetworkInfo networkInfo in networkInfos)
            {
                FeatureServiceInfo featureServiceInfo = GetFeatureServiceInfo(networkInfo.NetworkId);
                string layerDefs = GetLayerDefs(featureServiceInfo);

                
                JObject layers = await new ApiService().QueryFeatureService(layerDefs);
                var layerFilter = layers["layers"].FirstOrDefault(x => x["features"].Any());
                if (layerFilter == null) continue;
                features.Add(new FeatureServiceInfo()
                {
                    ObjectId = featureServiceInfo.ObjectId,
                    AssetGroup = featureServiceInfo.AssetGroup,
                    AssetType = featureServiceInfo.AssetType,
                    GlobalId = (string)layerFilter["features"][0]["attributes"]["globalid"],
                    LayerId = (int)layerFilter["id"],
                    TerminalId = networkInfo.TerminalId,
                    PercentAlong = networkInfo.PercentAlong,
                    Tier = networkInfo.Tier,
                    TargetTier = networkInfo.TargetTier
                });
            }
            return features;
        }
        public virtual string GetLayerDefs(FeatureServiceInfo featureServiceInfo)
        {
            string whereClause = $"OBJECTID={featureServiceInfo.ObjectId} AND ASSETTYPE={featureServiceInfo.AssetType} AND ASSETGROUP={featureServiceInfo.AssetGroup}";
            return $"{(int)Constant.Layers.ELECTRIC_DEVICE}:{whereClause};{(int)Constant.Layers.ELECTRIC_LINE}:{whereClause};{(int)Constant.Layers.ELECTRIC_JUNCION}:{whereClause}";
        }
        public virtual async Task<JObject> GetConnectivityItemsAndElementItems(FeatureServiceInfo featureServiceInfo, List<NetworkInfo> parameters)
        {
            var dict = GetTraceConfig(featureServiceInfo, parameters);
            JObject traceResults = await new ApiService().Trace(dict);
            ConnectivityItems = new HashSet<ConnectivityItem>(traceResults["traceResults"]["connectivity"].ToObject<HashSet<ConnectivityItem>>().Where(x => x.ViaNetworkSourceId == (long)Constant.sourceMapping.ELECTRIC_LINE));
            ElementItems = traceResults["traceResults"]["elements"].ToObject<HashSet<ElementItem>>();

            return JObject.FromObject(
                new {
                    ConnectivityItems = ConnectivityItems,
                    ElementItems = ElementItems
                }
            );
        }
        public virtual Dictionary<string, string> GetTraceConfig(FeatureServiceInfo featureService, List<NetworkInfo> parameters)
        {
            return new Dictionary<string, string>();
        }
        public abstract Task<Dictionary<string, List<FeederTraceResult>>> RunTrace(List<NetworkInfo> parameters);

        protected HashSet<Node> CreateGraph(Node node, HashSet<ConnectivityItem> items)
        {
            HashSet<Node> graphs = new HashSet<Node>(new NodeGlobalIdComparer());
            HashSet<ConnectivityItem> connectivityItems = new HashSet<ConnectivityItem>(items);
            graphs.Add(node);
            var q = new Queue<Node>();
            q.Enqueue(node);

            while (q.Count > 0)
            {
                var currentNode = q.Dequeue();
                if (currentNode == null) continue;

                HashSet<Node> childrens = FindChildren(currentNode, ref connectivityItems);
                foreach (Node child in childrens)
                {
                    q.Enqueue(child);
                    graphs.Add(child);
                }
            }

            return graphs;
        }

        private HashSet<Node> FindChildren(Node node, ref HashSet<ConnectivityItem> connectivityItems)
        {
            HashSet<Node> children = new HashSet<Node>(new NodeGlobalIdComparer());

            IEnumerable<ConnectivityItem> firstSubconnectToNodeQuery = connectivityItems.Where(x => x.FromGlobalId == node.GlobalId || x.ToGlobalId == node.GlobalId).ToList();
            List<ConnectivityItem> childConnectivityItems = connectivityItems.Where(x => firstSubconnectToNodeQuery.Any(y => y.ViaGlobalId == x.ViaGlobalId)).ToList();

            foreach (ConnectivityItem childConnectivityItem in childConnectivityItems)
            {
                connectivityItems.Remove(childConnectivityItem);
                if (Enum.IsDefined(typeof(Constant.sourceMapping), childConnectivityItem.FromNetworkSourceId))
                    children.Add(CreateNode(node, childConnectivityItem, true));
                if (Enum.IsDefined(typeof(Constant.sourceMapping), childConnectivityItem.ToNetworkSourceId))
                    children.Add(CreateNode(node, childConnectivityItem));
            }
            return children;
        }
        private Node CreateNode(Node node, ConnectivityItem item, bool isFrom = false)
        {
            return new Node()
            {
                Rank = node.Rank + 1,
                ObjectId = isFrom ? item.FromObjectId : item.ToObjectId,
                GlobalId = isFrom ? item.FromGlobalId : item.ToGlobalId,
                NetworkSourceId = isFrom ? item.FromNetworkSourceId : item.ToNetworkSourceId,
                Parent = node,
                ViaObjectId = item.ViaObjectId,
                ViaGlobalId = item.ViaGlobalId,
                ViaNetworkSourceId = item.ViaNetworkSourceId
            };
        }
    }
}
