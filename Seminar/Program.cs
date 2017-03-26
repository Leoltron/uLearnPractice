using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomplete;

namespace Seminar
{
    class Program
    {

        static void Main()
        {
            DateTime d;
            if(DateTime.TryParseExact("2014-09-03 12:20:28", "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out d))
            Console.WriteLine(d);
        }
    }

    class Program1
    {

        public static void Main1(string[] args)
        {
            var s = "a  b c";
            var i = 0;
            while (i < s.Length)
            {
                while (s[i] != ' ') i++;
                Console.WriteLine(i++);
            }


            var list = new List<string> {"a","ab","abc"};
            Console.WriteLine(RightBorderTask.GetRightBorderIndex(list,"abc",-1,list.Count));

            list = new List<string>();
            Console.WriteLine(RightBorderTask.GetRightBorderIndex(list, "abc", -1, list.Count));
            //File.WriteAllLines("test.txt", CaseAlternatorTask.AlternateCharCases("ⅲ ⅳ ⅷ").ToArray());
            Console.ReadLine();
        }

        public static int GetMovedSecondIterator(int[] a, int[] b, int ia, int ib)
        {
            while (ib < b.Length && a[ia] > b[ib])
                ib++;
            return ib-1;
        }

        public static int[] Union(int[] a, int[] b)
        {
            int ia = 0, iaOld = 0, ib = 0, ibOld = 0;
            var unionList = new List<int>();
            while (ia < a.Length && ib < b.Length)
            {
                var aMb = a[ia] > b[ib];
                if (aMb)
                {
                    while (ib < b.Length && a[ia] > b[ib])
                        ib++;
                    for (var i = ibOld; i < ib; i++)
                        unionList.Add(b[i]);
                    unionList.Add(a[ia]);
                    if(ib < b.Length && a[ia] == b[ib])
                        ib++;
                    ia++;
                }
                else
                {
                    while (ia < a.Length && a[ia] < b[ib])
                        ia++;
                    for (var i = iaOld; i < ia; i++)
                        unionList.Add(a[i]);
                    unionList.Add(b[ib]);
                    if (ia < a.Length && a[ia] == b[ib])
                        ia++;
                    ib++;
                }
                ibOld = ib;
                iaOld = ia;
            }
            if(ib < b.Length)
                for(var i = ib; i < b.Length; i++)
                    unionList.Add(b[i]);
            else if (ia < a.Length)
                for (var i = ia; i < a.Length; i++)
                    unionList.Add(a[i]);

            return unionList.ToArray();
        }

        static char getNumberL(int num)
        {
            var sb = new StringBuilder();
            for (long i = 1; num > sb.Length && i < long.MaxValue; i++)
            {
                sb.Append(i.ToString());
            }
            return sb.ToString()[num-1]; 
        }

        static char getNumber(long num)
        {
            if (num < 10)
                return num.ToString()[0];

            long clearedNum = num-10;
            long row = 2;
            long amount = 90;

            while (clearedNum - (amount * row) > 0) 
            {
                clearedNum = clearedNum - (amount * row);
                row++;
                amount = amount * 10;
            }

            string numString = (amount/9+clearedNum/row).ToString();
           
                return numString[(int)(clearedNum % row)];
            
        }
    }
}
