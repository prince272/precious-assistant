namespace PreciousUI.Forms
{
    partial class SplashScreenForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            this.companyLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.statusLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.SuspendLayout();
            // 
            // companyLabelControl
            // 
            this.companyLabelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.companyLabelControl.Location = new System.Drawing.Point(498, 165);
            this.companyLabelControl.Name = "companyLabelControl";
            this.companyLabelControl.Size = new System.Drawing.Size(180, 17);
            this.companyLabelControl.TabIndex = 0;
            this.companyLabelControl.Text = "© 2018 Humosoft Corporation";
            this.companyLabelControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            // 
            // statusLabelControl
            // 
            this.statusLabelControl.AllowHtmlString = true;
            this.statusLabelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabelControl.Appearance.Options.UseTextOptions = true;
            this.statusLabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.statusLabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.statusLabelControl.Location = new System.Drawing.Point(364, 88);
            this.statusLabelControl.Name = "statusLabelControl";
            this.statusLabelControl.Size = new System.Drawing.Size(232, 17);
            this.statusLabelControl.TabIndex = 1;
            this.statusLabelControl.Text = "---";
            this.statusLabelControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            // 
            // progressPanel1
            // 
            this.progressPanel1.AnimationAcceleration = 11F;
            this.progressPanel1.AnimationSpeed = 2F;
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AutoHeight = true;
            this.progressPanel1.BarAnimationElementThickness = 2;
            this.progressPanel1.BarAnimationMotionType = DevExpress.Utils.Animation.MotionType.WithAcceleration;
            this.progressPanel1.LineAnimationElementHeight = 5;
            this.progressPanel1.Location = new System.Drawing.Point(243, 111);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.ShowCaption = false;
            this.progressPanel1.ShowDescription = false;
            this.progressPanel1.Size = new System.Drawing.Size(445, 11);
            this.progressPanel1.TabIndex = 2;
            this.progressPanel1.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Line;
            this.progressPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            // 
            // SplashScreenForm
            // 
            this.AllowControlsInImageMode = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 198);
            this.Controls.Add(this.progressPanel1);
            this.Controls.Add(this.statusLabelControl);
            this.Controls.Add(this.companyLabelControl);
            this.Name = "SplashScreenForm";
            this.ShowMode = DevExpress.XtraSplashScreen.ShowMode.Image;
            this.SplashImage = ((System.Drawing.Image)(resources.GetObject("$this.SplashImage")));
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl companyLabelControl;
        private DevExpress.XtraEditors.LabelControl statusLabelControl;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;

    }
}
