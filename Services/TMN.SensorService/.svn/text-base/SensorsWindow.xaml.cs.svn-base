using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TMN.UI.Windows
{
    public partial class SensorsWindow : Window
    {
        TMNModelDataContext db = new TMNModelDataContext();

        public SensorsWindow()
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
                sensorsGrid.ItemsSource = db.Sensors.Where(s => s.Room == roomsDataGrid.SelectedItem as Room).OrderBy(s => s.ModulNumber);
            else
                sensorsGrid.ItemsSource = null;
        }

        private void sensorsGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newSensor = e.NewItem as Sensor;
            newSensor.ID = Guid.NewGuid();
            newSensor.Room = roomsDataGrid.SelectedItem as Room;
            newSensor.ModulNumber = (roomsDataGrid.SelectedItem as Room).Sensors.Count ;
            if (newSensor.ModulNumber >= 1 && newSensor.ModulNumber <= 3)
                newSensor.SensorType = SensorTypes.Temperature;
            else if(newSensor.ModulNumber == 4)
                newSensor.SensorType = SensorTypes.Humidity;
            else if (newSensor.ModulNumber > 100)
                newSensor.SensorType = SensorTypes.Circuit;
            else  if (newSensor.ModulNumber >= 11 && newSensor.ModulNumber <= 18)
                newSensor.SensorType = SensorTypes.POWER;
            else
                newSensor.SensorType = SensorTypes.NONE;
        }

    }
}
