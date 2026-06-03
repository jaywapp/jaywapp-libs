// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Etc.Element
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Graphics.Geometry;
using Pentacube.Manufacture.Canvas.Geometry.Helper;
using System;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Etc
{
  public class Element
  {
    public float X { get; set; }

    public float Y { get; set; }

    public Element()
    {
    }

    public Element(float x, float y)
      : this()
    {
      this.X = x;
      this.Y = y;
    }

    public Element(Point pt)
      : this((float) pt.X, (float) pt.Y)
    {
    }

    public override string ToString() => string.Format("X : {0}, Y : {1}", (object) this.X, (object) this.Y);

    public virtual Element Copy() => new Element()
    {
      X = this.X,
      Y = this.Y
    };

    public virtual PPolygonElement ToPPolygonElement() => new PPolygonElement((double) this.X, (double) this.Y);

    public static Element FromPoint(Point pt) => new Element()
    {
      X = (float) pt.X,
      Y = (float) pt.Y
    };

    protected void TransformByOrigin(Point origin, Action action)
    {
      float x = (float) origin.X;
      float y = (float) origin.Y;
      this.Offset(-x, -y);
      action();
      this.Offset(x, y);
    }

    public virtual void Offset(float offsetX, float offsetY)
    {
      this.X += offsetX;
      this.Y += offsetY;
    }

    public virtual void Rotate(float angle)
    {
      Point point = GeometryOperator.Rotate((double) this.X, (double) this.Y, (double) angle);
      this.X = (float) point.X;
      this.Y = (float) point.Y;
    }

    public virtual void Rotate(float angle, Point origin)
    {
      Point loc = new Point((double) this.X, (double) this.Y);
      this.TransformByOrigin(origin, (Action) (() => loc = GeometryOperator.Rotate(loc.X, loc.Y, (double) angle)));
      this.X = (float) loc.X;
      this.Y = (float) loc.Y;
    }

    public virtual void FlipHorizontal() => this.X = -this.X;

    public void FlipHorizontal(Point origin) => this.TransformByOrigin(origin, new Action(this.FlipHorizontal));

    public virtual void FlipVertical() => this.Y = -this.Y;

    public void FlipVertical(Point origin) => this.TransformByOrigin(origin, new Action(this.FlipVertical));
  }
}
