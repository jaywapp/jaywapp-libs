// Decompiled with JetBrains decompiler
// Type: SkiaViewer.App
// Assembly: SkiaViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A7BB30B5-7E8A-4BEA-B383-61FA18F68C44
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\SkiaViewer.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace SkiaViewer
{
  public class App : Application
  {
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent() => this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}
