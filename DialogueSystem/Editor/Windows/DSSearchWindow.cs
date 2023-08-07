using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView graphView;
        private Texture2D indentationIcon;

        public void Initialize(DSGraphView graphView)
        {
            this.graphView = graphView;

            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> treeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent(SearchWindowConstants.CreateElement)),
                new SearchTreeGroupEntry(new GUIContent(SearchWindowConstants.DialogueNode),1),

                new SearchTreeEntry(new GUIContent(SearchWindowConstants.SingleChoiceNode,indentationIcon))
                {
                    level = 2,
                    userData = new SingleChoiceNodeFactory(graphView),
                },

                new SearchTreeEntry(new GUIContent(SearchWindowConstants.MultipleChoiceNode, indentationIcon))
                {
                    level = 2,
                    userData = new MultipleChoiceNodeFactory(graphView),
                },

                new SearchTreeGroupEntry(new GUIContent(SearchWindowConstants.DialogueGroups),1),

                new SearchTreeEntry(new GUIContent(SearchWindowConstants.Group,indentationIcon))
                {
                    level = 2,
                    userData = new GroupFactory(graphView),
                },
            };

            return treeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            IElementFactory elementFactory = SearchTreeEntry.userData as IElementFactory;

            elementFactory?.AddNewToGraphView(graphView.GetLocalMousePosition(context.screenMousePosition));

            return elementFactory != null;
        }
    }
}