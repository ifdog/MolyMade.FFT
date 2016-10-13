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
using System.Windows.Shapes;

namespace MolyMade.FFT
{
	/// <summary>
	/// ImageWindow.xaml 的交互逻辑
	/// </summary>
	public partial class ImageWindow : Window
	{
		public ImageWindow(string title, BitmapImage image)
		{
			InitializeComponent();
			this.Title = title;
			this.image.Source = image;
			this.image.Width = image.Width;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
