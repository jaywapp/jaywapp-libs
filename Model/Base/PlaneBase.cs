using SkiaSharp;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model.Base
{
    public abstract class PlaneBase : GeometryBase
    {
        public bool IsFilled
        {
            get => Paint.Style == SKPaintStyle.Fill;
            set => Paint.Style = value ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
        }
     
        protected PlaneBase(Color color) : base(color)
        {
        }
    }
}
