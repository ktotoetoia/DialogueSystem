namespace DS
{
    public interface IDialogueNodeFactory
    {
        public IDialogueNode Create(SerializableNode node);
    }

    public interface IDialogueEdgeFactory
    {
        public IDialogueEdge Create(SerializableEdge edge, IDialogueNode from, IDialogueNode to);
    }
}