using System.Linq;

namespace DS
{
    public class DialogueBaseNodeConvertor
    {
        protected DSGraphView graphView;

        public DialogueBaseNodeConvertor(DSGraphView graphView)
        {
            this.graphView = graphView;
        }

        protected void ConvertBaseNode(DialogueBaseNode node, SerializableNode serializableNode)
        {
            node.Text = serializableNode.Text;
            node.Title = serializableNode.Name;
            node.AdditionalInformation = serializableNode.AdditionalInformation;

            graphView.AddNode(node);
        }

        protected void ConvertPort(SerializableGroup group, NodePairCollection nodes, IChoicePort port, DialogueBaseNode node)
        {
            SerializableNode from = nodes.Find(node).DataNode;
            DialogueBaseNode toNode = port.Port.connections.FirstOrDefault()?.input?.node as DialogueBaseNode;
            SerializableNode to = toNode == null ? null : nodes.Find(toNode).DataNode;
            SerializableEdge edge = group.ConnectNodes(from, to);
            edge.Text = port.Text;
        }
    }
}