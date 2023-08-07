namespace DS
{
    public class NodePair
    {
        public SerializableNode DataNode { get; set; }
        public DialogueBaseNode BaseNode { get; set; }

        public NodePair(SerializableNode dataNode, DialogueBaseNode baseNode)
        {
            DataNode = dataNode;
            BaseNode = baseNode;
        }
    }
}