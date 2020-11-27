using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PreciousUI.Internals
{
    public class BeceFileInfo
    {
        private const string pattern = @"\b((BECE\s*Past\s*Questions\s*\&\s*Answers)\s*\-\s*(?<Year>\d{4})\s*\(\s*(?<Subject>((\w+)\s*){1,3})\))";

        public BeceFileInfo(string filename)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException();
            string name = Path.GetFileNameWithoutExtension(filename);
            Match match = Regex.Match(name, pattern, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new ArgumentException("The file is invalid.", "filename");
            this.Subject = match.Groups["Subject"].Value.Trim();
            this.Year = int.Parse(match.Groups["Year"].Value.Trim());
            this.FileName = filename;
        }

        public string Subject { get; private set; }

        public int Year { get; private set; }

        public string FileName { get; private set; }

        public static BeceFileInfo[] GetBECEFileInfoCollection(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".htm") || s.EndsWith(".docx"));
            return files.Select(i => new BeceFileInfo(i)).ToArray();
        }
    }
}