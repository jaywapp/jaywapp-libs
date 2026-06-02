using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Algorithm.Enumerables
{
    public static class Combination
    {
        public static List<List<T>> Combinate<T>(IEnumerable<T> items, int count, IEqualityComparer<T> defaultComparer = null)
        {
            var result = new List<List<T>>();
            var comparer = defaultComparer ?? EqualityComparer<T>.Default;

            if (count == 0)
                return result;

            if (count == 1)
            {
                foreach (var item in items.ToList())
                    result.Add(new List<T>() { item, });

                return result;
            }

            foreach (var item in items.ToList())
            {
                var others = items.Where(i => !comparer.Equals(i, item)).ToList();
                var nexts = Combinate(others, count - 1, defaultComparer);

                foreach (var next in nexts)
                {
                    var list = new List<T>();

                    list.Add(item);
                    list.AddRange(next);

                    result.Add(list);
                }
            }

            return result;
        }
    }
}
