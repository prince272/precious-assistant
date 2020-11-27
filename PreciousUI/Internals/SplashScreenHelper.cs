using DevExpress.XtraSplashScreen;
using PreciousUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Internals
{
    internal class SplashScreenHelper
    {
        public static void ShowSplashScreen()
        {
            SplashScreenManager.ShowForm(null, typeof(SplashScreenForm), true, true, false, true);
        }

        public static void CloseSplashScreen()
        {
            SplashScreenManager.CloseForm(false, true);
        }

        public static void SetSplashScreenStatus(string status)
        {
            if (SplashScreenManager.Default == null ||
                !SplashScreenManager.Default.IsSplashFormVisible ||
                SplashScreenManager.Default.ActiveSplashFormTypeInfo.Mode != Mode.SplashScreen)
                return;
            SplashScreenManager.Default.SendCommand(SplashScreenForm.SplashScreenCommand.Status, status);
        }

        public static void ShowWaitIndicator()
        {
            SplashScreenManager.ShowForm(null, typeof(WaitIndicatorForm), true, true, false);
        }

        public static void CloseWaitIndicator()
        {
            SplashScreenManager.CloseForm(false, true);
        }

        public static void SetWaitIndicatorCaption(string caption)
        {
            if (SplashScreenManager.Default == null ||
                !SplashScreenManager.Default.IsSplashFormVisible ||
                SplashScreenManager.Default.ActiveSplashFormTypeInfo.Mode != Mode.WaitForm)
                return;
            SplashScreenManager.Default.SetWaitFormCaption(caption);
        }

        public static void SetWaitIndicatorDescription(string description)
        {
            if (SplashScreenManager.Default == null ||
                !SplashScreenManager.Default.IsSplashFormVisible ||
                SplashScreenManager.Default.ActiveSplashFormTypeInfo.Mode != Mode.WaitForm)
                return;
            SplashScreenManager.Default.SetWaitFormCaption(description);
        }
    }
}
