using Graphic.Component.Geometry.Interface;
using Graphic.Component.Geometry.Operator;

namespace Graphic.Component.Geometry.Model
{
    /// <summary>
    /// 선분
    /// </summary>
    public class Line : IGraphicTrace
    {
        #region Properties
        /// <summary>
        /// Point1
        /// </summary>
        public Point Pt1 { get; }
        /// <summary>
        /// Point2
        /// </summary>
        public Point Pt2 { get; }
        /// <summary>
        /// Width
        /// </summary>
        public double Width { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="width"></param>
        public Line(double x1, double y1, double x2, double y2, double width = 0)
        {
            Pt1 = new Point(x1, y1);
            Pt2 = new Point(x2, y2);
            Width = width;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="width"></param>
        public Line(Point pt1, Point pt2, double width = 0) 
            : this(pt1.X, pt1.Y, pt2.X, pt2.Y, width)
        {
        }
        #endregion
    }
}
