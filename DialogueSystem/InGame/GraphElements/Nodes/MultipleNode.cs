using System.Collections.Generic;

namespace DS
{
    public class MultipleNode : IDialogueNode
    {
        private List<IDialogueEdge> edges = new List<IDialogueEdge>();

        public IEnumerable<string> Choices
        {
            get
            {
                foreach (IDialogueEdge edge in edges) yield return edge.Text;
            }
        }

        public IEnumerable<IDialogueClass> Classes { get; set; }

        public string Name { get; set; }
        public string Text { get; set; }


        public IDialogueNode SelectNext(string info)
        {
            return edges.Find(e => e.Text.Equals(info)).To;
        }

        public void Connect(IDialogueEdge edge)
        {
            edges.Add(edge);
        }
    }
}