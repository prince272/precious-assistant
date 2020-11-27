using PreciousUI.Plugins.Properties;
using Syn.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PreciousUI.Plugins.Internals
{
    internal class BotTextHelper
    {

        private const string URL_PATTERN = @"(https?://)?((?:(\w+-)*\w+)\.)+(?:com|org|net|edu|gov|biz|info|name|museum)(\/?\w?-?=?_?\??&?)+[\.]?[a-z0-9\?=&_\-%#]*";

        public static string GetRandomText()
        {
            string[] random = new string[]
            {
                "I am not totally sure about that. Perhaps I'm just expressing my own concern about it.",
                "You should rephrase that to make me understand.",
                "What you said was too complicated for me.",
                "Sometimes I get really frustrated when you talk like this.",
                "I don't get what you are asking. Could you rephrase it?",
                "Can you please rephrase that with fewer ideas or different thoughts?",
                "I'm sorry. I'm not too familiar with that yet.",
                "That is a difficult question for me to answer. Ask me another question.",
                "Well, this is embarrassing. Try putting that in a more specific context.",
                "Quite honestly, I wouldn't worry myself about that.",
                "I do not understand that. Do not ask me any more questions please!!!",
            };
            string text = string.Format("{0}", SynUtility.Text.GetRandom(random.ToList()));
            return text;
        }

        public static string GetCommonText(string message)
        {
            string text = ProcessOpenCommand(message);
            if (!string.IsNullOrEmpty(text))
                return text;

            text = ProcessNumInTextCommand(message);
            if (!string.IsNullOrEmpty(text))
                return text;

            // The statment has been disabled due to some reasons.
            //text = ProcessWordsInNumsCommand(message);
            //if (!string.IsNullOrEmpty(text))
            //    return text;

            text = ProcessStandardCalCommand(message);
            if (!string.IsNullOrEmpty(text))
                return text;
            return null;
        }

        private static string ProcessStandardCalCommand(string message)
        {
            try
            {
                var expression = new NCalc.Expression(message, NCalc.EvaluateOptions.IgnoreCase);
                if (!expression.HasErrors())
                    return Convert.ToString(expression.Evaluate());
            }
            catch (Exception) { }
            return null;
        }

        public static string ProcessOpenCommand(string message)
        {
            string url = null;
            string pattern = @"^((\w+\s*){3})?";
            pattern += SynUtility.Text.GetWordsPattern(new List<string>() { "open", "launch", "run", "navigate" }, true);
            url = SynUtility.Text.TextAfterPattern(message.ToLower(), pattern).Trim();
            if (!string.IsNullOrEmpty(url))
            {
                if (Regex.IsMatch(url, URL_PATTERN))
                {
                    // Add the expression if the url does not start with it.
                    if (!url.ToLower().StartsWith("https://www.") && !url.ToLower().StartsWith("www."))
                        url = "https://www." + url;
                }
                try
                {
                    // Execute the url with the ProcessHelper class.
                    Process.Start(url, string.Empty);
                    return string.Format(Resources.OpenAppSucessMessage, "the specified program");
                }
                catch (Exception) { }
            }
            return string.Empty;
        }

        public static string ProcessNumInTextCommand(string message)
        {
            try
            {
                int num;
                if (int.TryParse(message.RemoveSpaces(), out num))
                    return num.ToText();
            }
            catch (Exception) { }
            return string.Empty;
        }

        public static string ProcessWordsInNumsCommand(string message)
        {
            try
            {
                int num = message.ToNumbers();
                if (num != 0)
                    return num.ToString();
            }
            catch (Exception) { }
            return string.Empty;
        }
    }
}