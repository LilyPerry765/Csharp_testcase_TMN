using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TMN
{
	public partial class CircuitLogWindow : Window
	{
		public CircuitLogWindow()
		{
			InitializeComponent();

			FillData();
		}

		private void FillData()
		{
			listView.ItemsSource = DB.Instance.UserLogs.Where(u => u.Action == "33" || u.Action == "32");
		}

		private void FillData(DateTime  date)
		{
			listView.ItemsSource = DB.Instance.UserLogs.Where(u => (u.Date == date) && (u.Action == "33" || u.Action == "32"));
		}

		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{

		}

		private void datePicker_CalendarClosed(object sender, RoutedEventArgs e)
		{

		}
	}
}
