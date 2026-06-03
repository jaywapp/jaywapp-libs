// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Helper.RectangleHelper
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Graphics.Geometry;
using Pentacube.Infrastructure.Helper;
using SkiaSharp;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Helper
{
  public static class RectangleHelper
  {
    public static PRect ConvertToPRect(this SKRect rect) => new PRect(new RangeDouble((double) rect.Left, (double) rect.Right), new RangeDouble((double) rect.Top, (double) rect.Bottom));

    public static Rect ConvertToRect(this SKRect r) => r.IsEmpty ? Rect.Empty : new Rect(new Point((double) r.Left, (double) r.Bottom), new Point((double) r.Right, (double) r.Top));

    public static Rect Scale(this Rect rect, double scale)
    {
      double left = rect.Left;
      double right = rect.Right;
      double top = rect.Top;
      double bottom = rect.Bottom;
      double width = rect.Width;
      double height = rect.Height;
      double num1 = (width * scale - width) / 2.0;
      double num2 = (height * scale - height) / 2.0;
      return new Rect(new Point(left - num1, bottom + num2), new Point(right + num1, top - num2));
    }
  }
}
