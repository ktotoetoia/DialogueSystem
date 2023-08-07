namespace DS
{
    public class SingleNodeFactory : IDialogueNodeFactory
    {
        private DialogueClassesParser classReader = new DialogueClassesParser();

        public IDialogueNode Create(SerializableNode serializableNode)
        {
            SingleNode node = new SingleNode()
            {
                Text = serializableNode.Text,
                Name = serializableNode.Name,
                Classes = classReader.ParseClasses(serializableNode.AdditionalInformation),
            };

            return node;
        }
    }
}