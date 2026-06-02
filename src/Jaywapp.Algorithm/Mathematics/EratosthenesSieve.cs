using System.Collections.Generic;

namespace Jaywapp.Algorithm.Mathematics
{
    public static class EratosthenesSieve
    {
        public static List<int> Calculate(int n)
        {
            var arr = new bool[n + 1];

            for(int i = 2; i <= n; i++)
            {
                if (!arr[i])
                {
                    var next = i + i;
                    for (int j = next; j <= n; j += i)
                        arr[j] = true;
                }
            }

            var result = new List<int>();

            for(int i = 2; i<= n; i++)
            {
                if (!arr[i])
                    result.Add(i);
            }

            return result;
        }
    }
}
