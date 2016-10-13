using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MolyMade.FFT
{
	public static class Extensions
	{
		public static Matrix<T> ToMatrix<T>(this IEnumerable<T[]> iEnumerable)
		{
			return new Matrix<T>(iEnumerable);
		}

		public static Matrix<T> ToMatrrix<T>(this T[,] array)
		{
			return new Matrix<T>(array);
		}

		public static T[] TakeEvery<T>(this T[] array, int start, int step)
		{
			if (start < 0 && start > array.Length) return null;
			var newArray = new T[start == 0 ? (array.Length - start)/step : (array.Length - start)/step + 1];
			for (int i = 0; i < newArray.Length; i++)
			{
				newArray[i] = array[start + i*step];
			}
			return newArray;
		}

		public static T[] PutEvery<T>(this T[] array, T[] slice, int start, int step)
		{
			if (start < 0 || start > array.Length) return array;
			var newArray = new T[array.Length];
			for (int i = 0; i < newArray.Length; i++)
			{
				if (i >= start && (i - start)%step == 0 && (i - start)/step < slice.Length)
				{
					newArray[i] = slice[(i - start)/step];
				}
				else
				{
					newArray[i] = array[i];
				}
			}
			return newArray;
		}

		public static T[,] Fold2D<T>(this T[] array, int width)
		{
			if (width == 0) return null;
			int height = array.Length%width == 0 ? array.Length/width : array.Length/width + 1;
			T[,] newArray = new T[height, width];
			for (int i = 0; i < array.Length; i++)
			{
				newArray[i/width, i%width] = array[i];
			}
			return newArray;
		}

		public static T[] Unfold2D<T>(this T[,] array)
		{
			int x = array.GetUpperBound(1);
			int y = array.GetUpperBound(0);
			var newArray = new T[(x + 1)*(y + 1)];
			for (int i = 0; i <= x; i++)
			{
				for (int j = 0; j <= y; j++)
				{
					newArray[(x + 1)*j + i] = array[j, i];
				}
			}
			return newArray;
		}

		public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T> action)
		{
			foreach (var v in iEnumerable)
			{
				action(v);
			}
		}
	}
}
