// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Service.RenderingTimer
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using System;
using System.Diagnostics;
using System.Threading;

namespace Pentacube.Manufacture.Canvas.Service
{
  public class RenderingTimer
  {
    private readonly Stopwatch _sw = new Stopwatch();
    private double _extraDelay;
    private int _frameLimit;

    public int FrameLimit
    {
      get => this._frameLimit;
      set
      {
        this._frameLimit = value > 0 ? value : throw new ArgumentOutOfRangeException("1 이상이어야 합니다");
        this.OneRenderTime = 1000.0 / (double) value;
      }
    }

    public double OneRenderTime { get; private set; }

    public RenderingTimer() => this.FrameLimit = 30;

    public void BeginRender()
    {
      this._sw.Reset();
      this._sw.Restart();
    }

    public void EndRender() => this.EndRender(out long _);

    public void EndRender(out long renderTime)
    {
      renderTime = this.EndRenderAndGetTime();
      this._extraDelay += this.OneRenderTime - (double) renderTime;
      int extraDelay = (int) this._extraDelay;
      if (extraDelay > 0)
      {
        Thread.Sleep(extraDelay);
        this._extraDelay -= (double) extraDelay;
      }
      else
      {
        if (this._extraDelay >= 0.0)
          return;
        this._extraDelay = 0.0;
      }
    }

    public long EndRenderAndGetTime()
    {
      this._sw.Stop();
      return this._sw.ElapsedMilliseconds;
    }
  }
}
