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
using PreciousUI.Forms;
using PreciousUI.Internals;

namespace PreciousUI.Forms
{
    public partial class ProductKeyForm : FormBase
    {
        private ProgramSecurity security;

        public ProductKeyForm(ProgramSecurity security)
        {
            if (security == null)
                throw new ArgumentNullException("security");
            this.security = security;
            InitializeComponent();
        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            //if password true then send true			
            bool sucess = security.PasswordEntry(tePassword.Text);

            if (!sucess)
            {
                XtraMessageBox.Show("Product Key is not valid! Please Enter a valid Product Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            XtraMessageBox.Show("Thank you for activation!", "Activate", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}