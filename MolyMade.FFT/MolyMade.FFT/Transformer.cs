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
	public class BitmapTransformer : IDisposable, ICloneable
	{
		internal Bitmap Bitmap;
		internal byte[] Pixels;
		internal byte[,] Alpha => Pixels.TakeEvery(3, 4).Fold2D(Bitmap.Width);
		internal byte[,] Red => Pixels.TakeEvery(2, 4).Fold2D(Bitmap.Width);
		internal byte[,] Green => Pixels.TakeEvery(1, 4).Fold2D(Bitmap.Width);
		internal byte[,] Blue => Pixels.TakeEvery(0, 4).Fold2D(Bitmap.Width);

		public byte[][,] Argb
		{
			get
			{
				Pixels = LockRead(Bitmap);
				return new byte[][,] {Alpha, Red, Green, Blue};
			}
			set { LockWrite(value); }
		}

		public BitmapTransformer(Bitmap bmp)
		{
			Bitmap = bmp;
		}

		public BitmapTransformer(string path) : this(new Bitmap(path))
		{
		}

		private byte[] LockRead(Bitmap bitmap)
		{
			var bmpData = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height),
				ImageLockMode.ReadOnly, Bitmap.PixelFormat);
			var ptr = bmpData.Scan0;
			var bytes = new byte[Math.Abs(bmpData.Stride)*bitmap.Height];
			Marshal.Copy(ptr, bytes, 0, bytes.Length);
			bitmap.UnlockBits(bmpData);
			return bytes;
		}

		private void LockWrite(byte[][,] pixels)
		{
			if (pixels.Length == 4 &&
			    pixels.All(i =>
				    i.GetUpperBound(0) == pixels[0].GetUpperBound(0) && i.GetUpperBound(1) == pixels[0].GetUpperBound(1)))
			{
				var bmpData = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height),
					ImageLockMode.WriteOnly, Bitmap.PixelFormat);
				var ptr = bmpData.Scan0;
				var bytes = new byte[4*pixels[0].GetUpperBound(0)*pixels[0].GetUpperBound(1)];
				Enumerable.Range(0, 4).ForEach(i => bytes.PutEvery(pixels[i].Unfold2D(), 3 - i, 4));
				Marshal.Copy(bytes, 0, ptr, pixels.Length);
				Bitmap.UnlockBits(bmpData);
			}
		}

		public void Dispose()
		{
			Bitmap.Dispose();
		}

		public object Clone()
		{
			return new BitmapTransformer(this.Bitmap);
		}
	}
}