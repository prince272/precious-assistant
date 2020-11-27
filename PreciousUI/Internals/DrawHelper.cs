using PreciousUI.Internals.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PreciousUI.Internals {
    public static class DrawHelper {

        private static FontFamily sAssetsFontFamily;
        public static FontFamily SegoeAssetsFontFamily
        {
            get
            {
                if (sAssetsFontFamily == null)
                    sAssetsFontFamily = LoadFontFamily(CommonExtension.GetResourceStream("Data.Fonts", "segmdl2.ttf"));
                return sAssetsFontFamily;
            }
        }

        // loads font family from file
        public static FontFamily LoadFontFamily(string fileName) {
            using (var pfc = new PrivateFontCollection()) {
                pfc.AddFontFile(fileName);
                return pfc.Families[0];
            }
        }

        // Load font family from stream
        public static FontFamily LoadFontFamily(Stream stream) {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return LoadFontFamily(buffer);
        }

        // load font family from byte array
        public static FontFamily LoadFontFamily(byte[] buffer) {
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                using (var pvc = new PrivateFontCollection()) {
                    pvc.AddMemoryFont(ptr, buffer.Length);
                    return pvc.Families[0];
                }
            } finally {
                handle.Free();
            }
        }

        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius) {
            var gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y);
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - (radius * 2), x, y + radius);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius) {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend) {
            var ratio = blend / 255d;
            var invRatio = 1d - ratio;
            var r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            var g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            var b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor) {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }
    }
}