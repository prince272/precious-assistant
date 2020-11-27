namespace PreciousUI.Modules.Panels
{
    partial class ConversationPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            this.waitLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // richEditControl
            // 
            this.richEditControl.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl.Location = new System.Drawing.Point(0, 0);
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.Options.Behavior.ShowPopupMenu = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditControl.Options.Hyperlinks.ModifierKeys = System.Windows.Forms.Keys.None;
            this.richEditControl.Options.Hyperlinks.ShowToolTip = false;
            this.richEditControl.ReadOnly = true;
            this.richEditControl.ShowCaretInReadOnly = false;
            this.richEditControl.Size = new System.Drawing.Size(931, 711);
            this.richEditControl.TabIndex = 0;
            this.richEditControl.Views.SimpleView.Padding = new System.Windows.Forms.Padding(20, 10, 20, 0);
            // 
            // waitLabelControl
            // 
            this.waitLabelControl.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitLabelControl.Appearance.Options.UseFont = true;
            this.waitLabelControl.Appearance.Options.UseTextOptions = true;
            this.waitLabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.waitLabelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.waitLabelControl.AutoEllipsis = true;
            this.waitLabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.waitLabelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waitLabelControl.Location = new System.Drawing.Point(0, 0);
            this.waitLabelControl.Name = "waitLabelControl";
            this.waitLabelControl.Size = new System.Drawing.Size(931, 711);
            this.waitLabelControl.TabIndex = 1;
            this.waitLabelControl.Visible = false;
            // 
            // ConversationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richEditControl);
            this.Controls.Add(this.waitLabelControl);
            this.Name = "ConversationPanel";
            this.Size = new System.Drawing.Size(931, 711);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraRichEdit.RichEditControl richEditControl;
        private DevExpress.XtraEditors.LabelControl waitLabelControl;
    }
}
