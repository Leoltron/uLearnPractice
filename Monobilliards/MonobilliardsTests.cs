using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Monobilliards
{
	[TestFixture]
	public class MonobilliardsTests
	{
		[Test]
		public void InspectEmpty()
		{
			AssertCheater(false, new int[] { });
		}
		[Test]
		public void SingleBallShouldBeFirst()
		{
			AssertCheater(false, new[] { 1 });
		}
		[Test]
		public void IncreasingShouldBeValid()
		{
			AssertCheater(false, new[] { 1, 2 });
			AssertCheater(false, new[] { 1, 2, 3 });
			AssertCheater(false, new[] { 1, 2, 3, 4 });
		}
		[Test]
		public void DereasingShouldBeValid()
		{
			AssertCheater(false, new[] { 2, 1 });
			AssertCheater(false, new[] { 3, 2, 1 });
			AssertCheater(false, new[] { 4, 3, 2, 1 });
		}

		[Test]
		public void Sample()
		{
			AssertCheater(true, new[] { 3, 1, 2 });
		}

	    [Test]
	    public void RandomTest()
	    {
            var rand = new Random();
	        for(var i = 0 ; i < 100; i++)
                RandomAssertNotCheater(10,rand);
	    }

		public void AssertCheater(bool isCheater, int[] balls)
		{
			Assert.AreEqual(
				isCheater, 
				Factory.CreateMonobilards().IsCheater(balls), 
				"IsCheater(new int[]{{{0}}})", string.Join(", ", balls));
		}

	    public void RandomAssertNotCheater(int ballAmount, Random rand)
	    {
	        var currentBalls = new Stack<int>();
	        var balls = new Stack<int>();
	        for (var i = 0; i < ballAmount; i++)
	        {
	            currentBalls.Push(i+1);
                if(rand.Next(3) == 1)
                    balls.Push(currentBalls.Pop());
	        }
            while(currentBalls.Count > 0)
                balls.Push(currentBalls.Pop());
            AssertCheater(false,balls.ToArray());
        }
	}
}