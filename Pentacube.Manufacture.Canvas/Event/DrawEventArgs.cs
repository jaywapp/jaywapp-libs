// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Event.DrawEventArgs
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using SkiaSharp;
using System;

namespace Pentacube.Manufacture.Canvas.Event
{
  public class DrawEventArgs : EventArgs
  {
    public SKCanvas Canvas { get; set; }

    public SKRect Bounds { get; set; }

    public DrawEventArgs(SKCanvas canvas, SKRect bounds)
    {
      this.Canvas = canvas;
      this.Bounds = bounds;
    }
  }
}
