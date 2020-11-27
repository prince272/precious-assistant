using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Internals
{
    public static class ProcessHelper
    {

        public static bool WaitForMainWindow(this Process process)
        {
            while (!process.HasExited && process.MainWindowHandle == IntPtr.Zero)
                Application.DoEvents();
            return !process.HasExited;
        }

        public static bool WaitForMainWindow(this Process process, uint timeout)
        {
            var start = DateTime.Now;
            while (!process.HasExited && process.MainWindowHandle == IntPtr.Zero)
            {
                Application.DoEvents();
                if ((DateTime.Now - start).TotalMilliseconds >= timeout)
                {
                    return false;
                }
            }
            return !process.HasExited;
        }

        public static void Start(string fileName, string arguments)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.Arguments = arguments;
                process.StartInfo.ErrorDialog = false;
                process.StartInfo.FileName = fileName;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(fileName);
                process.Start();
                process.WaitForMainWindow(3000);
            }
            catch (Exception)
            {

            }
        }
    }
}