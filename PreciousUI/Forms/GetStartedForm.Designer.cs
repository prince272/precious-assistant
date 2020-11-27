namespace PreciousUI.Forms
{
    partial class GetStartedForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetStartedForm));
            DevExpress.Utils.Animation.Transition transition1 = new DevExpress.Utils.Animation.Transition();
            DevExpress.Utils.Animation.SlideFadeTransition slideFadeTransition1 = new DevExpress.Utils.Animation.SlideFadeTransition();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.readMoreSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.nextSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.transitionManager = new DevExpress.Utils.Animation.TransitionManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlString = true;
            this.labelControl1.Appearance.FontSizeDelta = 1;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(265, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(390, 215);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = resources.GetString("labelControl1.Text");
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.completionWizardPage1});
            this.wizardControl1.Size = new System.Drawing.Size(740, 468);
            this.wizardControl1.Text = "Getting Started";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.readMoreSimpleButton);
            this.welcomeWizardPage1.Controls.Add(this.nextSimpleButton);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.Controls.Add(this.pictureEdit1);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(680, 300);
            this.welcomeWizardPage1.Text = "Welcome to Precious Assistant";
            // 
            // readMoreSimpleButton
            // 
            this.readMoreSimpleButton.Location = new System.Drawing.Point(361, 257);
            this.readMoreSimpleButton.Name = "readMoreSimpleButton";
            this.readMoreSimpleButton.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.readMoreSimpleButton.Size = new System.Drawing.Size(90, 30);
            this.readMoreSimpleButton.TabIndex = 3;
            this.readMoreSimpleButton.Text = "Read More";
            this.readMoreSimpleButton.Click += new System.EventHandler(this.readMoreSimpleButton_Click);
            // 
            // nextSimpleButton
            // 
            this.nextSimpleButton.Location = new System.Drawing.Point(265, 257);
            this.nextSimpleButton.Name = "nextSimpleButton";
            this.nextSimpleButton.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.nextSimpleButton.Size = new System.Drawing.Size(90, 30);
            this.nextSimpleButton.TabIndex = 2;
            this.nextSimpleButton.Text = "Continue";
            this.nextSimpleButton.Click += new System.EventHandler(this.nextSimpleButton_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(3, 31);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowDisposeImage = true;
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(245, 238);
            this.pictureEdit1.TabIndex = 1;
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.labelControl2);
            this.completionWizardPage1.Controls.Add(this.pictureEdit3);
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(680, 300);
            this.completionWizardPage1.Text = "Completing";
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Appearance.FontSizeDelta = 1;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(442, 130);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(176, 40);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "<color=0,0,0><size=+5>Let\'s Start!!!</size></color>\r\n";
            // 
            // pictureEdit3
            // 
            this.pictureEdit3.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit3.EditValue = ((object)(resources.GetObject("pictureEdit3.EditValue")));
            this.pictureEdit3.Location = new System.Drawing.Point(-76, 3);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.AllowDisposeImage = true;
            this.pictureEdit3.Properties.AllowFocused = false;
            this.pictureEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit3.Properties.ReadOnly = true;
            this.pictureEdit3.Properties.ShowMenu = false;
            this.pictureEdit3.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit3.Size = new System.Drawing.Size(464, 438);
            this.pictureEdit3.TabIndex = 2;
            // 
            // transitionManager
            // 
            this.transitionManager.FrameCount = 800;
            this.transitionManager.FrameInterval = 5000;
            this.transitionManager.ShowWaitingIndicator = false;
            transition1.BarWaitingIndicatorProperties.Caption = "";
            transition1.BarWaitingIndicatorProperties.Description = "";
            transition1.Control = this.labelControl1;
            transition1.EasingMode = DevExpress.Data.Utils.EasingMode.EaseInOut;
            transition1.LineWaitingIndicatorProperties.AnimationElementCount = 5;
            transition1.LineWaitingIndicatorProperties.Caption = "";
            transition1.LineWaitingIndicatorProperties.Description = "";
            transition1.RingWaitingIndicatorProperties.AnimationElementCount = 5;
            transition1.RingWaitingIndicatorProperties.Caption = "";
            transition1.RingWaitingIndicatorProperties.Description = "";
            transition1.ShowWaitingIndicator = DevExpress.Utils.DefaultBoolean.False;
            transition1.TransitionType = slideFadeTransition1;
            transition1.WaitingIndicatorProperties.Caption = "";
            transition1.WaitingIndicatorProperties.Description = "";
            this.transitionManager.Transitions.Add(transition1);
            // 
            // GetStartedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 468);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetStartedForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "Getting Started";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton nextSimpleButton;
        private DevExpress.Utils.Animation.TransitionManager transitionManager;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraEditors.SimpleButton readMoreSimpleButton;
    }
}