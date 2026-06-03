// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Service.PanAndZoomEventWatcher
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Manufacture.Canvas.Helper;
using Pentacube.Manufacture.Canvas.Interface;
using System;
using System.Windows;
using System.Windows.Input;

namespace Pentacube.Manufacture.Canvas.Service
{
  public class PanAndZoomEventWatcher
  {
    public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof (PanAndZoomEventWatcher), typeof (PanAndZoomEventWatcher), new PropertyMetadata((object) null, new System.Windows.PropertyChangedCallback(PanAndZoomEventWatcher.PropertyChangedCallback)));
    private bool _dragStart;
    private Point? _prevPosition;

    private static void PropertyChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is FrameworkElement e1))
        return;
      if (e.OldValue is PanAndZoomEventWatcher oldValue)
        oldValue.DetachEvents(e1);
      if (!(e.NewValue is PanAndZoomEventWatcher newValue))
        return;
      newValue.AttachEvents(e1);
    }

    public static PanAndZoomEventWatcher Attach(DependencyObject obj) => (PanAndZoomEventWatcher) obj.GetValue(PanAndZoomEventWatcher.AttachProperty);

    public static void SetAttach(DependencyObject obj, PanAndZoomEventWatcher watcher) => obj.SetValue(PanAndZoomEventWatcher.AttachProperty, (object) watcher);

    public Point? WpfMousePositionOnCanvas { get; private set; }

    public IPanAndZoomTarget PanZoom { get; }

    public EventHandler CanvasLoaded { get; set; }

    public EventHandler DrawOnBackground { get; set; }

    public PanAndZoomEventWatcher(IPanAndZoomTarget target) => this.PanZoom = target ?? throw new ArgumentNullException(nameof (target));

    public void AttachEvents(FrameworkElement e)
    {
      if (e == null)
        return;
      e.Loaded += new RoutedEventHandler(this.OnLoaded);
      e.SizeChanged += new SizeChangedEventHandler(this.OnSizeChange);
      e.MouseDown += new MouseButtonEventHandler(this.OnMouseDown);
      e.MouseUp += new MouseButtonEventHandler(this.OnMouseUp);
      e.MouseMove += new MouseEventHandler(this.OnMouseMove);
      e.MouseWheel += new MouseWheelEventHandler(this.OnMouseWheelUpDown);
      e.MouseLeave += new MouseEventHandler(this.OnMouseLeave);
    }

    public void DetachEvents(FrameworkElement e)
    {
      if (e == null)
        return;
      e.Loaded -= new RoutedEventHandler(this.OnLoaded);
      e.SizeChanged -= new SizeChangedEventHandler(this.OnSizeChange);
      e.MouseDown -= new MouseButtonEventHandler(this.OnMouseDown);
      e.MouseUp -= new MouseButtonEventHandler(this.OnMouseUp);
      e.MouseMove -= new MouseEventHandler(this.OnMouseMove);
      e.MouseWheel -= new MouseWheelEventHandler(this.OnMouseWheelUpDown);
      e.MouseLeave -= new MouseEventHandler(this.OnMouseLeave);
    }

    public void OnLoaded(object sender, RoutedEventArgs e)
    {
      if (!(sender is FrameworkElement canvas))
        throw new InvalidOperationException("FrameworkElement가 아닙니다");
      this.OnLoaded(canvas);
      EventHandler canvasLoaded = this.CanvasLoaded;
      if (canvasLoaded == null)
        return;
      canvasLoaded((object) this, (EventArgs) null);
    }

    private void OnLoaded(FrameworkElement canvas)
    {
      if (canvas == null || !canvas.IsInitialized)
        return;
      this.UpdateCanvasSize(new Size(canvas.ActualWidth, canvas.ActualHeight));
    }

    public void OnSizeChange(object sender, SizeChangedEventArgs e) => this.OnSizeChange(e);

    private void OnSizeChange(SizeChangedEventArgs args) => this.UpdateCanvasSize(args.NewSize);

    private void ZoomChange(double delta, Point datumPoint)
    {
      if (delta == 0.0)
        return;
      this.PanZoom.ZoomBy<IPanAndZoomTarget>(PanAndZoomEventWatcher.CalcZoomScale(PanAndZoomEventWatcher.CalcZoomStrength(delta)), datumPoint);
    }

    private static double CalcZoomStrength(double delta, double div = 120.0) => (delta >= 0.0 ? 1.0 : -1.0) * PanZoomHelper.GetInRangeValue(Math.Abs(delta / div), 0.5, 1.5);

    public static double CalcZoomScale(double strength, double stepDelta = 0.1) => 1.0 + stepDelta * strength;

    public void OnMouseWheelUpDown(object sender, MouseWheelEventArgs e) => this.OnMouseWheelUpDown(e);

    public void OnMouseDown(object sender, MouseButtonEventArgs e) => this.OnMouseDown(e);

    public void OnMouseUp(object sender, MouseButtonEventArgs e) => this.OnMouseUp(e);

    public void OnMouseMove(object sender, MouseEventArgs e) => this.OnMouseMove(e);

    private void OnMouseWheelUpDown(MouseWheelEventArgs args)
    {
      Point datumPoint = this.WpfMousePositionOnCanvas ?? args.GetPosition(args.Source as IInputElement);
      this.ZoomChange((double) args.Delta, datumPoint);
    }

    private void OnMouseDown(MouseButtonEventArgs args)
    {
      Point position = args.GetPosition(args.Source as IInputElement);
      if (args.LeftButton != MouseButtonState.Pressed)
        return;
      this._dragStart = true;
      this._prevPosition = new Point?(position);
    }

    private void OnMouseUp(MouseButtonEventArgs args)
    {
      if (args.LeftButton != MouseButtonState.Released)
        return;
      if (this._dragStart && this._prevPosition.HasValue)
      {
        this.PanZoom.Pan(this._prevPosition.Value - this.WpfMousePositionOnCanvas.Value);
        this._prevPosition = this.WpfMousePositionOnCanvas;
      }
      this._dragStart = false;
      this._prevPosition = new Point?();
    }

    private void OnMouseMove(MouseEventArgs args)
    {
      this.WpfMousePositionOnCanvas = new Point?(args.GetPosition(args.Source as IInputElement));
      if (args.LeftButton != MouseButtonState.Pressed || !this._dragStart || !this._prevPosition.HasValue)
        return;
      this.PanZoom.Pan(this._prevPosition.Value - this.WpfMousePositionOnCanvas.Value);
      this._prevPosition = this.WpfMousePositionOnCanvas;
    }

    public void OnMouseLeave(object sender, MouseEventArgs e) => this.WpfMousePositionOnCanvas = new Point?();

    private void UpdateCanvasSize(Size canvasSize)
    {
      if ((canvasSize.Width <= 0.0 ? 0 : (canvasSize.Height > 0.0 ? 1 : 0)) == 0)
        return;
      this.PanZoom.CanvasSize = canvasSize;
    }
  }
}
