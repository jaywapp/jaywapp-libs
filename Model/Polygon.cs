using Jaywapp.Graphic.Geometry.Helper;
using Jaywapp.Graphic.Geometry.Model.Base;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model
{
    public class Polygon : PlaneBase
    {
        #region Properties
        public List<Point> Points { get; set; } = new List<Point>();
        #endregion

        #region Constructor
        public Polygon(IEnumerable<Point> points, Color? color = null) : base(color ?? Colors.White)
        {
            Points = points.ToList();
        }
        #endregion

        #region Functions
        public override void Draw(SKCanvas canvas)
        {
            var path = new SKPath();
            path.AddPoly(Points.Convert().ToArray());

            canvas.DrawPath(path, GetPaint());
        }
        #endregion
    }
}
