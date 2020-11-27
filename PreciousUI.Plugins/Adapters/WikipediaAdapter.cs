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
using PreciousUI.Plugins.Internals.Wikipedia;

namespace PreciousUI.Plugins.Adapters
{
    public class WikipediaAdapter : IAdapter
    {
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string task = context.Element.Attribute("Task").Value;
                string name = context.Element.Attribute("Name").Value;
                switch (task.ToLower())
                {
                    case "search-wikipedia":
                        {
                            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                            {
                                WikipediaPlugin wikipediaPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WikipediaPlugin>();
                                wikipediaPlugin.QueryName = name;
                                var queryResult = wikipediaPlugin.GenerateQueryResult();
                                if (queryResult != null && queryResult.Search.Any())
                                {
                                    context.Data.Add(queryResult);
                                    return string.Format(Resources.DefineWordSucessMessage, queryResult.Search.First().GetExtract());
                                }
                            }
                            else
                            {
                                WordNetPlugin wordNetPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WordNetPlugin>();
                                wordNetPlugin.QueryName = name;
                                var synSetResult = wordNetPlugin.GenerateSynSetResult();
                                if (synSetResult != null)
                                {
                                    context.Data.Add(synSetResult);
                                    return string.Format(Resources.DefineWordSucessMessage, synSetResult.CommonSynSet);
                                }

                            }
                            return string.Format(Resources.NoInternetAccessMessage, name, string.Empty);
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
            get { return (XName)(SimlSpecification.Namespace.O + "Wikipedia"); }
        }
    }
}