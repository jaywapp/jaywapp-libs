using Graphic.Component.Geometry.Interface;

namespace Graphic.Component.Geometry
{
    public class Point : IGraphicComponent
    {
        #region Properties
        /// <summary>
        /// X 좌표
        /// </summary>
        public double X { get;}
        /// <summary>
        /// Y 좌표
        /// </summary>
        public double Y { get;}
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        #endregion
    }
}
