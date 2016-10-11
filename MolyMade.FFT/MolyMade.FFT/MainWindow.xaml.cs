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
			Transformer t = new Transformer();
			var x = t.Fft2D(new Matrix<double>(new double[,]
			{
				{1, 2, 3, 4, 5, 6, 7},
				{1, 2, 3, 4, 5, 6, 7}
			}));
			var y = t.Fft2DInv(x);
		}
	}
}
