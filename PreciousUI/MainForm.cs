using DevExpress.Utils.Serializing;
using DevExpress.XtraBars.Navigation;
using PreciousUI.Forms;
using PreciousUI.Internals;
using PreciousUI.Modules.Panels;
using PreciousUI.Plugins;
using PreciousUI.Properties;
using Syn.VA;
using Syn.VA.Events;
using Syn.VA.Interaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Speech.Synthesis;
using Syn.VA.Speech;
using PreciousUI.Controls;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Localization;
using DevExpress.XtraBars;
using System.Threading.Tasks;

namespace PreciousUI
{
    public partial class MainForm : FormBase, IXtraSerializable
    {
        internal const string CONVERSATION_PAGE = "CONVERSATION";
        internal const string FLAGGAME_PAGE = "FLAG GAME";
        internal const string PHOTOGALLERY_PAGE = "PHOTO GALLERY";
        private ExpandPanel expandPanel;
        private UserProfile currentProfile;

        public MainForm(string[] arguments)
        {
            this.StartupArguments = arguments;
            ProcessArguments();
            ShowSplashScreen();
            RestoreLayout();
            InitializeComponent();
            InitEditors();
            InitPages();
            InitBot();
            MainRichEditControl = FindMainRichEditControl();
            InitializeBeceBarButtons();
        }

        public string[] StartupArguments { get; private set; }

        public VirtualAssistant VirtualAssistant
        {
            get { return VirtualAssistant.Instance; }
        }

        [XtraSerializableProperty]
        public bool FirstUse { get; set; }

        [XtraSerializableProperty]
        public ExpandPanel ExpandPanel
        {
            get { return expandPanel; }
            set
            {
                if (expandPanel != value)
                {
                    expandPanel = value;
                    UpdateExpandPanel();
                }
            }
        }

        [XtraSerializableProperty]
        public Size FormSize { get; set; }

        [XtraSerializableProperty]
        public Point FormLocation { get; set; }

        [XtraSerializableProperty]
        public FormWindowState FormWindowState { get; set; }

        [XtraSerializableProperty]
        public bool CancelWhenClosing { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public UserProfile CurrentProfile
        {
            get
            {
                if (currentProfile == null)
                    currentProfile = new UserProfile();
                return currentProfile;
            }
        }

        public RichEditControl MainRichEditControl { get; internal set; }

        public void SwitchNextExpandPanel()
        {
            switch (ExpandPanel)
            {
                case ExpandPanel.None:
                    ExpandPanel = ExpandPanel.Panel1;
                    break;
                case ExpandPanel.Panel1:
                    ExpandPanel = ExpandPanel.Panel2;
                    break;
                case ExpandPanel.Panel2:
                    ExpandPanel = ExpandPanel.None;
                    break;
            }
        }

        public void SaveLayoutToXml(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.SetAttributes(fileName, FileAttributes.Normal);
                SaveLayoutCore(new XmlXtraSerializer(), fileName);
            }
            catch (Exception ex) { VirtualAssistant.Logger.Error(ex); }
        }

        public void RestoreLayoutFromXml(string fileName)
        {
            try
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                RestoreLayoutCore(new XmlXtraSerializer(), fileName);
            }
            catch (Exception ex) { VirtualAssistant.Logger.Error(ex); }
        }

        void InitBot()
        {
            VirtualAssistant.Logger.Info("Initializing bot...");
            SplashScreenHelper.SetSplashScreenStatus("Initializing bot...");
            VirtualAssistant.Plugins.Add(new SimlPlugin(DataDirectoryHelper.GetRelativeDirectoryPath("Data\\Resources\\SimlProject")));
            VirtualAssistant.Plugins.Add(new DSearchPlugin());
            VirtualAssistant.Plugins.Add(new WordNetPlugin(DataDirectoryHelper.GetRelativeDirectoryPath("Data\\Resources\\WordNet")));
            VirtualAssistant.Plugins.Add(new WikipediaPlugin());
            VirtualAssistant.Plugins.Add(new KJBiblePlugin());
            VirtualAssistant.Speech.Synthesizers.Add(new InBuiltSpeechSynthesizer());
            VirtualAssistant.Speech.Recognizers.Add(new InBuiltSpeechRecognizer(searchControl));
            VirtualAssistant.Speech.Synthesizers.Select("InBuilt");
            VirtualAssistant.Interaction.ResponseReceived += OnResponseReceived;
            VirtualAssistant.Interaction.MessageReceived += OnMessageReceived;

            // Update user name
            VirtualAssistant.Plugins.GetPluginByType<SimlPlugin>().Bot.MainUser.Settings["Name"].Value = CurrentProfile.FirstName;
            VirtualAssistant.Plugins.GetPluginByType<SimlPlugin>().Bot.MainUser.Settings["Name"].Changed += (s, e) =>
            {
                CurrentProfile.FirstName = VirtualAssistant.Plugins.GetPluginByType<SimlPlugin>().Bot.MainUser.Settings["Name"].Value;
            };
        }

