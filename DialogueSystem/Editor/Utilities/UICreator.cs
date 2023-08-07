using System;
using UnityEngine.UIElements;

namespace DS
{
    public static class UIUtility
    {
        public static Foldout CreateFoldout(string text, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = text,
                value = !collapsed,
            };

            return foldout;
        }

        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text,
            };

            return button;
        }

        public static TextField CreateTextField(string text = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = text,
                label = label,
            };

            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            return textField;
        }

        public static TextField CreateTextArea(string text = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(text, label, onValueChanged);

            textArea.multiline = true;

            return textArea;
        }
    }
}