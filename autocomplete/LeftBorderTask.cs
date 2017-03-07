using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (left == right - 1) return left;
            long sum = left + right;
            int m = (int) (sum/2);
            if (string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) > 0)
                return GetLeftBorderIndex(phrases, prefix, m, right);
            return GetLeftBorderIndex(phrases, prefix, left, m);
        }
    }
}