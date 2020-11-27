using PreciousUI.Plugins.Internals;
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
    public class AppAdapter : IAdapter
    {
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string task = context.Element.Attribute("Task").Value;
                string name = context.Element.Attribute("Name").Value;
                switch (task.ToLower())
                {
                    case "open-app":
                        {
                            DSearchPlugin dSearchPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<DSearchPlugin>();
                            dSearchPlugin.QueryName = name;
                            dSearchPlugin.QueryKind = FileKind.Apps | FileKind.Videos | FileKind.Music;
                            var pathResult = dSearchPlugin.GeneratePathResult();
                            if (pathResult != null)
                            {
                                context.Data.Add(pathResult);
                                ProcessHelper.Start(pathResult.CommonPath.FullName, string.Empty);
                                return string.Format(Resources.OpenAppSucessMessage, name);
                            }

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
                            return string.Format(Resources.OpenAppFailedMessage, name);
                        }
                    case "close-app":
                        {
                            DSearchPlugin dSearchPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<DSearchPlugin>();
                            dSearchPlugin.QueryName = name;
                            dSearchPlugin.QueryKind = FileKind.Apps | FileKind.Videos | FileKind.Music;
                            var pathResult = dSearchPlugin.GeneratePathResult();
                            if (pathResult != null)
                            {
                                //context.Data.Add(data.PathResult);
                                if (ProcessHelper.Close(pathResult.CommonPath.FullName, true))
                                    return string.Format(Resources.CloseAppSucessMessage, name);
                            }
                            return string.Format(Resources.CloseAppFailedMessage, name);
                        }
                    case "search-app":
                        {
                            DSearchPlugin dSearchPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<DSearchPlugin>();
                            dSearchPlugin.QueryName = name;
                            dSearchPlugin.QueryKind = FileKind.Apps | FileKind.Videos | FileKind.Music;
                            var pathResult = dSearchPlugin.GeneratePathResult();
                            if (pathResult != null)
                            {
                                context.Data.Add(pathResult);
                                return string.Format(Resources.SearchAppSucessMessage, name);
                            }

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

                            return string.Format(Resources.SearchAppFailedMessage, name);
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
            get { return (XName)(SimlSpecification.Namespace.O + "App"); }
        }
    }
}