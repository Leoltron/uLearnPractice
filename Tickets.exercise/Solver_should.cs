using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    class Solver_should
    {
        public void MakeTest(int totalLen, int totalSum, string answer)
        {
            var result = BigInteger.Parse(answer);
            Assert.AreEqual(result, Solver.Solve(totalLen, totalSum));
        }

        [Test]
        public void Test1() { MakeTest(2, 2, "4"); }
        [Test]
        public void Test2() { MakeTest(2, 5, "0"); }
        [Test]
        public void Test6() { MakeTest(10, 10, "4008004"); }
        [Test]
        public void Test7() { MakeTest(20, 20, "401200499400100"); }
    }
}
