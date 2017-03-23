using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();

		public static IEnumerable<Level> CreateLevels()
		{
		    yield return new Level("Zero", GetDefaultRocket(),new Vector(600, 200), 
				(size, v) => Vector.Zero, standardPhysics);
            yield return new Level("Heavy",GetDefaultRocket(),new Vector(600, 200),
                (size, v) => new Vector(0,0.9), standardPhysics);
            yield return new Level("Up",GetDefaultRocket(),new Vector(700, 500),
                (size, v) => new Vector(0, -300 / (size.Height-v.Y + 300.0)), standardPhysics);
		    var whiteHoleTarget = new Vector(700, 500);
            Func<double, double> whiteHoleForce = d => 140 * d / (Sqr(d) + 1);
            yield return new Level("WhiteHole",GetDefaultRocket(),whiteHoleTarget,
                (size, v) => (v-whiteHoleTarget).Normalize()* whiteHoleForce(GetDistance(v, whiteHoleTarget)),
                standardPhysics);
		    var blackHoleTarget = new Vector(700, 500);
		    var blackHoleLocation = new Vector(200, 500) + 0.5 * (blackHoleTarget - new Vector(200, 500));
            Func<double, double> blackHoleForce = d => 300 * d / (Sqr(d) + 1);
		    yield return new Level("BlackHole",GetDefaultRocket(),blackHoleTarget,
                (size, v) => (blackHoleLocation - v).Normalize() * blackHoleForce(GetDistance(v, blackHoleLocation)),
                standardPhysics);
            yield return new Level("BlackAndWhite",GetDefaultRocket(),whiteHoleTarget,
                (size, v) => 
                ((blackHoleLocation - v).Normalize() * blackHoleForce(GetDistance(v, blackHoleLocation))+
                (v - whiteHoleTarget).Normalize() * whiteHoleForce(GetDistance(v, whiteHoleTarget)))/2,
                standardPhysics);
        }

	    private static Rocket GetDefaultRocket()
	    {
	        return new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
	    }

	    public static double GetDistance(Vector a, Vector b)
	    {
	        var dx = a.X - b.X;
	        var dy = a.Y - b.Y;
            return Math.Sqrt(Sqr(dx) + Sqr(dy));
	    }

	    private static double Sqr(double a)
	    {
	        return a * a;
	    }
	}
}