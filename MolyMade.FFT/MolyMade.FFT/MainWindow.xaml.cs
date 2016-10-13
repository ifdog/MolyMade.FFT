using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MolyMade.FFT
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			BitmapTransformer bt = new BitmapTransformer(@"D:/1.bmp");
			ImageWindow iw = new ImageWindow("new image",bt.BitmapImage);
			iw.Show();
			
			var r = Fft.Fft2D(bt.Red.ToMatrix().MapNew(i => (double)i)).MapNew(i => (byte)(Math.Pow(i.x, 2) - Math.Pow(i.y, 2))).ToArray();
			var g = Fft.Fft2D(bt.Red.ToMatrix().MapNew(i => (double)i)).MapNew(i => (byte)(Math.Pow(i.x, 2) - Math.Pow(i.y, 2))).ToArray();
			var b = Fft.Fft2D(bt.Red.ToMatrix().MapNew(i => (double)i)).MapNew(i => (byte)(Math.Pow(i.x, 2) - Math.Pow(i.y, 2))).ToArray();

			var z = bt.Red;
			var x = z.ToMatrix().MapNew(i => Convert.ToDouble(i));
			var y = Fft.Fft2D(z.ToMatrix().MapNew(Convert.ToDouble));
			BitmapTransformer btf = new BitmapTransformer(r,g,b);
			ImageWindow iwf = new ImageWindow("fft",btf.BitmapImage);
			iwf.Show();

		}
	}
}
