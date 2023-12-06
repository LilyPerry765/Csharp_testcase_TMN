using System.Windows;

namespace TMN
{


    public partial class DebugWindow : Window
    {
        // HuaweiAlarmReceiver hReceiver = new HuaweiAlarmReceiver("192.168.0.10", "omc", "pendar1");
        ServiceCore serviceCore = new ServiceCore();

        public DebugWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            serviceCore.Start(startParamsTextBox.Text.Split(' '));
        }

        private void StoptButton_Click(object sender, RoutedEventArgs e)
        {
            serviceCore.Stop();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            serviceCore.Start(SwitchLogTextBox.Text);
        }


    }
}
