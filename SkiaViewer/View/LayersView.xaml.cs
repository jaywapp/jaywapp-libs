// Decompiled with JetBrains decompiler
// Type: SkiaViewer.View.LayersView
// Assembly: SkiaViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A7BB30B5-7E8A-4BEA-B383-61FA18F68C44
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\SkiaViewer.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SkiaViewer.View
{
  public partial class LayersView : UserControl, IComponentConnector
  {
    private bool _contentLoaded;

    public LayersView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SkiaViewer;component/view/layersview.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
