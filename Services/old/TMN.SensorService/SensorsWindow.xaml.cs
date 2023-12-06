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

namespace TMN.UI.Windows
{


    public partial class SensorsWindow : Window
    {
        TMNModelDataContext db = new TMNModelDataContext();

        public SensorsWindow()
        {
            InitializeComponent();
            roomsDataGrid.ItemsSource = db.Rooms.Where(r => r.Center == ConfiguringCenter);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            db.SubmitChanges();
            Close();
        }

        private void roomsDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newRomm = e.NewItem as Room;
            newRomm.ID = Guid.NewGuid();
            newRomm.CenterID = ConfiguringCenter.ID;
        }

        private Center ConfiguringCenter
        {
            get
            {
                //Todo: If the current user doesn't have permission of omc user, he can only configure his current center.
                // Configure selected center so that setting can be done from OMC server. If no center is celected, configures current center.
                return Center.Selected ?? Center.Current;
            }
        }

        private void roomsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomsDataGrid.SelectedItem is Room)
                sensorsGrid.ItemsSource = (roomsDataGrid.SelectedItem as Room).Sensors;
            else
                sensorsGrid.ItemsSource = null;
        }

        private void sensorsGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newSensor = e.NewItem as Sensor;
            newSensor.ID = Guid.NewGuid();

        }

    }
}
