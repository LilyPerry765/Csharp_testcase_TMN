using System.Windows;

namespace TMN
{


    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        ServiceCore core = new ServiceCore();

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            core.Start(new string[] { });
        }

        private void StoptButton_Click(object sender, RoutedEventArgs e)
        {
            core.Stop();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }
    }
}
