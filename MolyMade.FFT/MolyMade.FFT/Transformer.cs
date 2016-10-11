using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MolyMade.FFT
{
	public class Transformer
	{

		public Matrix<alglib.complex> Fft2D(Matrix<double> doubles)
		{
			return doubles.Select(d =>
			{
				alglib.complex[] rowComplexs;
				alglib.fftr1d(d, out rowComplexs);
				return rowComplexs;
			}).toMatrix().GetTransPosition().Select(d =>
			{
				alglib.complex[] rowComplexs = new alglib.complex[d.Length];
				alglib.fftc1d(ref rowComplexs);
				return rowComplexs;
			}).toMatrix().GetTransPosition();


		}

		private alglib.complex[][] zz(alglib.complex[][] old)
		{
			int x = old.GetUpperBound(0);
			int y = old.GetUpperBound(1);
			var neu = new alglib.complex[y+1][];
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
