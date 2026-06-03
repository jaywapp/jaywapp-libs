// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Event.RotateOriginEventArgs
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using System.Windows;

namespace Pentacube.Manufacture.Canvas.Geometry.Event
{
  public class RotateOriginEventArgs : RotateEventArgs
  {
    public Point Origin { get; }

    public RotateOriginEventArgs(double angle, Point origin)
      : base(angle)
    {
      this.Origin = origin;
    }
  }
}
