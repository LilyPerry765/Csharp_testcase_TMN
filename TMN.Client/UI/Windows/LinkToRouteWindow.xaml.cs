using System.Linq;
using System.Windows;

namespace TMN.UI.Windows
{

    public partial class LinkToRouteWindow : Window
    {
        public LinkToRouteWindow()
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
            cmbRoute.ItemsSource = DB.Instance.Routes.Where(r => r.Center == Center.Current);
        }

        public Route Route
        {
            get
            {
                return (Route)cmbRoute.SelectedItem;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
