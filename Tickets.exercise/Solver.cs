using System.Numerics;

namespace Tickets
{
    public static class Solver
    {
        public static BigInteger Solve(int totalLen, int totalSum)
        {
            if (totalSum % 2 != 0)
                return 0;
            totalSum /= 2;
            var t = new BigInteger[totalLen + 1, totalSum + 1];
            t[0, 0] = 1;
            for (var sum = 0; sum <= totalSum; sum++)
            for (var len = 1; len <= totalLen; len++)
            {
                t[len, sum] = 0;
                for (var i = 0; i <= 9 && sum - i >= 0; i++)
                    t[len, sum] += t[len - 1, sum - i];
            }
            var solve = t[totalLen, totalSum];
            return solve * solve;
        }
    }
}