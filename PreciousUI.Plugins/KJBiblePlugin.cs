using BibleNet.KJV;
using Syn.VA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Plugins
{
    public class KJBiblePlugin : Plugin
    {
        private KJBible kjBible;

        public string QueryName { get; set; }
        public bool IsQueryLimited { get; set; }
        public TestamentType QueryTestament { get; set; }

        public KJBiblePlugin()
        {
            this.kjBible = new KJBible();
        }

        public KJBibleResult GetVerseResult()
        {
            try
            {
                var verses = kjBible.GetVerses(QueryName, TestamentType.New | TestamentType.Old);
                if (verses != null && verses.Any())
                    return new KJBibleResult(verses.ToArray());
            }
            catch (Exception) { }
            return null;
        }

        public KJBibleResult GetQuotationResult()
        {
            try
            {
                var quotations = kjBible.GetQuotations(QueryName, IsQueryLimited);
                if (quotations != null && quotations.Any())
                    return new KJBibleResult(quotations.ToArray());
            }
            catch (Exception) { }
            return null;
        }
    }

    public class KJBibleResult
    {
        public KJBibleResult(KJBibleItem[] quotations)
        {
            Quotations = quotations;
            CommonQuotation = quotations[0];
        }

        public KJBibleItem CommonQuotation { get; private set; }
        public KJBibleItem[] Quotations { get; private set; }
    }
}