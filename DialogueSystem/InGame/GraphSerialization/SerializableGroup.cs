using System.Collections.Generic;
using UnityJSON;

namespace DS
{
    public class SerializableGroup
    {
        private IEdgeFactory edgeFactory = new EdgeFactory();
        private int count;

        [JSONNode] protected List<SerializableNode> nodes = new List<SerializableNode>();
        [JSONNode] protected List<SerializableEdge> edges = new List<SerializableEdge>();

        [JSONNode(NodeOptions.DontSerialize)]
        public IEnumerable<SerializableNode> Nodes
        {
            get
            {
                foreach (SerializableNode node in nodes) yield return node;
            }
        }

        [JSONNode(NodeOptions.DontSerialize)]
        public IEnumerable<SerializableEdge> Edges
        {
            get
            {
                foreach (SerializableEdge edge in edges) yield return edge;
            }
        }

        public string Name { get; set; }

        public void AddNode(SerializableNode node)
        {
            node.ID = count++;
            nodes.Add(node);
        }

        public SerializableEdge ConnectNodes(SerializableNode from, SerializableNode to)
        {
            SerializableEdge edge = edgeFactory.Create(from, to);
            edges.Add(edge);
            return edge;
        }
    }
}