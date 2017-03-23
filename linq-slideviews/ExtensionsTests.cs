using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace linq_slideviews
{
	[TestFixture]
	public class ExtensionsTests
	{
		[Test]
		public void MedianOfEmptyList_Fails()
		{
			Assert.Throws<InvalidOperationException>(
				() => new double[0].Median());
		}

		[Test]
		public void MedianOfSingleItemList()
		{
			var actual = new[] { 42.0 }.Median();
			Assert.That(actual, Is.EqualTo(42.0));
		}
		[Test]
		public void MedianOfTwoItemsList()
		{
			var actual = new[] { 1.0, 2.0 }.Median();
			Assert.That(actual, Is.EqualTo(1.5).Within(1e-7));
		}

		[Test]
		public void MedianOfLongOddList()
		{
			var list = Enumerable.Range(10, 1001).Select(i => (double)i).ToList();
			var actual = list.Median();
			Assert.That(actual, Is.EqualTo(list[1001 / 2]));
		}

		[Test]
		public void MedianOfLongEvenList()
		{
			var list = Enumerable.Range(10, 1000).Select(i => (double)i).ToList();
			var actual = list.Median();
			Assert.That(actual, Is.EqualTo((list[500] + list[499])/2).Within(1e-7));
		}

		[Test]
		public void MedianOfThreeItemsList()
		{
			var actual = new[] { 1.0, 2.0, 3000.0 }.Median();
			Assert.That(actual, Is.EqualTo(2.0));
		}

		[Test]
		public void BigramsOfSingleItemList()
		{
			var actual = new[] { 42.0 }.Bigramms();
			Assert.That(actual, Is.Empty);
		}

		[Test]
		public void BigramsOfTwoItemList()
		{
			var actual = new[] { 1, 2 }.Bigramms();
			Assert.That(actual, Is.EqualTo(new[] { Tuple.Create(1, 2) }));
		}

		[Test]
		public void BigramsOfLongList()
		{
			var actual = Enumerable.Range(100, 1000).Bigramms().ToList();
			for (int index = 0; index < actual.Count; index++)
			{
				var tuple = actual[index];
				Assert.That(tuple.Item1, Is.EqualTo(100+index));
				Assert.That(tuple.Item2, Is.EqualTo(101+index));
			}
		}
	}
}