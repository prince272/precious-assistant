using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PreciousUI.Internals;
using Syn.VA;
using Syn.VA.Events;
using PreciousUI.Plugins;
using PreciousUI.Properties;

namespace PreciousUI.Modules.Panels
{
    public partial class ConversationPanel : PanelBase
    {
        public ConversationPanel()
        {
            InitializeComponent();
            waitLabelControl.Text = Resources.WaitMessage;
            EditorHelper.InitConversationEdit(richEditControl);
            VirtualAssistant.Instance.Interaction.MessageReceived += Interaction_MessageReceived;
            VirtualAssistant.Instance.Interaction.ResponseReceived += Interaction_ResponseReceived;
        }

        void Interaction_ResponseReceived(object sender, ResponseGeneratedEventArgs e)
        {
            try
            {
                this.InvokeIfRequired(panel =>
                {
                    BotResponse botResponse = e.Response as BotResponse;
                    if (botResponse == null) return;
                    try
                    {
                        richEditControl.CreateNewDocument();
                        richEditControl.Document.BeginUpdate();
                        richEditControl.Document.RtfText = botResponse.RtfText;
                        richEditControl.Document.CaretPosition = richEditControl.Document.Range.Start;
                    }
                    finally { richEditControl.Document.EndUpdate(); }
                    richEditControl.ScrollToCaret();

                    waitLabelControl.Visible = false;
                    richEditControl.Visible = true;
                });
            }
            catch (Exception) { }
        }

        void Interaction_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                this.InvokeIfRequired(panel =>
                {
                    waitLabelControl.Visible = true;
                    richEditControl.Visible = false;
                });
            }
            catch (Exception) { }
        }
    }
}