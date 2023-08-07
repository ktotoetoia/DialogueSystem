using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace DS
{
    public class SerializableToGraphViewConvertor
    {
        private DSGraphView graphView;

        private List<INodeSerializer> nodeConvertors = new List<INodeSerializer>();

        public SerializableToGraphViewConvertor(DSGraphView graphView)
        {
            this.graphView = graphView;

            nodeConvertors = new List<INodeSerializer>()
            {
                new SingleChoiceNodeConvertor(graphView),
                new MultipleChoiceNodeConvertor(graphView),
            };
        }

        public void Load(SerializableGraph graph)
        {
            ConvertToGroup(graph);

            foreach (SerializableGroup groupGraph in graph.Groups)
            {
                Group group = new Group()
                {
                    title = groupGraph.Name,
                };

                graphView.AddElement(group);
                ConvertToGroup(groupGraph, group);
            }
        }

        public void ConvertToGroup(SerializableGroup graph, Group group = null)
        {
            NodePairCollection nodes = new NodePairCollection();

            foreach (INodeSerializer convertor in nodeConvertors)
            {
                NodePairCollection pairs = convertor.Deserialize(graph.Nodes.ToList());
                group?.AddElements(pairs.BaseNodes);
                nodes.Nodes.AddRange(pairs.Nodes);
            }

            foreach (SerializableEdge edge in graph.Edges)
            {
                DialogueBaseNode from = nodes.FindByID(edge.From)?.BaseNode;
                DialogueBaseNode to = nodes.FindByID(edge.To)?.BaseNode;

                Port fromPort = from.CreateOutputPort(edge.Text).Port;

                if (to == null) continue;

                Port toPort = to?.inputContainer.Children().OfType<Port>().FirstOrDefault();

                Edge e = fromPort.ConnectTo(toPort);
                graphView.AddElement(e);
            }
        }
    }
}