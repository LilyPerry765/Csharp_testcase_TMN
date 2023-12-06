using System.Windows;

namespace ZTEAlarmService
{
	public partial class DebugWindow : Window
	{
		ServiceCore core = new ServiceCore();

		public DebugWindow()
		{
			InitializeComponent();
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			core.Start();
		}

		private void StopButton_Click(object sender, RoutedEventArgs e)
		{
			core.Stop();
		}

		private void SettingButton_Click(object sender, RoutedEventArgs e)
		{
			new SettingsWindow().ShowDialog();
		}
	}
}