        void InitEditors()
        {
            VirtualAssistant.Logger.Info("Initializing editors...");
            EditorHelper.InitNavigationBar(navigationBar);
            EditorHelper.InitCommandBar(searchControl);
            EditorHelper.InitVAControl(virtualAssistantControl);
            barButtonItem1.ItemClick += (s, e) => { ShowUserProfileDialog(); };
            bbiVoiceSettings.ItemClick += (s, e) => { ProcessHelper.Start("control", "/name Microsoft.TextToSpeech"); };
            bbiSearchSettings.ItemClick += (s, e) => { ProcessHelper.Start("control", "/name Microsoft.IndexingOptions"); };
            bbiExportToPDF.ItemClick += (s, e) => { ExportDocumentToPDF(); };
            bbiExportToMHT.ItemClick += (s, e) => { ExportDocumentToMHT(); };
            bbiExportToDOC.ItemClick += (s, e) => { ExportDocumentToDOC(); };
            bbiExportToDOCX.ItemClick += (s, e) => { ExportDocumentToDOCX(); };
            bbiExportToRTF.ItemClick += (s, e) => { ExportDocumentToRTF(); };
            bbiExportToTXT.ItemClick += (s, e) => { ExportDocumentToText(); };
            bbiExportToODT.ItemClick += (s, e) => { ExportDocumentToODT(); };
            bbiExportToEPUB.ItemClick += (s, e) => { ExportDocumentToEPUB(); };
            bbiExportToEPUB.ItemClick += (s, e) => { ExportDocumentToXML(); };
            bbiPreviewPrint.ItemClick += (s, e) => { PrintDocumentPreview(); };
            bbiQuickPrint.ItemClick += (s, e) => { PrintDocumentQuick(); };
            bbiSaveDocument.ItemClick += (s, e) => { SaveDocument(); };
            bbiSaveDocumentAs.ItemClick += (s, e) => { bbiSaveDocument.Enabled = true; SaveDocumentAs(); };
            bbiPrint.ItemClick += (s, e) => { PrintDocument(); };
            bbiExit.ItemClick += (s, e) => { CancelWhenClosing = false; Close(); };
            bbiGetStarted.ItemClick += (s, e) => { GetStartedForm.OpenGetStartedWindow(); };
        }

        void InitPages()
        {
            VirtualAssistant.Logger.Info("Initializing pages...");
            SplashScreenHelper.SetSplashScreenStatus("Initializing pages...");
            try
            {
                navigationFrame.Pages.BeginUpdate();
                navigationFrame.Pages.Add(CreateNavigationPage(CONVERSATION_PAGE, new ConversationPanel()));
                navigationFrame.Pages.Add(CreateNavigationPage(FLAGGAME_PAGE, new FlagGamePanel()));
                // navigationFrame.Pages.Add(CreateNavigationPage(PHOTOGALLERY_PAGE, new PhotoGalleryrPanel()));
            }
            finally
            {
                navigationFrame.Pages.EndUpdate();
            }
            navigationBar.SelectedItem = navigationBar.Items[0];
        }

        void InitializeBeceBarButtons()
        {
            try
            {
                VirtualAssistant.Logger.Info("Adding Bece Bar Buttons...");
                var beceInfo = BeceFileInfo.GetBECEFileInfoCollection(DataDirectoryHelper.BECEDirectory);
                if (beceInfo == null || !beceInfo.Any()) return;
                IEnumerable<IGrouping<string, BeceFileInfo>> beceInfoGroupList = beceInfo.GroupBy(i => i.Subject);
                foreach (IGrouping<string, BeceFileInfo> beceInfoGroup in beceInfoGroupList)
                {
                    CreateBECEQuestionGroupGroup(beceInfoGroup);
                }
            }
            catch (Exception ex)
            {
                VirtualAssistant.Logger.Error(ex);
                bsiBece.Visibility = BarItemVisibility.Never;
            }
        }

