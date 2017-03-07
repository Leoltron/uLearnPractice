using System;

namespace Seminar
{
    public class RecursiveBinarySearchTask
    {
        public static string log;

        public static int BinSearchLeftBorder(long[] array, long value, int left, int right)
        {
            log = "";
            var s = left == -1 ? "-[ " : "[ ";
            for (var i = left; i < right; i++)
                if (i >= 0 && i < array.Length)
                    s += array[i] + " ";
            s += right == array.Length ? "]-" : "]";

            log = log + s + '\n';

            if (left == right - 1) return left;
            var m = (left + right)/2;
            if (array[m] < value)
                return BinSearchLeftBorder(array, value, m, right);
            return BinSearchLeftBorder(array, value, left, m);
        }
    }
}