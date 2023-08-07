using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS
{
    public class DSEditorWindow : EditorWindow
    {
        private const string defaultFilename = "MyDialogue";
        private const string windowName = "Dialogue System";
        private const string extension = "asset";
        private const string defaultPath = "";

        [SerializeField] private StyleSheet gridStyleSheet;
        [SerializeField] private StyleSheet gridStyleSheetVariables;
        [SerializeField] private StyleSheet nodesStyleSheet;
        private GraphSerializer graphSerializer;
        private GraphViewToSerializableConvertor graphViewToSerializable;
        private SerializableToGraphViewConvertor serializableToGraphView;
        private DSGraphView graphView;
        private TextField filenameTextField;
        private string lastPath;

        [OnOpenAsset()]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            string path = AssetDatabase.GetAssetPath(instanceID);
            Type assetType = AssetDatabase.GetMainAssetTypeAtPath(path);

            if (assetType == typeof(DialogueObject))
            {
                Open(path);
            }

            return assetType == typeof(DialogueObject);
        }


        [MenuItem("Window/DialogueSystem %g")]
        public static DSEditorWindow Open()
        {
            return GetWindow<DSEditorWindow>(windowName);
        }

        public static void Open(string path)
        {
            Open().LoadGraph(path);
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
        }

        private void AddStyles()
        {
            rootVisualElement.styleSheets.Add(gridStyleSheetVariables);
        }

        private void AddGraphView()
        {
            graphView = new DSGraphView(this, gridStyleSheet, nodesStyleSheet);
            graphSerializer = new GraphSerializer();
            graphViewToSerializable = new GraphViewToSerializableConvertor(graphView);
            serializableToGraphView = new SerializableToGraphViewConvertor(graphView);

            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();
            Button saveButton = UIUtility.CreateButton("Save", SaveChanges);
            Button saveAsButton = UIUtility.CreateButton("Save as", SaveAs);
            Button loadButton = UIUtility.CreateButton("Load", Load);

            filenameTextField = UIUtility.CreateTextField(defaultFilename, "Filename:");

            toolbar.Add(filenameTextField);
            toolbar.Add(saveButton);
            toolbar.Add(saveAsButton);
            toolbar.Add(loadButton);
            rootVisualElement.Add(toolbar);
        }

        public override void SaveChanges()
        {
            if (!string.IsNullOrEmpty(lastPath))
            {
                SaveGraph(lastPath);
                return;
            }

            SaveAs();
        }

        private void SaveAs()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Dialogue", defaultPath, extension, filenameTextField.text);

            if (!string.IsNullOrEmpty(path))
            {
                SaveGraph(path);
            }
        }

        public void OnDestroy()
        {
            SaveChanges();
        }

        private void SaveGraph(string path)
        {
            SerializableGraph serialized = graphViewToSerializable.ConvertToSerializable();
            graphSerializer.Serialize(serialized, path);

            UpdateFilenameTextfield(path);
        }

        private void Load()
        {
            string path = EditorUtility.OpenFilePanel("Load Dialogue", defaultPath, extension);

            if (!string.IsNullOrEmpty(path))
            {
                path = path.Substring(path.IndexOf("Assets"));
                LoadGraph(path);
            }
        }

        private void LoadGraph(string path)
        {
            graphView.ClearGraph();
            serializableToGraphView.Load(graphSerializer.Deserialize(path));

            UpdateFilenameTextfield(path);
        }

        private void UpdateFilenameTextfield(string path)
        {
            int dotIndex = path.LastIndexOf(".");
            int slashIndex = path.LastIndexOf("/");
            string fileName = path.Substring(slashIndex + 1, dotIndex - slashIndex - 1);

            filenameTextField.value = fileName;
            lastPath = path;
        }
    }
}