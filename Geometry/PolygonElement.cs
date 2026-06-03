using Graphic.Component.Geometry.Interface;
using Graphic.Component.Geometry.Model;
using Microsoft.VisualBasic;

namespace Graphic.Component.Geometry
{
    /// <summary>
    /// Polygon의 직선 Element
    /// </summary>
    public class PolygonElement : Point
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PolygonElement(double x, double y) : base(x, y)
        {
        }
    }

    /// <summary>
    /// PolygonElement관련 Extension
    /// </summary>
    public static class PolygonElementExt
    {
        public static IGraphicTrace CreateTrace(
            this PolygonElement prev, PolygonElement next)
        {
            if(next is PolygonCurveElement cNext)
                return new Curve(prev, cNext.Center, cNext, cNext.IsClockWise);

            return new Line(prev, next);
        }
    }
}
