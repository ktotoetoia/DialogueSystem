using UnityEditor.Experimental.GraphView;

namespace DS
{
    public class SingleChoicePort : IChoicePort
    {
        public string Text { get { return Port.portName; } }
        public Port Port { get; set; }

        public SingleChoicePort(Port outputPort, string title)
        {
            Port = outputPort;
            Port.portName = title;
        }
    }
}