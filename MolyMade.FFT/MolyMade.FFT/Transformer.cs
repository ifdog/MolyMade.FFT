using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace MolyMade.FFT
{
	public class BitmapTransformer
	{

		private readonly Bitmap _bitmap;
		private readonly byte[] _pixels;

		public byte[,] Alpha
		{
			get { return _pixels.TakeEvery(3, 4).Fold2D(_bitmap.Width); }
			set { _pixels.PutEvery(value.Unfold2D(), 3, 4); }
		}
		public byte[,] Red
		{
			get { return _pixels.TakeEvery(2, 4).Fold2D(_bitmap.Width); }
			set { _pixels.PutEvery(value.Unfold2D(), 2, 4); }
		}
		public byte[,] Green
		{
			get { return _pixels.TakeEvery(1, 4).Fold2D(_bitmap.Width); }
			set { _pixels.PutEvery(value.Unfold2D(), 1, 4); }
		}
		public byte[,] Blue
		{
			get { return _pixels.TakeEvery(0, 4).Fold2D(_bitmap.Width); }
			set { _pixels.PutEvery(value.Unfold2D(), 0, 4); }
		}

		public BitmapTransformer(Bitmap bmp)
		{
			_bitmap = bmp;
			_pixels = LockRead(_bitmap);

		}

		public BitmapTransformer(string path) : this(new Bitmap(path))
		{
		}

		private byte[] LockRead(Bitmap bitmap)
		{
			var bmpData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
				ImageLockMode.ReadOnly, _bitmap.PixelFormat);
			var ptr = bmpData.Scan0;
			var bytes = new byte[Math.Abs(bmpData.Stride)*bitmap.Height];
			Marshal.Copy(ptr,bytes,0,bytes.Length);
			bitmap.UnlockBits(bmpData);
			return bytes;
		}

	}
}
