namespace DS
{
    public class NodePair
    {
        public SerializableNode DataNode { get; set; }
        public DialogueNodeBase BaseNode { get; set; }

        public NodePair(SerializableNode dataNode, DialogueNodeBase baseNode)
        {
            DataNode = dataNode;
            BaseNode = baseNode;
        }
    }
}