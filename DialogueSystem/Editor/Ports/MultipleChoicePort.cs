using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DS
{
    public class MultipleChoicePort : IChoicePort
    {
        private NodeClassesInstaller classInstaller;
        private GraphView graphView;
        private TextField textField;

        public string Text { get { return textField.text; } }
        public Port Port { get; private set; }

        public MultipleChoicePort(NodeClassesInstaller classInstaller, GraphView graphView, Port outputPort, string title)
        {
            this.classInstaller = classInstaller;
            this.graphView = graphView;

            Port = outputPort;
            Port.portName = string.Empty;

            CreateElements(outputPort, title);
        }

        private void CreateElements(Port outputPort, string title)
        {
            Button deleteButton = UIUtility.CreateButton("X", () =>
            {
                graphView.DeleteElements(outputPort.connections);
                graphView.RemoveElement(outputPort);
            });

            textField = UIUtility.CreateTextField(title);

            classInstaller.AddChoiceTextFieldToClassList(textField);
            classInstaller.AddButtonToClassList(deleteButton);

            outputPort.Add(textField);
            outputPort.Add(deleteButton);
        }
    }
}