namespace DS
{
    public interface IDialogueNodeFactory
    {
        public IDialogueNode Create(SerializableNode node);
    }
}