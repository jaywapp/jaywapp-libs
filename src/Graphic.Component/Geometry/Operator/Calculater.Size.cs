using Graphic.Component.Geometry.Interface;
using Graphic.Component.Geometry.Model;
using Infra.Enumerable;
using System;
using System.Linq;

namespace Graphic.Component.Geometry.Operator
{
    public static partial class Calculater
    {
        /// <summary>
        /// IGraphicFigure의 넓이를 구합니다.
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        public static double CalculateSize(this IGraphicFigure figure)
        {
            if (figure is Circle circle)
                return CalculateSize(circle);
            else if (figure is Rectangle rect)
                return CalculateSize(rect);
            else if (figure is Polygon polygon)
                return CalculateSize(polygon);

            return 0;
        }

        /// <summary>
        /// 원의 넓이를 구합니다.
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        private static double CalculateSize(Circle circle)
            =>  circle.Radius * circle.Radius * Math.PI;

        /// <summary>
        /// 사각형의 넓이를 구합니다.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private static double CalculateSize(Rectangle rect)
            => rect.Width * rect.Height;

        /// <summary>
        /// 다각형의 넓이를 구합니다.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private static double CalculateSize(Polygon polygon)
        {
            var pairs = polygon.Elements.ChainPairing();

            var curveSizes = pairs
                .Select(p => p.Item1.CreateTrace(p.Item2))
                .OfType<Curve>()
                .Sum(c => CalculateSize(c) - CalculateSize(c.Pt1, c.Origin, c.Pt2));

            return CalculateSize(polygon.Elements.ToArray()) + curveSizes;
        }

        /// <summary>
        /// 호의 넓이를 구합니다.
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        private static double CalculateSize(Curve arc)
        {
            var angle = CalculateAngle(arc.Pt1, arc.Origin, arc.Pt2);
            var circle = new Circle(arc.Origin, arc.Radius);


            return CalculateSize(circle) * (angle / 360);
        }

        /// <summary>
        /// Point 목록으로 만들어진 다각형의 넓이를 구합니다.
        /// </summary>
        /// <see cref="https://ko.wikipedia.org/wiki/%EC%8B%A0%EB%B0%9C%EB%81%88_%EA%B3%B5%EC%8B%9D">
        /// 신발끈 공식
        /// </see>
        /// <param name="pts"></param>
        /// <returns></returns>
        private static double CalculateSize(params Point[] pts)
        {
            var pairs = pts.ChainPairing();

            return (pairs.Sum(p => p.Item1.X * p.Item2.Y)
              - pairs.Sum(p => p.Item2.X * p.Item1.Y)) * 0.5;
        }

    }
}
