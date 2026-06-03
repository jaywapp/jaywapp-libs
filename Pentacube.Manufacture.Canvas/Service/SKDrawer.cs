// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Service.SKDrawer
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Manufacture.Canvas.Event;
using Pentacube.Manufacture.Canvas.Geometry.Model.Layer;
using Pentacube.Manufacture.Canvas.Helper;
using Pentacube.Manufacture.Canvas.Interface;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Service
{
  public class SKDrawer : ReactiveObject, IRenderData
  {
    private List<SkiaLayer> _layers = new List<SkiaLayer>();

    public event EventHandler UpdateTrigger;

    public SKRect Bound
    {
      get
      {
        SKRect skRect = new SKRect();
        foreach (SkiaLayer layer in (IEnumerable<SkiaLayer>) this.Layers)
        {
          if (layer.IsVisible)
            skRect.Union(layer.Bound);
        }
        return skRect;
      }
    }

    public IReadOnlyList<SkiaLayer> Layers => (IReadOnlyList<SkiaLayer>) this._layers;

    public SKDrawer() => this.WhenAnyValue<SKDrawer, Size, Vector, double>((System.Linq.Expressions.Expression<Func<SKDrawer, Size>>) (x => x.PanZoom.CanvasSize), (System.Linq.Expressions.Expression<Func<SKDrawer, Vector>>) (x => x.PanZoom.Moved), (System.Linq.Expressions.Expression<Func<SKDrawer, double>>) (x => x.PanZoom.Zoomed)).Subscribe<(Size, Vector, double)>(new Action<(Size, Vector, double)>(this.OnPanZoomChanged));

    public void Draw(SKCanvas canvas)
    {
      SKCanvas skCanvas = canvas;
      Vector moved = this.PanZoom.Moved;
      double x = moved.X;
      moved = this.PanZoom.Moved;
      double y = moved.Y;
      skCanvas.Translate((float) x, (float) y);
      canvas.Scale((float) this.PanZoom.Zoomed, (float) -this.PanZoom.Zoomed);
      foreach (SkiaLayer layer in (IEnumerable<SkiaLayer>) this.Layers)
        layer.Draw(canvas);
    }

    private void OnPanZoomChanged((Size, Vector, double) obj) => this.Update();

    public void AddLayer(SkiaLayer layer)
    {
      layer.RequestUpdate += (EventHandler) ((o, s) => this.Update());
      this._layers.Add(layer);
    }

    public void AddLayers(IEnumerable<SkiaLayer> layers)
    {
      foreach (SkiaLayer layer in layers)
        this.AddLayer(layer);
    }

    public void Click(Point point)
    {
      // ISSUE: unable to decompile the method.
    }

    public void Update()
    {
      EventHandler updateTrigger = this.UpdateTrigger;
      if (updateTrigger == null)
        return;
      updateTrigger((object) this, EventArgs.Empty);
    }

    public ReactivePanZoom PanZoom { get; } = new ReactivePanZoom();

    public void ZoomFit() => this.ZoomArea(this.Bound);

    public void ZoomArea(Rect bound) => this.PanZoom.ZoomArea(bound);

    public void ZoomArea(SKRect bound, double scale = 1.0) => this.ZoomArea(bound.ConvertToRect().Scale(scale));

    public void ZoomEmpty() => this.PanZoom.ZoomEmpty<ReactivePanZoom>();

    public void Move(Point pt) => this.PanZoom.Move(pt);

    public void Zoom(double scaleFactor) => this.PanZoom.Zoom(scaleFactor);

    public void Render(object sender, RenderingEventArgs e)
    {
      SKRect rect;
      ref SKRect local = ref rect;
      Size canvasSize = e.CanvasSize;
      double width = canvasSize.Width;
      canvasSize = e.CanvasSize;
      double height = canvasSize.Height;
      local = new SKRect(0.0f, 0.0f, (float) width, (float) height);
      this.Render(rect);
    }

    public void Render(SKRect rect)
    {
    }
  }
}
