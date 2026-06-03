using Graphic.Component.Geometry.Operator;
using NUnit.Framework;

namespace Graphic.Component.Test.Geometry.Operator
{
    [TestFixture]
    public class TestCalculator
    {
        [TestCase(1, 1, 2, 1, 1)]
        public void TestMeasureDistance(
            double x1, double y1, double x2, double y2, 
            double expect)
        {
            // when
            var actual = Calculator.MeasureDistance(x1, y1, x2, y2);

            // then
            Assert.AreEqual(actual, expect);
        }

        [TestCase(1, 0, 0, 0, 0, 1, 90)]
        [TestCase(1, 0, 0, 0, 1, 1, 45)]
        public void TestMeasureAngle(
            double x1, double y1, double x2, double y2, double x3, double y3, 
            double expect)
        {
            // when 
            var actual = Calculator.MeasureAngle(x1, y1, x2, y2, x3, y3);

            // then
            Assert.AreEqual(actual, expect);
        }

    }
}
