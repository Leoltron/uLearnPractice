namespace Flags
{
    public static class Solver
    {
        public static long Solve(int n)
        {
            if (n <= 2) return 2;
            var results = new long[n];
            results[0] = 2;
            results[1] = 2;
            for (var i = 2; i < n; i++)
                results[i] = results[i - 1] + results[i - 2];
            return results[n - 1];
        }
    }
}