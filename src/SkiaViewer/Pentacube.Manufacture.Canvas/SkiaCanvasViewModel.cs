// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.SkiaCanvasViewModel
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Graphics.Geometry.GeometryOperator;
using Pentacube.Manufacture.Canvas.Event;
using Pentacube.Manufacture.Canvas.Service;
using Prism.Commands;
using ReactiveUI;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Reactive;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Pentacube.Manufacture.Canvas
{
  public class SkiaCanvasViewModel : ReactiveObject
  {
    public EventHandler CanvasMousePositionChanged;
    private Point? _mousePosition;
    private Point? _mouseDownPoint;
    public EventHandler CanvasClicked;

    public ReactiveCommand<EventArgs, Unit> PaintCommand { get; }

    public Color BackgroundColor { get; set; } = Colors.Black;

    public SKDrawer Drawer { get; set; }

    public SkiaCanvasViewModel()
    {
      this.Drawer = new SKDrawer();
      this.PaintCommand = ReactiveCommand.Create<EventArgs>(new Action<EventArgs>(this.Paint));
      this.InitializeMouseEvent();
      this.WhenAnyValue<SkiaCanvasViewModel, Point?>((System.Linq.Expressions.Expression<Func<SkiaCanvasViewModel, Point?>>) (x => x.MousePosition)).Subscribe<Point?>((Action<Point?>) (p =>
      {
        EventHandler mousePositionChanged = this.CanvasMousePositionChanged;
        if (mousePositionChanged == null)
          return;
        mousePositionChanged((object) this, (EventArgs) new CanvasMousePositionChangedEventArgs(p));
      }));
      this.WhenAnyValue<SkiaCanvasViewModel, Color>((System.Linq.Expressions.Expression<Func<SkiaCanvasViewModel, Color>>) (x => x.BackgroundColor)).Subscribe<Color>((Action<Color>) (c => this.Fetch()));
    }

    private void Paint(EventArgs e)
    {
      if (!(e is SKPaintSurfaceEventArgs surfaceEventArgs))
        return;
      SKCanvas canvas = surfaceEventArgs.Surface.Canvas;
      canvas.Clear(this.BackgroundColor.Convert());
      this.Drawer.Draw(canvas);
    }

    public void Fetch()
    {
    }

    public Point? MousePosition
    {
      get => this._mousePosition;
      set => this.RaiseAndSetIfChanged<SkiaCanvasViewModel, Point?>(ref this._mousePosition, value, nameof (MousePosition));
    }

    public DelegateCommand<MouseEventArgs> MouseMoveCommand { get; private set; }

    public DelegateCommand<MouseEventArgs> MouseLeftDownCommand { get; private set; }

    public DelegateCommand<MouseEventArgs> MouseLeftUpCommand { get; private set; }

    private void InitializeMouseEvent()
    {
      this.MouseMoveCommand = new DelegateCommand<MouseEventArgs>(new Action<MouseEventArgs>(this.OnMouseMove));
      this.MouseLeftUpCommand = new DelegateCommand<MouseEventArgs>(new Action<MouseEventArgs>(this.OnMouseLeftUp));
      this.MouseLeftDownCommand = new DelegateCommand<MouseEventArgs>(new Action<MouseEventArgs>(this.OnMouseLeftDown));
    }

    private void OnMouseLeftUp(MouseEventArgs args)
    {
      Point point = SkiaCanvasViewModel.GetPoint(args);
      if (args.LeftButton != MouseButtonState.Released || !this._mouseDownPoint.HasValue || !GeomOp.NearlyEqual(this._mouseDownPoint.Value, point))
        return;
      this.LeftClick(point);
    }

    private void LeftClick(Point pt) => this.Drawer.Click(this.Drawer.PanZoom.ConvertToObject(pt));

    private void OnMouseLeftDown(MouseEventArgs args)
    {
      Point point = SkiaCanvasViewModel.GetPoint(args);
      if (args.LeftButton != MouseButtonState.Pressed)
        return;
      this._mouseDownPoint = new Point?(point);
    }

    private void OnMouseMove(MouseEventArgs args) => this.MousePosition = new Point?(this.Drawer.PanZoom.ConvertToObject(SkiaCanvasViewModel.GetPoint(args)));

    private static Point GetPoint(MouseEventArgs args) => args.GetPosition(args.Source as IInputElement);
  }
}
