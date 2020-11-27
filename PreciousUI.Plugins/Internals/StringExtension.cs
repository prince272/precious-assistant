using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Plugins.Internals
{
    public static class StringExtension
    {
        public static string RemoveSpaces(this string mainValue)
        {
            while (mainValue.Contains(" "))
                mainValue = mainValue.Replace(" ", string.Empty);
            return mainValue;
        }

        public static string RemoveLastEnds(this string mainValue, string[] lastEnds, StringComparison comparison)
        {
            foreach (var l in lastEnds)
            {
                if (mainValue.EndsWith(l, comparison))
                    mainValue = mainValue.Substring(0, mainValue.Length - l.Length);
            }
            return mainValue.Trim();
        }

        public static bool StringContains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return true;
            bool sucess = source.IndexOf(toCheck, comp) >= 0;
            return sucess;
        }

        public static string ToText(this int num)
        {
            var numberText = new NumberText();
            return numberText.ToText(num);
        }

        public static int ToNumbers(this string words)
        {
            return NumberText.ToNumbers(words);
        }
        // And You All
        public static string TruncateAtLastSentence(this string value, int maxLength)
        {
            if (maxLength < 1) return string.Empty;

            if (value.Length > maxLength)
            {
                int index = 0;
                index = value.IndexOf(' ', maxLength - 1);
                if (index != -1)
                {
                    index = index - maxLength;
                    value = value.Substring(0, maxLength + index);

                    char[] tun = new char[] { '.', '!', '?' };
                    index = value.LastIndexOfAny(tun);
                    if (index != -1)
                    {
                        value = value.Substring(0, index + 1);
                    }
                    else if (char.IsLetter(value.Last()))
                    {
                        value = value + "...";
                    }
                }
            }
            return value.Trim();
        }
    }
}