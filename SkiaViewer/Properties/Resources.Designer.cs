// Decompiled with JetBrains decompiler
// Type: SkiaViewer.Properties.Resources
// Assembly: SkiaViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A7BB30B5-7E8A-4BEA-B383-61FA18F68C44
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\SkiaViewer.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SkiaViewer.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (SkiaViewer.Properties.Resources.resourceMan == null)
          SkiaViewer.Properties.Resources.resourceMan = new ResourceManager("SkiaViewer.Properties.Resources", typeof (SkiaViewer.Properties.Resources).Assembly);
        return SkiaViewer.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => SkiaViewer.Properties.Resources.resourceCulture;
      set => SkiaViewer.Properties.Resources.resourceCulture = value;
    }
  }
}
