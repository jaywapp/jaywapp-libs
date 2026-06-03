// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.SkiaLine
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
  public class SkiaLine : SkiaSegment
  {
    public SkiaLine(Point start, Point end, float width, Color color)
      : base(start, end, width, color)
    {
    }

    public SkiaLine(float xs, float ys, float xe, float ye, float width, Color color)
      : base(xs, ys, xe, ye, width, color)
    {
    }

    public SkiaLine(PLine line, Color color)
      : base((float) line.Xs, (float) line.Ys, (float) line.Xe, (float) line.Ye, (float) line.Width, color)
    {
    }

    public override bool Contains(Point pt) => false;

    public override SKPath CreatePath()
    {
      SKPath skPath = new SKPath();
      skPath.MoveTo(this.StartX, this.StartY);
      skPath.LineTo(this.EndX, this.EndY);
      return skPath;
    }

    public override ISkiaGeom Copy() => (ISkiaGeom) new SkiaLine(this.StartX, this.StartY, this.EndX, this.EndY, this.Width, this.Color);

    protected override void OffsetContent(float offsetX, float offsetY)
    {
      this.StartX += offsetX;
      this.StartY += offsetY;
      this.EndX += offsetX;
      this.EndY += offsetY;
    }

    protected override void RotateContent(float angle, Point origin)
    {
      Point point1 = GeometryOperator.Rotate((double) this.StartX, (double) this.StartY, (double) angle, origin.X, origin.Y);
      Point point2 = GeometryOperator.Rotate((double) this.EndX, (double) this.EndY, (double) angle, origin.X, origin.Y);
      this.StartX = (float) point1.X;
      this.StartY = (float) point1.Y;
      this.EndX = (float) point2.X;
      this.EndY = (float) point2.Y;
    }

    protected override void FlipContent(eFlipDirection direction, Point origin)
    {
      Point point1 = GeometryOperator.Flip(direction, (double) this.StartX, (double) this.StartY, origin.X, origin.Y);
      Point point2 = GeometryOperator.Flip(direction, (double) this.EndX, (double) this.EndX, origin.X, origin.Y);
      this.StartX = (float) point1.X;
      this.StartY = (float) point1.Y;
      this.EndX = (float) point2.X;
      this.EndY = (float) point2.Y;
    }
  }
}
