using UnityEditor.Experimental.GraphView;

namespace DS
{
    public class ChoicePortFactory
    {
        private NodeClassesInstaller classInstaller;
        private GraphView graphView;

        public ChoicePortFactory(NodeClassesInstaller classInstaller, GraphView graphView)
        {
            this.classInstaller = classInstaller;
            this.graphView = graphView;
        }

        public IChoicePort Create(Port outputPort, string title)
        {
            return new MultipleChoicePort(classInstaller, graphView, outputPort, title);
        }
    }
}