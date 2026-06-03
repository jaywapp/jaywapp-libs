// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.SkiaRect
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
  public class SkiaRect : SkiaGeom
  {
    public float X { get; private set; }

    public float Y { get; private set; }

    public float Width { get; private set; }

    public float Height { get; private set; }

    public float Left => this.X - this.Width / 2f;

    public float Right => this.X + this.Width / 2f;

    public float Bottom => this.Y - this.Height / 2f;

    public float Top => this.Y + this.Height / 2f;

    public Point LeftBottom => new Point((double) this.Left, (double) this.Bottom);

    public Point LeftTop => new Point((double) this.Left, (double) this.Top);

    public Point RightTop => new Point((double) this.Right, (double) this.Top);

    public Point RightBottom => new Point((double) this.Right, (double) this.Bottom);

    public SkiaRect(float x, float y, float width, float height, Color color, bool isFilled = true)
      : base(color, isFilled)
    {
      this.X = x;
      this.Y = y;
      this.Width = width;
      this.Height = height;
    }

    public SkiaRect(PRect rect, Color color, bool isFilled = true)
      : this((float) rect.CenterX, (float) rect.CenterY, (float) rect.Width, (float) rect.Height, color, isFilled)
    {
    }

    public override bool Contains(Point pt) => false;

    public override SKPath CreatePath()
    {
      SKPath skPath = new SKPath();
      float left = this.X - this.Width / 2f;
      float right = this.X + this.Width / 2f;
      float bottom = this.Y - this.Height / 2f;
      float top = this.Y + this.Height / 2f;
      skPath.AddRect(new SKRect(left, top, right, bottom));
      return skPath;
    }

    protected override void OffsetContent(float offsetX, float offsetY)
    {
      this.X += offsetX;
      this.Y += offsetY;
    }

    protected override void RotateContent(float angle, Point origin)
    {
      float num = angle % 360f;
      if ((double) num < 0.0)
        num = 360f + num;
      if ((double) num == 90.0 || (double) num == 270.0)
      {
        Point point = GeometryOperator.Rotate((double) this.X, (double) this.Y, (double) num, origin.X, origin.Y);
        this.X = (float) point.X;
        this.Y = (float) point.Y;
      }
      else
      {
        if ((double) num != 0.0 && (double) num != 180.0 && (double) num != 360.0)
          return;
        Point point = GeometryOperator.Rotate((double) this.X, (double) this.Y, (double) num, origin.X, origin.Y);
        float width = this.Width;
        float height = this.Height;
        this.X = (float) point.X;
        this.Y = (float) point.Y;
        this.Width = height;
        this.Height = width;
      }
    }

    protected override void FlipContent(eFlipDirection direction, Point origin)
    {
      Point point = GeometryOperator.Flip(direction, (double) this.X, (double) this.Y, origin.X, origin.Y);
      this.X = (float) point.X;
      this.Y = (float) point.Y;
    }

    public override ISkiaGeom Copy() => (ISkiaGeom) new SkiaRect(this.X, this.Y, this.Width, this.Height, this.Color, this.IsFilled);
  }
}
