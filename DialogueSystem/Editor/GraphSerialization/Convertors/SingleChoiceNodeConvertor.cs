using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class SingleChoiceNodeConvertor : DialogueBaseNodeConvertor, INodeSerializer
    {
        public SingleChoiceNodeConvertor(DSGraphView graphView) : base(graphView)
        {

        }

        public void Serialize(SerializableGroup group, NodePairCollection nodes)
        {
            foreach (SingleChoiceNode viewNode in nodes.BaseNodes.OfType<SingleChoiceNode>())
            {
                ConvertPort(group, nodes, viewNode.ChoicePort, viewNode);
                nodes.Find(viewNode).DataNode.OneLine = true;
            }
        }

        public NodePairCollection Deserialize(IEnumerable<SerializableNode> nodes)
        {
            NodePairCollection result = new NodePairCollection();

            foreach (var node in nodes.Where(x => x.OneLine))
            {
                SingleChoiceNode singleNode = new SingleChoiceNode(node.Position, graphView);

                ConvertBaseNode(singleNode, node);
                result.Add(singleNode, node);
            }

            return result;
        }
    }
}