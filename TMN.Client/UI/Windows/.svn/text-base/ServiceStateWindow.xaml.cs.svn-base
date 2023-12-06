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
using System.Threading;

namespace TMN.UI.Windows
{


    public partial class ServiceStateWindow : Window
    {
        System.Timers.Timer refreshTimer = new System.Timers.Timer(5000);
        private Center selectedCenter;
        private ServiceTypes? serviceType = null;

        public ServiceStateWindow()
        {
            InitializeComponent();
            refreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(refreshTimer_Elapsed);
        }

        public ServiceStateWindow(ServiceTypes type)
        {
            serviceType = type;
            InitializeComponent();
            refreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(refreshTimer_Elapsed);
        }

        public ServiceStateWindow(Center selectedCenter)
            : this()
        {
            this.selectedCenter = selectedCenter;
            Title += selectedCenter.DisplayName;
            centerColumn.Visibility = System.Windows.Visibility.Collapsed;
        }

        void refreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            refreshTimer.Stop();
            Refresh();
            refreshTimer.Start();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
            refreshTimer.Start();
        }

        private void refreshMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Thread(Refresh).Start();
        }

        private void Refresh()
        {
            IEnumerable<ServiceState> states = null;
            if (serviceType != null)
                states = DB.Instance.ServiceStates.Where(s => s.ServiceType == (int)ServiceTypes.CircuitService).ToArray().Where(s => !s.IsConnected).OrderBy(ss => ss.ServiceType);
            else if (selectedCenter == null)
            {
                states = DB.Instance.ServiceStates.ToArray().Where(s => !s.IsConnected);
            }
            else
            {
                states = DB.Instance.ServiceStates.Where(s => s.CenterID == selectedCenter.ID).ToArray().Where(s => !s.IsConnected).OrderBy(ss => ss.ServiceType);
            }
            Dispatcher.Invoke(new Action(() =>
            {
                int index = dataGrid.SelectedIndex;
                dataGrid.ItemsSource = states;
                dataGrid.SelectedIndex = index;
            }));
        }

        private void autoRefreshMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            refreshTimer.Start();
        }

        private void autoRefreshMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            refreshTimer.Stop();
        }

        private void viewDetailsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                string details = (dataGrid.SelectedItem as ServiceState).TechnicalReport;
                if (string.IsNullOrWhiteSpace(details))
                {
                    MessageBox.ShowInfo("اطلاعات بيشتری موجود نيست.", "توضيحات فنی");
                }
                else
                {
                    new Window()
                    {
                        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                        Owner = this,
                        Title = "توضيحات فنی",
                        Content = new TextBox()
                        {
                            IsReadOnly = true,
                            Text = details
                        }
                    }.ShowDialog();
                }
            }
        }
    }
}
