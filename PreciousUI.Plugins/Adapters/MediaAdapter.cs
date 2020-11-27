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
    public class MediaAdapter : IAdapter
    {
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string task = context.Element.Attribute("Task").Value;
                string name = context.Element.Attribute("Name").Value;
                switch (task.ToLower())
                {
                    case "play-media":
                        {
                            DSearchPlugin dSearchPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<DSearchPlugin>();
                            dSearchPlugin.QueryName = name;
                            dSearchPlugin.QueryKind = FileKind.Videos | FileKind.Music;
                            var pathResult = dSearchPlugin.GeneratePathResult();
                            if (pathResult != null)
                            {
                                context.Data.Add(pathResult);
                                ProcessHelper.Start(pathResult.CommonPath.FullName, string.Empty);
                                return string.Format(Resources.PlayMediaSucessMessage, name);
                            }
                            return string.Format(Resources.PlayMediaFailedMessage, name);
                        }
                    case "stop-media":
                        {
                            DSearchPlugin dSearchPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<DSearchPlugin>();
                            dSearchPlugin.QueryName = name;
                            dSearchPlugin.QueryKind = FileKind.Videos | FileKind.Music;
                            var pathResult = dSearchPlugin.GeneratePathResult();
                            if (pathResult != null)
                            {
                                context.Data.Add(pathResult);
                                ProcessHelper.Close(pathResult.CommonPath.FullName, true);
                                return string.Format(Resources.StopMediaSucessMessage, name);
                            }
                            return string.Format(Resources.StopMediaFailedMessage, name);
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
            get { return (XName)(SimlSpecification.Namespace.O + "Media"); }
        }
    }
}