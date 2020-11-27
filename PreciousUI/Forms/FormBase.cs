using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PreciousUI.Forms
{
    public partial class FormBase : DevExpress.XtraEditors.XtraForm
    {
        public FormBase()
        {
            InitializeComponent();
            Icon = Program.AppIcon;
        }

        protected override FormShowMode ShowMode { get { return FormShowMode.AfterInitialization; } }
    }
}