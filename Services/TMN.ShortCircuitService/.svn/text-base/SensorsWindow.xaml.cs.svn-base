using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TMN.UI.Windows
{
    public partial class CircuitsWindow : Window
    {
        TMNModelDataContext db = new TMNModelDataContext();

        public CircuitsWindow()
        {
            InitializeComponent();
            roomsDataGrid.ItemsSource = db.Rooms.Where(r => r.Center == ConfiguringCenter);
            roomsDataGrid.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            db.SubmitChanges();
            Close();
        }

        private void roomsDataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newRoom = e.NewItem as Room;
            newRoom.ID = Guid.NewGuid();
            newRoom.CenterID = ConfiguringCenter.ID;
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
                circuitsGrid.ItemsSource = db.Sensors.Where(s => s.Room == roomsDataGrid.SelectedItem as Room).OrderBy(s => s.ModulNumber);
            else
                circuitsGrid.ItemsSource = null;
        }

        private void circuitsGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newCircuit = e.NewItem as Sensor;
            newCircuit.ID = Guid.NewGuid();
            newCircuit.Room = roomsDataGrid.SelectedItem as Room;
            newCircuit.ModulNumber = (roomsDataGrid.SelectedItem as Room).Sensors.Count ;
            if (newCircuit.ModulNumber >= 1 && newCircuit.ModulNumber <= 3)
                newCircuit.SensorType = SensorTypes.Temperature;
            else if (newCircuit.ModulNumber == 4)
                newCircuit.SensorType = SensorTypes.Humidity;
            else if (newCircuit.ModulNumber > 100)
                newCircuit.SensorType = SensorTypes.Circuit;
            else if (newCircuit.ModulNumber >= 11 && newCircuit.ModulNumber <= 18)
                newCircuit.SensorType = SensorTypes.POWER;
            else
                newCircuit.SensorType = SensorTypes.NONE;
        }

    }
}
