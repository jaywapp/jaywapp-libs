// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base.SkiaGeom
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using Pentacube.Manufacture.Canvas.Geometry.Event;
using Pentacube.Manufacture.Canvas.Geometry.Interface;
using SkiaSharp;
using System;
using System.Windows;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base
{
  public abstract class SkiaGeom : ISkiaGeom, ISkiaDrawable
  {
    public EventHandler ContentChanged;
    public EventHandler RequestUpdate;
    private SKPath _cache;
    private Color _color;
    private Color _selectedColor = Colors.Blue;
    private bool _isSelected;

    public SKPaint Paint { get; } = new SKPaint();

    public Color Color
    {
      get => this._color;
      set
      {
        this._color = value;
        EventHandler contentChanged = this.ContentChanged;
        if (contentChanged == null)
          return;
        contentChanged((object) this, EventArgs.Empty);
      }
    }

    public Color SelectedColor
    {
      get => this._selectedColor;
      set
      {
        this._selectedColor = value;
        EventHandler contentChanged = this.ContentChanged;
        if (contentChanged == null)
          return;
        contentChanged((object) this, EventArgs.Empty);
      }
    }

    public bool IsSelected
    {
      get => this._isSelected;
      set
      {
        this._isSelected = value;
        EventHandler contentChanged = this.ContentChanged;
        if (contentChanged == null)
          return;
        contentChanged((object) this, EventArgs.Empty);
      }
    }

    public bool IsFilled
    {
      get => this.Paint.Style == SKPaintStyle.Fill;
      set
      {
        this.Paint.Style = value ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
        EventHandler contentChanged = this.ContentChanged;
        if (contentChanged == null)
          return;
        contentChanged((object) this, EventArgs.Empty);
      }
    }

    public SKRect Bound => this.Draw().TightBounds;

    public SkiaGeom() => this.ContentChanged += new EventHandler(this.InitializeCache);

    public SkiaGeom(Color color, bool isFilled = true)
      : this()
    {
      this.Color = color;
      this.IsFilled = isFilled;
    }

    public SKPath Draw()
    {
      this.Paint.Color = this.IsSelected ? this.SelectedColor.Convert() : this.Color.Convert();
      if (this._cache == null)
        this._cache = this.CreatePath();
      return this._cache;
    }

    public abstract SKPath CreatePath();

    public abstract ISkiaGeom Copy();

    public abstract bool Contains(Point pt);

    private void InitializeCache(object sender, EventArgs e)
    {
      this._cache = (SKPath) null;
      EventHandler requestUpdate = this.RequestUpdate;
      if (requestUpdate == null)
        return;
      requestUpdate((object) this, EventArgs.Empty);
    }

    internal void OnColorChanged(object sender, EventArgs e)
    {
      if (!(e is ColorChangedEventArgs changedEventArgs))
        return;
      this.Color = changedEventArgs.Color;
      EventHandler contentChanged = this.ContentChanged;
      if (contentChanged == null)
        return;
      contentChanged((object) this, EventArgs.Empty);
    }

    internal void Flip(object sender, EventArgs e)
    {
      switch (e)
      {
        case FlipOriginEventArgs flipOriginEventArgs:
          this.Flip(flipOriginEventArgs.Direction, new Point?(flipOriginEventArgs.Origin));
          break;
        case FlipEventArgs flipEventArgs:
          this.Flip(flipEventArgs.Direction);
          break;
      }
    }

    public void Flip(eFlipDirection direction, Point? origin = null)
    {
      Point origin1 = origin ?? new Point(0.0, 0.0);
      this.FlipContent(direction, origin1);
      EventHandler contentChanged = this.ContentChanged;
      if (contentChanged == null)
        return;
      contentChanged((object) this, EventArgs.Empty);
    }

    protected abstract void FlipContent(eFlipDirection direction, Point origin);

    internal void Offset(object sender, EventArgs e)
    {
      if (!(e is OffsetEventArgs offsetEventArgs))
        return;
      this.Offset(offsetEventArgs.OffsetX, offsetEventArgs.OffsetY);
    }

    public void Offset(float offsetX, float offsetY)
    {
      this.OffsetContent(offsetX, offsetY);
      EventHandler contentChanged = this.ContentChanged;
      if (contentChanged == null)
        return;
      contentChanged((object) this, EventArgs.Empty);
    }

    protected abstract void OffsetContent(float offsetX, float offsetY);

    internal void Rotate(object sender, EventArgs e)
    {
      switch (e)
      {
        case RotateOriginEventArgs rotateOriginEventArgs:
          this.Rotate(rotateOriginEventArgs.Angle, new Point?(rotateOriginEventArgs.Origin));
          break;
        case RotateEventArgs rotateEventArgs:
          this.Rotate(rotateEventArgs.Angle);
          break;
      }
    }

    public void Rotate(float angle, Point? origin = null)
    {
      Point origin1 = origin ?? new Point(0.0, 0.0);
      this.RotateContent(angle, origin1);
      EventHandler contentChanged = this.ContentChanged;
      if (contentChanged == null)
        return;
      contentChanged((object) this, EventArgs.Empty);
    }

    protected abstract void RotateContent(float angle, Point origin);
  }
}
