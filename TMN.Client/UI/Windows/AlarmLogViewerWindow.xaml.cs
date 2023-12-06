using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Enterprise;

namespace TMN.UI.Windows
{

    public partial class AlarmLogViewerWindow : Window
    {
        private AlarmSeverities? severity;
        private Guid? alarmID;
        private bool postPone = false;

        public AlarmLogViewerWindow(AlarmSeverities sevirity)
        {
            InitializeComponent();

            this.severity = sevirity;
            LoadAlarm();
        }

        public AlarmLogViewerWindow(bool postpone)
        {
            InitializeComponent();

            oldAlarmsCheckbox.Visibility = System.Windows.Visibility.Hidden;
            postPoneAlarmsCheckbox.Visibility = System.Windows.Visibility.Hidden;
            postPoneButton.Visibility = System.Windows.Visibility.Hidden;
            this.postPone = postpone;
            LoadAlarm();
        }

        public AlarmLogViewerWindow(Guid alarmID)
        {
            InitializeComponent();
            // When there is only one alarm, the check box doesn't make sence.
            oldAlarmsCheckbox.Visibility = System.Windows.Visibility.Hidden;
            postPoneAlarmsCheckbox.Visibility = System.Windows.Visibility.Hidden;
            this.alarmID = alarmID;
            LoadAlarm();
        }

