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
using TMN;
using System.ServiceProcess;
using Enterprise;

namespace MessageSenderService
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            txtPortName.Text = (string)RegSettings.Get("MessageSenderPortName");
            txtTimeInterval.Text = (string)RegSettings.Get("MessageSenderTimerInterval");
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RegSettings.Save("MessageSenderPortName", txtPortName.Text.Trim());
            RegSettings.Save("MessageSenderTimerInterval", txtTimeInterval.Text.Trim());
            Logger.WriteInfo("set values successfuly ...");

            RestartService();
        }

        private void RestartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController("MessageSenderService"))
                {
                    if (svc.CanStop)
                        svc.Stop();
                    while (svc.Status != ServiceControllerStatus.Stopped)
                        svc.Refresh();
                    svc.Start();
                }
                TMN.MessageBox.ShowInfo("تغييرات با موفقيت ثبت و سرويس مجددا راه اندازی شد", "ثبت تغييرات");
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TMN MessageSenderService");
                TMN.MessageBox.ShowError("راه اندازی سرويس با مشکل مواجه شد.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            new SendMessageWindow().ShowDialog();
        }
    }
}
