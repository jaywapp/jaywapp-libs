// Decompiled with JetBrains decompiler
// Type: SkiaSharp.Views.WPF.SKCustomControl
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using OpenTK;
using OpenTK.Graphics;
using SkiaSharp.Views.Desktop;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SkiaSharp.Views.WPF
{
  [DefaultEvent("PaintSurface")]
  [DefaultProperty("Name")]
  public class SKCustomControl : FrameworkElement
  {
    private readonly bool _designMode;
    private WriteableBitmap _bitmap;
    private bool _ignorePixelScaling;
    private GRContext _grContext;

    public SKCustomControl()
    {
      this._designMode = DesignerProperties.GetIsInDesignMode((DependencyObject) this);
      new GLControl(new GraphicsMode((ColorFormat) 32, 24, 8, 4)).MakeCurrent();
    }

    [Bindable(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public SKSize CanvasSize => this._bitmap != null ? new SKSize((float) this._bitmap.PixelWidth, (float) this._bitmap.PixelHeight) : SKSize.Empty;

    public bool IgnorePixelScaling
    {
      get => this._ignorePixelScaling;
      set
      {
        this._ignorePixelScaling = value;
        this.InvalidateVisual();
      }
    }

    [Category("Appearance")]
    public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface;

    protected override void OnRender(DrawingContext drawingContext)
    {
      base.OnRender(drawingContext);
      if (this._designMode || this.ActualWidth == 0.0 || this.ActualHeight == 0.0 || double.IsNaN(this.ActualWidth) || double.IsNaN(this.ActualHeight) || double.IsInfinity(this.ActualWidth) || double.IsInfinity(this.ActualHeight) || this.Visibility != Visibility.Visible)
        return;
      double num1 = 1.0;
      double num2 = 1.0;
      int num3;
      int num4;
      if (this.IgnorePixelScaling)
      {
        num3 = (int) this.ActualWidth;
        num4 = (int) this.ActualHeight;
      }
      else
      {
        Matrix transformToDevice = PresentationSource.FromVisual((Visual) this).CompositionTarget.TransformToDevice;
        num1 = transformToDevice.M11;
        num2 = transformToDevice.M22;
        num3 = (int) (this.ActualWidth * num1);
        num4 = (int) (this.ActualHeight * num2);
      }
      SKImageInfo info = new SKImageInfo(num3, num4, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
      if (this._bitmap == null || info.Width != this._bitmap.PixelWidth || info.Height != this._bitmap.PixelHeight)
        this._bitmap = new WriteableBitmap(num3, num4, 96.0 * num1, 96.0 * num2, PixelFormats.Pbgra32, (BitmapPalette) null);
      this._bitmap.Lock();
      using (SKSurface surface = this.CreateSurface(info, this._bitmap))
        this.OnPaintSurface(new SKPaintSurfaceEventArgs(surface, info));
      this._bitmap.AddDirtyRect(new Int32Rect(0, 0, num3, num4));
      this._bitmap.Unlock();
      drawingContext.DrawImage((ImageSource) this._bitmap, new Rect(0.0, 0.0, this.ActualWidth, this.ActualHeight));
    }

    protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
      EventHandler<SKPaintSurfaceEventArgs> paintSurface = this.PaintSurface;
      if (paintSurface == null)
        return;
      paintSurface((object) this, e);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
      base.OnRenderSizeChanged(sizeInfo);
      this.InvalidateVisual();
    }

    private SKSurface CreateSurface(SKImageInfo info, WriteableBitmap bitmap)
    {
      this._grContext?.Dispose();
      this._grContext = GRContext.CreateGl();
      return SKSurface.Create(info, bitmap.BackBuffer, bitmap.BackBufferStride);
    }
  }
}
