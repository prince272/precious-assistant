using Docs.Word;
using NHunspell;
using PreciousUI.Plugins.Internals.Wikipedia;
using Syn.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Plugins.Internals
{
    public class RtfDocumentHelper
    {
        public static Color PrimaryColor { get { return Color.FromArgb(0, 122, 204); } }

        public static string GetRtfText(object[] parameters)
        {
            if (parameters == null) return string.Empty;
            Document doc = Document.CreateNew();
            Write(doc, parameters.OfType<BotResponse>().FirstOrDefault());
            Write(doc, parameters.OfType<KJBibleResult>().FirstOrDefault());
            Write(doc, parameters.OfType<SynSetResult>().FirstOrDefault());
            Write(doc, parameters.OfType<ThesResult>().FirstOrDefault());
            Write(doc, parameters.OfType<SuggestionResult>().FirstOrDefault());
            Write(doc, parameters.OfType<QueryResult>().FirstOrDefault());
            Write(doc, parameters.OfType<PathResult>().FirstOrDefault());
            Write(doc, parameters.OfType<HyphenResult>().FirstOrDefault());
            return doc.WriteRTF(Encoding.Default);
        }

        private static void Write(Document doc, HyphenResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            //var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode("Hyphenated Word"));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            tun = par.AddTextrun(" ");
            tun = par.AddTextrun(RtfUtility.unicodeEncode(result.HyphenatedWord));
            tun.Style.FontStyle.Italic = true;
            tun.Style.TextColor = Color.DimGray;
            sec.AddParagraph();
        }
        private static void Write(Document doc, BotResponse response)
        {
            if (response == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode("Chat Message"));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            par = sec.AddParagraph(RtfUtility.unicodeEncode(response.Text));
            par.Style.Spacings.SpacingBefore = 5F;

            if (response.Rank == Syn.VA.ResponseRank.Low)
            {
                par.AddTextrun(" ");
                tun = par.AddTextrun(RtfUtility.unicodeEncode("Please CLICK HERE to search more info from Google."));
                tun = par.GetTextrun(response.Text.Length + 8, 10);
                hyp = tun.AddHyperlink(LinkType.WebPage, RtfUtility.unicodeEncode(LinkVariables.FindSearchAddress(LinkVariables.Google, response.From)));
                hyp.Textruns.Style.TextColor = Color.Black;
                hyp.Textruns.Style.FontStyle.Bold = true;
                hyp.Textruns.Style.FontStyle.Underlined = false;
            }
            sec.AddParagraph();
        }
        private static void Write(Document doc, KJBibleResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            //var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            par.Style.Spacings.SpacingAfter = 15F;
            tun = par.AddTextrun(RtfUtility.unicodeEncode("King James Version"));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode("BIBLE QUOTATION"));
            tun.Style.FontStyle.Bold = true;

            int count = 1;
            foreach (var item in result.Quotations)
            {
                par = sec.AddParagraph();
                par.Style.Spacings.SpacingBefore = 10F;
                par.Style.Spacings.SpacingAfter = 5F;

                tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("{0} {1}:{2}", item.Book.Book, item.Chapter, item.Verse)));
                tun.Style.FontStyle.Bold = true;
                par.AddLineBreak();
                tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("{0}", item.Scripture)));
                count++;
            }
            sec.AddParagraph();
        }
        private static void Write(Document doc, SynSetResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            //var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("Definition Of {0}", result.CommonSynSet.Word)));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            foreach (var group in result.SynSets.GroupBy(i => i.PartOfSpeech))
            {
                par = sec.AddParagraph();
                par.Style.Spacings.SpacingBefore = 15F;
                tun = par.AddTextrun(RtfUtility.unicodeEncode(group.Key.ToUpper()));
                tun.Style.FontStyle.Bold = true;

                int count = 1;
                foreach (var syn in group)
                {
                    par = sec.AddParagraph();
                    par.Style.Spacings.SpacingBefore = 10F;
                    tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("{0}. {1}", count, SynUtility.Text.UppercaseFirstLetter(syn.Defination))));
                    if (syn.Examples.FirstOrDefault() != null)
                    {
                        tun = par.AddTextrun(" ");
                        tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("example: {0}", SynUtility.Text.GetFormattedSentence(syn.Examples.ToList()))));
                        tun.Style.TextColor = Color.FromArgb(88, 88, 92);
                    }
                    if (syn.Examples.Any())
                    {
                        par.AddLineBreak();
                        tun = par.AddTextrun(RtfUtility.unicodeEncode(SynUtility.Text.GetFormattedSentence(syn.Synonyms.ToList())));
                    }
                    count++;
                }
            }
            sec.AddParagraph();
        }
        private static void Write(Document doc, ThesResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            //var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            par.Style.Spacings.SpacingAfter = 15F;
            tun = par.AddTextrun(RtfUtility.unicodeEncode("Synonyms Result"));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode("SYNONYMS"));
            tun.Style.FontStyle.Bold = true;

            int count = 1;
            foreach (var item in result.Meanings.SelectMany(i => i.Synonyms).Distinct())
            {
                par = sec.AddParagraph(RtfUtility.unicodeEncode(string.Format("{0}. {1}", count, SynUtility.Text.UppercaseFirstLetter(item))));
                count++;
            }
            sec.AddParagraph();
        }
        private static void Write(Document doc, SuggestionResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            //var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            par.Style.Spacings.SpacingAfter = 15F;
            tun = par.AddTextrun(RtfUtility.unicodeEncode("Suggestions Result"));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode("SUGGESTIONS"));
            tun.Style.FontStyle.Bold = true;

            int count = 1;
            foreach (var suggest in result.Suggestions)
            {
                par = sec.AddParagraph(RtfUtility.unicodeEncode(string.Format("{0}. {1}", count, SynUtility.Text.UppercaseFirstLetter(suggest))));
                count++;
            }

            sec.AddParagraph();
        }
        private static void Write(Document doc, QueryResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            //var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            par.Style.Spacings.SpacingAfter = 15F;
            tun = par.AddTextrun(RtfUtility.unicodeEncode("Online Results"));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            foreach (Wikipedia.Search s in result.Search)
            {
                par = sec.AddParagraph();
                tun = par.AddTextrun(RtfUtility.unicodeEncode(s.Title));
                tun.Style.FontStyle.Bold = true;

                par = sec.AddParagraph();
                par.Style.Spacings.SpacingAfter = 15F;
                par.AddTextrun(RtfUtility.unicodeEncode(s.GetExtract()));
            }

            sec.AddParagraph();
        }
        private static void Write(Document doc, PathResult result)
        {
            if (result == null) return;
            var sec = doc.Sections.Last;
            var par = (Paragraph)null;
            var tun = (Textrun)null;
            var hyp = (Hyperlink)null;

            par = sec.AddParagraph();
            tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("System Search Results +{0}", result.Paths.Length)));
            tun.Style.FontSize = 24F;
            tun.Style.TextColor = PrimaryColor;

            par = sec.AddParagraph();
            par.Style.Spacings.SpacingBefore = 15F;
            tun = par.AddTextrun("SEARCH SUGGESTIONS");
            tun.Style.FontStyle.Bold = true;

            int count = 1;
            foreach (var path in result.Paths)
            {
                par = sec.AddParagraph();
                par.Style.Spacings.SpacingBefore = 15F;

                tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("{0}. {1}", count, path.Name)));
                hyp = tun.AddHyperlink(LinkType.ExternalFile, RtfUtility.unicodeEncode(path.FullName.Replace(@"\", @"\\\\")));
                hyp.Textruns.Style.TextColor = Color.FromArgb(31, 78, 121);
                hyp.Textruns.Style.FontStyle.Underlined = false;

                par.AddTextrun(" ");
                tun = par.AddTextrun(RtfUtility.unicodeEncode(string.Format("- from {0} directory", path.DirectoryName)));
                tun.Style.TextColor = Color.DimGray;
                tun.Style.FontStyle.Italic = true;
                count++;
            }

            sec.AddParagraph();
        }
    }
}
