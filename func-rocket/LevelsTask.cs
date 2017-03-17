using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
        /**
         * Zero. ������� ����������.

        Heavy. ���������� ���������� 0.9, ������������ ����.

        Up. ���������� ���������� ����� � �������� � ������ ����������� �� ������� 300 / (d + 300.0), 
            ��� d � ��� ���������� �� ������� ���� ������������. ���� ������ ����� ���������� (X:700, Y:500)

        WhiteHole. ���������� ���������� �� ����. ������ ������� ���������� ����������� �� ������� 140*d / (d?+1),
            ��� d � ���������� �� ����.
    
        BlackHole. � �������� �������, ������������ ��������� ��������� ������ � ����, ��������� ��������. 
            ���������� ���������� � ��������. ������ ������� ���������� ����� 300*d / (d?+1), ��� d � ���������� �� ��������. 
            ��������� ��������� ������ � ��������� ���� ������ ���� ������ �� ��� � �� ������ WhiteHole.

        BlackAndWhite. ��������� ��������� ������ � ��������� ���� ������ ���� ������ �� ��� � �� ������ 
            WhiteHole. ���������� ����� �������� ��������������� ���������� �� ������� WhiteHole � BlackHole.

        ��� ������ ������ ������������� ����� �������������� ��������:

        ���������� �� ���������� ��������� ������ �� ���� ������ ���� � �������� �� 450 �� 550.
        ���� ����� ������������ �� ���� � ��������� ������������ ������ ������ ���� �� ����� PI/4.
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