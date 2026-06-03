// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Control.GraphicsCanvas
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Manufacture.Canvas.Event;
using Pentacube.Manufacture.Canvas.Interface;
using Pentacube.Manufacture.Canvas.Service;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Pentacube.Manufacture.Canvas.Control
{
  [ContentProperty("Canvas")]
  public class GraphicsCanvas : UserControl, IDisposable
  {
    public const int DefaultFrameLimit = 30;
    private readonly ContentControl _canvasWrapper = new ContentControl();
    public static readonly DependencyProperty FrameLimitProperty = DependencyProperty.Register(nameof (FrameLimit), typeof (int), typeof (GraphicsCanvas), (PropertyMetadata) new FrameworkPropertyMetadata((object) 30, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(GraphicsCanvas.FrameLimitChangedCallback)));
    public static readonly DependencyProperty CanvasProperty = DependencyProperty.Register(nameof (Canvas), typeof (object), typeof (GraphicsCanvas), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.NotDataBindable, new PropertyChangedCallback(GraphicsCanvas.CanvasChangedCallback)));
    public static readonly DependencyProperty RenderSourceProperty = DependencyProperty.Register(nameof (RenderSource), typeof (IRenderData), typeof (GraphicsCanvas), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(GraphicsCanvas.RenderTargetChangedCallback)));
    public static readonly DependencyProperty PaintProperty = DependencyProperty.Register(nameof (Paint), typeof (ICommand), typeof (GraphicsCanvas));
    public static readonly DependencyProperty PaintEventNameProperty = DependencyProperty.RegisterAttached("PaintEventName", typeof (string), typeof (GraphicsCanvas), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.NotDataBindable, new PropertyChangedCallback(GraphicsCanvas.PaintEventNameChangedCallback)));
    private Thread _renderThread;
    private bool _keepRenderLoop = true;
    private readonly AutoResetEvent _renderTrigger = new AutoResetEvent(false);
    private readonly RenderingTimer _timer = new RenderingTimer()
    {
      FrameLimit = 30
    };

    public event EventHandler<RenderingEventArgs> DrawOnBackground;

    public ICommand Paint
    {
      get => (ICommand) this.GetValue(GraphicsCanvas.PaintProperty);
      set => this.SetValue(GraphicsCanvas.PaintProperty, (object) value);
    }

    public int FrameLimit
    {
      get => (int) this.GetValue(GraphicsCanvas.FrameLimitProperty);
      set => this.SetValue(GraphicsCanvas.FrameLimitProperty, (object) value);
    }

    public object Canvas
    {
      get => this.GetValue(GraphicsCanvas.CanvasProperty);
      set => this.SetValue(GraphicsCanvas.CanvasProperty, value);
    }

    public IRenderData RenderSource
    {
      get => (IRenderData) this.GetValue(GraphicsCanvas.RenderSourceProperty);
      set => this.SetValue(GraphicsCanvas.RenderSourceProperty, (object) value);
    }

    public GraphicsCanvas()
    {
      this.Content = (object) this._canvasWrapper;
      this._canvasWrapper.Loaded += new RoutedEventHandler(this.CanvasLoaded);
      this._canvasWrapper.Unloaded += new RoutedEventHandler(this.CanvasUnloaded);
    }

    private void CanvasLoaded(object sender, RoutedEventArgs e) => this.StartRenderThread();

    private void CanvasUnloaded(object sender, RoutedEventArgs e) => this.StopRenderThread();

    protected virtual void PaintCanvas()
    {
      if (!(this._canvasWrapper.Content is UIElement content))
        return;
      content.InvalidateVisual();
    }

    private void DoCanvasUpdate(object sender, EventArgs e) => this.UpdateDrawing();

    private void UpdateDrawing() => this._renderTrigger.Set();

    public void Dispose() => this._renderTrigger?.Dispose();

    private static void FrameLimitChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      int newValue = (int) e.NewValue;
      ((GraphicsCanvas) d)._timer.FrameLimit = newValue;
    }

    private static void CanvasChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ((GraphicsCanvas) d)._canvasWrapper.Content = e.NewValue;
    }

    private static void RenderTargetChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      GraphicsCanvas graphicsCanvas = (GraphicsCanvas) d;
      if (e.OldValue is IRenderData oldValue)
      {
        oldValue.UpdateTrigger -= new EventHandler(graphicsCanvas.DoCanvasUpdate);
        graphicsCanvas.DrawOnBackground -= new EventHandler<RenderingEventArgs>(oldValue.Render);
      }
      if (!(e.NewValue is IRenderData newValue))
        return;
      newValue.UpdateTrigger += new EventHandler(graphicsCanvas.DoCanvasUpdate);
      graphicsCanvas.DrawOnBackground += new EventHandler<RenderingEventArgs>(newValue.Render);
    }

    private static void PaintEventNameChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      GraphicsCanvas parentFirstWrapper = GraphicsCanvas.GetParentFirstWrapper(d);
      if (parentFirstWrapper == null || e.NewValue == null)
        return;
      MethodInfo method = typeof (GraphicsCanvas).GetMethod("PaintEventGateway", BindingFlags.Instance | BindingFlags.NonPublic);
      EventInfo eventInfo1 = GraphicsCanvas.GetEvent(d.GetType(), e.OldValue);
      if (eventInfo1 != (EventInfo) null)
      {
        Delegate handler = Delegate.CreateDelegate(eventInfo1.EventHandlerType, (object) parentFirstWrapper, method);
        eventInfo1.RemoveEventHandler((object) d, handler);
      }
      EventInfo eventInfo2 = GraphicsCanvas.GetEvent(d.GetType(), e.NewValue);
      if (!(eventInfo2 != (EventInfo) null))
        return;
      Delegate handler1 = Delegate.CreateDelegate(eventInfo2.EventHandlerType, (object) parentFirstWrapper, method);
      eventInfo2.AddEventHandler((object) d, handler1);
    }

    private static EventInfo GetEvent(Type type, object eventName) => !(eventName is string name) ? (EventInfo) null : type.GetEvent(name);

    private void PaintEventGateway(object sender, EventArgs e)
    {
      ICommand paint = this.Paint;
      if (paint == null || !paint.CanExecute((object) e))
        return;
      paint.Execute((object) e);
    }

    private static GraphicsCanvas GetParentFirstWrapper(DependencyObject d)
    {
      DependencyObject dependencyObject = d;
      do
      {
        dependencyObject = dependencyObject is FrameworkElement frameworkElement2 ? frameworkElement2.Parent : (DependencyObject) null;
        if (dependencyObject is GraphicsCanvas graphicsCanvas2)
          return graphicsCanvas2;
      }
      while (dependencyObject != null);
      return (GraphicsCanvas) null;
    }

    public static string PaintEventName(DependencyObject obj) => (string) obj.GetValue(GraphicsCanvas.PaintEventNameProperty);

    public static void SetPaintEventName(DependencyObject obj, string eventName) => obj.SetValue(GraphicsCanvas.PaintEventNameProperty, (object) eventName);

    private void StartRenderThread()
    {
      this.StopRenderThread();
      this._renderThread?.Join();
      this._renderThread = new Thread(new ThreadStart(this.RenderLoopMethod))
      {
        IsBackground = true
      };
      this._renderThread.Start();
    }

    private void StopRenderThread()
    {
      this._keepRenderLoop = false;
      this._renderTrigger.Set();
    }

    private void RenderLoopMethod()
    {
      this._keepRenderLoop = true;
      while (this._keepRenderLoop)
      {
        this._timer.BeginRender();
        EventHandler<RenderingEventArgs> drawOnBackground = this.DrawOnBackground;
        if (drawOnBackground != null)
          drawOnBackground((object) this, new RenderingEventArgs(this.RenderSize));
        this.Dispatcher?.BeginInvoke((Delegate) new Action(this.PaintCanvas));
        this._timer.EndRender();
        this._renderTrigger.WaitOne();
      }
    }
  }
}
