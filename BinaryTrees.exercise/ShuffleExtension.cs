using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public static class ShuffleExtension
    {
        static Random rnd = new Random(42);

        public static List<T> Shuffle<T>(this IEnumerable<T> _list)
        {
            var list = _list.ToList();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

    }
}
