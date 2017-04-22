using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flags
{
    class Solver_should
    {
        public void MakeTest(int n, long output)
        {
            Assert.AreEqual(output, Solver.Solve(n));
        }

        [Test]
        public void Test1() { MakeTest(1, 2L); }
        [Test]
        public void Test2() { MakeTest(2, 2L); }
        [Test]
        public void Test3() { MakeTest(3, 4L); }
        [Test]
        public void Test4() { MakeTest(4, 6L); }
        [Test]
        public void Test5() { MakeTest(10, 110L); }
    }
}
