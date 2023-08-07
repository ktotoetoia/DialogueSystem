using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class SerializableGroupToDialogueGraph
    {
        private IDialogueNodeFactory multipleNodeFactory = new MultipleNodeFactory();
        private IDialogueNodeFactory singleNodeFactory = new SingleNodeFactory();

        public DialogueGraph Convert(SerializableGroup group)
        {
            List<IDialogueNode> nodes = ConvertNodes(group.Nodes.OrderBy(x => x.ID));
            List<IDialogueEdge> edges = ConvertEdges(group.Edges, nodes);

            return new DialogueGraph(nodes, edges);
        }

        private List<IDialogueNode> ConvertNodes(IEnumerable<SerializableNode> nodes)
        {
            List<IDialogueNode> result = new List<IDialogueNode>();

            foreach (SerializableNode node in nodes)
            {
                result.Add(ConvertNode(node));
            }

            return result;
        }

        private IDialogueNode ConvertNode(SerializableNode node)
        {
            if (node.OneLine)
            {
                return singleNodeFactory.Create(node);
            }

            return multipleNodeFactory.Create(node);
        }

        private List<IDialogueEdge> ConvertEdges(IEnumerable<SerializableEdge> edges, List<IDialogueNode> nodes)
        {
            List<IDialogueEdge> result = new List<IDialogueEdge>();

            foreach (SerializableEdge edge in edges.Where(x => x.From != -1 && x.To != -1))
            {
                result.Add(ConvertEdge(edge, nodes));
            }

            return result;
        }

        private IDialogueEdge ConvertEdge(SerializableEdge edge, List<IDialogueNode> nodes)
        {
            DialogueEdge dialogueEdge = new DialogueEdge(nodes[edge.From], nodes[edge.To])
            {
                Text = edge.Text,
            };

            return dialogueEdge;
        }
    }
}