        void CreateBECEQuestionGroupGroup(IGrouping<string, BeceFileInfo> questionGroup)
        {
            BarSubItem bsItem = new BarSubItem() { Tag = questionGroup };
            bsItem.Caption = questionGroup.Key.ToUpper();
            foreach (var question in questionGroup)
            {
                var bbItem = new BarButtonItem();
                bbItem.Caption = question.Year.ToString();
                bbItem.ItemClick += (s, e) => ProcessHelper.Start(question.FileName, string.Empty);
                bsItem.AddItem(bbItem);
            }
            bsiBece.AddItem(bsItem);
        }

        void UpdateExpandPanel()
        {
            switch (expandPanel)
            {
                case ExpandPanel.None:
                    leftPanelControl.Dock = DockStyle.Left;
                    leftPanelControl.Visible = true;
                    centerPanelControl.Dock = DockStyle.Fill;
                    centerPanelControl.Visible = true;
                    break;
                case ExpandPanel.Panel1:
                    leftPanelControl.Visible = true;
                    centerPanelControl.Visible = false;
                    leftPanelControl.Dock = DockStyle.Fill;
                    centerPanelControl.Dock = DockStyle.None;
                    break;
                case ExpandPanel.Panel2:
                    leftPanelControl.Visible = false;
                    leftPanelControl.Dock = DockStyle.None;
                    centerPanelControl.Dock = DockStyle.Fill;
                    centerPanelControl.Visible = true;
                    break;

            }
            Refresh();
        }

        void SaveLayout()
        {
            VirtualAssistant.Logger.Info("Saving layout...");
            FirstUse = false;
            FormSize = Size;
            FormLocation = Location;
            FormWindowState = WindowState;
            SaveLayoutToXml(DataDirectoryHelper.FormLayoutFilePath);
        }

        void RestoreLayout()
        {
            VirtualAssistant.Logger.Info("Restoring layout...");
            FirstUse = true;
            if (File.Exists(DataDirectoryHelper.FormLayoutFilePath))
                RestoreLayoutFromXml(DataDirectoryHelper.FormLayoutFilePath);
            if (!FirstUse)
            {
                Size = FormSize;
                Location = FormLocation;
                WindowState = FormWindowState;
                return;
            }
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.CenterScreen;
        }

        void ShowSplashScreen()
        {
            SplashScreenHelper.ShowSplashScreen();
        }

        void ShowGetStartedDialog()
        {
            using (GetStartedForm form = new GetStartedForm())
            {
                form.ShowDialog(this);
            }
        }

