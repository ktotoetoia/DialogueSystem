using System.Linq;
using UnityEngine;

namespace DS
{
    public class DialogueObject : ScriptableObject
    {
        private SerializableGroupToDialogueGraph convertor = new SerializableGroupToDialogueGraph();
        private GraphSerializer serializer = new GraphSerializer();

        [field: SerializeField] public string SerializedDialogue { get; set; }

        public DialogueGraph GetMainGraph()
        {
            return convertor.Convert(Deserialize());
        }

        public DialogueGraph GetGraph(string graphName)
        {
            return convertor.Convert(Deserialize().Groups.First(x => x.Name == graphName));
        }

        public DialogueGraph GetGraph(int index)
        {
            return convertor.Convert(Deserialize().Groups[index]);
        }

        private SerializableGraph Deserialize()
        {
            return serializer.Deserialize(this);
        }
    }
}