using PreciousUI.Plugins.Internals.Wikipedia;
using Syn.VA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Plugins
{
    public class WikipediaPlugin : Plugin
    {
        private WikipediaApi _wikipediaApi;

        public WikipediaPlugin()
        {
            this._wikipediaApi = new WikipediaApi(Language.English)
            {
                UseTLS = true,
                Limit = 5,
                What = What.Text
            };
        }

        public QueryResult GenerateQueryResult()
        {
            try
            {
                return this._wikipediaApi.Search(QueryName);
            }
            catch (Exception) { }
            return null;
        }

        public string QueryName { get; set; }
    }
}