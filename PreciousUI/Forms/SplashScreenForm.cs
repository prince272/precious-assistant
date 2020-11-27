using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using PreciousUI.Properties;
using DevExpress.Utils.Drawing.Helpers;
using PreciousUI.Internals;

namespace PreciousUI.Forms
{
    public partial class SplashScreenForm : SplashScreen
    {
        public SplashScreenForm()
        {
            InitializeComponent();
            FadeTimer.Interval = 1;
            statusLabelControl.Text = Resources.WaitMessage;
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            if (Enum.Equals(cmd, SplashScreenCommand.Status))
            {
                this.statusLabelControl.Text = string.Format("{0}", arg);
            }
        }

        #endregion

        public enum SplashScreenCommand
        {
            Status
        }

        void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, (int)WinApiHelper.WinMessages.WM_NCLBUTTONDOWN, WinApiHelper.HT_CAPTION, IntPtr.Zero);
            }
        }
    }
}