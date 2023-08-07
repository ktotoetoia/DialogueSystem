using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class NodePairCollection
    {
        public List<NodePair> Nodes { get; set; } = new();

        public IEnumerable<SerializableNode> DataNodes
        {
            get
            {
                foreach (SerializableNode node in Nodes.Select(x => x.DataNode)) yield return node;
            }
        }

        public IEnumerable<DialogueBaseNode> BaseNodes
        {
            get
            {
                foreach (DialogueBaseNode node in Nodes.Select(x => x.BaseNode)) yield return node;
            }
        }

        public NodePair Find(DialogueBaseNode node)
        {
            return Nodes.Find(x => x.BaseNode == node);
        }

        public NodePair Find(SerializableNode node)
        {
            return Nodes.Find(x => x.DataNode == node);
        }

        public NodePair FindByID(int id)
        {
            return Nodes.Find(x => x.DataNode.ID == id);
        }

        public void Add(DialogueBaseNode node, SerializableNode serializableNode)
        {
            Nodes.Add(new NodePair(serializableNode, node));
        }
    }
}