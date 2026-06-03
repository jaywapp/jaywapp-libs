// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Helper.GeometryOperator
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using System;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Geometry.Helper
{
  public static class GeometryOperator
  {
    public static double GetDistance(this Point pt1, Point pt2)
    {
      double x1 = Math.Abs(pt1.X - pt2.X);
      double x2 = Math.Abs(pt1.Y - pt2.Y);
      return Math.Sqrt(Math.Pow(x1, 2.0) + Math.Pow(x2, 2.0));
    }

    public static Point Rotate(
      double x,
      double y,
      double degree,
      double originX = 0.0,
      double originY = 0.0)
    {
      Point pt = GeometryOperator.Offset(new Point(x, y), -originX, -originY);
      double num = degree * Math.PI / 180.0;
      pt = new Point(Math.Cos(num) * pt.X + Math.Sin(num) * pt.Y, -Math.Sin(num) * pt.X + Math.Cos(num) * pt.Y);
      return GeometryOperator.Offset(pt, originX, originY);
    }

    public static Point Offset(Point pt, double offsetX, double offsetY) => new Point(pt.X + offsetX, pt.Y + offsetY);

    public static Point Flip(
      eFlipDirection direction,
      double x,
      double y,
      double originX = 0.0,
      double originY = 0.0)
    {
      Point pt = GeometryOperator.Offset(new Point(x, y), -originX, -originY);
      pt = direction != eFlipDirection.Horizontal ? new Point(pt.X, -pt.Y) : new Point(-pt.X, pt.Y);
      pt = GeometryOperator.Offset(pt, originX, originY);
      return pt;
    }
  }
}
