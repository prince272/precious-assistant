using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Internals
{
    internal static class TextHelper
    {
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