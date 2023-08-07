using UnityEditor.Experimental.GraphView;

namespace DS
{
    public interface IChoicePort
    {
        string Text { get; }
        Port Port { get; }
    }
}