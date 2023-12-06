using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System;
using Enterprise;
using System.ComponentModel;
using System.Windows.Input;

namespace TMN.UI.Windows
{

    public partial class ImportWindow : Window
    {
        private bool importStarted = false;

        public ImportWindow()
        {
            InitializeComponent();
        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Log Files(*.txt;*.log)|*.txt;*.log|All Files(*.*)|*.*"
            };
            if (dlg.ShowDialog() == true)
            {
                PathTextBox.Text = dlg.FileName;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Import())
            {
                Logger.WriteInfo("StatTrunk Import finished successfully.");
                MessageBox.ShowInfo("دريافت اطلاعات با موفقيت انجام شد.", "دريافت اطلاعات");
                DialogResult = true;
            }
            else if (importStarted)
            {
                Logger.WriteWarning("Data import failed.");
                MessageBox.ShowError("دريافت اطلاعات متوقف شد.");
            }
            progressBar.Value = 0;
        }

        private bool Import()
        {
            try
            {
                if (!File.Exists(PathTextBox.Text))
                {
                    MessageBox.ShowError("آدرس فايل معتبر نمی باشد.");
                    return false;
                }
                string content = File.ReadAllText(PathTextBox.Text.Trim(), Encoding.Unicode);
                if (!ValidateFile(content))
                    return false;

                importStarted = true;
                Cursor = Cursors.Wait;
                btnOK.IsEnabled = false;
                MatchCollection matches = Regex.Matches(content, @"(?<TGNO>\w+)\s+(?<LNO>\d+)\s+((?<CIC>\d+)-\s*\d+)?\s+\w+\s+(?<OPM>\w+)\s+(?<LTG>\d+-\s*\d+)\s+(?<DIU>\d+)-\s*(?<TS>\d+)");
                progressBar.Maximum = matches.Count;
                progressLabel.Content = "لطفا چند لحظه منتظر باشيد...";

                Cursor = Cursors.Arrow;
                StatTrunkImporter importer = new StatTrunkImporter();
                Logger.WriteInfo("Importing rows...");
                foreach (Match match in matches)
                {
                    importer.ImportRow(
                       match.Groups["TGNO"].Value,
                       int.Parse(match.Groups["LNO"].Value),
                       match.Groups["CIC"].Success ? (int?)int.Parse(match.Groups["CIC"].Value.Trim()) : null,
                       byte.Parse(match.Groups["TS"].Value.Trim()),
                       match.Groups["OPM"].Value,
                       match.Groups["LTG"].Value.Replace(" ", "0"),
                       int.Parse(match.Groups["DIU"].Value.Trim())
                              );
                    if (this.IsVisible)
                    {
                        Dispatcher.Invoke(new Action(delegate()
                        {
                            progressBar.Value++;
                            progressLabel.Content = string.Format("{0:0.##}%", (progressBar.Value / progressBar.Maximum) * 100);
                        }), System.Windows.Threading.DispatcherPriority.Background);
                    }
                    else
                    {
                        Logger.WriteWarning("StatTrunk Import canceled by user.");
                        return false;
                    }
                }
                Logger.WriteInfo("Importing rows finished.");
                Cursor = Cursors.Wait;
                System.Windows.Forms.Application.DoEvents();
                importer.SubmitChanges();
                Cursor = Cursors.Arrow;
                return true;
            }
            finally
            {
                Cursor = Cursors.Arrow;
                importStarted = false;
                btnOK.IsEnabled = true;
            }
        }

        private bool ValidateFile(string content)
        {
            if (content.Contains(@"STATTRUNK:TGNO=X;"))
            {
                return true;
            }
            else
            {
                MessageBox.ShowError("اين فايل معتبر نمی باشد. فايل مورد نظر بايد حاوی اطلاعات دستور STATTRUNK باشد.");
                return false;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (importStarted && DialogResult != true && MessageBox.ShowQuestion("دريافت اطلاعات باقيمانده لغو شود؟", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
