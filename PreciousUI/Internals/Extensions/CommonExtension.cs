using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Internals.Extensions
{
    internal static class CommonExtension
    {
        public static Stream GetResourceStream(string resourceName, string itemName)
        {
            string name = string.Format("PreciousUI.Resources.{0}.{1}", resourceName, itemName);
            Stream stream = ResourceStreamHelper.GetStream(name, Assembly.GetExecutingAssembly());
            return stream;
        }

        public static Image GetResourceImage(string resourceName, string itemName)
        {
            string name = string.Format("PreciousUI.Resources.Images.{0}.{1}", resourceName, itemName);
            Image image = ResourceImageHelper.CreateImageFromResources(name, Assembly.GetExecutingAssembly());
            return image;
        }
    }
}
