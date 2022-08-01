using Schema.TracingCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.TracingCore
{
    public class Graph
    {
        public class Node
        {
            public int Rank;
            public long ObjectId;
            public string GlobalId = string.Empty;
            public int NetworkSourceId;
            public Node Parent;
            public long ViaObjectId;
            public string ViaGlobalId = string.Empty;
            public int ViaNetworkSourceId;
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

        public HashSet<Node> createGraph(Node node, List<ConnectivityItem> connectivityItems)
        {
            HashSet<Node> graphs = new HashSet<Node>(new NodeGlobalIdComparer());
            List<ConnectivityItem> localConnectivityItems = new List<ConnectivityItem>(connectivityItems);
            graphs.Add(node);
            var q = new Queue<Node>();
            q.Enqueue(node);

            while (q.Count > 0)
            {
                var currentNode = q.Dequeue();
                if (currentNode == null) continue;

                HashSet<Node> childrens = FindChildren(currentNode, ref localConnectivityItems);
                foreach (Node child in childrens)
                {
                    q.Enqueue(child);
                    graphs.Add(child);
                }
            }

            return graphs;
        }

        private HashSet<Node> FindChildren(Node node, ref List<ConnectivityItem> connectivityItems)
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
