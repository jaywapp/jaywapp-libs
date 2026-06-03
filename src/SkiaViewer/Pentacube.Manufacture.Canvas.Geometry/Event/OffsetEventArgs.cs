// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Event.OffsetEventArgs
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using System;

namespace Pentacube.Manufacture.Canvas.Geometry.Event
{
  public class OffsetEventArgs : EventArgs
  {
    public float OffsetX { get; }

    public float OffsetY { get; }

    public OffsetEventArgs(double offsetX, double offsetY)
    {
      this.OffsetX = (float) offsetX;
      this.OffsetY = (float) offsetY;
    }
  }
}
