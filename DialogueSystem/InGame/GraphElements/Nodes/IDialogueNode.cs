using System.Collections.Generic;

namespace DS
{
    public interface IDialogueNode
    {
        string Name { get; }
        string Text { get; }
        IEnumerable<string> Choices { get; }
        IEnumerable<IDialogueClass> Classes { get; }
        IDialogueNode SelectNext(string info);
        void Connect(IDialogueEdge edge);
    }
}