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
            roomsDataGrid.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // user log
            //db.UserLogs.InsertOnSubmit(new UserLog
            //{
            //    ID = Guid.NewGuid(),
            //    CenterID = Center.Current.ID,
            //    UserID = User.Current.ID,
            //    Date = DateTime.Now.ToShortDateString(),
            //    Time = DateTime.Now.ToString("HH:mm:ss"),
            //    Action = "اضافه کردن سنسور",
            //    Description = ""
            //});

            db.SubmitChanges();
            Setting.Set(Setting.SENSOR_CHANGED + ConfiguringCenter.PointCode, "true");
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
                sensorsGrid.ItemsSource = db.Sensors.Where(s => s.Room == roomsDataGrid.SelectedItem as Room).OrderBy(s => s.ModulNumber);
            else
                sensorsGrid.ItemsSource = null;
        }

        private void sensorsGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newSensor = e.NewItem as Sensor;
            newSensor.ID = Guid.NewGuid();
            newSensor.Room = roomsDataGrid.SelectedItem as Room;
            //newSensor.ModulNumber = (roomsDataGrid.SelectedItem as Room).Sensors.Count ;
            //if (newSensor.ModulNumber >= 1 && newSensor.ModulNumber <= 3)
            //    newSensor.SensorType = SensorTypes.Temperature;
            //else if (newSensor.ModulNumber == 4)
            //    newSensor.SensorType = SensorTypes.Humidity;
            //else if (newSensor.ModulNumber > 100)
            //    newSensor.SensorType = SensorTypes.Circuit;
            //else if (newSensor.ModulNumber >= 11 && newSensor.ModulNumber <= 18)
            //    newSensor.SensorType = SensorTypes.POWER;
            //else
            //    newSensor.SensorType = SensorTypes.NONE;
        }

    }
}
