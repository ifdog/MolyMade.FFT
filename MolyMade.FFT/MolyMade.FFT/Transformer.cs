using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MolyMade.FFT
{
	public class Transformer
	{

		public double[,] Fft2D(double[,] doubles)
		{
			
			var rows = doubles.Select(d =>
			{
				alglib.complex[] rowComplexs;
				alglib.fftr1d(d,out rowComplexs);
				return rowComplexs;
			}).ToArray();

		}

		private alglib.complex[][] zz(alglib.complex[][] old)
		{
			int x = old.GetUpperBound(0);
			int y = old.GetUpperBound(1);
			var neu = new alglib.complex[y+1][x+1];
			for (int i = 0; i <= y; i++)
			{
				for (int j = 0; j <= x; j++)
				{
					neu[i, j] = old[j, i];
				}
			}
			return neu;
		}
	}
}
