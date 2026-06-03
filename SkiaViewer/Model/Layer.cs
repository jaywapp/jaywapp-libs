// Decompiled with JetBrains decompiler
// Type: SkiaViewer.Model.Layer
// Assembly: SkiaViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A7BB30B5-7E8A-4BEA-B383-61FA18F68C44
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\SkiaViewer.exe

using Pentacube.Manufacture.Canvas.Geometry.Model.Layer;
using ReactiveUI;
using System;
using System.Linq.Expressions;
using System.Windows.Media;

namespace SkiaViewer.Model
{
  public class Layer : ReactiveObject
  {
    private string _name;
    private bool _isChecked;
    private Color _color;
    public EventHandler ContentChanged;

    public string Name
    {
      get => this._name;
      set => this.RaiseAndSetIfChanged<SkiaViewer.Model.Layer, string>(ref this._name, value, nameof (Name));
    }

    public bool IsChecked
    {
      get => this._isChecked;
      set => this.RaiseAndSetIfChanged<SkiaViewer.Model.Layer, bool>(ref this._isChecked, value, nameof (IsChecked));
    }

    public Color Color
    {
      get => this._color;
      set => this.RaiseAndSetIfChanged<SkiaViewer.Model.Layer, Color>(ref this._color, value, nameof (Color));
    }

    public SkiaLayer SkiaLayer { get; }

    public Layer(SkiaLayer skiaLayer)
    {
      this.Name = skiaLayer.Name;
      this.SkiaLayer = skiaLayer;
      this.IsChecked = skiaLayer.IsVisible;
      this.Color = skiaLayer.Color;
      this.WhenAnyValue<SkiaViewer.Model.Layer, bool>((Expression<Func<SkiaViewer.Model.Layer, bool>>) (x => x.IsChecked)).Subscribe<bool>((Action<bool>) (isChecked =>
      {
        this.SkiaLayer.IsVisible = isChecked;
        EventHandler contentChanged = this.ContentChanged;
        if (contentChanged == null)
          return;
        contentChanged((object) this, EventArgs.Empty);
      }));
      this.WhenAnyValue<SkiaViewer.Model.Layer, Color>((Expression<Func<SkiaViewer.Model.Layer, Color>>) (x => x.Color)).Subscribe<Color>((Action<Color>) (color =>
      {
        this.SkiaLayer.Color = color;
        EventHandler contentChanged = this.ContentChanged;
        if (contentChanged == null)
          return;
        contentChanged((object) this, EventArgs.Empty);
      }));
    }
  }
}
