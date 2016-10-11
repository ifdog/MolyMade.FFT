using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolyMade.FFT
{
	public class Matrix<T>:IEnumerable<T[]>
	{
		public Matrix(T[,] elements)
		{
			_dataArray = this.ArrayCopy(elements);
		}

		public Matrix(T[][] elements)
		{
			int x = elements.Length;
			int y = elements.Select(e => e.Length).Max();
			
			_dataArray = new T[x,y];
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					_dataArray[i, j] = elements[i][j];
				}
			}
		}

		public Matrix(IEnumerable<T[]> elements):this(elements.ToArray())
		{
		}

		private T[,] _dataArray;
		public T this[int i, int j] => _dataArray[i,j];
		public T[,] ToArray() => this.ArrayCopy(_dataArray);
		public void TransPosition() => _dataArray = ArrayTransPosition(_dataArray);

		public Matrix<T> GetTransPosition()
		{
			return new Matrix<T>(this.ArrayTransPosition(this._dataArray));
		}


		public Matrix<TN> MapNew<TN>(Func<T,TN> func)
		{
			var array = this.ToArray();
			int x = array.GetUpperBound(0);
			int y = array.GetUpperBound(1);
			var newArray = new TN[x+1,y+1];
			for (int i = 0; i <= x; i++)
			{
				for (int j = 0; j <= y; j++)
				{
					newArray[i, j] = func(array[i, j]);
				}
			}
			return new Matrix<TN>(newArray);
		}

		private T[,] ArrayCopy(T[,] source)
		{
			int x = source.GetUpperBound(0);
			int y = source.GetUpperBound(1);
			var clone = new T[x + 1, y + 1];
			for (int i = 0; i <= x; i++)
			{
				for (int j = 0; j <= y; j++)
				{
					clone[i, j] = source[i, j];
				}
			}
			return clone;
		}

		private T[,] ArrayTransPosition(T[,] source)
		{
			int x = source.GetUpperBound(0);
			int y = source.GetUpperBound(1);
			var trans = new T[y + 1, x + 1];
			for (int i = 0; i <= y; i++)
			{
				for (int j = 0; j <= x; j++)
				{
					trans[i, j] = source[j, i];
				}
			}
			return trans;
		}

		public IEnumerator<T[]> GetEnumerator()
		{
			var eList = new List<T[]>();
			int x = _dataArray.GetUpperBound(0);
			int y = _dataArray.GetUpperBound(1);
			for (int i = 0; i <= x; i++)
			{
				var eArrayList = new List<T>();
				for (int j = 0; j <= y; j++)
				{
					eArrayList.Add(_dataArray[i, j]);
				}
				eList.Add(eArrayList.ToArray());
			}
			return eList.GetEnumerator();

		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}

}
