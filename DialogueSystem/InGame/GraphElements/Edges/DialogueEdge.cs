namespace DS
{
    public class DialogueEdge : IDialogueEdge
    {
        public IDialogueNode From { get; }
        public IDialogueNode To { get; }
        public string Text { get; set; }

        public DialogueEdge(IDialogueNode from, IDialogueNode to)
        {
            From = from;
            To = to;

            from.Connect(this);
        }
    }
}