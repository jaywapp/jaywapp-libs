// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Helper.PanZoomHelper
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Manufacture.Canvas.Interface;
using System;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Helper
{
  public static class PanZoomHelper
  {
    public static double GetInRangeValue(double value, double min, double max)
    {
      if (value > max)
        return max;
      return value < min ? min : value;
    }

    public static void ZoomEmpty<T>(this T canvas) where T : IPanZoomCanvas, I2DCanvasCoordinates => canvas.Zoom(1.0000001);

    public static void ZoomBy<T>(this T canvas, double scale, Point datumPoint) where T : IPanZoomCanvas, I2DCanvasCoordinates
    {
      if (scale == 1.0)
        return;
      Point fromObject = canvas.ConvertToObject(datumPoint);
      canvas.Zoom(scale);
      Vector vec = canvas.ConvertToCanvas(fromObject) - datumPoint;
      canvas.Pan(vec);
    }

    public static void ZoomArea(
      this IPanAndZoomTarget model,
      Rect cadBoundBox,
      Func<Size, Thickness> cadSizeToCadMargin)
    {
      model.ZoomArea(cadBoundBox, cadSizeToCadMargin(cadBoundBox.Size));
    }

    public static void ZoomArea(
      this IPanAndZoomTarget model,
      Rect cadBoundBox,
      Thickness cadMargin)
    {
      Rect cadBoundBox1 = new Rect(cadBoundBox.Location - new Vector(cadMargin.Left, cadMargin.Top), cadBoundBox.BottomRight + new Vector(cadMargin.Right, cadMargin.Bottom));
      model.ZoomArea(cadBoundBox1);
    }

    public static void ZoomArea(this IPanAndZoomTarget panZoom, Rect cadBoundBox)
    {
      if (panZoom.CanvasSize == Size.Empty || cadBoundBox == Rect.Empty)
        return;
      double scale = PanZoomHelper.CalcScaleInFilled(cadBoundBox.Size, panZoom.CanvasSize) / panZoom.Zoomed;
      panZoom.Zoom(scale);
      Point fromObject = new Point((cadBoundBox.Left + cadBoundBox.Right) / 2.0, (cadBoundBox.Top + cadBoundBox.Bottom) / 2.0);
      Point canvas = panZoom.ConvertToCanvas(fromObject);
      Point point = new Point(panZoom.CanvasSize.Width / 2.0, panZoom.CanvasSize.Height / 2.0);
      panZoom.Pan(canvas - point);
    }

    public static double CalcScaleInFilled(Size target, Size bound)
    {
      double num1 = target.Width != 0.0 ? bound.Width / target.Width : 0.0;
      double num2 = target.Height != 0.0 ? bound.Height / target.Height : 0.0;
      return num1 <= num2 ? num1 : num2;
    }

    public static void Move(this IPanAndZoomTarget panZoom, Point pt)
    {
      Point canvas = panZoom.ConvertToCanvas(pt);
      Point point;
      ref Point local = ref point;
      Size canvasSize = panZoom.CanvasSize;
      double x = canvasSize.Width / 2.0;
      canvasSize = panZoom.CanvasSize;
      double y = canvasSize.Height / 2.0;
      local = new Point(x, y);
      panZoom.Pan(canvas - point);
    }
  }
}
