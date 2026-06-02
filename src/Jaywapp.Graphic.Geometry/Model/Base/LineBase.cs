using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model.Base
{
    public abstract class LineBase : GeometryBase
    {
        public float Width
        {
            get => Paint.StrokeWidth;
            set => Paint.StrokeWidth = value;
        }

        protected LineBase(float width, Color color) : base(color)
        {
            Width = width;
        }
    }
}
