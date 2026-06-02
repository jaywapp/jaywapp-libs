using Jaywapp.Graphic.Geometry.Event;
using SkiaSharp;
using System;

namespace Jaywapp.Graphic.Geometry.Interface
{
    public interface IGeometry
    {
        SKPaint Paint { get; }
        bool IsVisible { get; set; }
        void Draw(SKCanvas canvas);

        void UpdateVisible(object sender, IsVisibleChangeEventArgs args);
        void UpdateColor(object sender, ColorChangeEventArgs args);
    }
}
