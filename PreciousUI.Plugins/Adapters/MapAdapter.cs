using PreciousUI.Plugins.Internals;
using PreciousUI.Plugins.Properties;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PreciousUI.Plugins.Adapters
{
    public class MapAdapter : IAdapter
    {
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string task = context.Element.Attribute("Task").Value;
                string name = context.Element.Attribute("Name").Value;
                string link = LinkVariables.FindSearchAddress(LinkVariables.GoogleMap, name);
                ProcessHelper.Start(link, string.Empty);
                return string.Format(Resources.ShowMapSucessMessage, link);
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
            get { return (XName)(SimlSpecification.Namespace.O + "Map"); }
        }
    }
}