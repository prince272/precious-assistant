using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PreciousUI.Plugins.Adapters
{
    public class ClipboardAdapter : IAdapter
    {
        public string Evaluate(Context context)
        {
            try
            {
                XAttribute attribute = context.Element.Attribute("Get");
                XAttribute attribute2 = context.Element.Attribute("Set");
                if (attribute != null)
                {
                    bool flag1 = attribute.Value.ToLower() == "text";
                    return Clipboard.GetText();
                }
                if (attribute2 != null)
                {
                    if (attribute2.Value.ToLower() == "text")
                    {
                        Clipboard.SetText(context.Element.Value);
                    }
                }
                else if (!string.IsNullOrEmpty(context.Element.Value))
                {
                    Clipboard.SetText(context.Element.Value);
                }
                return Clipboard.GetText();
            }
            catch (Exception ex)
            {
                SimlBot.Logger.Error(ex);
            }
            return string.Empty;
        }

        public bool IsRecursive
        {
            get
            {
                return true;
            }
        }

        public XName TagName
        {
            get
            {
                return (XName)(SimlSpecification.Namespace.O + "Clipboard");
            }
        }
    }
}