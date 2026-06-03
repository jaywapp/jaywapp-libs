namespace Graphic.Component.Geometry
{
    /// <summary>
    /// Polygon의 곡선 Element
    /// </summary>
    public class PolygonCurveElement : PolygonElement
    {
        #region Properties
        /// <summary>
        /// 곡선의 기준 중점
        /// </summary>
        public Point Center { get; set; }
        /// <summary>
        /// 곡선의 진행방향의 시계방향 여부
        /// </summary>
        public bool IsClockWise { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xc"></param>
        /// <param name="yc"></param>
        /// <param name="isClockwise"></param>
        public PolygonCurveElement(double x, double y, double xc, double yc, bool isClockwise)
            : base(x, y)
        {
            Center = new Point(xc, yc);
            IsClockWise = isClockwise;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="center"></param>
        /// <param name="isClockwise"></param>
        public PolygonCurveElement(Point pt, Point center, bool isClockwise)
            : this(pt.X, pt.Y, center.X, center.Y, isClockwise)
        {
        }
        #endregion
    }
}
