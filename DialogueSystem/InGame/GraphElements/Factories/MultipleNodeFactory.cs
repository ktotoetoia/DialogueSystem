namespace DS
{
    public class MultipleNodeFactory : IDialogueNodeFactory
    {
        private DialogueClassesParser classReader = new DialogueClassesParser();

        public IDialogueNode Create(SerializableNode serializableNode)
        {
            MultipleNode node = new MultipleNode()
            {
                Text = serializableNode.Text,
                Name = serializableNode.Name,
                Classes = classReader.ParseClasses(serializableNode.AdditionalInformation),
            };

            return node;
        }
    }
}