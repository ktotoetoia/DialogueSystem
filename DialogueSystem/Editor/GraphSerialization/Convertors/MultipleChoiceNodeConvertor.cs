using System.Collections.Generic;
using System.Linq;

namespace DS
{
    public class MultipleChoiceNodeConvertor : DialogueBaseNodeConvertor, INodeSerializer
    {
        public MultipleChoiceNodeConvertor(DSGraphView graphView) : base(graphView)
        {

        }

        public void Serialize(SerializableGroup group, NodePairCollection nodes)
        {
            foreach (MultipleChoiceNode viewNode in nodes.BaseNodes.OfType<MultipleChoiceNode>())
            {
                foreach (IChoicePort port in viewNode.Choices)
                {
                    ConvertPort(group, nodes, port, viewNode);
                }
            }
        }

        public NodePairCollection Deserialize(IEnumerable<SerializableNode> nodes)
        {
            NodePairCollection result = new NodePairCollection();

            foreach (var node in nodes.Where(x => !x.OneLine))
            {
                MultipleChoiceNode singleNode = new MultipleChoiceNode(node.Position, graphView);

                ConvertBaseNode(singleNode, node);
                result.Add(singleNode, node);
            }

            return result;
        }
    }
}