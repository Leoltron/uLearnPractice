using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            else
                return null;
        }

        /// <returns>
        /// Возвращает первые (в лексикографическом порядке) count (или меньше, если их меньше count) элементов словаря, 
        /// начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var indexStart = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var indexEnd = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
            var topElements = new string[Math.Min(count,indexEnd-indexStart+1)];
            for (var i = 0; i < topElements.Length; i++)
                topElements[i] = phrases[indexStart + i];
            return topElements;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var indexStart = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var indexEnd = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
            return indexEnd - indexStart + 1;
        }
    }
}
