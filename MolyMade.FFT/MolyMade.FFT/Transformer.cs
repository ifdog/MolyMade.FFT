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
	public class Transformer
	{

		private BitmapImage _image;
		private byte[] _pixels;
		public byte[,] ArrayOfAlpha => null;
		public byte[,] ArrayOfRed => null;
		public byte[,] ArrayOfGreen => null;
		public byte[,] ArrayOfBlue => null;

		public Transformer(string path)
		{
			_image = new BitmapImage(new Uri(path));
			_pixels = new byte[4*_image.PixelWidth*_image.PixelHeight];
			_image.CopyPixels(new Int32Rect(0,0,_image.PixelWidth,_image.PixelHeight),
				_pixels,_image.DecodePixelWidth*4,0 );
		}


	}
}
