using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Design;
using DevExpress.Skins;
using DevExpress.Skins.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Internals
{
    internal class SkinAppearanceHelper
    {
        public static Color PrimaryColor { get { return Color.FromArgb(0, 122, 204); } }
        public static Color SecondaryColor { get { return Color.FromArgb(236, 113, 20); } }
        public static Font PrimaryFont { get { return new Font("Segoe UI", 10F); } }

        public static void SetDefaultAppearance()
        {
            WindowsFormsSettings.SetDPIAware();
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.AllowPixelScrolling = DefaultBoolean.True;
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Touch;
            ((UserLookAndFeelDefault)UserLookAndFeelDefault.Default).LoadSettings(() =>
            {
                var skinCreator = new SkinBlobXmlCreator("HybridApp", "PreciousUI.SkinData.", typeof(Program).Assembly, null);
                SkinManager.Default.RegisterSkin(skinCreator);
                SplashScreenManager.RegisterUserSkin(skinCreator);
            });
            WindowsFormsSettings.DefaultFont = PrimaryFont;
            WindowsFormsSettings.DefaultMenuFont = PrimaryFont;
            UserLookAndFeel.Default.SetSkinStyle("HybridApp");
            Image caption = GetFormCaptionImage(PrimaryColor);
            FormSkins.GetSkin(UserLookAndFeelDefault.Default)[FormSkins.SkinFormCaption].SetActualImage(caption, true);
            FormSkins.GetSkin(UserLookAndFeelDefault.Default)[FormSkins.SkinFormFrameBottom].SetActualImage(caption, true);
            FormSkins.GetSkin(UserLookAndFeelDefault.Default)[FormSkins.SkinFormFrameRight].SetActualImage(caption, true);
            FormSkins.GetSkin(UserLookAndFeelDefault.Default)[FormSkins.SkinFormFrameLeft].SetActualImage(caption, true);
        }

        static Image GetFormCaptionImage(Color captionColor)
        {
            Bitmap bitmap = new Bitmap(62, 62);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.FillRectangle(new SolidBrush(captionColor), new Rectangle(-1, -1, 63, 63));
                return bitmap;
            }
        }
    }
}
