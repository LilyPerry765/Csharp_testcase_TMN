using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Input;
using Enterprise;
using System.Threading;
using System.ServiceModel;

namespace TMN
{

    public partial class MainWindow : Window
    {
        TMNModelDataContext db = new TMNModelDataContext();
        public MainWindow()
        {
            InitializeComponent();
            Title += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Refresh();
        }

        private void Refresh()
        {
            //using (var db = new TMNModelDataContext())
            {
                servicesDataGrid.ItemsSource = db.ServiceStates.Where(ss => ss.Center.SwitchType.Name.ToUpper().Contains(switchTextBox.Text.Trim().ToUpper())).ToArray().Where(ss => ss.ServiceName.ToLower().Contains(serviceTextBox.Text.Trim().ToLower()));
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            SelectedService.Center.Connect("administrator", "pendar110");
        }

        private ServiceState SelectedService
        {
            get
            {
                return servicesDataGrid.SelectedItem as ServiceState;
            }
        }

        private void StatusBar_Drop(object sender, DragEventArgs e)
        {
            try
            {
                string path = ((string[])e.Data.GetData("FileNameW"))[0];
                if (path != null)
                {
                    var assembly = Assembly.ReflectionOnlyLoadFrom(path);
                    var fileName = System.IO.Path.GetFileName(path);
                    versionFinderLabel.Text = string.Format("{0}   {1}", fileName, assembly.GetName().Version);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                versionFinderLabel.Text = "Invalid operation, check log for details.";
            }
        }

        private void startMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RunServiceCommand(ServiceCommand.Start);
        }

        private void stopMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RunServiceCommand(ServiceCommand.Stop);
        }


        private void SendDataForAccountCreatorService(string serverusername, string serverpassword)
        {
            try
            {
                string ip = ((ServiceState)servicesDataGrid.SelectedItem).Center.IPAddress;
                string port = Setting.Get(Setting.ACCOUNT_CREATOR_PORT, Setting.DEFAULT_ACCOUNT_CREATOR_PORT);
              //  string ip = this.IPAddress;

                NetTcpBinding binding = new NetTcpBinding();
                EndpointAddress ndPoint = new EndpointAddress(string.Format("net.tcp://{0}:{1}/MainService", ip, port));

                ChannelFactory<TMN.IMainService> channel = new ChannelFactory<TMN.IMainService>(binding);
                TMN.IMainService proxy = channel.CreateChannel(ndPoint);

                proxy.UpgradeService(serverusername, serverpassword);
                Logger.WriteInfo("send message to UpgradeService .");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                RunServiceCommand(ServiceCommand.Upgrade);
            }
        }

        private void upgradeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //RunServiceCommand(ServiceCommand.Upgrade);


            SendDataForAccountCreatorService(Setting.UpdaterUserName, Setting.UpdaterPassword);

        }

        private void RunServiceCommand(ServiceCommand command)
        {
            foreach (ServiceState item in servicesDataGrid.SelectedItems)
            {
                item.Status = "درحال " + command.ToString() + "...";
                ExecCommandAsync(command, item);
            }
        }

        private void ExecCommandAsync(ServiceCommand command, ServiceState SelectedService)
        {
            var thisService = SelectedService;

            string expMessage = null;
            new Thread((ctrl) =>
            {
                string result;
                try
                {

                    //Impersonation.TryImpersonate(thisService.Center.UserName, thisService.Center.Password, thisService.Center.IPAddress); 
                    ServiceController controller = ctrl as ServiceController;
                    switch (command)
                    {
                        case ServiceCommand.Start:
                            if (controller.Status != ServiceControllerStatus.Running)
                            {
                                controller.Start();
                                controller.WaitForStatus(ServiceControllerStatus.Running);
                            }
                            break;
                        case ServiceCommand.Stop:
                            if (controller.Status != ServiceControllerStatus.Stopped)
                            {
                                controller.Stop();
                                controller.WaitForStatus(ServiceControllerStatus.Stopped);
                            }
                            break;
                        case ServiceCommand.Upgrade:
                            if (SelectedService.Controller.Status == ServiceControllerStatus.Running)
                            {
                                controller.Stop();
                                controller.WaitForStatus(ServiceControllerStatus.Stopped);
                            }
                            controller.Start(new string[] { "/upgrade" });
                            controller.WaitForStatus(ServiceControllerStatus.Running);
                            break;
                    }
                    result = "انجام شد";
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                    result = "خطا";
                    expMessage = ex.Message;
                }
                Dispatcher.Invoke((Action)delegate()
               {
                   thisService.Status = result;
                   if (expMessage != null)
                       MessageBox.ShowError(expMessage);
               });
            }).Start(SelectedService.Controller);
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                SelectedService.Controller.Refresh();
                startMenuItem.IsEnabled = SelectedService != null && SelectedService.Controller.Status == ServiceControllerStatus.Stopped;
                stopMenuItem.IsEnabled = SelectedService != null && SelectedService.Controller.Status == ServiceControllerStatus.Running;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.ShowError("به دليل عدم ارتباط با مرکز امکان کنترل سرويس وجود ندارد");
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        enum ServiceCommand
        {
            Start,
            Stop,
            Upgrade
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }


    }


}
