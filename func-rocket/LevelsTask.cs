using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
        /**
         * Zero. Ќулева€ гравитаци€.

        Heavy. ѕосто€нна€ гравитаци€ 0.9, направленна€ вниз.

        Up. √равитаци€ направлена вверх и значение еЄ модул€ вычисл€етс€ по формуле 300 / (d + 300.0), 
            где d Ч это рассто€ние от нижнего кра€ пространства. ÷ель должна иметь координаты (X:700, Y:500)

        WhiteHole. √равитаци€ направлена от цели. ћодуль вектора гравитации вычисл€етс€ по формуле 140*d / (d?+1),
            где d Ч рассто€ние до цели.
    
        BlackHole. ¬ середине отрезка, соедин€ющего начальное положение ракеты и цель, находитс€ аномали€. 
            √равитаци€ направлена к аномалии. ћодуль вектора гравитации равен 300*d / (d?+1), где d Ч рассто€ние до аномалии. 
            Ќачальное положение ракеты и положение цели должны быть такими же как и на уровне WhiteHole.

        BlackAndWhite. Ќачальное положение ракеты и положение цели должны быть такими же как и на уровне 
            WhiteHole. √равитаци€ равна среднему арифметическому гравитаций на уровн€х WhiteHole и BlackHole.

        ¬се уровни должны удовлетвор€ть таким дополнительным услови€м:

        –ассто€ние от начального положени€ ракеты до цели должно быть в пределах от 450 до 550.
        ”гол между направлением на цель и начальным направлением ракеты должен быть не менее PI/4.
    **/
		static readonly Physics standardPhysics = new Physics();

		public static IEnumerable<Level> CreateLevels()
		{
            yield return new Level("Zero", 
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200), 
				(size, v) => Vector.Zero, standardPhysics);
            yield return new Level("Heavy",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => new Vector(0,0.9), standardPhysics);
            yield return new Level("Up",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(700, 500),
                (size, v) => new Vector(0, 300 / (size.Height-v.Y + 300.0)), standardPhysics);

		    var whiteHoleTarget = new Vector(700, 500);
            Func<double, double> whiteHoleForce = d => 140 * d / (Sqr(d) + 1);

            yield return new Level("WhiteHole",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                whiteHoleTarget,
                (size, v) => (v-whiteHoleTarget).Normalize()* whiteHoleForce(GetDistance(v, whiteHoleTarget)),
                standardPhysics);

		    var blackHoleTarget = new Vector(700, 500);
            var blackHoleStart = new Vector(200, 500);
		    var blackHoleLocation = blackHoleStart + 0.5 * (blackHoleTarget - blackHoleStart);
            Func<double, double> blackHoleForce = d => 300 * d / (Sqr(d) + 1);

		    yield return new Level("BlackHole",
                new Rocket(blackHoleStart, Vector.Zero, -0.5 * Math.PI),
                blackHoleTarget,
                (size, v) => (blackHoleLocation - v).Normalize() * blackHoleForce(GetDistance(v, blackHoleLocation)),
                standardPhysics);
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