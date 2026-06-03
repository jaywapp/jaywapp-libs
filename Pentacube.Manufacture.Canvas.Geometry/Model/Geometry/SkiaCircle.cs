// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.SkiaCircle
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Graphics.Geometry;
using Pentacube.Graphics.Geometry.GeometryOperator;
using Pentacube.Manufacture.Canvas.Geometry.Interface;
using Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base;
using SkiaSharp;
using System.Windows;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Geometry
{
  public class SkiaCircle : SkiaGeom
  {
    public float X { get; private set; }

    public float Y { get; private set; }

    public float Radius { get; private set; }

    public SkiaCircle(float x, float y, float radius, Color color, bool isFilled = true)
      : base(color, isFilled)
    {
      this.X = x;
      this.Y = y;
      this.Radius = radius;
    }

    public SkiaCircle(PCircle circle, Color color, bool isFilled)
      : this((float) circle.X, (float) circle.Y, (float) circle.Radius, color, isFilled)
    {
    }

    public override bool Contains(Point pt) => WPFGeomOperator.GetDistance(new Point((double) this.X, (double) this.Y), pt) <= (double) this.Radius;

    public override SKPath CreatePath()
    {
      SKPath skPath = new SKPath();
      skPath.AddCircle(this.X, this.Y, this.Radius);
      return skPath;
    }

    protected override void OffsetContent(float offsetX, float offsetY)
    {
      this.X += offsetX;
      this.Y += offsetY;
    }

    protected override void RotateContent(float angle, Point origin)
    {
      Point point = Pentacube.Manufacture.Canvas.Geometry.Helper.GeometryOperator.Rotate((double) this.X, (double) this.Y, (double) angle, origin.X, origin.Y);
      this.X = (float) point.X;
      this.Y = (float) point.Y;
    }

    protected override void FlipContent(eFlipDirection direction, Point origin)
    {
      Point point = Pentacube.Manufacture.Canvas.Geometry.Helper.GeometryOperator.Flip(direction, (double) this.X, (double) this.Y, origin.X, origin.Y);
      this.X = (float) point.X;
      this.Y = (float) point.Y;
    }

    public override ISkiaGeom Copy() => (ISkiaGeom) new SkiaCircle(this.X, this.Y, this.Radius, this.Color, this.IsFilled);
  }
}
