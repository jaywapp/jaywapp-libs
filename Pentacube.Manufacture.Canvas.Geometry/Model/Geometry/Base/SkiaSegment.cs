// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base.SkiaSegment
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using System.Windows;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base
{
  public abstract class SkiaSegment : SkiaGeom
  {
    public float StartX { get; protected set; }

    public float StartY { get; protected set; }

    public float EndX { get; protected set; }

    public float EndY { get; protected set; }

    public float Width
    {
      get => this.Paint.StrokeWidth;
      set => this.Paint.StrokeWidth = value;
    }

    public SkiaSegment(float xs, float ys, float xe, float ye, float width, Color color)
      : base(color, false)
    {
      this.StartX = xs;
      this.StartY = ys;
      this.EndX = xe;
      this.EndY = ye;
      this.Width = width;
      this.Color = color;
    }

    public SkiaSegment(Point start, Point end, float width, Color color)
      : this((float) start.X, (float) start.Y, (float) end.X, (float) end.Y, width, color)
    {
    }
  }
}
