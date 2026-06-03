// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.SkiaPolygon
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Graphics.Geometry;
using Pentacube.Manufacture.Canvas.Geometry.Interface;
using Pentacube.Manufacture.Canvas.Geometry.Model.Etc;
using Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base;
using Pentacube.Manufacture.Canvas.Geometry.Service;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Geometry
{
  public class SkiaPolygon : SkiaGeom
  {
    public List<Element> Elements { get; private set; } = new List<Element>();

    public SkiaPolygon(List<Element> elements, Color color, bool isFilled = true)
      : base(color, isFilled)
    {
      this.Elements = elements;
    }

    public SkiaPolygon(PPolygon polygon, Color color, bool isFilled = true)
      : this(SkiaPolygon.GetElements(polygon).ToList<Element>(), color, isFilled)
    {
    }

    public override bool Contains(Point pt) => false;

    private static IEnumerable<Element> GetElements(PPolygon polygon)
    {
      foreach (PPolygonElement element in polygon.Elements)
      {
        if (element is PPolygonArcElement ppolygonArcElement1)
        {
          ArcElement arcElement = new ArcElement();
          arcElement.X = (float) ppolygonArcElement1.X;
          arcElement.Y = (float) ppolygonArcElement1.Y;
          Point center = ppolygonArcElement1.Center;
          arcElement.CenterX = (float) center.X;
          center = ppolygonArcElement1.Center;
          arcElement.CenterY = (float) center.Y;
          arcElement.IsClockwise = ppolygonArcElement1.IsClockwise;
          yield return (Element) arcElement;
        }
        else
          yield return new Element()
          {
            X = (float) element.X,
            Y = (float) element.Y
          };
      }
    }

    public override SKPath CreatePath()
    {
      SKPathDrawingBuilder pathDrawingBuilder = new SKPathDrawingBuilder();
      pathDrawingBuilder.Draw(this.Elements.ToArray());
      return pathDrawingBuilder.Build();
    }

    protected override void OffsetContent(float offsetX, float offsetY) => this.Elements.ForEach((Action<Element>) (e => e.Offset(offsetX, offsetY)));

    protected override void RotateContent(float angle, Point origin) => this.Elements.ForEach((Action<Element>) (e => e.Rotate(angle, origin)));

    protected override void FlipContent(eFlipDirection direction, Point origin) => this.Elements.ForEach((Action<Element>) (e =>
    {
      if (direction == eFlipDirection.Horizontal)
        e.FlipHorizontal(origin);
      else
        e.FlipVertical(origin);
    }));

    public override ISkiaGeom Copy() => (ISkiaGeom) new SkiaPolygon(this.Elements.Select<Element, Element>((Func<Element, Element>) (e => e.Copy())).ToList<Element>(), this.Color, this.IsFilled);
  }
}
