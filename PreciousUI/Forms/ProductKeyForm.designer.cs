namespace PreciousUI.Forms {
    partial class ProductKeyForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tePassword = new DevExpress.XtraEditors.TextEdit();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOK = new DevExpress.XtraEditors.SimpleButton();
            this.lcDescription = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tePassword
            // 
            this.tePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tePassword.Location = new System.Drawing.Point(73, 52);
            this.tePassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tePassword.Name = "tePassword";
            this.tePassword.Properties.Mask.BeepOnError = true;
            this.tePassword.Properties.Mask.EditMask = "[A-Za-z0-9]{5}-[A-Za-z0-9]{5}-[A-Za-z0-9]{5}-[A-Za-z0-9]{5}-[A-Za-z0-9]{5}";
            this.tePassword.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.tePassword.Properties.Mask.ShowPlaceHolders = false;
            this.tePassword.Size = new System.Drawing.Size(328, 24);
            this.tePassword.TabIndex = 0;
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbCancel.Location = new System.Drawing.Point(355, 122);
            this.sbCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(79, 23);
            this.sbCancel.TabIndex = 1;
            this.sbCancel.Text = "Cancel";
            // 
            // sbOK
            // 
            this.sbOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbOK.Location = new System.Drawing.Point(259, 122);
            this.sbOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sbOK.Name = "sbOK";
            this.sbOK.Size = new System.Drawing.Size(79, 23);
            this.sbOK.TabIndex = 2;
            this.sbOK.Text = "OK";
            this.sbOK.Click += new System.EventHandler(this.sbOK_Click);
            // 
            // lcDescription
            // 
            this.lcDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lcDescription.Location = new System.Drawing.Point(73, 32);
            this.lcDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lcDescription.Name = "lcDescription";
            this.lcDescription.Size = new System.Drawing.Size(243, 17);
            this.lcDescription.TabIndex = 3;
            this.lcDescription.Text = "Please enter your product key to continue";
            // 
            // ProductKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 157);
            this.Controls.Add(this.lcDescription);
            this.Controls.Add(this.sbOK);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.tePassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductKeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Key";
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit tePassword;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOK;
        private DevExpress.XtraEditors.LabelControl lcDescription;
    }
}