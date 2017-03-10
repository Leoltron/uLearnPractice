using System.Collections.Generic;
using NUnit.Framework;

namespace Seminar
{
    [TestFixture]
    public class DictTests
    {
        [Test]
        public void PerfomanceTest()
        {
            var cap = 10000;
            var dict = new Dictionary<int,int>(cap);

        } 
    }
}