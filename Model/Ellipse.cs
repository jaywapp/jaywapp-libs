using Jaywapp.Graphic.Geometry.Model.Base;
using SkiaSharp;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model
{
    public class Ellipse : PlaneBase
    {
        #region Properties
        public float X { get; set; }
        public float Y { get; set; }    
        public float Width { get; set; }
        public float Height { get; set; }
        #endregion

        #region Constructor
        public Ellipse(float x, float y, float width, float height, Color? color = null) : base(color ?? Colors.White)
        {
            X = x; 
            Y = y;
            Width = width;  
            Height = height;
        }

        public Ellipse(Rectangle rectangle, Color? color = null)
            : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color)
        {
        }
        #endregion

        #region Functions
        public override void Draw(SKCanvas canvas)
        {
            canvas.DrawOval(X, Y, Width / 2, Height / 2, GetPaint());
        }
        #endregion
    }
}
