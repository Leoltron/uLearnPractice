using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    [TestFixture]
    class BinaryTreeInTask1_should
    {

        Random rnd = new Random();
       
        void TestAddContains<T>(IEnumerable<T> _values)
            where T : IComparable
        {
            var values = _values.Shuffle();
            var toAdd = values.Where(z => rnd.NextDouble() > 0.7).ToList();
            var tree = new BinaryTree<T>();
            foreach (var e in toAdd)
                tree.Add(e);
            foreach (var e in values)
                Assert.AreEqual(toAdd.Contains(e), tree.Contains(e));
        }

        [Test]
        public void WorkWithIntegers()
        {
            TestAddContains(Enumerable.Range(0, 20));
        }

        [Test]
        public void WorkWithStrings()
        {
            TestAddContains(Enumerable.Range(1, 20).Select(z => new string( (char)('a' + z), z)));
        }

        [Test]
        public void WorkWithTuples()
        {
            TestAddContains(Enumerable.Range(1, 20).Select(z => Tuple.Create(z / 3, new string((char)('a' + z), z))));
        }
        
        [Test]
        public void NotFailOnStackOverflow()
        {
            var tree = new BinaryTree<int>();
            foreach (var e in Enumerable.Range(0, 10000))
            {
                tree.Add(e);
                tree.Contains(e);
            }

        }
    }
}
