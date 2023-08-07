using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    public class MultipleChoiceNodeFactory : IElementFactory
    {
        private DSGraphView graphView;

        public MultipleChoiceNodeFactory(DSGraphView graphView)
        {
            this.graphView = graphView;
        }

        public GraphElement Create(Vector2 position)
        {
            MultipleChoiceNode node = new MultipleChoiceNode(position, graphView);

            return node;
        }

        public GraphElement AddNewToGraphView(Vector2 position)
        {
            MultipleChoiceNode node = Create(position) as MultipleChoiceNode;

            graphView.AddNode(node);

            return node;
        }
    }
}