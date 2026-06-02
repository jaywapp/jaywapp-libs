using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Helper
{
    public static class SKHelper
    {
        public static IEnumerable<SKPoint> Convert(this IEnumerable<Point> points)
        {
            return points.Select(p => new SKPoint((float)p.X, (float)p.Y));
        }

        public static Color Convert(this SKColor color)
        {
            return Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static SKColor Convert(this Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }
    }
}
