using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class DialogueGraph
    {
        public IEnumerable<IDialogueNode> Nodes { get;protected set; }
        public IEnumerable<IDialogueEdge> Edges { get; protected set; }


        public DialogueGraph(IEnumerable<IDialogueNode> nodes, IEnumerable<IDialogueEdge> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public IDialogueNode GetNode()
        {
            return Nodes.First();
        }

        public IDialogueNode GetNode(string nodeName)
        {
            return Nodes.First(x => x.Name.Equals(nodeName));
        }

        public IDialogueNode GetFirstNodeWithClassName(string className)
        {
            return Nodes.FirstOrDefault(x => x.Classes.Any(x => x.Name.Equals(className)));
        }

        public IEnumerable<IDialogueNode> GetNodesOfClass<T>()
            where T : IDialogueClass
        {
            return Nodes.Where(x => x.Classes.OfType<T>().Any());
        }

        public IEnumerable<IDialogueNode> GetNodesWithClassName(string className)
        {
            return Nodes.Where(x => x.Classes.Any(x => x.Name.Equals(className)));
        }
    }
}