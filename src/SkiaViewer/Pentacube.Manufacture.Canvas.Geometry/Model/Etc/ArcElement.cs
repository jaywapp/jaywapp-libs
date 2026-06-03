// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Etc.ArcElement
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Graphics.Geometry;
using Pentacube.Graphics.Geometry.GeometryOperator;
using System;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Etc
{
  public class ArcElement : Element
  {
    public float CenterX { get; set; }

    public float CenterY { get; set; }

    public bool IsClockwise { get; set; }

    public ArcElement()
    {
    }

    public ArcElement(float x, float y, float cx, float cy, bool isClockwise)
      : base(x, y)
    {
      this.CenterX = cx;
      this.CenterY = cy;
      this.IsClockwise = isClockwise;
    }

    public ArcElement(Point pt, Point center, bool isClockwise)
      : this((float) pt.X, (float) pt.Y, (float) center.X, (float) center.Y, isClockwise)
    {
    }

    public override string ToString() => string.Format("X : {0}, Y : {1}, CX: {2}, CY : {3} ({4}", (object) this.X, (object) this.Y, (object) this.CenterX, (object) this.CenterY, (object) ArcElement.GetText(this.IsClockwise));

    private static string GetText(bool isClockwise) => !isClockwise ? "CCW" : "CW";

    public float GetRadius() => (float) WPFGeomOperator.GetDistance((double) this.X, (double) this.Y, (double) this.CenterX, (double) this.CenterY);

    public override Element Copy()
    {
      ArcElement arcElement = new ArcElement();
      arcElement.X = this.X;
      arcElement.Y = this.Y;
      arcElement.CenterX = this.CenterX;
      arcElement.CenterY = this.CenterY;
      arcElement.IsClockwise = this.IsClockwise;
      return (Element) arcElement;
    }

    public override PPolygonElement ToPPolygonElement() => (PPolygonElement) new PPolygonArcElement((double) this.X, (double) this.Y, (double) this.CenterX, (double) this.CenterY, this.IsClockwise);

    public override void Offset(float offsetX, float offsetY)
    {
      base.Offset(offsetX, offsetY);
      this.CenterX += offsetX;
      this.CenterY += offsetY;
    }

    public override void Rotate(float angle)
    {
      base.Rotate(angle);
      Point point = PGeomOperator.Rotate((double) this.CenterX, (double) this.CenterY, (double) angle);
      this.CenterX = (float) point.X;
      this.CenterY = (float) point.Y;
    }

    public override void Rotate(float angle, Point origin)
    {
      base.Rotate(angle, origin);
      Point center = new Point((double) this.CenterX, (double) this.CenterY);
      this.TransformByOrigin(origin, (Action) (() => center = PGeomOperator.Rotate(center.X, center.Y, (double) angle)));
      this.CenterX = (float) center.X;
      this.CenterY = (float) center.Y;
    }

    public override void FlipHorizontal()
    {
      base.FlipHorizontal();
      this.CenterX = -this.CenterX;
      this.IsClockwise = !this.IsClockwise;
    }

    public override void FlipVertical()
    {
      base.FlipVertical();
      this.CenterY = -this.CenterY;
      this.IsClockwise = !this.IsClockwise;
    }
  }
}
