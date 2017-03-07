using System;
using System.Collections.Generic;
using System.Text;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (prefix.Length == 0 || phrases.Count == 0) return phrases.Count;
            while (left < right)
            {
                var middle = (right + left) / 2;
                if (string.Compare(prefix, phrases[middle].Substring(0, prefix.Length), StringComparison.OrdinalIgnoreCase) >= 0)
                    left = middle;
                else right = middle + 1;
            }
            if (phrases[right].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return right + 1;
            return phrases.Count;
        }
    }
}