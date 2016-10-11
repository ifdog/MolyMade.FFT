using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

		//zz了
		public static Array TakeEvery(this Array array,int start, int step)
		{
			int i = start;
			Type genericList = typeof(List<>);
			Type implementedLisType =  genericList.MakeGenericType(array.GetValue(0).GetType());
			object o = Activator.CreateInstance(implementedLisType);
			while (i<array.Length)
			{
				implementedLisType.InvokeMember("Add", 
					BindingFlags.InvokeMethod, null, o, new object[] {array.GetValue(i)});
				i += step;
			}
			return implementedLisType.InvokeMember("ToArray", 
				BindingFlags.InvokeMethod, null, o, null) as Array;
		}

		public static T[,] Fold2D<T>(this T[] array, int width)
		{
			if (width == 0) return null;
			int height = array.Length%width == 0 ? array.Length/width : array.Length/width + 1;
			T[,] newArray = new T[height,width];
			for (int i = 0; i < array.Length; i++)
			{
				newArray[i/width, i%width] = array[i];
			}
			return newArray;
		}

		public static Matrix<alglib.complex> Fft2D(Matrix<double> source)
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

		public static Matrix<double> Fft2DInv(Matrix<alglib.complex> source)
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
