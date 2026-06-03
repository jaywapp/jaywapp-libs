// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Service.SKPathDrawingBuilder
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Graphics.Geometry;
using Pentacube.Graphics.Geometry.GeometryOperator;
using Pentacube.Manufacture.Canvas.Geometry.Model.Etc;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Geometry.Service
{
  public class SKPathDrawingBuilder
  {
    private SKPath _path;

    public SKPathDrawingBuilder() => this._path = new SKPath();

    public SKPath Build()
    {
      this._path.Close();
      return this._path;
    }

    public void Draw(IPGeom geom, float offsetX = 0.0f, float offsetY = 0.0f)
    {
      switch (geom)
      {
        case PCircle circle:
          this.DrawCircle(circle, offsetX, offsetY);
          break;
        case PRect rect:
          this.DrawRect(rect, offsetX, offsetY);
          break;
        case PPolygon polygon:
          this.DrawPolygon(polygon, offsetX, offsetY);
          break;
        case PLine line:
          this.DrawLine(line, offsetX, offsetY);
          break;
        case PArc arc:
          this.DrawArc(arc, offsetX, offsetY);
          break;
      }
    }

    public void Draw(params Point[] points) => this.Draw(((IEnumerable<Point>) points).Select<Point, Element>((Func<Point, Element>) (pt => new Element(pt))).ToArray<Element>());

    public void Draw(params Element[] elements)
    {
      // ISSUE: unable to decompile the method.
    }

    private void DrawArc(Element before, ArcElement next)
    {
      Point center = new Point((double) next.CenterX, (double) next.CenterY);
      Point start = new Point((double) before.X, (double) before.Y);
      Point end = new Point((double) next.X, (double) next.Y);
      float radius = next.GetRadius();
      SKRect oval = SKPathDrawingBuilder.CreateOval(center, radius);
      if (next.IsClockwise)
      {
        double x = start.X;
        double y = start.Y;
        start = new Point(end.X, end.Y);
        end = new Point(x, y);
      }
      (float startAngle, float sweepAngle) = SKPathDrawingBuilder.GetArcAngles(start, center, end);
      this._path.ArcTo(oval, startAngle, sweepAngle, true);
    }

    private void DrawLine(Element next) => this._path.LineTo(next.X, next.Y);

    private void DrawCircle(PCircle circle, float offsetX = 0.0f, float offsetY = 0.0f) => this._path.AddCircle((float) circle.Center.X + offsetX, (float) circle.Center.Y + offsetY, (float) circle.Radius);

    private void DrawRect(PRect rect, float offsetX = 0.0f, float offsetY = 0.0f)
    {
      float width = (float) rect.Width;
      float height = (float) rect.Height;
      double num1 = rect.Center.X + (double) offsetX - (double) width / 2.0;
      double num2 = rect.Center.X + (double) offsetX + (double) width / 2.0;
      double num3 = rect.Center.Y + (double) offsetY + (double) height / 2.0;
      double num4 = rect.Center.Y + (double) offsetY - (double) height / 2.0;
      this._path.AddRect(new SKRect((float) num1, (float) num3, (float) num2, (float) num4));
    }

    private void DrawPolygon(PPolygon polygon, float offsetX = 0.0f, float offsetY = 0.0f) => this._path.AddPoly(SKPathDrawingBuilder.GetElements(polygon, offsetX, offsetY).ToArray<SKPoint>());

    private void DrawLine(PLine line, float offsetX = 0.0f, float offsetY = 0.0f)
    {
      SKPoint point1 = new SKPoint((float) line.Xs + offsetX, (float) line.Ys + offsetY);
      SKPoint point2 = new SKPoint((float) line.Xe + offsetX, (float) line.Ye + offsetY);
      this._path.MoveTo(point1);
      this._path.LineTo(point2);
    }

    private void DrawArc(PArc arc, float offsetX = 0.0f, float offsetY = 0.0f)
    {
      Point point1;
      ref Point local1 = ref point1;
      Point center = arc.Center;
      double x1 = center.X + (double) offsetX;
      center = arc.Center;
      double y1 = center.Y + (double) offsetY;
      local1 = new Point(x1, y1);
      Point point2;
      ref Point local2 = ref point2;
      Point start = arc.Start;
      double x2 = start.X + (double) offsetX;
      start = arc.Start;
      double y2 = start.Y + (double) offsetY;
      local2 = new Point(x2, y2);
      Point end1;
      ref Point local3 = ref end1;
      Point end2 = arc.End;
      double x3 = end2.X + (double) offsetX;
      end2 = arc.End;
      double y3 = end2.Y + (double) offsetY;
      local3 = new Point(x3, y3);
      float distance = (float) WPFGeomOperator.GetDistance(point2, point1);
      SKRect oval = SKPathDrawingBuilder.CreateOval(point1, distance);
      if (arc.IsClockwise)
      {
        double x4 = point2.X;
        double y4 = point2.Y;
        point2 = new Point(end1.X, end1.Y);
        end1 = new Point(x4, y4);
      }
      (float startAngle, float sweepAngle) = SKPathDrawingBuilder.GetArcAngles(point2, point1, end1);
      this._path.AddArc(oval, startAngle, sweepAngle);
    }

    private static SKRect CreateOval(Point center, float radius) => new SKRect((float) center.X - radius, (float) center.Y - radius, (float) center.X + radius, (float) center.Y + radius);

    private static (float, float) GetArcAngles(Point start, Point center, Point end)
    {
      float arcAngle = SKPathDrawingBuilder.GetArcAngle(center, start);
      float num = SKPathDrawingBuilder.GetArcAngle(center, end) - arcAngle;
      if ((double) num < 0.0)
        num += 360f;
      return (arcAngle, num);
    }

    private static float GetArcAngle(Point center, Point point)
    {
      float num = (float) Vector.AngleBetween(new Vector(1.0, 0.0), point - center);
      if ((double) num < 0.0)
        num += 360f;
      return num;
    }

    private static IEnumerable<SKPoint> GetElements(
      PPolygon polygon,
      float offsetX,
      float offsetY)
    {
      List<Point> list = polygon.Elements.Select<PPolygonElement, Point>((Func<PPolygonElement, Point>) (e => e.Location)).ToList<Point>();
      Point center = SKPathDrawingBuilder.GetCenter((IEnumerable<Point>) list);
      foreach (Point point in list)
        yield return new SKPoint((float) ((double) offsetX + point.X - center.X), (float) ((double) offsetY + point.Y - center.Y));
    }

    private static Point GetCenter(IEnumerable<Point> pts)
    {
      double num1 = pts.Min<Point>((Func<Point, double>) (p => p.X));
      double num2 = pts.Max<Point>((Func<Point, double>) (p => p.X));
      double num3 = pts.Min<Point>((Func<Point, double>) (p => p.Y));
      double num4 = pts.Max<Point>((Func<Point, double>) (p => p.Y));
      double num5 = num2;
      return new Point((num1 + num5) / 2.0, (num3 + num4) / 2.0);
    }
  }
}
