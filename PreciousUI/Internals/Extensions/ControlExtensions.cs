using System;
using System.Diagnostics;

namespace System.Windows.Forms
{
    public static class ControlExtensions
    {
        public static T InvokeIfRequired<T>(this T source, Action<T> action)
            where T : Control
        {
                if (!source.InvokeRequired)
                    action(source);
                else
                    source.Invoke(new Action(() => action(source)));
            return source;
        }
    }
}