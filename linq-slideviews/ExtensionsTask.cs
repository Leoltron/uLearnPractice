using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка.
		/// Медиана списка из четного количества элементов — среднее арифметическое двух серединных элементов списка.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
		    var list = items.ToList();
		    if (list.Count == 0)
		        throw new InvalidOperationException();

		    return list.Count % 2 == 0 ? (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2 : list[list.Count / 2];
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigramms<T>(this IEnumerable<T> items)
		{
		    var firstItemPassed = false;
		    var prevItem = default(T);
		    foreach (var item in items)
		    {
		        if (firstItemPassed)
		            yield return Tuple.Create(prevItem, item);
		        else
		            firstItemPassed = true;
		        prevItem = item;

		    }
		}
	}
	
}