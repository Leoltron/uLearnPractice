using System.Drawing;

namespace func_rocket
{
	/// <param name="location">
	/// находитс€ в пределах пр€моугольника (0, 0) Ч (spaceSize.Width, spaceSize.Height)
	/// </param>
	public delegate Vector Gravity(Size spaceSize, Vector location);
}