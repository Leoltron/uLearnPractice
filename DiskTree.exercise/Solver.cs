using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiskTree
{
    public static class Solver
    {
        public static IEnumerable<string> Solve(IEnumerable<string> input)
        {
            return input.OrderBy(s => s, new Comparer())
                .Select(line => line.Split('\\')).ToList().GetDirTree();
        }

        private static IEnumerable<string> GetDirTree(this List<string[]> paths, int depth = 0)
        {
            if (depth > 0)
                yield return paths.First()[depth - 1].AddSpacesToLeft(depth - 1);
            foreach (var treePart in paths.Where(path => path.Length > depth)
                .GroupBy(path => path[depth])
                .OrderBy(group => group.Key, new Comparer())
                .SelectMany(group => group.ToList().GetDirTree(depth + 1)))
                yield return treePart;
        }

        private static string AddSpacesToLeft(this string s, int spacesAmount)
        {
            if (spacesAmount < 0) throw new ArgumentOutOfRangeException(nameof(spacesAmount));
            if (spacesAmount == 0)
                return s;
            var sb = new StringBuilder();
            for (var i = 0; i < spacesAmount; i++)
                sb.Append(' ');
            sb.Append(s);
            return sb.ToString();
        }

        private class Comparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == y) return 0;
                if (x.StartsWith(y)) return 1;
                if (y.StartsWith(x)) return -1;
                return string.CompareOrdinal(x,y);
            }
        }
    }
}