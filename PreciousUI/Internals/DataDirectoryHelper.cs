using DevExpress.Utils.Drawing.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Internals
{
    internal class DataDirectoryHelper
    {
        public static string EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetRelativePath(string name)
        {
            string relative = name;
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string s = "\\";
            for (int i = 0; i <= 10; i++)
            {
                if (System.IO.File.Exists(path + s + relative))
                    return (path + s + relative);
                else
                    s += "..\\";
            }
            return null;
        }

        public static string GetRelativeDirectoryPath(string name)
        {
            string relative = name;
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string s = "\\";
            for (int i = 0; i <= 10; i++)
            {
                if (System.IO.Directory.Exists(path + s + relative))
                    return (path + s + relative);
                else
                    s += "..\\";
            }
            return null;
        }


        public static string GetNextAvailablePath(string path, long maxBytes)
        {
            string directoryName = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            int count = 0;

            do
            {
                path = Path.Combine(directoryName, CreateNumberedPath(fileName, count));
                if (File.Exists(path))
                {
                    long totalBytes = new FileInfo(path).Length;
                    if (totalBytes < maxBytes)
                        break;
                }
                count++;
            }
            while (File.Exists(path));

            return path;
        }

        public static string CreateNumberedPath(string path, int number)
        {
            string extension = Path.GetExtension(path);
            string plainName = Path.GetFileNameWithoutExtension(path);
            return string.Format("{0}{1}.{2}", plainName, number, extension);
        }

        public static IDisposable SingleInstanceApplicationGuard(string applicationName, out bool isRunning)
        {
            Mutex mutex = new Mutex(true, applicationName);
            if (mutex.WaitOne(0, false))
            {
                isRunning = false;
                return mutex;
            }
            var currentProcess = Process.GetCurrentProcess();
            foreach (Process process2 in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if ((process2.Id != currentProcess.Id) && (process2.MainWindowHandle != IntPtr.Zero))
                {
                    NativeMethods.SetForegroundWindow(process2.MainWindowHandle);
                    NativeMethods.ShowWindowAsync(process2.MainWindowHandle, (int)NativeMethods.ShowWindowCommands.Restore);
                    break;
                }
            }
            isRunning = true;
            return mutex;
        }

        public static void SetAutoStartup(bool enabled)
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                string exePath = Application.ExecutablePath + " -startup";
                if (enabled)
                {
                    registryKey.SetValue(Application.ProductName, exePath);
                }
                else
                {
                    registryKey.DeleteValue(Application.ProductName, false);
                }
            }
            catch (Exception)
            {
            }
        }

        public static string[] GetProductKeys()
        {
            return new string[] { 
                "12345-54321-12345-54321-12345",
                "BLBJT-LQHMK-GTDJX-6N2K2-GQDPY",
                "DW7GR-VYMER-9SDTE-E8XCB-65W6N",
                "XCHEP-4QVT7-97QMR-7KHC4-QPY4N",
                "QMMLE-8GBF4-GPZAD-8BGMB-WN6DU",
                "YVDQK-SZ4LC-32Y4K-3RC76-RQXXB",
                "ZB5P2-TA93J-VFC7C-9B2SQ-6C3KD",
                "RYJWB-7WW24-NZYJ2-UPTAE-N9MW4",
                "RVESR-CE7DX-9BTXQ-3QPDR-M7L74",
                "FPQ7D-BL6ZT-5WCN2-FH2HK-D6Y3M",
                "S4FHC-X5GPF-WDWRX-GHV9M-XHLTQ",
                "5KS5H-H8EFT-ZUD5U-PYXMY-N35X7",
                "V5E5Y-PAERF-S54ZT-5TWRX-U47VM",
                "59RBV-UYTQT-AV8KP-JKA2S-3R938",
                "M3ZJ2-TWL8Q-XPYSL-SCU23-NPR7R",
                "SDKLC-AYNH2-5LGTA-PM38D-JJ47Q",
                "VLTW9-NF5LK-8R3TV-LN8MY-N5C6M",
                "5M8EC-PSD74-8RRVE-5L6WW-MQHSB",
                "AFV5Z-77H35-WKLEJ-5W5AW-3QM8D",
                "3ASUU-3M3KU-9EX8Q-STL22-YWENG",
                "DQN5A-8EWPD-PC5TJ-SQ837-SHNJU",
                "E6FBC-VUQRM-LQV6L-5NM4S-YSS85",
                "8FSAC-FKN4H-MVUV5-JLN9L-ALBG3",
                "K5ELG-YBZRR-QF4CC-LTJDG-NVJWH",
                "3WXHC-9BQLK-WXNUH-MFFDT-AVZBF",
                "XY9NQ-GE9C2-HZ25L-2UZBN-Q8FWP",
                "KNGZK-QUYDX-4ZELA-JKPQ4-KMDG3",
                "5ZFUC-898PZ-CCPPV-F4YN5-73C5S",
                "4BVFB-T4VBC-24R34-68HGV-L6VY7",
                "RV7LN-LW935-UA6BH-UM8LG-SV9KP",
                "DBR6N-P6TW3-WPZKL-ZMNMH-4TA77",
                "C8FKU-7G4PW-DB8M8-2DR3Z-RV7J9",
                "PQBF2-HD9KL-YEVRL-W3TB2-M9KF4",
                "LVUHZ-KMXHX-RVVJU-U2U2N-C7G3U",
                "S7KYC-2F69W-A3BHP-J3YXL-9WENA",
                "K4REN-68TZA-9QYPQ-3FRM2-EFWJ5",
                "RECEM-QP5P6-SXEY9-HCMLL-ZATN3",
                "DPS9U-HAGMQ-FK6FU-F69WA-2L96E",
                "KKAYF-S5CNX-2C7R8-KJWTU-5QPKD",
                "2T375-LYXK9-HZ6GR-MJ8B2-Z9PHW",
                "G4697-XJF3K-QR5XA-NKTUW-TKMH4",
                "EJNTD-U8R6X-PTFFN-9LLGZ-HJDYD",
                "7QT6U-RH6B5-MWX8C-WMFKW-3DVXN",
                "5HHXB-WCS5U-KDD8F-5ZJ5G-U3N9N",
                "GFDQB-LX7UQ-NR5PD-MMUCE-92QK3",
                "34TCR-NLP3W-6A4X2-8TQXZ-DZPWR",
                "Z3YJU-8WZ5F-93NJP-U3BG5-H6ZB2",
                "JHTCD-KUQ8R-S2NLN-C5WTC-ANYZ8",
                "56RH3-Y5RNB-KCNBR-GKURS-ZECCV",
                "CFV5K-4JUB2-82X7B-C7KDV-FABJ9",
                "PAT7T-RA43E-XEVPL-VVWXY-QK9EY",
                "K9TS3-BZW5U-KKZ87-9XHZN-AD4XE",
                "G5HZ3-KTHD9-ZMKUC-F6G7Y-GVKPP",
                "E7S43-7R69V-WUYGG-5M3XM-ERMTM",
                "3KJM7-HTYTD-FYJRS-YMMTZ-3EU2N",
                "GY2J7-SAVV9-CULFV-BYJVQ-7JNSA",
                "6789V-PEV7T-WKJK5-EPYFB-6LHE6",
                "GKQ7H-DA9UF-J5XQ6-QHVXP-L5DQ2",
                "WVU5F-UUJPE-3TD5K-687XD-HUUAN",
                "A7JTM-64QZ4-JWBZX-ZWJBC-V5SNR",
                "MW6RW-LPFQU-26WAE-EC48F-NTF8R",
                "AWDMW-6RN7E-YKVUV-FGXBL-3RL8T",
                "QA28A-B63VB-B96L5-9Y3LF-EYM4W",
                "LJ9PC-XAN8V-KHPCM-QDTAW-9SYDU",
                "7PNZD-8QF2D-XZ3VS-J9AE9-ECMSY",
                "FD95K-YGVTR-3HEAR-G2AV9-XE2T9",
                "GPT7G-RBKGD-R5H8H-WYS2S-24ZBW",
                "CUZ5B-ZB6SH-DLMUX-WK9H7-GJXXA",
                "SPNX3-PES5Z-HXE4Q-Q8S6D-UN5ZQ",
                "JLMNH-TNXB5-4Y6G3-N37U8-DPHF2",
                "NNPG8-DSVZE-QS6NH-EW9HN-3LW52",
                "FPN8Y-K6P82-ZM8K7-7HWL5-9R6VM",
                "63WXG-B3E8P-ARNKX-T5RVE-XLYES",
                "NFA9Q-Y9H2F-NSWYQ-B8TRL-ZDGGL",
                "2K85T-2YEMK-EKTAR-Q6356-YLKVZ",
                "TXK6T-YJAND-GX4LT-4EDHD-VM6TZ",
                "LB6N7-S5RRT-BVE9B-T3TYH-DZ2PT",
                "XSAQD-X68TE-8NNWZ-6E3W2-YQ8ME",
                "TGCVL-5C9KB-PD6YC-STMKB-VVZC7",
                "R2XS8-TQC59-AM9N4-85HAQ-FFLBY",
                "XDDPK-RJJVC-JVFBJ-2UCHJ-MLH23",
                "KTW8N-2N9AA-NDRW8-YBCQY-6K9S2",
                "6LQRZ-X629Q-CXK8R-U582S-EM5JR",
                "5AQ96-NMR9U-FMHL8-AS652-DUAN2",
                "2HAUA-EMM4X-Y2PJV-QND7Y-XJMN5",
                "62VYR-2CN4X-NC4FR-Y75QX-MPRJE",
                "D4T7N-CP48A-YEFH6-CJP2Q-U5NR3",
                "VPXC7-VNQPM-RHYFS-BGQZZ-DLHVU",
                "JVL7U-X5LUZ-P7V63-W9P3D-5JW3F",
                "76BYP-D2G3U-JRBSH-MRB64-NKPPZ",
                "H6J7M-BKSSC-5MJ3P-QG78Z-44ZY4",
                "ZRWRV-G5TTX-WSPDC-PQSWT-KVDMM",
                "BJJFZ-MRMVA-JPUN6-3SZVG-8FPTM",
                "HS76S-DWBSU-VJ8QQ-QKV2S-3NFB7",
                "YGZVP-5T2FE-QFXQT-CJVLJ-HLEMM",
                "BG4QN-MXEXW-JGKCX-ZSU4P-ZPUYS",
                "KKTMN-HPRTH-DCLYW-PQD3P-PNXC9",
                "MJKJS-5CS73-YB9NG-DWUCR-HX3CU",
                "C89EC-JAWAJ-EWD5B-XUJVX-2XS82",
                "2SC2E-33KGE-CXFFP-GKK3L-MZXKV",
                "W8AMQ-45TVW-AHBFC-9WSPK-ZWY7H",
            };
        }

        public static string FormLayoutFilePath { get { return Path.Combine(EnsureDirectoryExists(Path.Combine(Application.UserAppDataPath, "Layout")), "FormLayout.xml"); } }

        public static string PhotoGalleryLayoutFilePath { get { return Path.Combine(EnsureDirectoryExists(Path.Combine(Application.UserAppDataPath, "Layout")), "PhotoGalleryLayout.xml"); } }

        public static string BECEDirectory { get { return GetRelativeDirectoryPath("Data\\Resources\\Bece Questions"); } }

        public static string LogFile
        {
            get
            {
                string file = null;
                int index = 0;
                file = Path.Combine(EnsureDirectoryExists(Path.Combine(Application.UserAppDataPath, "Bug Reports")), string.Format("LogFile", index));
                return GetNextAvailablePath(file, 10000000);
            }
        }
    }
}