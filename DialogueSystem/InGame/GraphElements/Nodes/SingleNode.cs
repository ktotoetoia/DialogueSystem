using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class SingleNode : IDialogueNode
    {
        private IDialogueEdge nextEdge;

        public IEnumerable<string> Choices
        {
            get
            {
                if(nextEdge == null)
                {
                    return Enumerable.Empty<string>();
                }

                return new List<string>() { nextEdge.Text };
            }
        }

        public IEnumerable<IDialogueClass> Classes { get; set; }

        public string Name { get; set; }
        public string Text { get; set; }

        public IDialogueNode SelectNext(string info)
        {
            return nextEdge.To;
        }

        public void Connect(IDialogueEdge edge)
        {
            nextEdge = edge;
        }
    }
}