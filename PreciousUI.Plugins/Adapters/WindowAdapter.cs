using Microsoft.CSharp.RuntimeBinder;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using Syn.Utility;
using Syn.VA;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace PreciousUI.Plugins.Adapters
{
    public class WindowAdapter : IAdapter
    {

        public WindowAdapter()
        {

        }

        #region IAdapter Members
        string IAdapter.Evaluate(Syn.Bot.Siml.Context context)
        {
            try
            {
                string result = string.Empty;
                XAttribute taskAttribute = context.Element.Attribute("Task");
                XAttribute getAttribute = context.Element.Attribute("Get");
                object arg;

                if (taskAttribute != null)
                {
                    switch (taskAttribute.Value.ToLower())
                    {

                        #region Lock
                        case "lock":
                            SynUtility.Computer.Lock();
                            break;
                        #endregion

                        #region Shutdown
                        case "shutdown":
                            SynUtility.Computer.Shutdown();
                            break;
                        #endregion

                        #region Restart
                        case "restart":
                            SynUtility.Computer.Restart();
                            break;
                        #endregion

                        #region Log Off
                        case "logo-off":
                            SynUtility.Computer.LogOff();
                            break;
                        #endregion

                        #region Recycle Bin
                        case "clear-trash":
                            SynUtility.Win32.EmptyRecycleBin();
                            break;
                        #endregion

                        #region Restore
                        case "restore":
                            SynUtility.Win32.RestoreWindow(SynUtility.Win32.GetForegroundWindow());
                            break;
                        #endregion

                        #region Maximize
                        case "maximize":
                            SynUtility.Win32.MaximizeWindow(SynUtility.Win32.GetForegroundWindow());
                            break;
                        #endregion

                        #region Minimize
                        case "minimize":
                            SynUtility.Win32.MinimizeWindow(SynUtility.Win32.GetForegroundWindow());
                            break;
                        #endregion

                        #region Close
                        case "close":
                            SynUtility.Win32.CloseWindow(SynUtility.Win32.GetForegroundWindow());
                            break;
                        #endregion

                        #region Tile
                        case "tile":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P0 == null)
                            {
                                CallSiteCommand.P0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TileHorizontally", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P0.Target(CallSiteCommand.P0, arg);
                            break;
                        #endregion

                        #region Tile Horizontal
                        case "tile-horizontal":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P1 == null)
                            {
                                CallSiteCommand.P1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TileHorizontally", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P1.Target(CallSiteCommand.P1, arg);
                            break;
                        #endregion

                        #region Tile Vertical
                        case "tile-vertical":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P2 == null)
                            {
                                CallSiteCommand.P2 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TileVertically", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P2.Target(CallSiteCommand.P2, arg);
                            break;
                        #endregion

                        #region Cascade
                        case "cascade":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P3 == null)
                            {
                                CallSiteCommand.P3 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "CascadeWindows", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P3.Target(CallSiteCommand.P3, arg);
                            break;
                        #endregion

                        #region Toggle
                        case "toggle":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P4 == null)
                            {
                                CallSiteCommand.P4 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "CascadeWindows", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P4.Target(CallSiteCommand.P4, arg);
                            break;
                        #endregion

                        #region Switch
                        case "switch":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P5 == null)
                            {
                                CallSiteCommand.P5 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "WindowSwitcher", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P5.Target(CallSiteCommand.P5, arg);
                            break;
                        #endregion

                        #region Minimize All
                        case "minimize-all":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P6 == null)
                            {
                                CallSiteCommand.P6 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "MinimizeAll", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P6.Target(CallSiteCommand.P6, arg);
                            break;
                        #endregion

                        #region Undo Minimize All
                        case "undo-minimize-all":
                            arg = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
                            if (arg == null) return string.Empty;
                            if (CallSiteCommand.P7 == null)
                            {
                                CallSiteCommand.P7 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "UndoMinimizeALL", null, typeof(WindowAdapter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
                            }
                            CallSiteCommand.P7.Target(CallSiteCommand.P7, arg);
                            break;
                        #endregion
                    }

                }
                else if (getAttribute != null)
                {
                    switch (getAttribute.Value.ToLower())
                    {
                        case "ip":
                            return SynUtility.Network.GetExternalIp();

                        case "user-name":
                            return Environment.UserName;

                        case "os-version":
                            return Environment.OSVersion.Version.Major.ToString(context.Bot.Culture);

                        case "processor-count":
                            return Environment.ProcessorCount.ToString(context.Bot.Culture);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                SimlBot.Logger.Error(ex);
            }
            return string.Empty;
        }

        bool IAdapter.IsRecursive
        {
            get { return true; }
        }

        XName IAdapter.TagName
        {
            get { return SimlSpecification.Namespace.O + "Window"; }
        }
        #endregion

        [CompilerGenerated]
        private static class CallSiteCommand
        {
            public static CallSite<Action<CallSite, object>> P0 { get; set; }

            public static CallSite<Action<CallSite, object>> P1 { get; set; }

            public static CallSite<Action<CallSite, object>> P2 { get; set; }

            public static CallSite<Action<CallSite, object>> P3 { get; set; }

            public static CallSite<Action<CallSite, object>> P4 { get; set; }

            public static CallSite<Action<CallSite, object>> P5 { get; set; }

            public static CallSite<Action<CallSite, object>> P6 { get; set; }

            public static CallSite<Action<CallSite, object>> P7 { get; set; }
        }

    }
}