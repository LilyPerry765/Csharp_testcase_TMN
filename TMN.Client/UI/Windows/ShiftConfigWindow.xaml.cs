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
    /// <summary>
    /// Interaction logic for ShiftConfigWindow.xaml
    /// </summary>
    public partial class ShiftConfigWindow : Window
    {
        public ShiftConfigWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var shifts = DB.Instance.Shifts.ToList();
            morningBox.Date = TS2DT(shifts.Single(s => s.ID == (int)Shifts.صبح).StartTime.Value);
            afterNoonBox.Date = TS2DT(shifts.Single(s => s.ID == (int)Shifts.بعدازظهر).StartTime.Value);
            nightBox.Date = TS2DT(shifts.Single(s => s.ID == (int)Shifts.شب).StartTime.Value);
        }

        private DateTime TS2DT(TimeSpan ts)
        {
            DateTime dt=DateTime.Today;
            return new DateTime(dt.Year, dt.Month, dt.Day, ts.Hours, ts.Minutes, ts.Seconds);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var db = DB.Instance;
            db.Shifts.Single(s => s.ID == 1).StartTime = morningBox.Date.Value.TimeOfDay;
            db.Shifts.Single(s => s.ID == 2).StartTime = afterNoonBox.Date.Value.TimeOfDay;
            db.Shifts.Single(s => s.ID == 4).StartTime = nightBox.Date.Value.TimeOfDay;
            db.SubmitChanges();
            DialogResult = true;
        }
    }
}
