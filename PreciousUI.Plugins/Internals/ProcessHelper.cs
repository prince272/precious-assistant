using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Plugins.Internals
{
    public class ProcessHelper {
        public static void Start(string fileName, string argument) {
            try {
                var p = new Process();
                p.StartInfo.Arguments = argument;
                p.StartInfo.ErrorDialog = false;
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(fileName);
                p.StartInfo.FileName = fileName;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            } catch (Exception) {

            } finally {
            }
        }

        public static bool Close(string path, bool force) {
            int trace = 0;
            try {
                var processes = GetProcesses(path);
                foreach (var p in processes) {
                    if (force)
                        p.Kill();
                    else
                        p.CloseMainWindow();
                    trace = 1;
                }
            } catch (Exception) {

            }
            return trace != 0;
        }

        public static Process[] GetProcesses(string path) {
            if (ShellObject.IsPlatformSupported) {
                var shellObj = ShellObject.FromParsingName(path);
                if (shellObj.IsLink) {
                    path = shellObj.Properties.System.Link.TargetParsingPath.Value;
                }
            }
            if (Path.GetExtension(path).ToLower() == ".exe") {
                return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(path));
            }
            return FileUtil.WhoIsLocking(path).ToArray();
        }
    }
}
