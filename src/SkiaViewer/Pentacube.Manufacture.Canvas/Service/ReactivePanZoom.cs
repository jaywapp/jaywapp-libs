// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Service.ReactivePanZoom
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Manufacture.Canvas.Geometry.Interface;
using Pentacube.Manufacture.Canvas.Helper;
using Pentacube.Manufacture.Canvas.Interface;
using ReactiveUI;
using System;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Service
{
  public class ReactivePanZoom : 
    ReactiveObject,
    IPanAndZoomTarget,
    IPanZoomCanvas,
    IPanZoom,
    I2DCanvasCoordinates
  {
    private Vector _moved;
    private double _zoomed = 1.0;
    private Size _canvasSize = Size.Empty;

    public Size CanvasSize
    {
      get => this._canvasSize;
      set => this.RaiseAndSetIfChanged<ReactivePanZoom, Size>(ref this._canvasSize, value, nameof (CanvasSize));
    }

    public Vector Moved
    {
      get => this._moved;
      private set => this.RaiseAndSetIfChanged<ReactivePanZoom, Vector>(ref this._moved, value, nameof (Moved));
    }

    public double Zoomed
    {
      get => this._zoomed;
      private set => this.RaiseAndSetIfChanged<ReactivePanZoom, double>(ref this._zoomed, value, nameof (Zoomed));
    }

    public bool IsPanning { get; set; } = true;

    public PanAndZoomEventWatcher EventWatcher { get; }

    public ReactivePanZoom() => this.EventWatcher = new PanAndZoomEventWatcher((IPanAndZoomTarget) this);

    public void Pan(Vector vec) => this.Moved -= vec;

    public void Zoom(double scale)
    {
      if (scale == 1.0)
        return;
      this.Zoomed = scale > 0.0 && !double.IsNaN(scale) && !double.IsInfinity(scale) ? PanZoomHelper.GetInRangeValue(scale * this.Zoomed, 1E-07, double.MaxValue) : throw new ArgumentException("Invalid zoom scale", nameof (scale));
    }

    public Point ConvertToObject(Point fromCanvas) => new Point((fromCanvas.X - this.Moved.X) / this.Zoomed, (fromCanvas.Y - this.Moved.Y) / -this.Zoomed);

    public Point ConvertToCanvas(Point fromObject) => new Point(this.Zoomed * fromObject.X + this.Moved.X, -this.Zoomed * fromObject.Y + this.Moved.Y);
  }
}
