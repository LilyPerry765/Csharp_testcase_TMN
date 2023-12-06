using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Enterprise;


namespace TMN.UI.Windows
{
    public partial class ArchiveWindow : Window
    {
		LogAlarmPaging paging;
		ToolBar toolbar;

        private void ExportText()
        {
			try
			{
				Logger.WriteInfo("Exporting Text from ArchiveAlarm");

				Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
				{
					Filter = "Text Files(*.txt) | *.txt",
					Title = "ذخیره فایل",
					FileName = "LogAlarm"
				};

				if (dialog.ShowDialog() == true)
				{
					if (IsSearchMode)
						Export.ToText(paging.CalculateSearch(Center.Selected.ID, txtSearch.Text), dialog.FileName);
					else
						Export.ToText(paging.CalculateNormal(Center.Selected.ID), dialog.FileName);
				}
			}
			catch (Exception)
			{
				Logger.WriteError("Can not Export Text from ArchiveAlarm");
			}
        }

        private void ExportExcel()
        {
			//try
			//{
			//    Logger.WriteInfo("Exporting Excel from ArchiveAlarm");
			//    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
			//    {
			//        Filter = "Excel Files(*.xls) | *.xls",
			//        Title = "ذخیره فایل",
			//        FileName = "LogAlarm"
			//    };

			//    if (dialog.ShowDialog() == true)
			//    {
			//        if (LogAlarmPaging.IsSearchMode)
			//            Export.ToExcel(paging.CalculateSearch(Center.Selected.ID, txtSearch.Text), dialog.FileName);
			//        else
			//            Export.ToExcel(paging.CalculateNormal(Center.Selected.ID), dialog.FileName);
			//    }
			//}
			//catch (Exception)
			//{
			//    Logger.WriteError("Can not Export Excel from ArchiveAlarm");
			//}
        }

		private void ExportXML()
		{
			try
			{
				Logger.WriteInfo("Exporting XML from ArchiveAlarm");

				Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
				{
					Filter = "XML Files(*.xml) | *.xml",
					Title = "ذخیره فایل",
					FileName = "LogAlarm"
				};

				if (dialog.ShowDialog() == true)
				{
					if (IsSearchMode)
						Export.ToXML(paging.CalculateSearch(Center.Selected.ID, txtSearch.Text), dialog.FileName);
					else
						Export.ToXML(paging.CalculateNormal(Center.Selected.ID), dialog.FileName);
				}
			}
			catch (Exception)
			{
				Logger.WriteError("Can not Export XML from ArchiveAlarm");
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
            if (IsSearchMode)
                paging.CalculateSearch(Center.Selected.ID, txtSearch.Text);
            else
                paging.CalculateNormal(Center.Selected.ID);

            datePicker_CalendarClosed(null, null);
        }

        public ArchiveWindow()
        {
            InitializeComponent();
            Center.SelectedChanged += new Action(Center_SelectedChanged);

			paging = new LogAlarmPaging();
            paging.PageSize = int.Parse(txtPageSize.Text);
            paging.PageNumber = 1;
            IsSearchMode = false;

            toolbar = CreateToolbar();

            txtPageSize.Text = TextSettings.Get("PAGE_SIZE", "20");
        }

		 void btnExportText_Click(object sender, RoutedEventArgs e)
		{
			if (listView.ItemsSource != null)
				ExportText();
		}

         void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            if (listView.ItemsSource != null)
                ExportExcel();
        }

		void btnExportXML_Click(object sender, RoutedEventArgs e)
		{
			if (listView.ItemsSource != null)
				ExportXML();
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

				paging.Database = string.Format("TMN_{0}{1}{2}{3}", year, month, day, ".mdf");

				GetData();
            }
        }

        private void GetData()
        {
			Logger.WriteInfo("Getting ArchiveAlarm data");

			paging.PageNumber = 1;

			LogAlarms = paging.GetRecords(Center.Selected.ID,IsSearchMode , txtSearch.Text);
			if (LogAlarms != null)
			{
				listView.ItemsSource = LogAlarms;
				txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
			}
        }

        private void btnFirstRecord_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void txtSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
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

        private void btnNextRecord_Click(object sender, RoutedEventArgs e)
        {
			Logger.WriteInfo("Getting ArchiveAlarm data");

            paging.PageNumber++;
			LogAlarms = paging.GetRecords(Center.Selected.ID, IsSearchMode, txtSearch.Text);

            if (LogAlarms != null)
            {
				if (paging.PageNumber < paging.TotalPage)
                {
					listView.ItemsSource = LogAlarms;
					txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
                }
            }
        }

        private void btnPrevRecord_Click(object sender, RoutedEventArgs e)
        {
			Logger.WriteInfo("Getting ArchiveAlarm data");

            paging.PageNumber--;
            LogAlarms = paging.GetRecords(Center.Selected.ID,IsSearchMode , txtSearch.Text);

            if (LogAlarms != null)
            {
                if (paging.PageNumber > 1)
                {
					listView.ItemsSource = LogAlarms;
					txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
                }
            }
        }

        private void btnLastRecord_Click(object sender, RoutedEventArgs e)
        {
			Logger.WriteInfo("Getting ArchiveAlarm data");

			paging.PageNumber = paging.TotalPage;
			LogAlarms = paging.GetRecords(Center.Selected.ID, IsSearchMode, txtSearch.Text);

            if (LogAlarms != null)
            {
				listView.ItemsSource = LogAlarms;
				txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
            }
        }

        private void txtPageSize_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtPageSize.Text == string.Empty || !StringExtensions.IsNumber(txtPageSize.Text))
                return;

            if (e.Key == Key.Enter)
            {
                paging.PageSize = int.Parse(txtPageSize.Text);
                TextSettings.Set("PAGE_SIZE", txtPageSize.Text);

                if (IsSearchMode)
                    paging.CalculateSearch(Center.Selected.ID, txtSearch.Text);
                else
                    paging.CalculateNormal(Center.Selected.ID);

                GetData();
            }
        }

        private void txtPosition_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtPosition.Text == string.Empty || !StringExtensions.IsNumber(txtPosition.Text))
                return;

            if (e.Key == Key.Enter)
            {
                paging.PageNumber = int.Parse(txtPosition.Text);

				LogAlarms = paging.GetRecords(Center.Selected.ID, IsSearchMode, txtSearch.Text);
				listView.ItemsSource = LogAlarms;

				txtPosition.Text = string.Format("صفحه ي {0} از {1}", paging.PageNumber, paging.TotalPage);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			new ArchiveView(LogAlarms, listView.SelectedIndex).ShowDialog();
        }

        private void listView_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.toolbarTray.ToolBars.Add(toolbar.Extract());
        }

        private void listView_Unloaded(object sender, RoutedEventArgs e)
        {
            toolbar.Extract();
        }


		public List<ArchiveService.LogAlarm> LogAlarms
		{
			get;
			set;
		}

		public bool IsSearchMode
		{
			get;
			set;
		}
    }
}