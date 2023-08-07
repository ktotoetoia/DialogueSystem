using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS
{
    public class DialogueBaseNode : Node
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

        public DialogueBaseNode(Vector2 position, DSGraphView graphView)
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
            TextField titleTextField = CreateTitle();

            classInstaller.AddFilenameTextFieldToClassList(titleTextField);
            titleContainer.Insert(0, titleTextField);
            titleContainer.Insert(1, additionalInformationTextField);
        }

        protected TextField CreateTitle()
        {
            titleTextField = UIUtility.CreateTextField("Dialogue Title", null, callback =>
            {
                Title = callback.newValue;
            });

            return titleTextField;
        }

        protected virtual void AddText()
        {
            Foldout textFoldout = UIUtility.CreateFoldout("Dialogue Text");

            textField = UIUtility.CreateTextArea("Dialogue Text");
            additionalInformationTextField = UIUtility.CreateTextArea("\"[className]{value},\"");

            VisualElement customDataContainer = new VisualElement();

            classInstaller.AddCustomDataContainerToUSSClasses(customDataContainer);
            classInstaller.AddQuoteTextFieldToClassList(textField);
            classInstaller.AddQuoteTextFieldToClassList(additionalInformationTextField);

            textFoldout.Add(textField);
            textFoldout.Add(additionalInformationTextField);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
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

            base.BuildContextualMenu(menuEvent);
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