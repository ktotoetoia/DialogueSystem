using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DS
{
    public class GraphViewManipulatorController
    {
        private GraphView graphView;

        public GraphViewManipulatorController(GraphView graphView)
        {
            this.graphView = graphView;
        }

        public void AddBasicManipulators()
        {
            graphView.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            graphView.AddManipulator(new ContentDragger());
            graphView.AddManipulator(new SelectionDragger());
            graphView.AddManipulator(new RectangleSelector());
        }
    }
}