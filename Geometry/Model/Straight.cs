using Graphic.Component.Geometry.Interface;

namespace Graphic.Component.Geometry.Model
{
    public class Straight : IGraphicComponent
    {
        #region Properties
        /// <summary>
        /// 직선이 지나는 좌표
        /// </summary>
        public Point Point { get; }
        /// <summary>
        /// 기울기
        /// </summary>
        public double Inclination { get; }
        /// <summary>
        /// 너비
        /// </summary>
        public double Width { get; }
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="inclination"></param>
        /// <param name="width"></param>
        public Straight(Point pt, double inclination, double width = 0)
        {
            Point = pt;
            Inclination = inclination;
            Width = width;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="inclination"></param>
        /// <param name="width"></param>
        public Straight(double x, double y, double inclination, double width = 0) 
            : this(new Point(x, y), inclination, width)
        {
        }
        #endregion
    }
}
