using Microsoft.WindowsAPICodePack.Shell;
using PreciousUI.Plugins.Internals;
using Syn.VA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Plugins
{
    public class DSearchPlugin : Plugin
    {
        public string QueryName { get; set; }
        public FileKind QueryKind { get; set; }
        public int QueryLimit { get; set; }

        public DSearchPlugin()
        {
            QueryLimit = 20;
        }

        public PathResult GeneratePathResult()
        {
            try
            {
                List<string> files = new List<string>();
                files.AddRange(DSearchHelper.GetFiles(QueryName, QueryKind));
                if (files.Count != 0)
                    return new PathResult(files.Select(f => new PathInfo(f)).ToArray());
            }
            catch (Exception) { }
            return null;
        }
    }

    public class PathResult
    {
        public PathResult(PathInfo[] paths)
        {
            this.Paths = paths;
            this.CommonPath = paths[0];
        }

        public PathInfo[] Paths { get; private set; }

        public PathInfo CommonPath { get; private set; }
    }

    public class PathInfo
    {
        public PathInfo(string path)
        {
            this.FullName = path;
            this.Name = Path.GetFileNameWithoutExtension(path);
            this.FullDirectoryName = Path.GetDirectoryName(path);
            this.DirectoryName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(path));
        }

        public string FullName { get; private set; }
        public string Name { get; private set; }

        public string FullDirectoryName { get; private set; }
        public string DirectoryName { get; private set; }
    }

    [Flags]
    public enum FileKind {
        Unknown = -1,
        Apps = 1,
        Documents = 2,
        Music = 4,
        Photos = 8,
        Videos = 16,
        Folder = 32,
        All = 127,
    }
}