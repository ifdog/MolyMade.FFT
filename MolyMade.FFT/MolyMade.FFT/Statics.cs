using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolyMade.FFT
{
	public static class Statics
	{
		public static Matrix<T> ToMatrix<T>(this IEnumerable<T[]> iEnumerable)
		{
			return new Matrix<T>(iEnumerable);
		}
	}
}
