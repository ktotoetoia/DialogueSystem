using System.Collections.Generic;

namespace DS
{
    [System.Serializable]
    public class SerializableGroup
    {
        private IEdgeFactory edgeFactory = new EdgeFactory();
        private int count;

        public List<SerializableNode> Nodes  = new List<SerializableNode>();
        public List<SerializableEdge> Edges  = new List<SerializableEdge>();

        public string Name;

        public void AddNode(SerializableNode node)
        {
            node.ID = count++;
            Nodes.Add(node);
        }

        public SerializableEdge ConnectNodes(SerializableNode from, SerializableNode to)
        {
            SerializableEdge edge = edgeFactory.Create(from, to);
            Edges.Add(edge);
            return edge;
        }
    }
}