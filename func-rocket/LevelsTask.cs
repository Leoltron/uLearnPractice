using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics StandardPhysics = new Physics();

	    public static IEnumerable<Level> CreateLevels()
	    {
	        yield return new Level("Zero", GetDefaultRocket(), new Vector(600, 200),
	            (size, v) => Vector.Zero, StandardPhysics);
	        yield return new Level("Heavy", GetDefaultRocket(), new Vector(600, 200),
	            (size, v) => new Vector(0, 0.9), StandardPhysics);
	        yield return new Level("Up", GetDefaultRocket(), new Vector(700, 500),
	            (size, v) => new Vector(0, -300 / (size.Height - v.Y + 300.0)), StandardPhysics);
	        var whiteHoleTarget = new Vector(700, 500);

	        Func<Vector, double, Vector> gravityForce = (dv, k) => dv.Normalize() * (k * dv.Length / (Sqr(dv.Length) + 1));
	        yield return new Level("WhiteHole", GetDefaultRocket(), whiteHoleTarget,
	            (size, v) => gravityForce(v - whiteHoleTarget, 140),
	            StandardPhysics);
	        var blackHoleTarget = new Vector(700, 500);
	        var blackHoleLocation = new Vector(200, 500) + 0.5 * (blackHoleTarget - new Vector(200, 500));

	        yield return new Level("BlackHole", GetDefaultRocket(), blackHoleTarget,
	            (size, v) => gravityForce(blackHoleLocation - v, 300),
	            StandardPhysics);
	        yield return new Level("BlackAndWhite", GetDefaultRocket(), whiteHoleTarget,
	            (size, v) =>
	                (gravityForce(v - whiteHoleTarget, 140) + gravityForce(blackHoleLocation - v, 300)) / 2,
	            StandardPhysics);
	    }

	    private static Rocket GetDefaultRocket()
	    {
	        return new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
	    }

	    public static double GetDistance(Vector a, Vector b)
	    {
	        return (a - b).Length;

	    }

	    private static double Sqr(double a)
	    {
	        return a * a;
	    }
	}
}