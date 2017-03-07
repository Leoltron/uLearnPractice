using System.Collections.Generic;

namespace Monobilliards
{
	public interface IMonobilliards
	{
		bool IsCheater(IList<int> inspectedBalls);
	}
}