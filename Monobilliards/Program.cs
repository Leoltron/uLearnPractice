using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monobilliards
{
	class Program
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine() ?? "0");
			var inspectedBalls = new List<int>(n);
			for (int i = 0; i < n; i++)
				inspectedBalls.Add(int.Parse(Console.ReadLine() ?? "NA"));
			var monobilliards = new Monobilliards();
			var verdict = monobilliards.IsCheater(inspectedBalls) ? "Cheater" : "Not a proof";
			Console.WriteLine(verdict);
		}
	}
}