using System;

namespace Graphic.Component.Geometry.Operator
{
    public static partial class Calculater
    {
        /// <summary>
        /// (x1, y1), (x2, y2), (x3, y3)에서 (x2, y2)의 각도를 구합니다.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        public static double CalculateAngle(
            double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var a = CalculateDistance(x1, y1, x3, y3);
            var b = CalculateDistance(x1, y1, x2, y2);
            var c = CalculateDistance(x2, y2, x3, y3);

            var angle = Math.Acos(
                (Math.Pow(b, 2) + Math.Pow(c, 2) - Math.Pow(a, 2)) / (2 * b * c));

            return Math.Round(angle * 180 / Math.PI, 4);
        }

        /// <summary>
        /// Pt1, Pt2, Pt3에서 Pt2의 각도를 구합니다.
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        /// <returns></returns>
        public static double CalculateAngle(Point pt1, Point pt2, Point pt3)
            => CalculateAngle(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y);
    }
}
