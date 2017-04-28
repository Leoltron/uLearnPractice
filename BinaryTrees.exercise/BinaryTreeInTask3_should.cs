using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    [TestFixture]
    public class BinaryTreeInTask3_should
    {
        //Эта черная магия нужна, чтобы код тестов, которые используют индексацию, компилировался 
        //при отсутствии индексатора в вашем классе.
        //Совсем скоро вы и сами научитесь писать что-то подобное.

        public static PropertyInfo GetIndexer<T>(BinaryTree<T> t)
            where T : IComparable
        {
            return t.GetType()
                .GetProperties()
                .Select(z => new { prop = z, ind = z.GetIndexParameters() })
                .Where(z => z.ind.Length == 1 && z.ind[0].ParameterType == typeof(int))
                .SingleOrDefault()
                ?.prop;
        }

        public static Func<int, T> MakeAccessor<T>(BinaryTree<T> tree)
            where T : IComparable
        {
            var prop = GetIndexer(tree);
            var param = Expression.Parameter(typeof(int));
            return Expression.Lambda<Func<int, T>>(
                Expression.MakeIndex(Expression.Constant(tree), prop, new[] { param }),
                param)
                .Compile();
        }

        public void Test<T>(IEnumerable<T> _values)
            where T : IComparable
        {
            var values = _values.Shuffle();
            var tree = new BinaryTree<T>();
            if (GetIndexer(tree) == null)
                Assert.Fail("Your BinaryTree does not implement indexing");
            foreach(var e in values)
                tree.Add(e);

            var orderedValues = values.OrderBy(z => z).ToList();
            var indexer = MakeAccessor(tree);
            for (int i = 0; i < values.Count; i++)
                Assert.AreEqual(orderedValues[i], indexer(i));
        }

        [Test]
        public void SupportIndexers()
        {
            Test(Enumerable.Range(1, 30));
        }

    }
}
