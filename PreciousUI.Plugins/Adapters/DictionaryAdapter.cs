using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using Syn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PreciousUI.Plugins.Properties;
using Syn.VA;
using PreciousUI.Plugins.Internals.Wikipedia;

namespace PreciousUI.Plugins.Adapters
{
    public class DictionaryAdapter : IAdapter
    {
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string task = context.Element.Attribute("Task").Value;
                string name = context.Element.Attribute("Name").Value;
                switch (task.ToLower())
                {
                    case "define":
                        {
                            WordNetPlugin wordNetPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WordNetPlugin>();
                            wordNetPlugin.QueryName = name;
                            var synSetResult = wordNetPlugin.GenerateSynSetResult();
                            if (synSetResult != null)
                            {
                                context.Data.Add(synSetResult);
                                return string.Format(Resources.DefineWordSucessMessage, synSetResult.CommonSynSet);
                            }

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
                            return string.Format(Resources.DefineWordFailedMessage, name);
                        }

                    case "synonyms":
                        {
                            WordNetPlugin wordNetPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WordNetPlugin>();
                            wordNetPlugin.QueryName = name;
                            var thesResult = wordNetPlugin.GenerateThesResult();
                            if (thesResult != null)
                            {
                                context.Data.Add(thesResult);
                                return string.Format(Resources.SynonymsWordSucessMessage, 
                                SynUtility.Text.GetFormattedSentence(thesResult.Meanings.SelectMany(i => i.Synonyms).Distinct().ToList()));
                            }
                            return string.Format(Resources.SynonymsWordFailedMessage, name);
                        }

                    case "suggestions":
                        {
                            WordNetPlugin wordNetPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WordNetPlugin>();
                            wordNetPlugin.QueryName = name;
                            var suggestionResult = wordNetPlugin.GenerateSuggestionResult();
                            if (suggestionResult != null)
                            {
                                context.Data.Add(suggestionResult);
                                return string.Format(Resources.SuggestionsWordSucessMessage,
                                SynUtility.Text.GetFormattedSentence(suggestionResult.Suggestions.Distinct().ToList()));
                            }
                            return string.Format(Resources.SuggestionsWordFailedMessage, name);
                        }

                    case "spell-check":
                        {
                            WordNetPlugin wordNetPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WordNetPlugin>();
                            wordNetPlugin.QueryName = name;
                            var hyphenResult = wordNetPlugin.GenerateHyphenResult();
                            if (hyphenResult != null)
                            {
                                context.Data.Add(hyphenResult);
                                return string.Format(Resources.SpellCheckSucessMessage, hyphenResult.HyphenatedWord.Replace("=", "-"));
                            }
                            return string.Format(Resources.SpellCheckFailedMessage, name);
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
            get { return (XName)(SimlSpecification.Namespace.O + "Dictionary"); }
        }
    }
}