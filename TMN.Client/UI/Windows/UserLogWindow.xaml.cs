using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Enterprise;
using TMN.UI.Windows;

namespace TMN
{
	public partial class UserLogWindow : Window
	{
		UserLogPaging paging;
		ToolBar toolbar;

		private void ExportText()
		{
			try
			{
				Logger.WriteInfo("Exporting Text from UserLog");
				Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
				{
					Filter = "Text Files(*.txt) | *.txt",
					Title = "ذخیره فایل",
					FileName = "UserLog"
				};

				if (dialog.ShowDialog() == true)
				{
					if (IsSearchMode)
						Export.ToText(paging.CalculateSearch(Center.Selected.ID, IsShowAllUserLog, txtSearch.Text), dialog.FileName);
					else
						Export.ToText(paging.CalculateNormal(Center.Selected.ID, IsShowAllUserLog), dialog.FileName);
				}
			}
			catch (Exception)
			{
				Logger.WriteError("Can not Export Text from UserLog");
			}
		}

		private void ExportExcel()
		{
			//try
			//{
			//    Logger.WriteInfo("Exporting Excel from UserLog");
			//    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
			//    {
			//        Filter = "Excel Files(*.xls) | *.xls",
			//        Title = "ذخیره فایل",
			//        FileName = "UserLog"
			//    };

			//    if (dialog.ShowDialog() == true)
			//    {
			//        if (UserLogPaging.IsSearchMode)
			//            Export.ToExcel(paging.CalculateSearch(Center.Selected.ID, txtSearch.Text), dialog.FileName);
			//        else
			//            Export.ToExcel(paging.CalculateNormal(Center.Selected.ID), dialog.FileName);
			//    }
			//}
			//catch (Exception)
			//{
			//    Logger.WriteError("Can not Export Excel from UserLog");
			//}
		}

		private void ExportXML()
		{
			try
			{
				Logger.WriteInfo("Exporting XML from UserLog");
				Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
				{
					Filter = "XML Files(*.xml) | *.xml",
					Title = "ذخیره فایل",
					FileName = "UserLog"
				};

				if (dialog.ShowDialog() == true)
				{
					if (IsSearchMode)
						Export.ToXML(paging.CalculateSearch(Center.Selected.ID,IsShowAllUserLog , txtSearch.Text), dialog.FileName);
					else
						Export.ToXML(paging.CalculateNormal(Center.Selected.ID,IsShowAllUserLog ), dialog.FileName);
				}
			}
			catch (Exception)
			{
				Logger.Write(LogType.Error, "Can not Export XML from UserLog");
			}
		}

		private ToolBar CreateToolbar()
		{
			const int imgSize = 25;
			ToolBar tb = new ToolBar();

			Button btnExportText = new Button()
			{
				ToolTip = "خروجی text",
				Content = new Image()
				{
					Width = imgSize,
					Height = imgSize,
					Source = ImageSourceHelper.GetImageSource("text.png")
				},
				ClickMode = ClickMode.Press
			};
			btnExportText.Click += new RoutedEventHandler(btnExportText_Click);


			Button btnExportExcel = new Button()
			{
				ToolTip = "خروجی excel",
				Content = new Image()
				{
					Width = imgSize,
					Height = imgSize,
					Source = ImageSourceHelper.GetImageSource("excel.png")
				},
				ClickMode = ClickMode.Press
			};
			btnExportExcel.Click += new RoutedEventHandler(btnExportExcel_Click);


			Button btnExportXML = new Button()
			{
				ToolTip = "خروجی xml",
				Content = new Image()
				{
					Width = imgSize,
					Height = imgSize,
					Source = ImageSourceHelper.GetImageSource("xml.png")
				},
				ClickMode = ClickMode.Press
			};
			btnExportXML.Click += new RoutedEventHandler(btnExportXML_Click);


			tb.Items.Add(btnExportText);
			tb.Items.Add(btnExportExcel);
			tb.Items.Add(btnExportXML);

			return tb;
		}

		void Center_SelectedChanged()
		{
			//if (UserLogPaging.IsSearchMode)
			//    paging.CalculateSearch(Center.Selected.ID, txtSearch.Text);
			//else
			//    paging.CalculateNormal(Center.Selected.ID);

			//datePicker_CalendarClosed(null, null);
		}


		public UserLogWindow()
		{
			InitializeComponent();

			comboBoxUsers.ItemsSource = DB.Instance.Users.OrderBy(u => u.UserName);

			//Center.SelectedChanged += new Action(Center_SelectedChanged);

			paging = new UserLogPaging();
			paging.PageSize = int.Parse(txtPageSize.Text);
			paging.PageNumber = 1;
			IsSearchMode = false;
			IsShowAllUserLog = false;


			toolbar = CreateToolbar();

			txtPageSize.Text = TextSettings.Get("PAGE_SIZE", "20");
		}

		void btnExportExcel_Click(object sender, RoutedEventArgs e)
		{
			if (listView.ItemsSource != null)
				ExportExcel();
		}

		void btnExportText_Click(object sender, RoutedEventArgs e)
		{
			if (listView.ItemsSource != null)
				ExportText();
		}

		void btnExportXML_Click(object sender, RoutedEventArgs e)
		{
			if (listView.ItemsSource != null)
				ExportXML();
		}

