using System.Collections.Generic;

namespace DS
{
    public interface INodeSerializer
    {
        public void Serialize(SerializableGroup graph, NodePairCollection nodes);
        public NodePairCollection Deserialize(IEnumerable<SerializableNode> nodes);
    }
}