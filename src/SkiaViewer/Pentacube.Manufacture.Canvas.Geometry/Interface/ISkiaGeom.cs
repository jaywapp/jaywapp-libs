// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Interface.ISkiaGeom
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using SkiaSharp;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Interface
{
  public interface ISkiaGeom : ISkiaDrawable
  {
    SKPaint Paint { get; }

    Color Color { get; set; }

    ISkiaGeom Copy();
  }
}
