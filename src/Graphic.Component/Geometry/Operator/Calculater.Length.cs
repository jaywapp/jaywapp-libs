using Graphic.Component.Geometry.Interface;
using Graphic.Component.Geometry.Model;
using System;

namespace Graphic.Component.Geometry.Operator
{
    public static partial class Calculater
    {
        /// <summary>
        /// IGraphicTrace의 길이를 구합니다.
        /// </summary>
        /// <param name="trace"></param>
        /// <returns></returns>
        public static double CalculateLength(this IGraphicTrace trace)
        {
            if (trace is Line line)
                return CalculateLength(line);
            else if (trace is Curve curve)
                return CalculateLength(curve);

            return 0;
        }

        /// <summary>
        /// 선분의 길이를 계산합니다.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static double CalculateLength(Line line)
            => CalculateDistance(line.Pt1, line.Pt2);

        /// <summary>
        /// 곡선의 길이를 구합니다.
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        private static double CalculateLength(Curve curve)
        {
            var angle = curve.IsClockWise
             ? Calculater.CalculateAngle(curve.Pt1, curve.Origin, curve.Pt2)
             : Calculater.CalculateAngle(curve.Pt2, curve.Origin, curve.Pt1);


            return (2 * Math.PI * curve.Radius) * (angle / 360);
        }
    }
}
