using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS
{
    public class DSGraphView : GraphView
    {
        private StyleSheet[] graphViewStyleSheets;
        private GraphViewManipulatorController manipulatorController;
        private DSSearchWindow searchWindow;
        private DSEditorWindow editorWindow;

        public DSGraphView(DSEditorWindow editorWindow, params StyleSheet[] styleSheet)
        {
            graphViewStyleSheets = styleSheet;

            this.editorWindow = editorWindow;

            AddGridBackground();
            AddSearchWindow();
            AddStyles();
            AddManipulators();

            elementsAddedToGroup += UpdateGroup;
        }

        private void UpdateGroup(Group group, IEnumerable<GraphElement> elements)
        {
            foreach (DialogueNodeBase node in elements.OfType<DialogueNodeBase>().ToList())
            {
                var portsToDisconnect = new List<Port>();
                var ports = node.GetPorts();

                foreach (Port port in ports)
                {
                    foreach (Edge edge in port.connections)
                    {
                        if (!group.ContainsElement(edge.input.node) || !group.ContainsElement(edge.output.node))
                        {
                            portsToDisconnect.Add(port);
                        }
                    }
                }

                node.DisconnectPorts(portsToDisconnect);
            }
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private void AddStyles()
        {
            foreach (StyleSheet styleSheet in graphViewStyleSheets)
            {
                styleSheets.Add(styleSheet);
            }
        }

        private void AddManipulators()
        {
            manipulatorController = new GraphViewManipulatorController(this);

            manipulatorController.AddBasicManipulators();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            Group startGroup = GetGroupOrDefault(startPort.node);

            foreach (Port port in ports)
            {
                Group portGroup = GetGroupOrDefault(port.node);

                if (startPort.direction != port.direction &&
                    ((portGroup == null && startGroup == null) || portGroup == startGroup))
                {
                    compatiblePorts.Add(port);
                }
            }

            return compatiblePorts;
        }

        private Group GetGroupOrDefault(GraphElement element)
        {
            return graphElements.OfType<Group>().FirstOrDefault(x => x.ContainsElement(element));
        }

        public void AddSearchWindow()
        {
            if (searchWindow == null)
            {
                searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();

                searchWindow.Initialize(this);
            }

            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition)
        {
            return contentViewContainer.WorldToLocal(mousePosition - editorWindow.position.position);
        }

        public void AddNode(GraphElement graphElement)
        {
            AddElement(graphElement);
        }

        public void ClearGraph()
        {
            DeleteElements(graphElements);
        }
    }
}