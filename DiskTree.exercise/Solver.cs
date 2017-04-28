using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public static class Solver
    {
        public static IEnumerable<string> Solve(IEnumerable<string> input)
        {
            return input.Select(line => line.Split('\\')).ToList().GetDirTree();
        }

        private static IEnumerable<string> GetDirTree(this List<string[]> paths, int depth = 0)
        {
            if (depth > 0)
                yield return new string(' ', depth - 1) + paths.First()[depth - 1];
            foreach (var treePart in paths.Where(path => path.Length > depth)
                .GroupBy(path => path[depth])
                .OrderBy(group => group.Key, StringComparer.Ordinal)
                .SelectMany(group => group.ToList().GetDirTree(depth + 1)))
                yield return treePart;
        }
    }
}