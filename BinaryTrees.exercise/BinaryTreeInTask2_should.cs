using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{

    [TestFixture]
    public class BinaryTreeInTask2_should
    {
       

        public void TestIEnumerable<T>(IEnumerable<T> _values)
            where T : IComparable
        {
            var values = _values.Shuffle();
            var orderedValues = values.OrderBy(z => z).ToList();
            var tree = new BinaryTree<T>();
            foreach (var e in values)
                tree.Add(e);



            Assert.True(tree is IEnumerable<T>, "You binary tree does not implement IEnumerable");
            var en = tree as IEnumerable<T>;
            Assert.True(en.Zip(orderedValues, (a, b) => a.Equals(b)).All(z => z));
        }

        [Test]
        public void SortIntegers()
        {
            TestIEnumerable(Enumerable.Range(0, 20));
        }


    }
}
