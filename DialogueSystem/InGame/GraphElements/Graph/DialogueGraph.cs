using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class DialogueGraph
    {
        private List<IDialogueNode> nodes;
        private List<IDialogueEdge> edges;

        public IEnumerable<IDialogueNode> Nodes
        {
            get
            {
                foreach (IDialogueNode node in nodes) yield return node;
            }
        }

        public IEnumerable<IDialogueEdge> Edges
        {
            get
            {
                foreach (IDialogueEdge edge in edges) yield return edge;
            }
        }

        public DialogueGraph(List<IDialogueNode> nodes, List<IDialogueEdge> edges)
        {
            this.nodes = nodes;
            this.edges = edges;
        }

        public IDialogueNode GetNode()
        {
            return nodes[0];
        }

        public IDialogueNode GetNode(string nodeName)
        {
            return nodes.Find(x => x.Name.Equals(nodeName));
        }

        public IDialogueNode GetFirstNodeWithClassName(string className)
        {
            return nodes.FirstOrDefault(x => x.Classes.Any(x => x.Name.Equals(className)));
        }

        public IEnumerable<IDialogueNode> GetNodesOfClass<T>()
            where T : IDialogueClass
        {
            return nodes.Where(x => x.Classes.OfType<T>().Any());
        }

        public IEnumerable<IDialogueNode> GetNodesWithClassName(string className)
        {
            return nodes.Where(x => x.Classes.Any(x => x.Name.Equals(className)));
        }
    }
}