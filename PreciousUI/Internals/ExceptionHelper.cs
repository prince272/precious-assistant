using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PreciousUI.Internals {
    public class ExceptionHelper {

        private ExceptionHelper() {
            IsEnabled = true;
        }

        #region Default
        private static ExceptionHelper dException;
        public static ExceptionHelper Default {
            get {
                if (dException == null)
                    dException = new ExceptionHelper();
                return dException;
            }
        }
        #endregion

        private Exception ErrorException = null;
        public bool IsEnabled { get; set; }

        public void Initialize() {
            if (Debugger.IsAttached) return;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
        }
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            try {
                if (!IsEnabled) return;
                ErrorException = e.Exception;
                ShowErrorMessage();
                Error(ErrorException);
            } catch (Exception) { }
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            try {
                if (!IsEnabled) return;
                ErrorException = e.ExceptionObject as Exception;
                ShowErrorMessage();
                Error(ErrorException);
            } catch (Exception) { } finally { ExitCommand(); }
        }
        void ShowErrorMessage() {
            XtraMessageBox.Show(ErrorException.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void ExitCommand() {
            Environment.Exit(1);
        }

        #region Logger
        public void Debug(string message, params object[] args) {
            string str = string.Format(message, args);
            this.LogMessage(str, LogLevel.Debug);
        }
        public void Error(Exception exception) {
            this.LogMessage(ExpandException(exception), LogLevel.Error);
        }
        public void Error(string message, params object[] args) {
            string str = string.Format(message, args);
            this.LogMessage(str, LogLevel.Error);
        }
        public void Fatal(string message, params object[] args) {
            string str = string.Format(message, args);
            this.LogMessage(str, LogLevel.Fatal);
        }
        public void Info(string message, params object[] args) {
            string str = string.Format(message, args);
            this.LogMessage(str, LogLevel.Info);
        }
        public void Log(LogLevel level, string message, params object[] args) {
            this.LogMessage(string.Format(message, args), level);
        }
        void LogMessage(string message, LogLevel level) {
            if (IsEnabled) {
                WriteToFile(new LogReceivedEventArgs(message, level));
                if (this.LogReceived != null) {
                    LogReceived(this, new LogReceivedEventArgs(message, level));
                }
            }
        }
        string ExpandException(Exception exception) {
            List<string> list = new List<string>();
            Exception exception2 = exception;
            list.Add(exception2.ToString());
            while (exception2.InnerException != null) {
                list.Add(exception2.InnerException.ToString());
            }
            return string.Join(Environment.NewLine, (IEnumerable<string>)list);
        }
        public void Trace(string message, params object[] args) {
            string str = string.Format(message, args);
            this.LogMessage(str, LogLevel.Trace);
        }
        public void Warn(string message, params object[] args) {
            string str = string.Format(message, args);
            this.LogMessage(str, LogLevel.Warn);
        }
        public event EventHandler<LogReceivedEventArgs> LogReceived;
        void WriteToFile(LogReceivedEventArgs e) {
            try {
                File.AppendAllText(Application.UserAppDataPath + "\\LogFile.txt", e.ToString());
            } catch (Exception) { }
        }
        #endregion
    }

    public class LogReceivedEventArgs : EventArgs {

        private readonly DateTime time = DateTime.Now;

        public LogReceivedEventArgs(string message, LogLevel level) {
            this.Message = message;
            this.Level = level;
        }

        public override string ToString() {
            object[] objArray1 = new object[] { DateTime.Now, this.Level, this.Message };
            return string.Format("{0} [{1}] {2}", (object[])objArray1);
        }

        public LogLevel Level { get; set; }

        public string Message { get; set; }

        public DateTime Time { get { return time; } }
    }
    public enum LogLevel {
        Debug,
        Error,
        Fatal,
        Info,
        Trace,
        Warn
    }
}