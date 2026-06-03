using Graphic.Component.Geometry.Interface;
using Graphic.Component.Geometry.Model;
using System;

namespace Graphic.Component.Geometry.Operator
{
    public static partial class Calculater
    {
        /// <summary>
        /// (x1, y1), (x2, y2) 사이의 거리를 구합니다.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double CalculateDistance(double x1, double y1, double x2, double y2)
            => Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        /// <summary>
        /// c1과 c2간의 거리를 구합니다.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static double CalculateDistnace(this IGraphicComponent c1, IGraphicComponent c2)
        {
            if(c1 is Point pt1)
            {
                if (c2 is Point pt2)
                    return CalculateDistance(pt1, pt2);
                else if (c2 is Line line2)
                    return CalculateDistance(pt1, line2);
            }
            else if(c1 is Line line1)
            {
                if (c2 is Point pt2)
                    return CalculateDistance(pt2, line1);
            }

            return 0;
        }

        /// <summary>
        /// Pt1, Pt2 사이의 거리를 구합니다.
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static double CalculateDistance(Point pt1, Point pt2)
                 => CalculateDistance(pt1.X, pt1.Y, pt2.X, pt2.Y);

        /// <summary>
        /// Point와 Line간의 거리를 구합니다.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private static double CalculateDistance(Point pt, Line line)
        {
            var distnace = Math.Min(
                CalculateDistance(pt, line.Pt1),
                CalculateDistance(pt, line.Pt2));

            return distnace - (line.Width / 2);
        }
    }
}
