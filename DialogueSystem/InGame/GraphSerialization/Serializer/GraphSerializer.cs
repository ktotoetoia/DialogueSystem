using System.Globalization;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace DS
{
    public class GraphSerializer
    {
        public void Serialize(SerializableGraph graph, string path)
        {
            if (!SerializeIfExist(graph, path))
            {
                CreateDialogueObject(graph, path);
            }
        }

        private bool SerializeIfExist(SerializableGraph graph, string path)
        {
            DialogueObject dialogue = AssetDatabase.LoadAssetAtPath<DialogueObject>(path);

            if (dialogue != null)
            {
                SetDialogueText(dialogue, graph);
            }

            return dialogue != null;
        }

        private void CreateDialogueObject(SerializableGraph graph, string path)
        {
            DialogueObject dialogue = ScriptableObject.CreateInstance<DialogueObject>();

            AssetDatabase.CreateAsset(dialogue, path);
            SetDialogueText(dialogue, graph);
        }

        private void SetDialogueText(DialogueObject dialogue, SerializableGraph graph)
        {
            dialogue.SerializedDialogue = JsonUtility.ToJson(graph);

            EditorUtility.SetDirty(dialogue);
        }

        public SerializableGraph Deserialize(string path)
        {
            DialogueObject dialogue = AssetDatabase.LoadAssetAtPath<DialogueObject>(path);
            if (dialogue == null) return new SerializableGraph();
            return Deserialize(dialogue);
        }

        public SerializableGraph Deserialize(DialogueObject dialogue)
        {
            return JsonUtility.FromJson<SerializableGraph>(dialogue.SerializedDialogue);
        }
    }
}