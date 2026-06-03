// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Layer.SkiaLayer
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Manufacture.Canvas.Geometry.Event;
using Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Layer
{
  public class SkiaLayer
  {
    private Color _color;
    private List<SkiaGeom> _geoms = new List<SkiaGeom>();
    public EventHandler RequestUpdate;
    public EventHandler ColorChanged;
    public EventHandler OffsetEvent;
    public EventHandler RotateEvent;
    public EventHandler FlipEvent;

    public int No { get; }

    public string Name { get; }

    public bool IsVisible { get; set; }

    public Color Color
    {
      get => this._color;
      set
      {
        this._color = value;
        EventHandler colorChanged = this.ColorChanged;
        if (colorChanged == null)
          return;
        colorChanged((object) this, (EventArgs) new ColorChangedEventArgs(value));
      }
    }

    public IReadOnlyList<SkiaGeom> Geoms => (IReadOnlyList<SkiaGeom>) this._geoms;

    public SKRect Bound
    {
      get
      {
        SKRect skRect = new SKRect();
        foreach (SkiaGeom geom in (IEnumerable<SkiaGeom>) this.Geoms)
          skRect.Union(geom.Bound);
        return skRect;
      }
    }

    public SkiaLayer(int no, string name, IEnumerable<SkiaGeom> geoms = null)
    {
      this.No = no;
      this.Name = name;
      if (geoms == null)
        return;
      this.Add(geoms);
    }

    public void Draw(SKCanvas canvas)
    {
      if (!this.IsVisible)
        return;
      SKRect localClipBounds = canvas.LocalClipBounds;
      foreach (SkiaGeom geom in (IEnumerable<SkiaGeom>) this.Geoms)
      {
        SKPath path = geom.Draw();
        SKRect bounds = path.Bounds;
        if (localClipBounds.IntersectsWith(bounds))
          canvas.DrawPath(path, geom.Paint);
      }
    }

    private void SetEvent(SkiaGeom geom)
    {
      this.ColorChanged += new EventHandler(geom.OnColorChanged);
      this.RotateEvent += new EventHandler(geom.Rotate);
      this.OffsetEvent += new EventHandler(geom.Offset);
      this.FlipEvent += new EventHandler(geom.Flip);
      geom.RequestUpdate += new EventHandler(this.OnRequestUpdate);
    }

    private void ClearEvent(SkiaGeom geom)
    {
      this.ColorChanged -= new EventHandler(geom.OnColorChanged);
      this.RotateEvent -= new EventHandler(geom.Rotate);
      this.OffsetEvent -= new EventHandler(geom.Offset);
      this.FlipEvent -= new EventHandler(geom.Flip);
      geom.RequestUpdate -= new EventHandler(this.OnRequestUpdate);
    }

    private void OnRequestUpdate(object sender, EventArgs e)
    {
      EventHandler requestUpdate = this.RequestUpdate;
      if (requestUpdate == null)
        return;
      requestUpdate((object) this, EventArgs.Empty);
    }

    public void Clear()
    {
      foreach (SkiaGeom geom in (IEnumerable<SkiaGeom>) this.Geoms)
        this.ClearEvent(geom);
      this._geoms.Clear();
    }

    public void Add(SkiaGeom geom)
    {
      this._geoms.Add(geom);
      this.SetEvent(geom);
    }

    public void Add(IEnumerable<SkiaGeom> geoms)
    {
      this._geoms.AddRange(geoms);
      foreach (SkiaGeom geom in geoms.ToList<SkiaGeom>())
        this.SetEvent(geom);
    }

    public void Remove(SkiaGeom geom)
    {
      this._geoms.Remove(geom);
      this.ClearEvent(geom);
    }

    public void Remove(IEnumerable<SkiaGeom> geoms)
    {
      foreach (SkiaGeom geom in geoms.ToList<SkiaGeom>())
      {
        this._geoms.Remove(geom);
        this.ClearEvent(geom);
      }
    }

    public void Offset(double offsetX, double offsetY)
    {
      EventHandler offsetEvent = this.OffsetEvent;
      if (offsetEvent == null)
        return;
      offsetEvent((object) this, (EventArgs) new OffsetEventArgs(offsetX, offsetY));
    }

    public void Rotate(double angle)
    {
      EventHandler rotateEvent = this.RotateEvent;
      if (rotateEvent == null)
        return;
      rotateEvent((object) this, (EventArgs) new RotateEventArgs(angle));
    }

    public void Rotate(double angle, Point origin)
    {
      EventHandler rotateEvent = this.RotateEvent;
      if (rotateEvent == null)
        return;
      rotateEvent((object) this, (EventArgs) new RotateOriginEventArgs(angle, origin));
    }
  }
}
