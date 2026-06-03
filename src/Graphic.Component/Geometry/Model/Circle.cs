using Graphic.Component.Geometry.Interface;

namespace Graphic.Component.Geometry.Model
{
    /// <summary>
    /// 원
    /// </summary>
    public class Circle : IGraphicFigure
    {
        #region Properties
        /// <summary>
        /// 원의 중점
        /// </summary>
        public Point Center { get; }
        /// <summary>
        /// 원의 반지름
        /// </summary>
        public double Radius { get; }
        /// <summary>
        /// 내부가 채워진 원 여부
        /// </summary>
        public bool IsFilled { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="isFilled"></param>
        public Circle(Point center, double radius, bool isFilled = false)
        {
            Center = center;
            Radius = radius;
            IsFilled = isFilled;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="isFilled"></param>

        public Circle(double x, double y, double radius, bool isFilled = false)
            : this(new Point(x, y), radius, isFilled)
        {
        }
        #endregion
    }
}
