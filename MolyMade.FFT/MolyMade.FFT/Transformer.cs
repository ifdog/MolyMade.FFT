using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace MolyMade.FFT
{
	public class BitmapTransformer
	{

		private readonly BitmapImage _image;
		private readonly byte[] _pixels;
		public byte[] ArrayOfAlpha => _pixels.TakeEvery(3,4);
		public byte[] ArrayOfRed => _pixels.TakeEvery(2,4);
		public byte[] ArrayOfGreen => _pixels.TakeEvery(1,4);
		public byte[] ArrayOfBlue => _pixels.TakeEvery(0,4);

		public byte[,] Alpha
		{
			get { return _pixels.TakeEvery(3, 4).Fold2D(_image.PixelWidth); }
			set
			{
				
			}
		}

		public BitmapTransformer(BitmapImage bmp)
		{
			_image = bmp;
			_pixels = new byte[4*_image.PixelWidth*_image.PixelHeight];
			_image.CopyPixels(new Int32Rect(0, 0, _image.PixelWidth, _image.PixelHeight),
				_pixels, _image.PixelWidth*4, 0);
		}

		public BitmapTransformer(string path) : this(new BitmapImage(new Uri(path)))
		{
		}


	}
}
