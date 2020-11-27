using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Plugins.Internals
{
    public class LinkVariables {
        public static string Google {
            get { return "http://www.google.com/search?q="; }
        }

        public static string Youtube {
            get { return "https://www.youtube.com/results?search_query="; }
        }

        public static string Yahoo {
            get { return "https://search.yahoo.com/search?p="; }
        }

        public static string Wikipedia {
            get { return "https://www.wikipedia.org/wiki/Special:Search?search="; }
        }

        public static string Facebook {
            get { return "https://www.facebook.com/search/?q="; }
        }
        public static string Skype { get { return "https://www.skype.com"; } }

        public static string Pinterest {
            get { return "https://www.pinterest.com"; }
        }

        public static string GoogleMap {
            get { return "http://maps.google.com/?q="; }
        }

        public static string FindSearchAddress(string queryUrl, string query) {
            return string.Format("{0}{1}", queryUrl, query);
        }
    }
}