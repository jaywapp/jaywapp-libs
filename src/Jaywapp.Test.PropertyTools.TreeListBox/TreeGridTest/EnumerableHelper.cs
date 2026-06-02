using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jaywapp.Test.PropertyTools.TreeListBox
{
    public static class EnumerableHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        public static void Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> selector)
        {
            var ordered = collection.OrderBy(selector).ToList();

            collection.Clear();
            foreach(var item in ordered)
                collection.Add(item);
        }
    }
}
