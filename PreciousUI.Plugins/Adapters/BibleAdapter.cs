using PreciousUI.Plugins.Properties;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using Syn.VA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PreciousUI.Plugins.Adapters
{
    public class BibleAdapter : IAdapter
    {
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string task = context.Element.Attribute("Task").Value;
                string name = context.Element.Attribute("Name").Value;
                switch (task.ToLower())
                {
                    case "quote-bible":
                        {
                            KJBiblePlugin kjBiblePlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<KJBiblePlugin>();
                            kjBiblePlugin.QueryName = name;
                            kjBiblePlugin.IsQueryLimited = false;
                            kjBiblePlugin.QueryTestament = BibleNet.KJV.TestamentType.Old | BibleNet.KJV.TestamentType.New;
                            var quotationResult = kjBiblePlugin.GetQuotationResult();
                            if (quotationResult != null)
                            {
                                context.Data.Add(quotationResult);
                                return string.Format(Resources.OpenBibleSucessMessage, quotationResult.CommonQuotation);
                            }
                            return string.Format(Resources.OpenBibleFailedMessage, name);
                        }

                    case "search-verse":
                        {
                            KJBiblePlugin kjBiblePlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<KJBiblePlugin>();
                            kjBiblePlugin.QueryName = name;
                            kjBiblePlugin.IsQueryLimited = false;
                            kjBiblePlugin.QueryTestament = BibleNet.KJV.TestamentType.Old | BibleNet.KJV.TestamentType.New;
                            var verseResult = kjBiblePlugin.GetVerseResult();
                            if (verseResult != null)
                            {
                                context.Data.Add(verseResult);
                                return string.Format(Resources.OpenBibleSucessMessage, verseResult.CommonQuotation);
                            }
                            return string.Format(Resources.OpenBibleFailedMessage, name);
                        }
                }
            }
            catch (Exception ex) { SimlBot.Logger.Error(ex); }
            return Resources.EvaluateErrorMessage;
        }

        bool IAdapter.IsRecursive
        {
            get { return true; }
        }

        System.Xml.Linq.XName IAdapter.TagName
        {
            get { return (XName)(SimlSpecification.Namespace.O + "Bible"); }
        }
    }
}