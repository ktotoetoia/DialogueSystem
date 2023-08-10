namespace DS
{
    public class DialogueEdgeFactory : IDialogueEdgeFactory
    {
        public IDialogueEdge Create(SerializableEdge edge, IDialogueNode from, IDialogueNode to)
        {
            return new DialogueEdge(from, to)
            {
                Text = edge.Text,
            };
        }
    }
}