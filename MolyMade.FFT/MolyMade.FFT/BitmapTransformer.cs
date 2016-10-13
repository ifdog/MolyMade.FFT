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
using System.Windows.Media.Imaging;


namespace MolyMade.FFT
{
	public class BitmapTransformer : IDisposable, ICloneable
	{
		public Bitmap Bmp;
		internal byte[] Pixels;
		internal byte[,] Red => Pixels.TakeEvery(2, 3).Fold2D(Bmp.Width);
		internal byte[,] Green => Pixels.TakeEvery(1, 3).Fold2D(Bmp.Width);
		internal byte[,] Blue => Pixels.TakeEvery(0, 3).Fold2D(Bmp.Width);
		public BitmapImage BitmapImage => Bmp.ToBitmapImage();

		public byte[][,] Argb
		{
			get
			{
				return new byte[][,] {Red, Green, Blue};
			}
			set { LockWrite(value); }
		}

		public BitmapTransformer(Bitmap bmp)
		{
			Bmp = bmp;
			Pixels = LockRead(Bmp);
		}

		public BitmapTransformer(string path) : this(new Bitmap(path))
		{
		}

		public BitmapTransformer(byte[,] r, byte[,] g, byte[,] b)
		{
			var p = new byte[][,] {r, g, b};
			if (p.All(i =>
				i.GetUpperBound(0) == p[0].GetUpperBound(0) && i.GetUpperBound(1) == p[0].GetUpperBound(1)))
			{
				Bmp = new Bitmap(r.GetUpperBound(0), r.GetUpperBound(1));
				Argb = p;
			}
		}

		private byte[] LockRead(Bitmap bitmap)
		{
			var bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height),
				ImageLockMode.ReadOnly, Bmp.PixelFormat);
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
				var bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height),
					ImageLockMode.WriteOnly, Bmp.PixelFormat);
				var ptr = bmpData.Scan0;
				var bytes = new byte[4*pixels[0].GetUpperBound(0)*pixels[0].GetUpperBound(1)];
				Enumerable.Range(0, 4).ForEach(i => bytes.PutEvery(pixels[i].Unfold2D(), 3 - i, 4));
				Marshal.Copy(bytes, 0, ptr, pixels.Length);
				Bmp.UnlockBits(bmpData);
			}
		}

		public void Dispose()
		{
			Bmp.Dispose();
		}

		public object Clone()
		{
			return new BitmapTransformer(this.Bmp);
		}
	}
}