using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using PreciousUI.Properties;

namespace PreciousUI.Forms
{
    public partial class WaitIndicatorForm : WaitForm
    {
        public WaitIndicatorForm()
        {
            InitializeComponent();
            this.FadeTimer.Interval = 1;
            this.progressPanel1.AutoHeight = true;
            this.progressPanel1.Caption = Resources.WaitMessage;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }
    }
}