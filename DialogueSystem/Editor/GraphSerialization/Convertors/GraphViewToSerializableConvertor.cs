using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace DS
{
    public class GraphViewToSerializableConvertor
    {
        private List<INodeSerializer> nodeConvertors = new List<INodeSerializer>();
        private DSGraphView graphView;

        public GraphViewToSerializableConvertor(DSGraphView graphView)
        {
            this.graphView = graphView;

            nodeConvertors = new List<INodeSerializer>()
            {
                new SingleChoiceNodeConvertor(graphView),
                new MultipleChoiceNodeConvertor(graphView),
            };
        }

        public SerializableGraph ConvertToSerializable()
        {
            SerializableGraph graph = new SerializableGraph();
            List<DialogueNodeBase> groupedChilds = new List<DialogueNodeBase>();
            IEnumerable<GraphElement> graphElements = graphView.graphElements;

            foreach (Group group in graphElements.OfType<Group>())
            {
                IEnumerable<DialogueNodeBase> childs = group.containedElements.OfType<DialogueNodeBase>();
                SerializableGroup serializableGroup = ConvertToSerializableGraph(childs);
                serializableGroup.Name = group.title;
                graph.Groups.Add(serializableGroup);
                groupedChilds.AddRange(childs);
            }

            ConvertToSerializableGraph(graph, graphElements.OfType<DialogueNodeBase>().Except(groupedChilds));

            return graph;
        }

        private SerializableGroup ConvertToSerializableGraph(IEnumerable<DialogueNodeBase> viewNodes)
        {
            return ConvertToSerializableGraph(new SerializableGroup(), viewNodes);
        }

        private SerializableGroup ConvertToSerializableGraph(SerializableGroup graph, IEnumerable<DialogueNodeBase> viewNodes)
        {
            var nodes = ConvertNodes(graph, viewNodes);
            ConvertEdges(graph, nodes);

            return graph;
        }

        private NodePairCollection ConvertNodes(SerializableGroup graph, IEnumerable<DialogueNodeBase> viewNodes)
        {
            NodePairCollection nodes = new();

            foreach (DialogueNodeBase viewNode in viewNodes)
            {
                SerializableNode node = ConvertNode(viewNode);

                nodes.Add(viewNode, node);
                graph.AddNode(node);
            }

            return nodes;
        }

        private SerializableNode ConvertNode(DialogueNodeBase viewNode)
        {
            SerializableNode node = new SerializableNode()
            {
                Position = viewNode.GetPosition().position,
                Name = viewNode.Title,
                Text = viewNode.Text,
                AdditionalInformation = viewNode.AdditionalInformation,
            };

            return node;
        }

        private void ConvertEdges(SerializableGroup graph, NodePairCollection nodes)
        {
            foreach (INodeSerializer convertor in nodeConvertors)
            {
                convertor.Serialize(graph, nodes);
            }
        }
    }
}