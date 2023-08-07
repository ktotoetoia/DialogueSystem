using UnityEngine.UIElements;

namespace DS
{
    public class NodeClassesInstaller
    {
        public void AddFilenameTextFieldToClassList(TextField textField)
        {
            textField.AddToClassList(NodeClasses.TextField);
            textField.AddToClassList(NodeClasses.TextFieldHidden);
            textField.AddToClassList(NodeClasses.FilenameTextField);
        }

        public void AddCustomDataContainerToUSSClasses(VisualElement customDataContainer)
        {
            customDataContainer.AddToClassList(NodeClasses.CustomDataContainer);
        }

        public void AddQuoteTextFieldToClassList(TextField textField)
        {
            textField.AddToClassList(NodeClasses.TextField);
            textField.AddToClassList(NodeClasses.QuoteTextField);
        }

        public void AddButtonToClassList(Button button)
        {
            button.AddToClassList(NodeClasses.Button);
        }

        public void AddMainContainerToClassList(VisualElement mainContainer)
        {
            mainContainer.AddToClassList(NodeClasses.MainContainer);
        }

        public void AddExtensionContainerToClassList(VisualElement extensionContainer)
        {
            extensionContainer.AddToClassList(NodeClasses.ExtensionContainer);
        }

        public void AddChoiceTextFieldToClassList(TextField textField)
        {
            textField.AddToClassList(NodeClasses.TextField);
            textField.AddToClassList(NodeClasses.ChoiceTextField);
            textField.AddToClassList(NodeClasses.TextFieldHidden);
        }
    }
}