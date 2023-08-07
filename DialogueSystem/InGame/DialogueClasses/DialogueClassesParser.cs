using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DS
{
    public class DialogueClassesParser
    {
        private const string commentsPattern = "\".*?\"";

        public IDialogueClassParser Parser { get; set; } = new DefaultDialogueClassParser();

        public IEnumerable<IDialogueClass> ParseClasses(string classes)
        {
            IEnumerable<string> classesList = RemoveComments(classes)
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x));

            return classesList.Select(x => ParseClass(x)).ToList();
        }

        public IDialogueClass ParseClass(string classString)
        {
            return Parser.Parse(classString);
        }

        private string RemoveComments(string text)
        {
            return Regex.Replace(text, commentsPattern, string.Empty);
        }
    }
}