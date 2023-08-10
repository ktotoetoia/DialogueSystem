using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    public class GroupFactory : IElementFactory
    {
        private DSGraphView graphView;

        public GroupFactory(DSGraphView graphView)
        {
            this.graphView = graphView;
        }

        public GraphElement Create(Vector2 position)
        {
            Group group = new Group();
            group.SetPosition(new Rect(position, Vector2.zero));
            return group;
        }

        public GraphElement AddNewToGraphView(Vector2 position)
        {
            Group group = Create(position) as Group;

            graphView.AddNode(group);

            group.AddElements(graphView.selection.OfType<DialogueNodeBase>());

            return group;
        }
    }
}