﻿using System;
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

		public static Matrix<T> ToMatrrix<T>(this T[,] array)
		{
			return new Matrix<T>(array);
		}

		public static T[] TakeEvery<T>(this T[] array, int start,int step)
		{
			if (start < 0 && start > array.Length) return null;
			var newArray = new T[(array.Length-start)/step];
			for (int i = 0; i < newArray.Length; i++)
			{
				newArray[i] = array[start+i*step];
			}
			return newArray;
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
	}
}
