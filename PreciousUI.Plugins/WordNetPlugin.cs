using NHunspell;
using Syn.Utility;
using Syn.VA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wnlib;
using WordNetClasses;

namespace PreciousUI.Plugins
{
    public class WordNetPlugin : Plugin
    {
        private WN _wordNet;
        private Hyphen _hyphen;
        private Hunspell _hunspell;
        private MyThes _thes;

        public WordNetPlugin(string wordNetDirectory)
        {
            try
            {
                string spellPath = wordNetDirectory + "\\spell";
                string dictPath = wordNetDirectory + "\\dict\\";
                _wordNet = new WN(dictPath);
                _hunspell = new Hunspell(Path.Combine(spellPath, "en_us.aff"), Path.Combine(spellPath, "en_us.dic"));
                _thes = new MyThes(Path.Combine(spellPath, "th_en_US_new.dat"));
                _hyphen = new Hyphen(Path.Combine(spellPath, "hyph_en_US.dic"));
            }
            catch (Exception ex)
            {
                VirtualAssistant.Instance.Logger.Error(ex);
            }
        }

        public string QueryName { get; set; }

        public SynSetResult GenerateSynSetResult()
        {
            try
            {
                string word = FormatWord(QueryName);
                var searchs = FindAllSearch(word).OfType<Wnlib.Search>().SelectMany(item => item.senses.OfType<SynSet>().
                         Select(syn => new SynSetInfo(item.word, syn)));
                if (searchs.Any())
                    return new SynSetResult(searchs.ToArray());
            }
            catch (Exception) { }
            return null;
        }

        public ThesResult GenerateThesResult()
        {
            try
            {
                string word = FormatWord(QueryName);
                return _thes.Lookup(word, _hunspell);
            }
            catch (Exception) { }
            return null;
        }

        public SuggestionResult GenerateSuggestionResult()
        {
            try
            {
                string word = FormatWord(QueryName);
                var suggestions = _hunspell.Suggest(word);
                if (suggestions != null && suggestions.Any())
                    return new SuggestionResult(suggestions.ToArray());
            }
            catch (Exception) { 
            }
            return null;
        }

        public HyphenResult GenerateHyphenResult()
        {
            try
            {
                string word = FormatWord(QueryName);
                return _hyphen.Hyphenate(word);
            }
            catch (Exception) { }
            return null;
        }

        IEnumerable<Search> FindAllSearch(string word)
        {
            var list = new ArrayList();
            SearchSet bobj2 = null;
            bool sucess = false;
            _wordNet.OverviewFor(word, "noun", ref sucess, ref bobj2, list);
            _wordNet.OverviewFor(word, "verb", ref sucess, ref bobj2, list);
            _wordNet.OverviewFor(word, "adj", ref sucess, ref bobj2, list);
            _wordNet.OverviewFor(word, "adv", ref sucess, ref bobj2, list);
            return list.OfType<Wnlib.Search>();
        }

        string FormatWord(string word)
        {
            word = word.Trim();
            word = word.Replace(' ', '_');
            word = word.StartsWith("a_") ? word.Remove(0, 2) : word;
            word = word.StartsWith("an_") ? word.Remove(0, 2) : word;
            return word;
        }
    }

    public class SuggestionResult
    {
        public SuggestionResult(string[] suggestions)
        {
            this.Suggestions = suggestions;
            this.CommonSuggestion = suggestions[0];
        }

        public string CommonSuggestion { get; private set; }
        public string[] Suggestions { get; private set; }
    }

    public class SynSetResult
    {
        public SynSetResult(SynSetInfo[] synsets)
        {
            this.SynSets = synsets;
            this.CommonSynSet = synsets[0];
        }

        public SynSetInfo[] SynSets { get; private set; }

        public SynSetInfo CommonSynSet { get; private set; }
    }

    public class SynSetInfo
    {
        private Wnlib.SynSet synSet;
        private string pOfSpeech;
        private IReadOnlyCollection<string> synonyms;
        private string defination;
        private IReadOnlyCollection<string> examples;
        private string word;

        public SynSetInfo(string word, Wnlib.SynSet synSet)
        {
            this.synSet = synSet;
            this.word = SynUtility.Text.UppercaseFirstLetter(word);
            this.defination = GetDefination();
            this.pOfSpeech = GetPartOfSpeech();
            this.synonyms = GetSynonyms();
            this.examples = GetExamples();
        }

        private string GetDefination()
        {
            string def = synSet.defn.Trim();
            def = Regex.Replace(def, "\".+\"", "");
            def = Regex.Replace(def, "\".+\"", "");
            def = def.Trim();
            if (!def.EndsWith(".") && !def.EndsWith(";") && !def.EndsWith(":"))
            {
                def += '.';
            }
            return def;
        }
        private IReadOnlyCollection<string> GetExamples()
        {
            return Regex.Matches(synSet.defn, "\".+\"").OfType<Match>().Select(item => SynUtility.Text.UppercaseFirstLetter(item.Value.Trim())).ToArray();
        }

        private IReadOnlyCollection<string> GetSynonyms()
        {
            List<string> list = new List<string>();
            foreach (Lexeme lex in synSet.words)
            {
                string word = FormatSynonym(lex.word);
                list.Add(word);
            }
            return list.Distinct().ToArray();
        }

        private string GetPartOfSpeech()
        {
            switch (synSet.pos.flag)
            {
                case Wnlib.PartsOfSpeech.Adj:
                    return "Adjective";
                case Wnlib.PartsOfSpeech.Adv:
                    return "Adverb";
                case Wnlib.PartsOfSpeech.Noun:
                    return "Noun";
                case Wnlib.PartsOfSpeech.Verb:
                    return "Verb";
                default:
                    return "Unknown";

            }
        }

        public static string FormatSynonym(string synonym)
        {
            string[] split = synonym.Split(new char[] { '_' },
            StringSplitOptions.RemoveEmptyEntries).Select(i => SynUtility.Text.UppercaseFirstLetter(i)).ToArray();
            return (string.Join(" ", split));
        }

        public string Word { get { return word.Replace('_', ' '); } }

        public string Defination { get { return defination; } }

        public IReadOnlyCollection<string> Synonyms { get { return synonyms; } }

        public IReadOnlyCollection<string> Examples { get { return examples; } }

        public string PartOfSpeech { get { return pOfSpeech; } }

        public override string ToString()
        {
            return Word + " is " + Defination;
        }
    }
}