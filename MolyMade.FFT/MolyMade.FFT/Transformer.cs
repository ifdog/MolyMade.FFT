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

		private readonly BitmapImage _image;
		private readonly byte[] _pixels;
		public byte[] ArrayOfAlpha => _pixels.TakeEvery(3,4);
		public byte[] ArrayOfRed => _pixels.TakeEvery(2,4);
		public byte[] ArrayOfGreen => _pixels.TakeEvery(1,4);
		public byte[] ArrayOfBlue => _pixels.TakeEvery(0,4);
		public Matrix<double> MatrixOfRed => ArrayOfRed.Select(b => (double)b).ToArray().Fold2D(_image.PixelWidth).ToMatrrix();
		public Matrix<double> MatrixOfGreen => ArrayOfGreen.Select(b => (double)b).ToArray().Fold2D(_image.PixelWidth).ToMatrrix();
		public Matrix<double> MatrixOfBlue => ArrayOfBlue.Select(b => (double)b).ToArray().Fold2D(_image.PixelWidth).ToMatrrix();
		public Matrix<alglib.complex> FftOfRed => Fft2D(MatrixOfRed);
		public Matrix<alglib.complex> FftOfGreen => Fft2D(MatrixOfGreen);
		public Matrix<alglib.complex> FftOfBlue => Fft2D(MatrixOfBlue);

		public Transformer(string path)
		{
			_image = new BitmapImage(new Uri(path));
			_pixels = new byte[4*_image.PixelWidth*_image.PixelHeight];
			_image.CopyPixels(new Int32Rect(0,0,_image.PixelWidth,_image.PixelHeight),
				_pixels,_image.PixelWidth*4,0 );
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
