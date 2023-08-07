using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    public class SingleChoiceNodeFactory : IElementFactory
    {
        private DSGraphView graphView;

        public SingleChoiceNodeFactory(DSGraphView graphView)
        {
            this.graphView = graphView;
        }

        public GraphElement Create(Vector2 position)
        {
            SingleChoiceNode node = new SingleChoiceNode(position, graphView);

            node.CreateOutputPort("Next Choice");

            return node;
        }

        public GraphElement AddNewToGraphView(Vector2 position)
        {
            SingleChoiceNode group = Create(position) as SingleChoiceNode;

            graphView.AddNode(group);

            return group;
        }
    }
}