using System.Globalization;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityJSON;

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
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            dialogue.SerializedDialogue = graph.ToJSONString();
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return JSON.Deserialize<SerializableGraph>(dialogue.SerializedDialogue);
        }
    }
}