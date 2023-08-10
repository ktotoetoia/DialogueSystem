using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class SerializableGroupToDialogueGraph
    {
        private IDialogueNodeFactory multipleNodeFactory = new MultipleNodeFactory();
        private IDialogueNodeFactory singleNodeFactory = new SingleNodeFactory();
        private IDialogueEdgeFactory edgeFactory = new DialogueEdgeFactory();

        public DialogueGraph Convert(SerializableGroup group)
        {
            List<IDialogueNode> nodes = ConvertNodes(group.Nodes.OrderBy(x => x.ID));
            List<IDialogueEdge> edges = ConvertEdges(group.Edges, nodes);

            return new DialogueGraph(nodes, edges);
        }

        private List<IDialogueNode> ConvertNodes(IEnumerable<SerializableNode> nodes)
        {
            return nodes.Select(x => ConvertNode(x)).ToList();
        }

        private IDialogueNode ConvertNode(SerializableNode node)
        {
            if (node.OneLine)
            {
                return singleNodeFactory.Create(node);
            }

            return multipleNodeFactory.Create(node);
        }

        private List<IDialogueEdge> ConvertEdges(IEnumerable<SerializableEdge> edges, IEnumerable<IDialogueNode> nodes)
        {
            return GetConnectedEdges(edges).Select(x => ConvertEdge(x,nodes)).ToList();
        }

        private IEnumerable<SerializableEdge> GetConnectedEdges(IEnumerable<SerializableEdge> edges)
        {
            return edges.Where(x => x.From != -1 && x.To != -1);
        }

        private IDialogueEdge ConvertEdge(SerializableEdge edge, IEnumerable<IDialogueNode> nodes)
        {
            return edgeFactory.Create(edge, nodes.ElementAt(edge.From), nodes.ElementAt(edge.To));
        }
    }
}