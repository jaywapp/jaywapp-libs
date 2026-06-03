// Decompiled with JetBrains decompiler
// Type: Pentacube.Manufacture.Canvas.Geometry.Helper.EnumerableHelper
// Assembly: Pentacube.Manufacture.Canvas.Geometry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78E90F98-FDC2-4CCE-9BBE-78ED2C9A3E10
// Assembly location: C:\Users\junyoung\Downloads\Release (1)\Pentacube.Manufacture.Canvas.Geometry.dll

using System;
using System.Collections.Generic;

namespace Pentacube.Manufacture.Canvas.Geometry.Helper
{
  public static class EnumerableHelper
  {
    public static IEnumerable<TResult> ChainZip<TSource, TResult>(
      this IEnumerable<TSource> sources,
      Func<TSource, TSource, TResult> zipFunc)
    {
      return sources.ChainZipGenerator<TSource, TResult>(zipFunc, false);
    }

    private static IEnumerable<TResult> ChainZipGenerator<TSource, TResult>(
      this IEnumerable<TSource> sources,
      Func<TSource, TSource, TResult> zipFunc,
      bool isCircular)
    {
      using (IEnumerator<TSource> enumerator = sources.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          ;
        else
        {
          int count = 1;
          TSource first = enumerator.Current;
          TSource source = first;
          while (enumerator.MoveNext())
          {
            ++count;
            TSource nextItem = enumerator.Current;
            yield return zipFunc(source, nextItem);
            source = nextItem;
            nextItem = default (TSource);
          }
          if (count > 1 & isCircular)
            yield return zipFunc(source, first);
          first = default (TSource);
        }
      }
    }
  }
}
