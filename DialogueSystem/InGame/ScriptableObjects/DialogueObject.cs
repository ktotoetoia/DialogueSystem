using System.Linq;
using UnityEngine;

namespace DS
{
    public class DialogueObject : ScriptableObject
    {
        private GraphSerializer serializer = new GraphSerializer();
        private SerializableGroupToDialogueGraph convertor = new SerializableGroupToDialogueGraph();
        [field: SerializeField] public string SerializedDialogue { get; set; }

        public DialogueGraph GetGraph()
        {
            return convertor.Convert(serializer.Deserialize(this));
        }

        public DialogueGraph GetGraph(string graphName)
        {
            SerializableGraph graph = serializer.Deserialize(this);

            return convertor.Convert(graph.Groups.First(x => x.Name== graphName));
        }
        public DialogueGraph GetGraph(int index)
        {
            SerializableGraph graph = serializer.Deserialize(this);

            return convertor.Convert(graph.Groups[index]);
        }
    }
}