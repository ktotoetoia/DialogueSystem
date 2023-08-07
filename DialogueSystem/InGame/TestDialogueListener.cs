using UnityEngine;

namespace DS
{
    public class TestExample : MonoBehaviour
    {
        [SerializeField] private DialogueObject dialogue;

        private DialogueGraph graph;
        private IDialogueNode node;

        private void Start()
        {
            graph = dialogue.GetGraph();
            node = graph.GetFirstNodeWithClassName("start");
        }

        private void OnGUI()
        {
            Rect labelPosition = new Rect(new Vector2(350, 200), new Vector2(200, 200));
            Rect buttonPosition = new Rect(new Vector2(350, 300), new Vector2(200, 20));


            foreach (string message in node.Choices)
            {
                if (GUI.Button(buttonPosition, message))
                {
                    node = node.SelectNext(message);
                }

                buttonPosition.position -= new Vector2(0, 20);
            }
        }
    }
}