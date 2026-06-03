// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Helper.ColorConverter
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using SkiaSharp;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Helper
{
  public static class ColorConverter
  {
    public static SKColor Convert(this Color color) => new SKColor(color.R, color.G, color.B, color.A);

    public static Color Convert(this SKColor color) => new Color()
    {
      R = color.Red,
      G = color.Green,
      B = color.Blue,
      A = color.Alpha
    };
  }
}
