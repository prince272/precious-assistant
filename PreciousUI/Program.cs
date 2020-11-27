using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using PreciousUI.Internals;
using Syn.VA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace PreciousUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments) {
            ExceptionHelper.Default.Initialize();
            bool isRunning;
            using (DataDirectoryHelper.SingleInstanceApplicationGuard(Application.ProductName + " 1.5", out isRunning)) {
                // Return if the application is already running.
                if (isRunning) return;

                // Enable visual styles for the application.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Set a default skin that is used for every control on this current thread.
                SkinAppearanceHelper.SetDefaultAppearance();

                var Security = new ProgramSecurity(DataDirectoryHelper.GetProductKeys(), "Humosoft\\Security");
                if (!Security.Algorithm()) return;

                // Starts the application.
                Application.Run(MainForm = new MainForm(arguments) { CancelWhenClosing = false });
            }
        }
        public static MainForm MainForm { get; private set; }
        public static Icon AppIcon { get { return ResourceImageHelper.CreateIconFromResources("PreciousUI.AppIcon.ico", Assembly.GetExecutingAssembly()); } }
    }
}
