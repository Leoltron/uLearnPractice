using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace linq_slideviews
{
	[TestFixture]
	public class ExtensionsSecretTests
	{
		private bool strangeEnumerated = false;
		public IEnumerable<double> StrangeSequence(double itemValue, int count)
		{
			Assert.IsFalse(strangeEnumerated, "Sequence should not be enumerated twice!");
			strangeEnumerated = true;
			for (int i = 0; i < count; i++)
				yield return itemValue;
		}

		public IEnumerable<double> KillingSequence(double itemValue, int count)
		{
			for (int i = 0; i < count; i++)
				yield return itemValue;
			Assert.Fail("Sequence should be enumerated lazily!");
		}

		[Test]
		public void Meidan_DontEnumerateSequenceTwice()
		{
			strangeEnumerated = false;
			StrangeSequence(3.0, 10).Median();
		}

		[Test]
		public void Bigrams_DontEnumerateSequenceTwice()
		{
			strangeEnumerated = false;
			StrangeSequence(3.0, 10).Bigramms().ToList();
		}

		[Test]
		public void Bigrams_IsLazy()
		{
			KillingSequence(3.0, 10).Bigramms().Take(9).ToList();
		}
	}
}