namespace PreciousUI
{
    partial class MainForm
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
            this.leftPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.virtualAssistantControl = new PreciousUI.Controls.VirtualAssistantControl();
            this.centerPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.navigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navigationBar = new DevExpress.XtraBars.Navigation.OfficeNavigationBar();
            this.barPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bsiFile = new DevExpress.XtraBars.BarSubItem();
            this.bbiSaveDocument = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSaveDocumentAs = new DevExpress.XtraBars.BarButtonItem();
            this.bsiExport = new DevExpress.XtraBars.BarSubItem();
            this.bbiExportToPDF = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToHTML = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToMHT = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToDOC = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToDOCX = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToRTF = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToTXT = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToODT = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToEPUB = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportToXML = new DevExpress.XtraBars.BarButtonItem();
            this.bsiPrint = new DevExpress.XtraBars.BarSubItem();
            this.bbiPreviewPrint = new DevExpress.XtraBars.BarButtonItem();
            this.bbiQuickPrint = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPrint = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExit = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bciAllowVoice = new DevExpress.XtraBars.BarCheckItem();
            this.bbiVoiceSettings = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSearchSettings = new DevExpress.XtraBars.BarButtonItem();
            this.bsiBece = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bbiSavePrintAs = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSavePrint = new DevExpress.XtraBars.BarButtonItem();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.toastNotificationsManager = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(this.components);
            this.defaultToolTipController = new DevExpress.Utils.DefaultToolTipController(this.components);
            this.taskbarAssistant = new DevExpress.Utils.Taskbar.TaskbarAssistant();
            this.bbiGetStarted = new DevExpress.XtraBars.BarButtonItem();
            this.bbiHelp = new DevExpress.XtraBars.BarSubItem();
            ((System.ComponentModel.ISupportInitialize)(this.leftPanelControl)).BeginInit();
            this.leftPanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.virtualAssistantControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPanelControl)).BeginInit();
            this.centerPanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationBar)).BeginInit();
            this.navigationBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barPanelControl)).BeginInit();
            this.barPanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPanelControl
            // 
            this.defaultToolTipController.SetAllowHtmlText(this.leftPanelControl, DevExpress.Utils.DefaultBoolean.Default);
            this.leftPanelControl.Appearance.BackColor = System.Drawing.Color.Black;
            this.leftPanelControl.Appearance.Options.UseBackColor = true;
            this.leftPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.leftPanelControl.Controls.Add(this.virtualAssistantControl);
            this.leftPanelControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanelControl.Location = new System.Drawing.Point(0, 0);
            this.leftPanelControl.Name = "leftPanelControl";
            this.leftPanelControl.Size = new System.Drawing.Size(430, 686);
            this.leftPanelControl.TabIndex = 1;
            // 
            // virtualAssistantControl
            // 
            this.virtualAssistantControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.virtualAssistantControl.Location = new System.Drawing.Point(105, 482);
            this.virtualAssistantControl.Name = "virtualAssistantControl";
            this.virtualAssistantControl.Properties.AllowDisposeImage = true;
            this.virtualAssistantControl.Properties.AllowFocused = false;
            this.virtualAssistantControl.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.virtualAssistantControl.Properties.Appearance.Options.UseBackColor = true;
            this.virtualAssistantControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.virtualAssistantControl.Properties.Caption.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.virtualAssistantControl.Properties.Caption.Appearance.Options.UseBackColor = true;
            this.virtualAssistantControl.Properties.Caption.Appearance.Options.UseTextOptions = true;
            this.virtualAssistantControl.Properties.Caption.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.virtualAssistantControl.Properties.Caption.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.virtualAssistantControl.Properties.Caption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.virtualAssistantControl.Properties.Caption.ContentPadding = new System.Windows.Forms.Padding(10, 10, 10, 20);
            this.virtualAssistantControl.Properties.Caption.Offset = new System.Drawing.Point(0, 0);
            this.virtualAssistantControl.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.virtualAssistantControl.Properties.ReadOnly = true;
            this.virtualAssistantControl.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.virtualAssistantControl.Properties.ShowMenu = false;
            this.virtualAssistantControl.Properties.ZoomAccelerationFactor = 1D;
            this.virtualAssistantControl.Size = new System.Drawing.Size(194, 133);
            this.virtualAssistantControl.TabIndex = 0;
            // 
            // centerPanelControl
            // 
            this.defaultToolTipController.SetAllowHtmlText(this.centerPanelControl, DevExpress.Utils.DefaultBoolean.Default);
            this.centerPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.centerPanelControl.Controls.Add(this.navigationFrame);
            this.centerPanelControl.Controls.Add(this.navigationBar);
            this.centerPanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanelControl.Location = new System.Drawing.Point(430, 0);
            this.centerPanelControl.Name = "centerPanelControl";
            this.centerPanelControl.Size = new System.Drawing.Size(667, 686);
            this.centerPanelControl.TabIndex = 1;
            // 
            // navigationFrame
            // 
            this.defaultToolTipController.SetAllowHtmlText(this.navigationFrame, DevExpress.Utils.DefaultBoolean.Default);
            this.navigationFrame.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame.Location = new System.Drawing.Point(0, 40);
            this.navigationFrame.Name = "navigationFrame";
            this.navigationFrame.SelectedPage = null;
            this.navigationFrame.Size = new System.Drawing.Size(667, 646);
            this.navigationFrame.TabIndex = 1;
            this.navigationFrame.Text = "navigationFrame1";
            this.navigationFrame.SelectedPageChanged += new DevExpress.XtraBars.Navigation.SelectedPageChangedEventHandler(this.navigationFrame_SelectedPageChanged);
            // 
            // navigationBar
            // 
            this.navigationBar.AutoSize = false;
            this.navigationBar.Controls.Add(this.barPanelControl);
            this.navigationBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationBar.Location = new System.Drawing.Point(0, 0);
            this.navigationBar.Name = "navigationBar";
            this.navigationBar.NavigationClient = this.navigationFrame;
            this.navigationBar.Size = new System.Drawing.Size(667, 40);
            this.navigationBar.TabIndex = 0;
            this.navigationBar.Text = global::PreciousUI.Properties.Settings.Default.ss;
            // 
            // barPanelControl
            // 
            this.defaultToolTipController.SetAllowHtmlText(this.barPanelControl, DevExpress.Utils.DefaultBoolean.Default);
            this.barPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barPanelControl.Controls.Add(this.barDockControlLeft);
            this.barPanelControl.Controls.Add(this.barDockControlRight);
            this.barPanelControl.Controls.Add(this.barDockControlBottom);
            this.barPanelControl.Controls.Add(this.barDockControlTop);
            this.barPanelControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.barPanelControl.Location = new System.Drawing.Point(436, 0);
            this.barPanelControl.Name = "barPanelControl";
            this.barPanelControl.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.barPanelControl.Size = new System.Drawing.Size(231, 40);
            this.barPanelControl.TabIndex = 6;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 34);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 6);
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this.barPanelControl;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiFile,
            this.bbiExit,
            this.bsiExport,
            this.bbiQuickPrint,
            this.bbiPreviewPrint,
            this.bbiSavePrintAs,
            this.bsiPrint,
            this.bbiSavePrint,
            this.bbiExportToPDF,
            this.bbiExportToHTML,
            this.bbiExportToMHT,
            this.bbiExportToDOC,
            this.bbiExportToDOCX,
            this.bbiExportToRTF,
            this.bbiExportToTXT,
            this.bbiExportToODT,
            this.bbiExportToEPUB,
            this.bbiExportToXML,
            this.bbiPrint,
            this.bbiSaveDocumentAs,
            this.bbiSaveDocument,
            this.bsiBece,
            this.barSubItem1,
            this.bciAllowVoice,
            this.bbiVoiceSettings,
            this.bbiSearchSettings,
            this.barButtonItem1,
            this.bbiGetStarted,
            this.bbiHelp});
            this.barManager.MaxItemId = 40;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiBece),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiHelp)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // bsiFile
            // 
            this.bsiFile.Caption = "File";
            this.bsiFile.Id = 0;
            this.bsiFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSaveDocument),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSaveDocumentAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExit)});
            this.bsiFile.Name = "bsiFile";
            // 
            // bbiSaveDocument
            // 
            this.bbiSaveDocument.Caption = "Save Document..";
            this.bbiSaveDocument.Enabled = false;
            this.bbiSaveDocument.Id = 27;
            this.bbiSaveDocument.Name = "bbiSaveDocument";
            // 
            // bbiSaveDocumentAs
            // 
            this.bbiSaveDocumentAs.Caption = "Save Document as..";
            this.bbiSaveDocumentAs.Id = 26;
            this.bbiSaveDocumentAs.Name = "bbiSaveDocumentAs";
            // 
            // bsiExport
            // 
            this.bsiExport.Caption = "Export Document";
            this.bsiExport.Id = 5;
            this.bsiExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToPDF),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToHTML),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToMHT),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToDOC),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToDOCX),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToRTF),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToTXT),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToODT),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToEPUB),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExportToXML)});
            this.bsiExport.Name = "bsiExport";
            // 
            // bbiExportToPDF
            // 
            this.bbiExportToPDF.Caption = "Export to PDF";
            this.bbiExportToPDF.Id = 15;
            this.bbiExportToPDF.Name = "bbiExportToPDF";
            // 
            // bbiExportToHTML
            // 
            this.bbiExportToHTML.Caption = "Export to HTML";
            this.bbiExportToHTML.Id = 16;
            this.bbiExportToHTML.Name = "bbiExportToHTML";
            // 
            // bbiExportToMHT
            // 
            this.bbiExportToMHT.Caption = "Export to MHT";
            this.bbiExportToMHT.Id = 17;
            this.bbiExportToMHT.Name = "bbiExportToMHT";
            // 
            // bbiExportToDOC
            // 
            this.bbiExportToDOC.Caption = "Export to DOC";
            this.bbiExportToDOC.Id = 18;
            this.bbiExportToDOC.Name = "bbiExportToDOC";
            // 
            // bbiExportToDOCX
            // 
            this.bbiExportToDOCX.Caption = "Export to DOCX";
            this.bbiExportToDOCX.Id = 19;
            this.bbiExportToDOCX.Name = "bbiExportToDOCX";
            // 
            // bbiExportToRTF
            // 
            this.bbiExportToRTF.Caption = "Export to RTF";
            this.bbiExportToRTF.Id = 20;
            this.bbiExportToRTF.Name = "bbiExportToRTF";
            // 
            // bbiExportToTXT
            // 
            this.bbiExportToTXT.Caption = "Export to TXT";
            this.bbiExportToTXT.Id = 21;
            this.bbiExportToTXT.Name = "bbiExportToTXT";
            // 
            // bbiExportToODT
            // 
            this.bbiExportToODT.Caption = "Export to ODT";
            this.bbiExportToODT.Id = 22;
            this.bbiExportToODT.Name = "bbiExportToODT";
            // 
            // bbiExportToEPUB
            // 
            this.bbiExportToEPUB.Caption = "Export to EPUB";
            this.bbiExportToEPUB.Id = 23;
            this.bbiExportToEPUB.Name = "bbiExportToEPUB";
            // 
            // bbiExportToXML
            // 
            this.bbiExportToXML.Caption = "Export to XML";
            this.bbiExportToXML.Id = 24;
            this.bbiExportToXML.Name = "bbiExportToXML";
            // 
            // bsiPrint
            // 
            this.bsiPrint.Caption = "Print Document";
            this.bsiPrint.Id = 11;
            this.bsiPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPreviewPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiQuickPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPrint)});
            this.bsiPrint.Name = "bsiPrint";
            // 
            // bbiPreviewPrint
            // 
            this.bbiPreviewPrint.Caption = "Preview Print";
            this.bbiPreviewPrint.Id = 9;
            this.bbiPreviewPrint.Name = "bbiPreviewPrint";
            // 
            // bbiQuickPrint
            // 
            this.bbiQuickPrint.Caption = "Quick Print";
            this.bbiQuickPrint.Id = 8;
            this.bbiQuickPrint.Name = "bbiQuickPrint";
            // 
            // bbiPrint
            // 
            this.bbiPrint.Caption = "Print";
            this.bbiPrint.Id = 25;
            this.bbiPrint.Name = "bbiPrint";
            // 
            // bbiExit
            // 
            this.bbiExit.Caption = "Exit";
            this.bbiExit.Id = 3;
            this.bbiExit.Name = "bbiExit";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Tools";
            this.barSubItem1.Id = 29;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciAllowVoice),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiVoiceSettings),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSearchSettings)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "User Profile";
            this.barButtonItem1.Id = 36;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bciAllowVoice
            // 
            this.bciAllowVoice.BindableChecked = true;
            this.bciAllowVoice.Caption = "Allow voice";
            this.bciAllowVoice.Checked = true;
            this.bciAllowVoice.Id = 31;
            this.bciAllowVoice.Name = "bciAllowVoice";
            this.bciAllowVoice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiVoiceSettings
            // 
            this.bbiVoiceSettings.Caption = "Voice Settings";
            this.bbiVoiceSettings.Id = 32;
            this.bbiVoiceSettings.Name = "bbiVoiceSettings";
            // 
            // bbiSearchSettings
            // 
            this.bbiSearchSettings.Caption = "Indexing Options";
            this.bbiSearchSettings.Id = 33;
            this.bbiSearchSettings.Name = "bbiSearchSettings";
            // 
            // bsiBece
            // 
            this.bsiBece.Caption = "BECE";
            this.bsiBece.Id = 28;
            this.bsiBece.Name = "bsiBece";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 3);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(231, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 40);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(231, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(231, 34);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 6);
            // 
            // bbiSavePrintAs
            // 
            this.bbiSavePrintAs.Caption = "Save Print As...";
            this.bbiSavePrintAs.Id = 10;
            this.bbiSavePrintAs.Name = "bbiSavePrintAs";
            // 
            // bbiSavePrint
            // 
            this.bbiSavePrint.Caption = "Save Print...";
            this.bbiSavePrint.Id = 14;
            this.bbiSavePrint.Name = "bbiSavePrint";
            // 
            // searchControl
            // 
            this.searchControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchControl.Location = new System.Drawing.Point(0, 686);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.FontSizeDelta = 2;
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.AutoHeight = false;
            this.searchControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.searchControl.Size = new System.Drawing.Size(1097, 40);
            this.searchControl.TabIndex = 0;
            this.searchControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchControl_KeyDown);
            // 
            // toastNotificationsManager
            // 
            this.toastNotificationsManager.ApplicationId = "fcfaf5b6-43bb-490c-a61d-e6e4f6e6cc99";
            this.toastNotificationsManager.ApplicationName = "precious";
            this.toastNotificationsManager.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] {
            new DevExpress.XtraBars.ToastNotifications.ToastNotification("a2381511-0195-45a8-97d1-9ff865264cc3", null, "Welcome to Precious", "I\'m here to assist you 24 hours a day, 7 days in a week. Thank you!", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor i" +
                    "ncididunt ut labore et dolore magna aliqua.", DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Text02)});
            // 
            // taskbarAssistant
            // 
            this.taskbarAssistant.IconsAssembly = "precious.exe";
            this.taskbarAssistant.ParentControl = this;
            // 
            // bbiGetStarted
            // 
            this.bbiGetStarted.Caption = "Get Started";
            this.bbiGetStarted.Id = 38;
            this.bbiGetStarted.Name = "bbiGetStarted";
            // 
            // bbiHelp
            // 
            this.bbiHelp.Caption = "Help";
            this.bbiHelp.Id = 39;
            this.bbiHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiGetStarted)});
            this.bbiHelp.Name = "bbiHelp";
            // 
            // MainForm
            // 
            this.defaultToolTipController.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 726);
            this.Controls.Add(this.centerPanelControl);
            this.Controls.Add(this.leftPanelControl);
            this.Controls.Add(this.searchControl);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(420, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Precious Assistant 1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.leftPanelControl)).EndInit();
            this.leftPanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.virtualAssistantControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPanelControl)).EndInit();
            this.centerPanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationBar)).EndInit();
            this.navigationBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barPanelControl)).EndInit();
            this.barPanelControl.ResumeLayout(false);
            this.barPanelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl leftPanelControl;
        private DevExpress.XtraEditors.PanelControl centerPanelControl;
        private DevExpress.XtraBars.Navigation.OfficeNavigationBar navigationBar;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private Controls.VirtualAssistantControl virtualAssistantControl;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager;
        private DevExpress.Utils.DefaultToolTipController defaultToolTipController;
        private DevExpress.Utils.Taskbar.TaskbarAssistant taskbarAssistant;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem bsiFile;
        private DevExpress.XtraBars.BarButtonItem bbiExit;
        private DevExpress.XtraBars.BarSubItem bsiExport;
        private DevExpress.XtraBars.BarButtonItem bbiQuickPrint;
        private DevExpress.XtraBars.BarButtonItem bbiPreviewPrint;
        private DevExpress.XtraBars.BarButtonItem bbiSavePrintAs;
        private DevExpress.XtraBars.BarSubItem bsiPrint;
        private DevExpress.XtraBars.BarButtonItem bbiSavePrint;
        private DevExpress.XtraBars.BarButtonItem bbiExportToPDF;
        private DevExpress.XtraBars.BarButtonItem bbiExportToHTML;
        private DevExpress.XtraBars.BarButtonItem bbiExportToMHT;
        private DevExpress.XtraBars.BarButtonItem bbiExportToDOC;
        private DevExpress.XtraBars.BarButtonItem bbiExportToDOCX;
        private DevExpress.XtraBars.BarButtonItem bbiExportToRTF;
        private DevExpress.XtraBars.BarButtonItem bbiExportToTXT;
        private DevExpress.XtraBars.BarButtonItem bbiExportToODT;
        private DevExpress.XtraBars.BarButtonItem bbiExportToEPUB;
        private DevExpress.XtraBars.BarButtonItem bbiExportToXML;
        private DevExpress.XtraBars.BarButtonItem bbiPrint;
        private DevExpress.XtraBars.BarButtonItem bbiSaveDocument;
        private DevExpress.XtraBars.BarButtonItem bbiSaveDocumentAs;
        private DevExpress.XtraBars.BarSubItem bsiBece;
        private DevExpress.XtraEditors.PanelControl barPanelControl;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarCheckItem bciAllowVoice;
        private DevExpress.XtraBars.BarButtonItem bbiVoiceSettings;
        private DevExpress.XtraBars.BarButtonItem bbiSearchSettings;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarSubItem bbiHelp;
        private DevExpress.XtraBars.BarButtonItem bbiGetStarted;

    }
}

