// Decompiled with JetBrains decompiler
// Type: SkiaViewer.View.LayersViewModel
// Assembly: SkiaViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A7BB30B5-7E8A-4BEA-B383-61FA18F68C44
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\SkiaViewer.exe

using Pentacube.Manufacture.Canvas.Geometry.Model.Layer;
using Pentacube.Manufacture.Canvas.Service;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SkiaViewer.View
{
  public class LayersViewModel : ReactiveObject
  {
    private ObservableCollection<SkiaViewer.Model.Layer> _layers = new ObservableCollection<SkiaViewer.Model.Layer>();

    public SKDrawer Drawer { get; }

    public ObservableCollection<SkiaViewer.Model.Layer> Layers
    {
      get => this._layers;
      set => this.RaiseAndSetIfChanged<LayersViewModel, ObservableCollection<SkiaViewer.Model.Layer>>(ref this._layers, value, nameof (Layers));
    }

    public LayersViewModel(SKDrawer drawer)
    {
      this.Drawer = drawer;
      this.Layers = new ObservableCollection<SkiaViewer.Model.Layer>(drawer.Layers.Select<SkiaLayer, SkiaViewer.Model.Layer>((Func<SkiaLayer, SkiaViewer.Model.Layer>) (l => new SkiaViewer.Model.Layer(l))));
      foreach (SkiaViewer.Model.Layer layer in (Collection<SkiaViewer.Model.Layer>) this.Layers)
        layer.ContentChanged += new EventHandler(this.OnContentChanged);
    }

    public void Fetch()
    {
      foreach (SkiaViewer.Model.Layer layer in (Collection<SkiaViewer.Model.Layer>) this.Layers)
        layer.ContentChanged -= new EventHandler(this.OnContentChanged);
      this.Layers = new ObservableCollection<SkiaViewer.Model.Layer>(this.Drawer.Layers.Select<SkiaLayer, SkiaViewer.Model.Layer>((Func<SkiaLayer, SkiaViewer.Model.Layer>) (l => new SkiaViewer.Model.Layer(l))));
      foreach (SkiaViewer.Model.Layer layer in (Collection<SkiaViewer.Model.Layer>) this.Layers)
        layer.ContentChanged += new EventHandler(this.OnContentChanged);
    }

    private void OnContentChanged(object sender, EventArgs e) => this.Drawer.Update();
  }
}
