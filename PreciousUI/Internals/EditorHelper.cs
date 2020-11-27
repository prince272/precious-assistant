using DevExpress.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Internals
{
    internal class EditorHelper
    {
        public static void InitSymbolButton(SimpleButton simpleButton)
        {
            simpleButton.BorderStyle = BorderStyles.NoBorder;
            simpleButton.ShowFocusRectangle = DefaultBoolean.False;
            simpleButton.AppearanceHovered.BackColor = DrawHelper.BlendColor(simpleButton.BackColor, simpleButton.ForeColor, 30);
            simpleButton.AppearancePressed.BackColor = DrawHelper.BlendColor(simpleButton.BackColor, simpleButton.ForeColor, 50);
        }

        public static void InitNavigationBar(OfficeNavigationBar navigationBar)
        {
            Font font = SkinAppearanceHelper.PrimaryFont;
            navigationBar.AppearanceItem.Normal.Font = new Font(font.Name, 10.1F, FontStyle.Bold);
            navigationBar.AutoSize = false;
            navigationBar.MaxItemCount = 3;
            navigationBar.Height = 40;
        }

        public static void InitCommandBar(SearchControl searchControl)
        {
            searchControl.Properties.AutoHeight = false;
            searchControl.Properties.Appearance.FontSizeDelta = 2;
            searchControl.Properties.BorderStyle = BorderStyles.NoBorder;
            searchControl.Properties.ShowClearButton = false;
            searchControl.Properties.ShowNullValuePromptWhenFocused = true;
            searchControl.Properties.ShowSearchButton = false;
            searchControl.Properties.NullValuePrompt = "Ask me any question";
            searchControl.Properties.MaskBoxPadding = new Padding(7, 0, 7, 0);
            searchControl.Height = 40;
            searchControl.Properties.Buttons.Clear();
            searchControl.Properties.Buttons.Add(new SendButton(searchControl));
            searchControl.Properties.Buttons.Add(new MicrophoneButton(searchControl));
            searchControl.Properties.Buttons.Add(new ExpandButton(searchControl));
        }

        public static void InitVAControl(Controls.VirtualAssistantControl vaControl)
        {
            if (vaControl.Parent == null) return;
            vaControl.Properties.Caption.Appearance.ForeColor = DrawHelper.BlendColor(Color.White, SkinAppearanceHelper.PrimaryColor, 210);
            vaControl.Properties.Caption.Appearance.FontSizeDelta = 3;
            vaControl.Properties.Caption.Alignment = ContentAlignment.BottomCenter;
            vaControl.Bounds = GetVAControlBounds(vaControl.Parent);
            vaControl.Parent.SizeChanged += (s, e) => vaControl.Bounds = GetVAControlBounds(vaControl.Parent);
        }

        public static void InitConversationEdit(DevExpress.XtraRichEdit.RichEditControl richEditControl)
        {
            richEditControl.ActiveViewType = RichEditViewType.Simple;
            richEditControl.Appearance.Text.FontSizeDelta = 3;
            richEditControl.ReadOnly = true;
            richEditControl.ShowCaretInReadOnly = false;
            richEditControl.Options.Hyperlinks.ModifierKeys = Keys.None;
            richEditControl.Options.Hyperlinks.EnableUriCorrection = true;
            richEditControl.Options.Hyperlinks.ShowToolTip = false;
            richEditControl.Options.Behavior.ShowPopupMenu = DocumentCapability.Hidden;
            Padding oldPadding = richEditControl.Views.SimpleView.Padding;
            richEditControl.Views.SimpleView.Padding = GetConversationRichEditPadding(richEditControl.Bounds, oldPadding);
            richEditControl.SizeChanged += (s, e) => richEditControl.Views.SimpleView.Padding = GetConversationRichEditPadding(richEditControl.Bounds, oldPadding);
        }

        static Padding GetConversationRichEditPadding(Rectangle ownerBounds, Padding ownerPadding)
        {
            Padding newOwnerPadding = new Padding();
            newOwnerPadding.Left = ownerPadding.Left;
            newOwnerPadding.Top = ownerPadding.Top;
            newOwnerPadding.Bottom = ownerPadding.Bottom;
            int right = (ownerBounds.Width - ownerPadding.Left);
            right = right - Math.Min(500, right) + newOwnerPadding.Left;
            newOwnerPadding.Right = right;
            return newOwnerPadding;
        }

        static Rectangle GetVAControlBounds(Control parent)
        {
            var rect = new Rectangle();
            rect.Height = Math.Min(600, parent.ClientSize.Height);
            rect.Width = Math.Min(600, parent.ClientSize.Width);
            rect.X = (parent.ClientSize.Width / 2) - rect.Width / 2;
            rect.Y = (parent.ClientSize.Height / 2) - rect.Height / 2;
            return rect;
        }

    }

    public class SendButton : EditorButton
    {
        private SearchControl owner;
        private const string sendChar = "";

        public SendButton(SearchControl owner)
        {
            this.Appearance.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.AppearanceHovered.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.AppearancePressed.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.Caption = sendChar;
            this.Enabled = true;
            this.Kind = ButtonPredefines.Glyph;
            this.Visible = false;
            this.Width = 40;
            this.ToolTip = "Send";
            this.owner = owner;
            this.owner.TextChanged += OnTextChanged;
            this.Click += SendButton_Click;
        }

        void SendButton_Click(object sender, EventArgs e)
        {
            this.owner.SendKey(new KeyEventArgs(Keys.Enter));
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            this.Visible = !string.IsNullOrWhiteSpace(owner.Text);
        }
    }

    public class MicrophoneButton : EditorButton
    {
        private SearchControl owner;
        private const string mircopChar = "";
        public MicrophoneButton(SearchControl owner)
        {
            this.Appearance.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.AppearanceHovered.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.AppearancePressed.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.Caption = mircopChar;
            this.Enabled = true;
            this.Kind = ButtonPredefines.Glyph;
            this.Visible = true;
            this.Width = 40;
            this.ToolTip = "Microphone";
            this.owner = owner;
            this.owner.TextChanged += OnTextChanged;
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            this.Visible = string.IsNullOrWhiteSpace(owner.Text);
        }
    }

    public class ExpandButton : EditorButton
    {
        private SearchControl owner;
        private const string ExpChar = "";
        public ExpandButton(SearchControl owner)
        {
            this.Appearance.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.AppearanceHovered.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.AppearancePressed.Font = new Font(DrawHelper.SegoeAssetsFontFamily, 4.25F, FontStyle.Regular, GraphicsUnit.Millimeter);
            this.Caption = ExpChar;
            this.Enabled = true;
            this.Kind = ButtonPredefines.Glyph;
            this.Visible = true;
            this.Width = 40;
            this.IsLeft = true;
            this.ToolTip = "Expand";
            this.owner = owner;
            this.Click += ExpandButton_Click;
        }

        private void ExpandButton_Click(object sender, EventArgs e)
        {
            if (Program.MainForm == null) return;
            Program.MainForm.SwitchNextExpandPanel();
        }
    }
}