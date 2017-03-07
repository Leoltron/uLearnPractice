using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            StringBuilder builder = new StringBuilder();
            var afterPrefix = GetNextString(prefix);
            //if (afterPrefix.subngth == 0 || phrases.Count == 0) return phrases.Count;
            while (left < right)
            {
                var s = left == -1 ? "-[ " : "[ ";
                for (var i = left; i < right; i++)
                    if (i >= 0 && i < phrases.Count)
                        s += phrases[i] + " ";
                s += right == phrases.Count ? "]-" : "]";
                Console.WriteLine(s);

                var middle = (right + left) / 2;
                if (string.Compare(afterPrefix, phrases[middle], StringComparison.OrdinalIgnoreCase) <= 0)
                    right = middle;
                else left = middle + 1;
            }
            if (phrases.Count <= right 
                || string.Compare(afterPrefix, phrases[right], StringComparison.OrdinalIgnoreCase) >= 0
                || phrases[right].StartsWith(afterPrefix, StringComparison.OrdinalIgnoreCase))
                return right;
            return phrases.Count;
        }

        private static string GetNextString(string s)
        {
            var stringBuilder = new StringBuilder(s);
            for (var i = stringBuilder.Length - 1; i >= 0; i--)
                if (char.ToLower(stringBuilder[i]) != 'z')
                {
                    stringBuilder[i]++;
                    return stringBuilder.ToString();
                }

            return "";
        }
    }
}