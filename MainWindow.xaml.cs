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
using Reactive.Bindings;
using System.Reactive.Linq;


namespace mmm
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = this;
			
		}
		//async void aaa()
		//{
		//	top = await Observable.Interval( TimeSpan.FromSeconds(1) ).Select( x => (int)x );
		//	top = await Observable.Interval( TimeSpan.FromSeconds(1) ).Select( x => (int)x );
		//	top = await Observable.Interval( TimeSpan.FromSeconds(1) ).Select( x => (int)x );
		//}

		//public int top { get; set; } = 100;
		public IReactiveProperty<int> top { get; set; }
			= Observable.Interval( TimeSpan.FromSeconds( 1 ) ).Select( x => (int)x*5 ).ToReactiveProperty();
	}
}
