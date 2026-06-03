// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Event.CanvasMousePositionChangedEventArgs
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using System;

namespace Pentacube.Manufacture.Canvas.Event
{
  public class CanvasMousePositionChangedEventArgs : EventArgs
  {
    public System.Windows.Point? Point { get; }

    public CanvasMousePositionChangedEventArgs(System.Windows.Point? pt) => this.Point = pt;
  }
}
