using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Plugins.Internals {
    internal class DSearchHelper {
        public static string[] GetFiles(string fileName, FileKind fileKind) {
            string arguments = string.Format("{0} AND {1}", ParseFileKind(fileKind), ParseFileName(fileName));
            List<string> files = new List<string>();
            files.AddRange(GetFiles(arguments));
            if (fileKind.HasFlag(FileKind.Apps))
                files.AddRange(GetSystemFiles(fileName));
            return files.ToArray();
        }
        public static string[] GetFiles(string fileName, string fileExtension) {
            string arguments = string.Format("{0} {1}", ParseFileExtension(fileExtension), ParseFileName(fileName));
            string[] files = GetFiles(arguments);
            return files;
        }

        static string[] GetFiles(string arguments) {
            List<string> list = new List<string>();
            try {
                using (var process = new Process()) {
                    process.StartInfo.Arguments = "/b " + arguments;
                    process.StartInfo.FileName = "dirsearch.exe";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.ErrorDialog = false;
                    // Execute FINDSTR
                    process.Start();
                    StreamReader output = process.StandardOutput;
                    String line;
                    while ((line = output.ReadLine()) != null) {
                        line = FixPath(line);
                        if (!string.IsNullOrEmpty(line))
                            list.Add(line);
                    }
                }
            } catch (Exception) { }
            return list.ToArray();
        }
        static string[] GetSystemFiles(string name) {
            string directory = Environment.SystemDirectory;
            string path = directory + "\\" + name.Trim() + ".exe";
            if (File.Exists(path))
                return new string[] { path };
            return new string[0];
        }
        static string ParseFileName(string name) {
            string result = string.Empty;
            result += string.Format("name:\"{0}\"", name);
            result += string.Format(" OR title:\"{0}\"", name);
            return result;
        }
        static string ParseFileKind(FileKind kind) {
            if (kind == FileKind.Unknown)
                return "";
            FileKind[] kinds = ((FileKind[])Enum.GetValues(typeof(FileKind))).Where(x => kind.HasFlag(x)).ToArray();
            kinds = kinds.Where(x => x != FileKind.All).ToArray();
            string parseKind = string.Empty;
            for (int i = 0; i < kinds.Length; i++) {

                if (kinds[i] == FileKind.Apps)
                    parseKind += "kind:program";
                else if (kinds[i] == FileKind.Documents)
                    parseKind += "kind:docs";
                else if (kinds[i] == FileKind.Music)
                    parseKind += "kind:music";
                else if (kinds[i] == FileKind.Photos)
                    parseKind += "kind:pics";
                else if (kinds[i] == FileKind.Videos)
                    parseKind += "kind:video";
                else if (kinds[i] == FileKind.Folder)
                    parseKind += "kind:folder";

                if (i < kinds.Length - 1)
                    parseKind += " OR ";
            }
            return parseKind;
        }
        static string ParseFileExtension(string extension) {
            if (string.IsNullOrEmpty(extension))
                return string.Empty;
            return "ext:" + extension;
        }
        static string FixPath(string path) {
            path = Path.GetFullPath(path);
            if (Directory.Exists(path) || File.Exists(path))
                return path;

            string directory = Path.HasExtension(path) ? Path.GetDirectoryName(path) : path;
            string filename = Path.HasExtension(path) ? Path.GetFileName(path) : string.Empty;
            string[] folders = directory.Split('\\');
            string result = folders.FirstOrDefault();
            for (int i = 1; i < folders.Length; i++) {

                string parse = result + "\\" + folders[i];
                if (Directory.Exists(parse)) {
                    result = parse;
                    continue;
                }
                parse = FindNonLocalizedName(parse);
                if (!string.IsNullOrEmpty(parse))
                    result = result + "\\" + parse;
            }
            result = result + "\\" + filename;
            return result;
        }

        static string FindNonLocalizedName(string path) {
            string result = string.Empty;
            string[] directories = Directory.GetDirectories(Path.GetDirectoryName(path));
            for (int i = 0; i < directories.Length; i++) {
                var folder = ShellFolder.FromParsingName(directories[i]);
                if (folder != null) {
                    string localizedName1 = Path.GetFileName(path);
                    string localizedName2 = ShellFolder.FromParsingName(directories[i]).Name;
                    if (string.Equals(localizedName1, localizedName2,
                        StringComparison.InvariantCultureIgnoreCase)) {
                        result = Path.GetFileName(directories[i]);
                        break;
                    }
                }
            }

            return result;
        }

        static bool CheckIfPathEquals(string path1, string path2) {
            path1 = Path.GetFullPath(path1);
            path2 = Path.GetFullPath(path2);
            return string.Equals(path1, path2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
