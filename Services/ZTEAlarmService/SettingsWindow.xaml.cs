using System;
using System.Linq;
using System.ServiceProcess;
using System.Windows;
using Enterprise;
using TMN;

namespace ZTEAlarmService
{

	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
			Logger.WriteInfo("Loading settings...");
			LoadData();
		}

		private void LoadData()
		{
			using (TMNModelDataContext db = new TMNModelDataContext())
			{
				cmbCenters.ItemsSource = db.Centers.OrderBy(c => c.Name);
			}
			cmbCenters.SelectedValue = Center.CurrentCenterID;
			txtIP.Text = (string)RegSettings.Get("ZTESqlServerIP");
			txtUserName.Text = (string)RegSettings.Get("ZTESqlServerUser");
			txtPassword.Text = (string)RegSettings.Get("ZTESqlServerPass");
			ConnectionMethod = (string)RegSettings.Get("ZTEConnectionMethod");
		}

		private void RestrtService()
		{
			try
			{
				using (ServiceController svc = new ServiceController("ZTEAlarmService"))
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
				Logger.Write(ex, "TMN ZTE Alarm Service");
				TMN.MessageBox.ShowError("راه اندازی سرويس با مشکل مواجه شد.");
			}
		}

		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			Center.CurrentCenterID = (Guid)cmbCenters.SelectedValue;
			RegSettings.Save("ZTESqlServerIP", txtIP.Text.Trim());
			RegSettings.Save("ZTESqlServerUser", txtUserName.Text.Trim());
			RegSettings.Save("ZTESqlServerPass", txtPassword.Text.Trim());
			RegSettings.Save("ZTEConnectionMethod", ConnectionMethod);
			Logger.WriteInfo("Settings saved. Restarting service...");
			RestrtService();		
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private string ConnectionMethod
		{
			get
			{
				if (windowsComboBoxItem.IsSelected)
					return ConnectionType.WindowsAuthentication.ToString();
				else
					return ConnectionType.SQLAuthentication.ToString();
			}
			set
			{
				if (value == ConnectionType.SQLAuthentication.ToString())
					sqlComboBoxItem.IsSelected = true;
				else
					windowsComboBoxItem.IsSelected = true;
			}
		}
	}
}
