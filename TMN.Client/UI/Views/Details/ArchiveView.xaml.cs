using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace TMN.UI.Windows
{
    public partial class ArchiveView : Window
    {
        ICollectionView cv;

        public ArchiveView()
        {
            InitializeComponent();
        }

		public ArchiveView(List<ArchiveService.LogAlarm> logs,int position)
		{
			InitializeComponent();

			this.CurrentLogAlarms = logs;
			this.CurrentPosition = position;
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			this.DataContext = CurrentLogAlarms;

			if (CurrentLogAlarms != null)
            {
				cv = CollectionViewSource.GetDefaultView(CurrentLogAlarms);
                cv.MoveCurrentToPosition(CurrentPosition);
                txtPosition.Text = (cv.CurrentPosition + 1).ToString();
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (cv == null)
                return;

            cv.MoveCurrentToFirst();
            txtPosition.Text = (cv.CurrentPosition + 1).ToString();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (cv == null || cv.CurrentPosition <= 0)
                return;

            cv.MoveCurrentToPrevious();
            txtPosition.Text = (cv.CurrentPosition + 1).ToString();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
			if (cv == null || cv.CurrentPosition > CurrentLogAlarms.Count) 
			return;

            cv.MoveCurrentToNext();
            txtPosition.Text = (cv.CurrentPosition + 1).ToString();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (cv == null)
                return;

            cv.MoveCurrentToLast();
            txtPosition.Text = (cv.CurrentPosition + 1).ToString();
        }

        private void txtPosition_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtPosition.Text == string.Empty || !StringExtensions.IsNumber(txtPosition.Text))
                return;

            if (e.Key == Key.Enter)
            {
                int current = int.Parse(txtPosition.Text);

				if (current > CurrentLogAlarms.Count)
                    cv.MoveCurrentToLast();
                else if (current <= 1)
                    cv.MoveCurrentToFirst();
                else
                    cv.MoveCurrentToPosition(current - 1);
            }
        }

		public List<ArchiveService.LogAlarm> CurrentLogAlarms
		{
			get;
			set;
		}

		public int CurrentPosition
		{
			get;
			set;
		}
    }
}
