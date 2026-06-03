using Graphic.Component.Geometry.Interface;
using System;

namespace Graphic.Component.Geometry.Model
{
    public class Rectangle : IGraphicFigure
    {
        #region Properties
        /// <summary>
        /// 좌상단
        /// </summary>
        public Point LeftTop { get; }
        /// <summary>
        /// 좌하단
        /// </summary>
        public Point LeftBottom { get; }
        /// <summary>
        /// 우상단
        /// </summary>
        public Point RightTop { get; }
        /// <summary>
        /// 우하단
        /// </summary>
        public Point RightBottom { get; }
        /// <summary>
        /// 내부가 채워진 사각형 여부
        /// </summary>
        public bool IsFilled { get; }
        /// <summary>
        /// 너비
        /// </summary>
        public double Width => Math.Abs(RightBottom.X - LeftBottom.X);
        /// <summary>
        /// 높이
        /// </summary>
        public double Height => Math.Abs(RightTop.Y - RightBottom.Y);
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="leftTop"></param>
        /// <param name="leftBottom"></param>
        /// <param name="rightTop"></param>
        /// <param name="rightBottom"></param>
        /// <param name="isFilled"></param>
        public Rectangle(
            Point leftTop, Point leftBottom, 
            Point rightTop, Point rightBottom, 
            bool isFilled = false)
        {
            if (leftTop.X != leftBottom.X)
                throw new ArgumentException();
            if (rightTop.X != rightBottom.X)
                throw new ArgumentException();

            if (leftTop.Y != rightTop.Y)
                throw new ArgumentException();
            if (leftBottom.Y != rightBottom.Y)
                throw new ArgumentException();

            LeftTop = leftTop;
            LeftBottom = leftBottom;
            RightTop = rightTop;
            RightBottom = rightBottom;

            IsFilled = isFilled;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="center"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isFilled"></param>
        public Rectangle(Point center, double width, double height, bool isFilled = false)
        {
            var x = width / 2;
            var y = height / 2;

            LeftTop = new Point(center.X - x, center.Y + y);
            RightTop = new Point(center.X + x, center.Y + y);
            LeftBottom = new Point(center.X - x, center.Y - y);
            RightBottom = new Point(center.X + x, center.Y - y);
            IsFilled = isFilled;
        }
        #endregion
    }
}
