using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    public class SingleChoiceNode : DialogueNodeBase
    {
        public IChoicePort ChoicePort { get; protected set; }

        public SingleChoiceNode(Vector2 position, DSGraphView graphView) : base(position, graphView)
        {
            Draw();
        }

        public override IChoicePort CreateOutputPort(string title)
        {
            if (ChoicePort == null)
            {
                Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                ChoicePort = new SingleChoicePort(outputPort, title);

                outputContainer.Add(outputPort);
            }

            RefreshPorts();

            return ChoicePort;
        }
    }
}