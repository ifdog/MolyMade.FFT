using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MolyMade.FFT
{
	public class Transformer
	{

		public Matrix<alglib.complex> Fft2D(Matrix<double> source)
		{
			return source.Select(s =>
			{
				alglib.complex[] rowComplexs;
				alglib.fftr1d(s, out rowComplexs);
				return rowComplexs;
			}).ToMatrix().GetTransPosition().Select(s =>
			{
				alglib.fftc1d(ref s);
				return s;
			}).ToMatrix().GetTransPosition();
		}

		public Matrix<double> Fft2DInv(Matrix<alglib.complex> source)
		{
			return source.GetTransPosition().Select(s =>
			{
				alglib.fftc1dinv(ref s);
				return s;
			}).ToMatrix().GetTransPosition().Select(s =>
			{
				double[] doubles;
				alglib.fftr1dinv(s, out doubles);
				return doubles;
			}).ToMatrix();
		}
	}
}
