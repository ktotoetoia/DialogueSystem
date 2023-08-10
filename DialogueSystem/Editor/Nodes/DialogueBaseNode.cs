using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS
{
    public class DialogueNodeBase : Node
    {
        protected NodeClassesInstaller classInstaller = new NodeClassesInstaller();
        protected DSGraphView graphView;

        public string AdditionalInformation
        {
            get
            {
                return additionalInformationTextField.value;
            }
            set
            {
                additionalInformationTextField.value = value;
            }
        }

        public string Title
        {
            get
            {
                return titleTextField.value;
            }
            set
            {
                titleTextField.value = value;
            }
        }

        public string Text
        {
            get
            {
                return textField.value;
            }
            set
            {
                textField.value = value;
            }
        }

        private TextField textField;
        private TextField titleTextField;
        private TextField additionalInformationTextField;

        public DialogueNodeBase(Vector2 position, DSGraphView graphView)
        {
            SetPosition(new Rect(position, Vector2.zero));

            this.graphView = graphView;
            classInstaller.AddMainContainerToClassList(mainContainer);
            classInstaller.AddExtensionContainerToClassList(extensionContainer);
        }

        public virtual void Draw()
        {
            AddTitle();
            AddText();
            AddPorts();
            RefreshExpandedState();
        }

        protected virtual void AddTitle()
        {
            titleTextField = UIUtility.CreateTextField("Dialogue Title");

            classInstaller.AddFilenameTextFieldToClassList(titleTextField);
            titleContainer.Insert(0, titleTextField);
        }

        protected virtual void AddText()
        {
            Foldout textFoldout = UIUtility.CreateFoldout("Dialogue Text");
            VisualElement customDataContainer = new VisualElement();

            textField = CreateTextArea(textFoldout, "Dialogue Text");
            additionalInformationTextField = CreateTextArea(textFoldout,"\"[className]{value},\"");

            classInstaller.AddCustomDataContainerToUSSClasses(customDataContainer);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
        }

        private TextField CreateTextArea(Foldout foldout,string text)
        {
            TextField textArea = UIUtility.CreateTextArea(text);

            classInstaller.AddQuoteTextFieldToClassList(textArea);
            foldout.Add(textArea);
            
            return textArea;
        }

        protected virtual void AddPorts()
        {
            inputContainer.Add(CreateInputPort());
        }

        protected virtual Port CreateInputPort()
        {
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

            inputPort.portName = "Dialogue Connection";

            return inputPort;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent menuEvent)
        {
            menuEvent.menu.AppendAction("Disconnect All Ports", actionEvent => DisconnectAllPorts());
            menuEvent.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectPorts(inputContainer));
            menuEvent.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectPorts(outputContainer));
        }

        protected void DisconnectAllPorts()
        {
            DisconnectPorts(inputContainer);
            DisconnectPorts(outputContainer);
        }

        protected void DisconnectPorts(VisualElement container)
        {
            DisconnectPorts(container.Children().OfType<Port>().Where(x => x.connected));
        }

        public void DisconnectPorts(IEnumerable<Port> ports)
        {
            foreach (Port port in ports)
            {
                graphView.DeleteElements(port.connections);
            }
        }

        public IEnumerable<Port> GetPorts()
        {
            List<Port> inputPorts = inputContainer.Query<Port>().ToList();
            List<Port> outputPorts = outputContainer.Query<Port>().ToList();

            return inputPorts.Concat(outputPorts);
        }

        public virtual IChoicePort CreateOutputPort(string title)
        {
            return null;
        }
    }
}