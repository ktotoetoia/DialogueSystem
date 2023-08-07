using System.Text.RegularExpressions;

namespace DS
{
    public class DefaultDialogueClassParser : IDialogueClassParser
    {
        private const string squareBracketsPattern = @"\[(.*?)\]";
        private const string bracesPattern = @"\{(.*?)\}";

        public virtual IDialogueClass Parse(string classString)
        {
            return new DefaultDialogueClass()
            {
                Name = GetInside(classString, squareBracketsPattern),
                Value = GetInside(classString, bracesPattern),
            };
        }

        private string GetInside(string text, string pattern)
        {
            Match match = Regex.Match(text, pattern);
            return match.Groups[1].Value;
        }
    }
}