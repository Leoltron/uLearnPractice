using System;

namespace func_rocket
{
	public class ControlTask
	{
        /**
         * Ракета должна по модулю отклонения быть не более максимума - до этого поворачивать наименее долгим способом.
         * После этого ракета отколняется в зависимости от того, как надо поправить вектор скорости, чтобы он совпал
         *  по направлению с вектором
         * от ракеты до цели.
         **/
        private const double MaxDAngle = Math.PI / 4;
	    public static Turn ControlRocket(Rocket rocket, Vector target)
        {
		    var dVelocityAngle = rocket.Velocity.Angle - (target - rocket.Location).Angle;

           /* if (Math.Abs(dVelocityAngle) > MaxDAngle)
            {
                var dDirectionAngle = rocket.Direction % (Math.PI * 2) - (target - rocket.Location).Angle;
                return dDirectionAngle > 0 ? Turn.Left : Turn.Right;
            }
            else
            {*/
                var targetAngle = rocket.Velocity.Angle - 2 * dVelocityAngle;
                return targetAngle <0 ? Turn.Left : Turn.Right;
            //}


        }

	    public static double FormatAngle(double angle)
	    {
	        while (angle < 0)
	            angle += Math.PI * 2;
	        angle -= Math.PI;
	        return angle;
	    }
	}
}