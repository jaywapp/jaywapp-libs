// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.SkiaArc
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Graphics.Geometry;
using Pentacube.Manufacture.Canvas.Geometry.Helper;
using Pentacube.Manufacture.Canvas.Geometry.Interface;
using Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base;
using SkiaSharp;
using System.Windows;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Geometry
{
  public class SkiaArc : SkiaSegment
  {
    public float CenterX { get; private set; }

    public float CenterY { get; private set; }

    public float Radius { get; private set; }

    public bool IsClockwise { get; private set; } = true;

    public SkiaArc(Point start, Point end, Point center, float width, bool isCW, Color color)
      : base(start, end, width, color)
    {
      this.CenterX = (float) center.X;
      this.CenterY = (float) center.Y;
      this.IsClockwise = isCW;
      this.SetRadius();
    }

    public SkiaArc(
      float xs,
      float ys,
      float xe,
      float ye,
      float xc,
      float yc,
      float width,
      bool isCW,
      Color color)
      : base(xs, ys, xe, ye, width, color)
    {
      this.CenterX = xc;
      this.CenterY = yc;
      this.IsClockwise = isCW;
      this.SetRadius();
    }

    public SkiaArc(PArc arc, Color color)
      : this(arc.Start, arc.End, arc.Center, (float) arc.Width, arc.IsClockwise, color)
    {
    }

    public override bool Contains(Point pt) => false;

    private void SetRadius() => this.Radius = (float) new Point((double) this.StartX, (double) this.StartY).GetDistance(new Point((double) this.CenterX, (double) this.CenterY));

    public override SKPath CreatePath()
    {
      SKRect oval = new SKRect(this.CenterX - this.Radius, this.CenterY - this.Radius, this.CenterX + this.Radius, this.CenterY + this.Radius);
      Point start = new Point((double) this.StartX, (double) this.StartY);
      Point end = new Point((double) this.EndX, (double) this.EndY);
      Point center = new Point((double) this.CenterX, (double) this.CenterY);
      if (this.IsClockwise)
      {
        start = new Point((double) this.EndX, (double) this.EndY);
        end = new Point((double) this.StartX, (double) this.StartY);
      }
      (float startAngle, float sweepAngle) = SkiaArc.GetArcAngles(start, center, end);
      SKPath skPath = new SKPath();
      skPath.AddArc(oval, startAngle, sweepAngle);
      return skPath;
    }

    private static (float, float) GetArcAngles(Point start, Point center, Point end)
    {
      float arcAngle = SkiaArc.GetArcAngle(center, start);
      float num = SkiaArc.GetArcAngle(center, end) - arcAngle;
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

    protected override void OffsetContent(float offsetX, float offsetY)
    {
      this.StartX += offsetX;
      this.StartY += offsetY;
      this.EndX += offsetX;
      this.EndY += offsetY;
      this.CenterX += offsetX;
      this.CenterY += offsetY;
    }

    protected override void RotateContent(float angle, Point origin)
    {
      Point point1 = GeometryOperator.Rotate((double) this.StartX, (double) this.StartY, (double) angle, origin.X, origin.Y);
      Point point2 = GeometryOperator.Rotate((double) this.EndX, (double) this.EndY, (double) angle, origin.X, origin.Y);
      Point point3 = GeometryOperator.Rotate((double) this.CenterX, (double) this.CenterY, (double) angle, origin.X, origin.Y);
      this.StartX = (float) point1.X;
      this.StartY = (float) point1.Y;
      this.EndX = (float) point2.X;
      this.EndY = (float) point2.Y;
      this.CenterX = (float) point3.X;
      this.CenterY = (float) point3.Y;
    }

    protected override void FlipContent(eFlipDirection direction, Point origin)
    {
      Point point1 = GeometryOperator.Flip(direction, (double) this.StartX, (double) this.StartY, origin.X, origin.Y);
      Point point2 = GeometryOperator.Flip(direction, (double) this.EndX, (double) this.EndY, origin.X, origin.Y);
      Point point3 = GeometryOperator.Flip(direction, (double) this.CenterX, (double) this.CenterY, origin.X, origin.Y);
      this.StartX = (float) point1.X;
      this.StartY = (float) point1.Y;
      this.EndX = (float) point2.X;
      this.EndY = (float) point2.Y;
      this.CenterX = (float) point3.X;
      this.CenterY = (float) point3.Y;
      this.IsClockwise = !this.IsClockwise;
    }

    public override ISkiaGeom Copy() => (ISkiaGeom) new SkiaArc(this.StartX, this.StartY, this.EndX, this.EndY, this.CenterX, this.CenterY, this.Width, this.IsClockwise, this.Color);
  }
}
