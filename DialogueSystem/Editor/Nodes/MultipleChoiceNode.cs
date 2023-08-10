using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS
{
    public class MultipleChoiceNode : DialogueNodeBase
    {
        public List<IChoicePort> Choices { get; protected set; } = new List<IChoicePort>();
        protected ChoicePortFactory portFactory;

        public MultipleChoiceNode(Vector2 position, DSGraphView graphView) : base(position, graphView)
        {
            portFactory = new ChoicePortFactory(classInstaller, graphView);
            Draw();
        }

        public override void Draw()
        {
            base.Draw();
            AddChoiceButton();
        }

        protected void AddChoiceButton()
        {
            Button choiceButton = UIUtility.CreateButton("Add Choice", () =>
            {
                CreateOutputPort("New Choice");
            });

            classInstaller.AddButtonToClassList(choiceButton);
            mainContainer.Insert(1, choiceButton);
        }

        protected override void AddPorts()
        {
            base.AddPorts();
        }

        public override IChoicePort CreateOutputPort(string title)
        {
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            IChoicePort port = portFactory.Create(outputPort, title);
            Choices.Add(port);

            outputContainer.Add(outputPort);

            RefreshExpandedState();
            return port;
        }
    }
}