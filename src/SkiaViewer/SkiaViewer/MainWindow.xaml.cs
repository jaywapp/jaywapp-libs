// Decompiled with JetBrains decompiler
// Type: SkiaViewer.MainWindow
// Assembly: SkiaViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A7BB30B5-7E8A-4BEA-B383-61FA18F68C44
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\SkiaViewer.exe

using CubicSMT.Infrastructure.Helper;
using Ookii.Dialogs.Wpf;
using Pentacube.Graphics.Geometry;
using Pentacube.Manufacture.Canvas;
using Pentacube.Manufacture.Canvas.Geometry.Model.Geometry;
using Pentacube.Manufacture.Canvas.Geometry.Model.Geometry.Base;
using Pentacube.Manufacture.Canvas.Geometry.Model.Layer;
using SkiaViewer.View;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;

namespace SkiaViewer
{
  public partial class MainWindow : Window, IComponentConnector
  {
    internal SkiaCanvasView _canvas;
    internal LayersView _layersView;
    private bool _contentLoaded;

    public SkiaCanvasViewModel SkiaCanvasViewModel { get; }

    public LayersViewModel LayersViewModel { get; }

    public MainWindow()
    {
      this.InitializeComponent();
      this.SkiaCanvasViewModel = new SkiaCanvasViewModel();
      this.LayersViewModel = new LayersViewModel(this.SkiaCanvasViewModel.Drawer);
      this._canvas.DataContext = (object) this.SkiaCanvasViewModel;
      this._layersView.DataContext = (object) this.LayersViewModel;
    }

    private static SkiaLayer CreateSampleLayer(string name, int count, Color color)
    {
      SkiaLayer skiaLayer = new SkiaLayer(1, name);
      skiaLayer.IsVisible = true;
      int num = 3;
      bool isFilled = false;
      for (int index1 = 0; index1 < count; index1 += num)
      {
        for (int index2 = 0; index2 < count; index2 += num)
        {
          skiaLayer.Add((SkiaGeom) new SkiaCircle((float) index1, (float) index2, (float) num, color, isFilled));
          isFilled = !isFilled;
        }
      }
      skiaLayer.Color = color;
      return skiaLayer;
    }

    private void ZoomFit(object sender, RoutedEventArgs e) => this.SkiaCanvasViewModel.Drawer.ZoomFit();

    private void Test(object sender, RoutedEventArgs e)
    {
      this.SkiaCanvasViewModel.Drawer.AddLayers((IEnumerable<SkiaLayer>) new List<SkiaLayer>()
      {
        MainWindow.CreateSampleLayer("Layer1", 10, Colors.White),
        MainWindow.CreateSampleLayer("Layer2", 10, Colors.Yellow),
        MainWindow.CreateSampleLayer("Layer3", 10, Colors.Blue)
      });
      this.LayersViewModel.Fetch();
    }

    private void LoadGerbers(object sender, RoutedEventArgs e)
    {
      VistaOpenFileDialog vistaOpenFileDialog1 = new VistaOpenFileDialog();
      vistaOpenFileDialog1.Title = "Gerber File 열기";
      vistaOpenFileDialog1.DefaultExt = ".gdo";
      vistaOpenFileDialog1.Filter = "Gerber files (*.gdo, *.gbr, *.art) |*.gdo;*.gbr;*.art|Zip files (*.zip) |*.zip|All files  (*.*)|*.*";
      vistaOpenFileDialog1.Multiselect = true;
      vistaOpenFileDialog1.CheckFileExists = true;
      VistaOpenFileDialog vistaOpenFileDialog2 = vistaOpenFileDialog1;
      bool? nullable = vistaOpenFileDialog2.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      List<string> stringList = MainWindow.Extract(vistaOpenFileDialog2.FileNames);
      List<Color> source = new List<Color>()
      {
        Colors.White,
        Colors.Yellow,
        Colors.Green,
        Colors.Blue,
        Colors.Red,
        Colors.Orange,
        Colors.Lime,
        Colors.SkyBlue,
        Colors.MediumPurple,
        Colors.Gray
      };
      source.ForEach((Action<Color>) (c => c.A = (byte) 120));
      int num = 0;
      foreach (string path in stringList)
      {
        Color color = source.ElementAtOrDefault<Color>(num);
        Pentacube.Manufacture.Model.SMTIF.Gerber.Gerber gerber = new Pentacube.Manufacture.Model.SMTIF.Gerber.Gerber(path);
        List<SkiaGeom> list = gerber.Geoms.Select<IPGeom, SkiaGeom>((Func<IPGeom, SkiaGeom>) (g => MainWindow.Create(g, color))).Where<SkiaGeom>((Func<SkiaGeom, bool>) (c => c != null)).ToList<SkiaGeom>();
        SkiaLayer layer = new SkiaLayer(num, gerber.Name, (IEnumerable<SkiaGeom>) list);
        layer.Color = color;
        ++num;
        this.SkiaCanvasViewModel.Drawer.AddLayer(layer);
      }
      this.LayersViewModel.Fetch();
    }

    private static SkiaGeom Create(IPGeom geom, Color color)
    {
      switch (geom)
      {
        case PLine line:
          return (SkiaGeom) new SkiaLine(line, color);
        case PArc arc:
          return (SkiaGeom) new SkiaArc(arc, color);
        case PRect rect:
          return (SkiaGeom) new SkiaRect(rect, color);
        case PCircle circle:
          return (SkiaGeom) new SkiaCircle(circle, color, true);
        case PPolygon polygon:
          return (SkiaGeom) new SkiaPolygon(polygon, color);
        default:
          return (SkiaGeom) null;
      }
    }

    private static List<string> Extract(params string[] fileNames)
    {
      List<string> stringList = new List<string>();
      foreach (string fileName in fileNames)
      {
        if (File.Exists(fileName))
        {
          if (Path.GetExtension(fileName) == ".zip")
            stringList.AddRange(MainWindow.Decompress(fileName));
          else
            stringList.Add(fileName);
        }
      }
      return stringList;
    }

    private static IEnumerable<string> Decompress(string zipFile)
    {
      string[] extensions = new string[3]
      {
        ".gdo",
        ".art",
        ".gbr"
      };
      foreach (string str in zipFile.Decompress().ToList<string>())
      {
        string path = str;
        if (((IEnumerable<string>) extensions).Any<string>((Func<string, bool>) (e => FileHelper.IsExtension(path, e))))
          yield return path;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SkiaViewer;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object) this, handler);

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ZoomFit);
          break;
        case 2:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Test);
          break;
        case 3:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.LoadGerbers);
          break;
        case 4:
          this._canvas = (SkiaCanvasView) target;
          break;
        case 5:
          this._layersView = (LayersView) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
