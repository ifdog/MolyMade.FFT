using System;
using System.Collections;
using System.Collections.Generic;
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
			int x = elements.Select(e => e.Length).Max();
			int y = elements.Length;
			_dataArray = new T[x,y];
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					_dataArray[x, y] = elements[x][y];
				}
			}
		}

		public Matrix(IEnumerable<T[]> elements):this(elements.ToArray())
		{
		}

		public T this[int i, int j] => _dataArray[i,j];

		public T[,] ToArray() => this.ArrayCopy(_dataArray);
		public void TransPosition() => _dataArray = ArrayTransPosition(_dataArray);

		public Matrix<T> GetTransPosition()
		{
			return new Matrix<T>(this.ArrayTransPosition(this._dataArray));
		}

		private  T[,] _dataArray;

		private T[,] ArrayCopy(T[,] source)
		{
			int x = source.GetUpperBound(0);
			int y = source.GetUpperBound(1);
			var clone = new T[x + 1, y + 1];
			for (int i = 0; i <= x; i++)
			{
				for (int j = 0; j <= y; j++)
				{
					clone[x, y] = source[x, y];
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
					trans[x, y] = source[y, x];
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
