using Jaywapp.Graphic.Geometry.Model.Base;
using SkiaSharp;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model
{
    public class Rectangle : PlaneBase
    {
        #region Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }    
        public float Height { get; set; }
        #endregion

        #region Constructor
        public Rectangle(float x, float y, float width, float height, Color? color = null) : base(color ?? Colors.White)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        #endregion

        #region Functions
        public override void Draw(SKCanvas canvas)
        {
            canvas.DrawRect(X, Y, Width, Height, GetPaint());
        }
        #endregion
    }
}
