namespace DS
{
    public interface IDialogueEdge
    {
        public IDialogueNode From { get; }
        public IDialogueNode To { get; }
        string Text { get; set; }
    }
}