        private void LoadAlarm()
        {
            Cursor = Cursors.Wait;
            IEnumerable<TMN.LogAlarm> alarms;
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                if (alarmID == null && postPone == false)
                {
                    //alarms = db.LogAlarms.Where(a => a.CenterID == Center.Selected.ID && (oldAlarmsCheckbox.IsChecked == true || a.IsRead == false) && (postPoneAlarmsCheckbox.IsChecked == false || (AlarmSeverities)(a.Severity + 10) == severity) && (postPoneAlarmsCheckbox.IsChecked == true || (AlarmSeverities)a.Severity == severity));

                    byte? ts = (postPoneAlarmsCheckbox.IsChecked == true) ? (byte?)(severity + 10) : (byte?)severity;
                    alarms = db.LogAlarms.Where(a => a.CenterID == Center.Selected.ID && (oldAlarmsCheckbox.IsChecked == true || a.IsRead == false) && a.Severity == ts);
                }
                else if (postPone == true)
                {
                    alarms = db.LogAlarms.Where(a => a.CenterID == Center.Selected.ID && (int)a.Severity >= 11 && (int)a.Severity <= 13 && a.IsRead == false);
                }
                else
                {
                    alarms = db.LogAlarms.Where(a => a.ID == alarmID.Value);
                }

                logsDataGrid.ItemsSource = new ObservableCollection<LogAlarm>(alarms.OrderByDescending(d => d.Time));

                countTextBlock.Text = string.Format("تعداد: {0}", logsDataGrid.Items.Count);
            }
            Cursor = Cursors.Arrow;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            GC.Collect();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
          //  if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.ALARMS_DELETE))
        //    {
                if (MessageBox.ShowQuestion("کليه آلارم های اين صفحه حذف شوند؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DeleteAlarms();
                    DialogResult = true;
                    Close();
                }
         //   }
         //   else
         //   {
         //       MessageBox.Show(MessageTypes.AccessDenied);
          //  }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void DeleteAlarms()
        {
            byte? ts = (postPoneAlarmsCheckbox.IsChecked == true) ? (byte?)(severity + 10) : (byte?)severity;
            new System.Threading.Thread((arg) =>
            {
                try
                {
                    using (TMNModelDataContext db = new TMNModelDataContext())
                    {
                        int count = 0;
                        if (alarmID == null && postPone == false)
                        {
                            count = db.ExecuteCommand("DELETE FROM LogAlarm  Where  Severity = {0} AND CenterID={1};", arg, Center.Selected.ID);

                            // user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmRemove, string.Format("Severity = {0}", arg));
                        }
                        else if (postPone == true)
                        {
                            count = db.ExecuteCommand("DELETE FROM LogAlarm  Where  Severity >= 11 AND Severity <= 13 AND CenterID={0};",  Center.Selected.ID); // taligh hazf shod

                            // user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmRemove, "remove postPone alarm");

                        }
                        else
                        {
                            count = db.ExecuteCommand("DELETE FROM LogAlarm  Where  ID={0};", alarmID);

                            //  user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmRemove, string.Format("ID = {0}", alarmID));
                        }
                        if (count > 0)
                        {
                            Logger.WriteInfo("{0} alarm(s) deleted.", count);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }).Start(ts);
        }

        private void AcknowledgeAlarms()
        {
            byte? ts = (postPoneAlarmsCheckbox.IsChecked == true) ? (byte?)(severity + 10) : (byte?)severity;
            new System.Threading.Thread((arg) =>
            {
                try
                {
                    Logger.WriteDebug("Marking alarm(s) as read because of being shown...");
                    using (TMNModelDataContext db = new TMNModelDataContext())
                    {
                        if (alarmID == null && postPone == false)
                        {
                            db.ExecuteCommand("UPDATE LogAlarm SET IsRead=1,  Severity = {2}  Where IsRead=0 AND Severity = {0} AND CenterID={1};", arg, Center.Selected.ID, (int)severity);

                            // user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                     Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmAccept, string.Format("Severity = {0}", (int)severity));
                        }
                        else if (postPone == true)
                        {
                            db.ExecuteCommand("UPDATE LogAlarm SET IsRead=1,  Severity = Severity % 10  Where IsRead=0 AND CenterID={1} AND Severity >= 11 AND Severity <= 13;", Center.Selected.ID); // az taligh kharej shod

                            // user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmAccept, "exit from postPone");
                        }
                        else
                        {
                            db.ExecuteCommand("UPDATE LogAlarm SET IsRead=1 Where ID={0};", alarmID);

                            //  user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmAccept, alarmID);
                        }




                        // user log
                        db.UserLogs.InsertOnSubmit(new UserLog
                        {
                            ID = Guid.NewGuid(),
                            CenterID = Center.Current.ID,
                            UserID = User.Current.ID,
                            Date = DateTime.Now,
                            Description = "تایید آلارم"
                        });

                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }

            }).Start(ts);
            
        }

        private void logTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  Search(SearchDirection.OnPosition);
            if (logsDataGrid.Items.CanFilter)
            {
                logsDataGrid.Items.Filter = (a) => (a as LogAlarm).Data.Contains(searchTextBox.Text);
                countTextBlock.Text = string.Format("تعداد: {0}", logsDataGrid.Items.Count);
            }
        }

        //private void Search(SearchDirection direction)
        //{
        //    int index = 0;
        //    if (!string.IsNullOrEmpty(searchTextBox.Text))
        //    {
        //        int start, count;
        //        switch (direction)
        //        {
        //            case SearchDirection.OnPosition:
        //                start = logTextBox.SelectionStart;
        //                count = logTextBox.Text.Length - start;
        //                index = logTextBox.Text.IndexOf(searchTextBox.Text, start, count, StringComparison.InvariantCultureIgnoreCase);
        //                break;
        //            case SearchDirection.Forward:
        //                start = logTextBox.SelectionStart + logTextBox.SelectionLength;
        //                count = logTextBox.Text.Length - start;
        //                index = logTextBox.Text.IndexOf(searchTextBox.Text, start, count, StringComparison.InvariantCultureIgnoreCase);
        //                break;
        //            case SearchDirection.Backward:
        //                start = logTextBox.SelectionStart - 1;
        //                count = start + 1;
        //                index = logTextBox.Text.LastIndexOf(searchTextBox.Text, start, count, StringComparison.InvariantCultureIgnoreCase);
        //                break;
        //            default:
        //                throw new NotSupportedException("Direction not supported.");
        //        }
        //    }
        //    if (index > -1)
        //    {
        //        logTextBox.Focus();
        //        logTextBox.Select(index, searchTextBox.Text.Length);
        //        Rect start = logTextBox.GetRectFromCharacterIndex(logTextBox.SelectionStart, true);
        //        Rect end;
        //        if (logTextBox.SelectedText.Contains(Environment.NewLine))
        //        {
        //            string selected = logTextBox.SelectedText;
        //            int length = selected.IndexOf(Environment.NewLine);
        //            end = logTextBox.GetRectFromCharacterIndex(logTextBox.SelectionStart + length, true);
        //        }
        //        else
        //        {
        //            end = logTextBox.GetRectFromCharacterIndex(logTextBox.SelectionStart + logTextBox.SelectionLength, true);
        //        }
        //        logTextBox.BringIntoView(Rect.Union(start, end));
        //        searchTextBox.Focus();
        //        searchTextBox.Foreground = Brushes.Black;
        //    }
        //    else
        //    {
        //        searchTextBox.Foreground = Brushes.Red;
        //    }
        //}

        private void searchNextButton_Click(object sender, RoutedEventArgs e)
        {
            //   Search(SearchDirection.Forward);
        }

        private void searchPrevButton_Click(object sender, RoutedEventArgs e)
        {
            //   Search(SearchDirection.Backward);
        }

        private enum SearchDirection
        {
            OnPosition,
            Forward,
            Backward
        }

        private void logTextBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //logTextBox.Focus();
        }

        ~AlarmLogViewerWindow()
        {
            Logger.WriteInfo("~AlarmLogViewerWindow");
        }

        private void oldAlarmsCheckbox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            postPoneButton.IsEnabled = !postPoneAlarmsCheckbox.IsChecked.Value && !oldAlarmsCheckbox.IsChecked.Value;
            LoadAlarm();
        }

        private void AckButton_Click(object sender, RoutedEventArgs e)
        {
            AcknowledgeAlarms();
            DialogResult = true;
            Close();
        }

        private void postPoneButton_Click(object sender, RoutedEventArgs e)
        {
            PostPoneAlarms();
            DialogResult = true;
            Close();
        }

        private void PostPoneAlarms()
        {
            new System.Threading.Thread(() =>
            {
                try
                {
                    Logger.WriteDebug("Marking alarm(s) as PostPone.");
                    using (TMNModelDataContext db = new TMNModelDataContext())
                    {
                        if (alarmID == null)
                        {
                            db.ExecuteCommand("UPDATE LogAlarm SET Severity = severity + 10 Where IsRead=0 AND Severity={0} AND CenterID={1};", (int)severity, Center.Selected.ID); // bar sevirity taligh

                            // user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmPostPone, string.Format("Severity = {0}", (int)severity));
                        }
                        else
                        {
                            db.ExecuteCommand("UPDATE LogAlarm SET Severity = severity + 10 Where ID={0};", alarmID);  // taligh mikonad

                            //  user log
                            db.ExecuteCommand("INSERT INTO [UserLog] ([ID],[CenterID],[UserID],[Date],[Action],[Description]) VALUES ({0},{1},{2},{3},{4},{5})",
                                               Guid.NewGuid(), Center.Current.ID, User.Current.ID, DateTime.Now, ActionType.AlarmPostPone, alarmID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }

            }).Start();
        }

    }
}
