using Jaywapp.Graphic.Geometry.Model.Base;
using SkiaSharp;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model
{
    public class Segment : LineBase
    {
        #region Properties
        public float StartX { get; set; }
        public float StartY { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }
        #endregion

        #region Constructor
        public Segment(float xs, float ys, float xe, float ye, float width, Color? color = null) : base(width, color ?? Colors.White) 
        {
            StartX = xs;
            StartY = ys;
            EndX = xe;
            EndY = ye;
        }
        #endregion

        #region Functions
        public override void Draw(SKCanvas canvas)
        {
            canvas.DrawLine(StartX, StartY, EndX, EndY, GetPaint());
        }
        #endregion
    }
}