		private void GetData()
		{
			Logger.WriteInfo("Getting UserLog data");

			paging.PageNumber = 1;

			List<ArchiveService.UserLog> list = paging.GetRecords(Center.Selected.ID, IsSearchMode, IsShowAllUserLog, txtSearch.Text);
			if (list  != null)
			{
				listView.ItemsSource = list ;
				txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
			}
		}

		private void btnFirstRecord_Click(object sender, RoutedEventArgs e)
		{
			GetData();
		}

		private void btnNextRecord_Click(object sender, RoutedEventArgs e)
		{
			Logger.WriteInfo("Getting UserLog data");

			paging.PageNumber++;
			List<ArchiveService.UserLog> list = paging.GetRecords(Center.Selected.ID, IsSearchMode, IsShowAllUserLog, txtSearch.Text);

			if (list  != null)
			{
				if (paging.PageNumber < paging.TotalPage)
				{
					listView.ItemsSource = list ;
					txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
				}
			}
		}

		private void txtPosition_KeyDown(object sender, KeyEventArgs e)
		{
			if (txtPosition.Text == string.Empty || !StringExtensions.IsNumber(txtPosition.Text))
				return;

			if (e.Key == Key.Enter)
			{
				paging.PageNumber = int.Parse(txtPosition.Text);

				List<ArchiveService.UserLog> list = paging.GetRecords(Center.Selected.ID, IsSearchMode, IsShowAllUserLog, txtSearch.Text);
				listView.ItemsSource = list ;

				txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
			}
		}

		private void btnPrevRecord_Click(object sender, RoutedEventArgs e)
		{
			Logger.WriteInfo("Getting UserLog data");

			paging.PageNumber--;
			List<ArchiveService.UserLog> list = paging.GetRecords(Center.Selected.ID, IsSearchMode, IsShowAllUserLog, txtSearch.Text);

			if (list  != null)
			{
				if (paging.PageNumber > 1)
				{
					listView.ItemsSource = list ;
					txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
				}
			}
		}

		private void btnLastRecord_Click(object sender, RoutedEventArgs e)
		{
			Logger.WriteInfo("Getting UserLog data");

			paging.PageNumber = paging.TotalPage;
			List<ArchiveService.UserLog> list = paging.GetRecords(Center.Selected.ID, IsSearchMode, IsShowAllUserLog, txtSearch.Text);

			if (list  != null)
			{
				listView.ItemsSource = list  ;
				txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
			}
		}

		private void txtPageSize_KeyUp(object sender, KeyEventArgs e)
		{
			if (txtPageSize.Text == string.Empty || !StringExtensions.IsNumber(txtPageSize.Text))
				return;

			if (e.Key == Key.Enter)
			{
				paging.PageSize = int.Parse(txtPageSize.Text);
				TextSettings.Set("PAGE_SIZE", txtPageSize.Text);

				if (IsSearchMode)
					paging.CalculateSearch(Center.Selected.ID,IsShowAllUserLog , txtSearch.Text);
				else
					paging.CalculateNormal(Center.Selected.ID, IsShowAllUserLog);

				GetData();
			}
		}

		private void listView_Loaded(object sender, RoutedEventArgs e)
		{
			MainWindow.Instance.toolbarTray.ToolBars.Add(toolbar.Extract());
		}

		private void datePicker_CalendarClosed(object sender, RoutedEventArgs e)
		{
			if (datePicker.SelectedDate != null)
			{
				string day = string.Empty;
				string month = string.Empty;
				string year = datePicker.SelectedDate.Value.Year.ToString() + "_";

				if (datePicker.SelectedDate.Value.Month <= 9)
					month = "0" + datePicker.SelectedDate.Value.Month.ToString() + "_";
				else
					month = datePicker.SelectedDate.Value.Month.ToString() + "_";

				if (datePicker.SelectedDate.Value.Day <= 9)
					day = "0" + datePicker.SelectedDate.Value.Day.ToString();
				else
					day = datePicker.SelectedDate.Value.Day.ToString();

				paging.DatabaseName = string.Format("TMN_{0}{1}{2}{3}", year, month, day, ".mdf");

				GetData();
			}
		}

		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (txtSearch.Text == string.Empty)
				{
					IsSearchMode = false;

					GetData();
				}
				else
				{
					IsSearchMode = true;

					GetData();
				}
			}
		}

		private void listView_Unloaded(object sender, RoutedEventArgs e)
		{
			toolbar.Extract();
		}

		private void txtPosition_GotFocus(object sender, RoutedEventArgs e)
		{
			txtPosition.Text = string.Empty;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void comboBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (allUserCheckBox.IsChecked == true)
			{
				allUserCheckBox.IsChecked = false;
				IsShowAllUserLog = false;
			}

			if (IsSearchMode)
				paging.CalculateSearch((Guid)comboBoxUsers.SelectedValue, IsShowAllUserLog, txtSearch.Text);
			else
				paging.CalculateNormal((Guid)comboBoxUsers.SelectedValue, IsShowAllUserLog);
		}

		private void allUserCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			if (allUserCheckBox.IsChecked == true)
			{
				IsShowAllUserLog = true;

				paging.GetRecords(User.Current.ID, IsSearchMode, IsShowAllUserLog, txtSearch.Text);
			}
		}

		public  bool IsSearchMode
		{
			get;
			set;
		}

		public  bool IsShowAllUserLog
		{
			get;
			set;
		}
	}
}