        void ShowUserProfileDialog()
        {
            using (UserProfileForm form = new UserProfileForm(CurrentProfile))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    CurrentProfile.Assign(form.CurrentUser);
                    VirtualAssistant.Plugins.GetPluginByType<SimlPlugin>().Bot.MainUser.Settings["Name"].Value = CurrentProfile.FirstName;
                }
            }
        }

        void ProcessArguments()
        {
            VirtualAssistant.Logger.Info("Processing arguments...");
            for (int i = 0; i < StartupArguments.Length; i++)
            {
                switch (StartupArguments[i].ToLower())
                {
                    case "":
                        break;

                    default:
                        break;
                }
            }
        }

        void SendGetUserMessage()
        {
            VirtualAssistant.Plugins.GetPluginByType<SimlPlugin>().
            ProcessMessage(new Syn.VA.Interaction.Message("GREET-USER", MessageType.EventMessage));
        }

        #region IXtraSerializable Members

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) { }
        void IXtraSerializable.OnEndSerializing() { }
        void IXtraSerializable.OnStartDeserializing(DevExpress.Utils.LayoutAllowEventArgs e) { }
        void IXtraSerializable.OnStartSerializing() { }

        #endregion

        #region Print and Export
        void ShowExportErrorMessage(Exception exception)
        {
            XtraMessageBox.Show(this, exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void OpenExportedFile(string fileName)
        {
            if (XtraMessageBox.Show(this, "Do you want to open the exported file?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProcessHelper.Start(fileName, string.Empty);
            }
        }
        string GetFileName(string extension, string filter)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = filter;
                dialog.FileName = Application.ProductName;
                dialog.DefaultExt = extension;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }
            return string.Empty;
        }
        protected virtual void ExportTo(string ext, string filter)
        {
            string fileName = GetFileName(string.Format("*.{0}", ext), filter);
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    this.ExportToCore(fileName, ext);
                    OpenExportedFile(fileName);
                }
                catch (Exception exception)
                {
                    ShowExportErrorMessage(exception);
                }
            }
        }
        protected void ExportToCore(String filename, string ext)
        {
            if (MainRichEditControl == null) return;
            Cursor oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            if (ext == "pdf") MainRichEditControl.ExportToPdf(filename);
            if (ext == "html") MainRichEditControl.SaveDocument(filename, DocumentFormat.Html);
            if (ext == "mht") MainRichEditControl.SaveDocument(filename, DocumentFormat.Mht);
            if (ext == "doc") MainRichEditControl.SaveDocument(filename, DocumentFormat.Doc);
            if (ext == "docx") MainRichEditControl.SaveDocument(filename, DocumentFormat.OpenXml);
            if (ext == "rtf") MainRichEditControl.SaveDocument(filename, DocumentFormat.Rtf);
            if (ext == "txt") MainRichEditControl.SaveDocument(filename, DocumentFormat.PlainText);
            if (ext == "odt") MainRichEditControl.SaveDocument(filename, DocumentFormat.OpenDocument);
            if (ext == "epub") MainRichEditControl.SaveDocument(filename, DocumentFormat.ePub);
            if (ext == "xml") MainRichEditControl.SaveDocument(filename, DocumentFormat.WordML);
            Cursor.Current = oldCursor;
        }
        protected void ExportDocumentToPDF()
        {
            ExportTo("pdf", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_PDFFiles) + " (*.pdf)|*.pdf");
        }
        protected void ExportDocumentToHTML()
        {
            ExportTo("html", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_HtmlFiles) + " (*.html)|*.html");
        }
        protected void ExportDocumentToMHT()
        {
            ExportTo("mht", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_MhtFiles) + " (*.mht)|*.mht");
        }
        protected void ExportDocumentToDOC()
        {
            ExportTo("doc", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_DocFiles) + " (*.doc)|*.doc");
        }
        protected void ExportDocumentToDOCX()
        {
            ExportTo("docx", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_OpenXmlFiles) + " (*.docx)|*.docx");
        }
        protected void ExportDocumentToRTF()
        {
            ExportTo("rtf", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_RtfFiles) + " (*.rtf)|*.rtf");
        }
        protected void ExportDocumentToText()
        {
            ExportTo("txt", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_TextFiles) + " (*.txt)|*.txt");
        }
        protected void ExportDocumentToODT()
        {
            ExportTo("odt", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_OpenDocumentFiles) + " (*.odt)|*.odt");
        }
        protected void ExportDocumentToEPUB()
        {
            ExportTo("epub", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_ePubFiles) + " (*.epub)|*.epub");
        }
        protected void ExportDocumentToXML()
        {
            ExportTo("xml", XtraRichEditLocalizer.GetString(XtraRichEditStringId.FileFilterDescription_WordMLFiles) + " (*.xml)|*.xml");
        }
        protected void PrintDocumentPreview()
        {
            if (MainRichEditControl == null) return;
            MainRichEditControl.ShowPrintPreview();
        }
        protected void PrintDocumentQuick()
        {
            if (MainRichEditControl == null) return;
            MainRichEditControl.Print();
        }
        protected void PrintDocument()
        {
            if (MainRichEditControl == null) return;
            MainRichEditControl.ShowPrintDialog();
        }
        protected void SaveDocumentAs()
        {
            if (MainRichEditControl == null) return;
            MainRichEditControl.SaveDocumentAs(this);
        }
        protected void SaveDocument()
        {
            if (MainRichEditControl == null) return;
            MainRichEditControl.SaveDocument();
        }
        #endregion

        EmotionState GetRandomEmotionState()
        {
            List<EmotionState> states = new List<EmotionState>();
            states.Add(EmotionState.Blink);
            states.Add(EmotionState.Wink);
            states.Add(EmotionState.Siri);
            states.Add(EmotionState.Optimistic);
            states.Add(EmotionState.NeedMore);
            states.Add(EmotionState.Look);
            states.Add(EmotionState.Holiday);
            states.Add(EmotionState.Heart);
            states.Add(EmotionState.BouncyLeft);
            states.Add(EmotionState.BouncyRight);
            return Syn.Utility.SynUtility.Timing.RandomItem<EmotionState>(states);
        }

        NavigationPageBase CreateNavigationPage(string caption, PanelBase panel)
        {
            NavigationPageBase page = navigationFrame.CreateNewPage();
            page.Caption = caption;
            panel.Parent = page;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(1);
            panel.Paint += (s, args) => ControlPaint.DrawBorder(args.Graphics, panel.Bounds,
            Color.FromArgb(200, 200, 200), ButtonBorderStyle.Solid);
            return page;
        }

        RichEditControl FindMainRichEditControl()
        {
            try
            {
                return navigationFrame.Pages.FindFirst(page => page.Caption == CONVERSATION_PAGE).Controls.OfType<ConversationPanel>().First().Controls.OfType<RichEditControl>().First();
            }
            catch (Exception) { }
            return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SplashScreenHelper.CloseSplashScreen();
            if (FirstUse)
            {
                ShowGetStartedDialog();
                ShowUserProfileDialog();
            }
            SendGetUserMessage();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            VirtualAssistant.Logger.Info("Showing windows form.");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (CancelWhenClosing)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    Hide();
                }
            }
            VirtualAssistant.Logger.Info("Closing windows form.");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            SaveLayout();
            VirtualAssistant.Logger.Info("Windows form closed.");
        }

        protected virtual void OnResponseReceived(object sender, ResponseGeneratedEventArgs e)
        {
            if (this.VirtualAssistant.Speech.Synthesizers.Current != null)
            {
                this.VirtualAssistant.Speech.Synthesizers.Current.Speak(e.Response.Text);
            }

            try
            {
                this.InvokeIfRequired(form =>
                {
                    this.virtualAssistantControl.LoadEmotionState(GetRandomEmotionState());
                    this.virtualAssistantControl.Properties.Caption.Text = e.Response.Text.TruncateAtLastSentence(300);
                });
            }
            catch (ObjectDisposedException) { }
        }

        protected virtual void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                this.InvokeIfRequired(form =>
                {
                    this.virtualAssistantControl.Properties.Caption.Text = Resources.WaitMessage;
                });
            }
            catch (ObjectDisposedException) { }
        }

        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path)
        {
            System.IO.Stream stream = path as System.IO.Stream;
            if (stream != null)
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo("FormLayout", this) },
                                             stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo("FormLayout", this) },
                                            path.ToString(), this.GetType().Name);
        }

        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path)
        {
            System.IO.Stream stream = path as System.IO.Stream;
            if (stream != null)
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo("FormLayout", this) },
                    stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo("FormLayout", this) },
                    path.ToString(), this.GetType().Name);
        }

        private void navigationFrame_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {

        }

        private void searchControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(searchControl.Text))
            {
                SimlPlugin simlPlugin = VirtualAssistant.Plugins.GetPluginByType<SimlPlugin>();
                if (simlPlugin == null)
                    return;
                simlPlugin.ProcessMessage(searchControl.Text);
                searchControl.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {

        }
    }

    public class InBuiltSpeechSynthesizer : ISpeechSynthesizer
    {
        private SpeechSynthesizer speechSynthesizer;

        public InBuiltSpeechSynthesizer()
        {

            this.speechSynthesizer = new SpeechSynthesizer();
            this.speechSynthesizer.SetOutputToDefaultAudioDevice();
            this.speechSynthesizer.VisemeReached += delegate(object s, VisemeReachedEventArgs e)
            {
                if (VisemeReached != null)
                {
                    VisemeArrivedEventArgs arrivedEventArgs = new VisemeArrivedEventArgs();
                    arrivedEventArgs.Duration = e.Duration;
                    arrivedEventArgs.Viseme = e.Viseme;
                    VisemeReached(this, arrivedEventArgs);
                }
            };
            this.speechSynthesizer.SpeakCompleted += delegate
            {
                if (SpeakCompleted != null)
                    SpeakCompleted(this, EventArgs.Empty);
            };
            this.IsEnabled = true;
        }

        public IEnumerable<string> AvailableVoices
        {
            get { throw new NotImplementedException(); }
        }

        public string CurrentVoice
        {
            get
            {
                return this.speechSynthesizer.Voice.Name;
            }
        }

        public Gender Gender
        {
            get
            {
                switch (this.speechSynthesizer.Voice.Gender)
                {
                    case VoiceGender.NotSet:
                        return Gender.NotSet;
                    case VoiceGender.Male:
                        return Gender.Male;
                    case VoiceGender.Female:
                        return Gender.Female;
                    case VoiceGender.Neutral:
                        return Gender.Neutral;
                    default:
                        return Gender.NotSet;
                }
            }
        }

        public int Volume
        {
            get
            {
                return this.speechSynthesizer.Volume;
            }
            set
            {
                this.speechSynthesizer.Volume = value;
            }
        }

        public int Rate
        {
            get
            {
                return this.speechSynthesizer.Rate;
            }
            set
            {
                this.speechSynthesizer.Rate = value;
            }
        }

        public bool IsEnabled { get; set; }

        public string Name
        {
            get { return "InBuilt"; }
        }

        public void Pause()
        {
            this.speechSynthesizer.Pause();
        }

        public void Resume()
        {
            if (this.IsEnabled)
            {
                this.speechSynthesizer.Resume();
            }
        }

        public void SelectVoice(string voiceName)
        {
            if (this.AvailableVoices.Contains(voiceName))
            {
                this.speechSynthesizer.SelectVoice(voiceName);
                return;
            }
            foreach (InstalledVoice current in this.speechSynthesizer.GetInstalledVoices())
            {
                string value = VirtualAssistant.Instance.SettingsManager["Speech"]["Gender"].Value;
                if (current.VoiceInfo.Gender.ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.speechSynthesizer.SelectVoice(current.VoiceInfo.Name);
                    return;
                }
            }
            using (IEnumerator<InstalledVoice> enumerator = this.speechSynthesizer.GetInstalledVoices().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    InstalledVoice current2 = enumerator.Current;
                    this.speechSynthesizer.SelectVoice(current2.VoiceInfo.Name);
                }
            }
        }

        public void Speak(string message)
        {
            if (this.IsEnabled)
            {
                this.speechSynthesizer.SpeakAsyncCancelAll();
                this.speechSynthesizer.SpeakAsync(message);
            }
        }

        public void Stop()
        {
            this.speechSynthesizer.SpeakAsyncCancelAll();
        }

        public event EventHandler SpeakCompleted;

        public event EventHandler<VisemeArrivedEventArgs> VisemeReached;
    }

    public class InBuiltSpeechRecognizer : ISpeechRecognizer
    {
        private const string CHROME_EXE = "chrome.exe";
        private const string CHROME_DOWNLOADLINK = "http://www.chrome.com/download";
        private const string CHROME_RECOGNITION = " --chrome-frame --window-size=500,200 --window-position=580,240 --app=http://www.brainasoft.com/braina/sph/";
        private RecognizerStatus status = RecognizerStatus.Stopped;
        private Thread thread;
        private string prevGeneratedText;
        private bool enabled;
        private bool allowThreading;
        private object lockThis = new object();

        public InBuiltSpeechRecognizer(SearchControl searchControl)
        {
            if (searchControl == null) throw new ArgumentNullException("searchControl");
            if (searchControl.Properties.Buttons.OfType<MicrophoneButton>().SingleOrDefault() == null) throw new ArgumentException();

            this.SearchControl = searchControl;
            this.MicrophoneButton = searchControl.Properties.Buttons.OfType<MicrophoneButton>().SingleOrDefault();
            this.MicrophoneButton.Click += delegate { Enabled = !Enabled; };
        }

        protected SearchControl SearchControl { get; private set; }
        protected MicrophoneButton MicrophoneButton { get; private set; }

        public void OpenSpeechRecognizer()
        {
            string arguments = CHROME_RECOGNITION;
            ProcessHelper.Start(CHROME_EXE, arguments);
        }

        public void CloseSpeechRecognizer()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(CHROME_EXE));
                if (processes == null || processes.Length == 0) return;
                foreach (Process process in processes)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        process.CloseMainWindow();
                    }
                }
            }
            catch (Exception) { }
        }

        public bool CheckIfSpeechRecognizerInstalled()
        {
            try
            {
                Process.Start(CHROME_EXE).Kill();
                return true;
            }
            catch (Win32Exception) { }
            return false;
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (!CheckIfSpeechRecognizerInstalled())
                {
                    string caption = "Speech Recognition";
                    string message = "It looks like Google Chrome is either not installed or is having trouble opening. Would you like to install it?";
                    if (XtraMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        ProcessHelper.Start(CHROME_DOWNLOADLINK, string.Empty);
                    return;
                }

                if (enabled != value)
                {
                    enabled = value;

                    if (enabled)
                    {
                        MicrophoneButton.Appearance.ForeColor = Color.Red;
                        MicrophoneButton.AppearanceHovered.ForeColor = Color.Red;
                        MicrophoneButton.AppearancePressed.ForeColor = Color.Red;
                        OpenSpeechRecognizer();
                        Start();
                    }
                    else
                    {
                        MicrophoneButton.Appearance.ForeColor = Color.Black;
                        MicrophoneButton.AppearanceHovered.ForeColor = Color.Black;
                        MicrophoneButton.AppearancePressed.ForeColor = Color.Black;
                        CloseSpeechRecognizer();
                        Stop();
                    }
                }
            }
        }

        void StartThread()
        {
            allowThreading = true;
            thread = new Thread(DoThread);
            thread.IsBackground = true;
            thread.Start();
        }

        void DoThread()
        {
            lock (lockThis)
            {
                try
                {
                    prevGeneratedText = string.Empty;
                    while (allowThreading)
                    {
                        string generatedText = GetGeneratedText();
                        // Return if the wasn't any text generated.
                        if (string.IsNullOrEmpty(generatedText)) continue;

                        // Check for any changes to the text generated.
                        if (!string.Equals(prevGeneratedText, generatedText))
                        {
                            // Update
                            prevGeneratedText = generatedText;
                            switch (generatedText)
                            {
                                case "RECOGNITION STARTED":
                                case "[Braina]RECOGNITION STARTED[HLI]":
                                    status = RecognizerStatus.Started;
                                    UpdateGeneratedText(string.Empty);
                                    break;

                                case "RECOGNITION STOPPED":
                                case "[Braina]RECOGNITION STOPPED[HLI]":
                                    status = RecognizerStatus.Stopped;
                                    UpdateGeneratedText(string.Empty);
                                    break;

                                default:
                                    if (status == RecognizerStatus.Started || status == RecognizerStatus.Listening)
                                    {
                                        string pattern = @"\[Braina\](.+)\[HLI\]";
                                        Match match = Regex.Match(generatedText, pattern, RegexOptions.IgnoreCase);
                                        if (match.Success)
                                        {
                                            generatedText = match.Captures[0].Value;
                                            status = RecognizerStatus.Listening;
                                            UpdateGeneratedText(generatedText);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Statement to handle the exception.
                }
                finally
                {
                    UpdateGeneratedText(string.Empty);
                    status = RecognizerStatus.Stopped;
                }
            }
        }

        void DestoryThread()
        {
            if (thread != null)
            {
                allowThreading = false;
                thread.Abort();
                thread.Join();
                thread = null;
            }
        }

        void RecreateThread()
        {
            DestoryThread();
            StartThread();
        }

        void UpdateGeneratedText(string text)
        {
            if (!SearchControl.IsDisposed)
            {
                SearchControl.BeginInvoke(new Action(() =>
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        SearchControl.SendKey(new KeyEventArgs(Keys.Enter));
                    }
                    SearchControl.Text = text;
                }));
            }
            RaiseSpeechRecognized(100, text);
        }

        string GetGeneratedText()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(CHROME_EXE));
                if (processes == null || processes.Length == 0) return null;
                foreach (Process process in processes)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        return process.MainWindowTitle;
                    }
                }
            }
            catch (Exception) { }
            return null;
        }

        #region ISpeechRecognizer Members
        public void LoadChoices(IEnumerable<string> choiceList)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return "InBuilt"; }
        }

        public void Start()
        {
            RecreateThread();
        }

        public void Stop()
        {
            DestoryThread();
        }

        public RecognizerStatus Status
        {
            get { return status; }
        }

        public event EventHandler<UtteranceRecognizedEventArgs> SpeechRecognized;
        #endregion

        void RaiseSpeechRecognized(float confidence, string text)
        {
            if (SpeechRecognized != null)
            {
                var recognizedEventArgs = new UtteranceRecognizedEventArgs();
                recognizedEventArgs.Confidence = confidence;
                recognizedEventArgs.Text = text;
                SpeechRecognized(this, recognizedEventArgs);
            }
        }

        void RaiseRecognizerStatusChanged()
        {
            if (RecognizerStatusChanged != null)
            {
                RecognizerStatusChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler RecognizerStatusChanged;

    }

    public enum ExpandPanel
    {
        None,
        Panel1,
        Panel2,
    }
}