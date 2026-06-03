// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Interface.IPanZoomCanvas
// Assembly: Pentacube.Manufacture.Canvas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95F00886-C15B-4B56-B9D2-3D0B6582848B
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.exe

using Pentacube.Manufacture.Canvas.Geometry.Interface;
using System.Windows;

namespace Pentacube.Manufacture.Canvas.Interface
{
  public interface IPanZoomCanvas : IPanZoom
  {
    Size CanvasSize { get; set; }

    void Zoom(double scale);

    void Pan(Vector vec);
  }
}
