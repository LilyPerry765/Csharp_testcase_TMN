using System.Windows;
using Microsoft.Win32;
using System.Linq;
using System.Diagnostics;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for InstructionAttachmentsList.xaml
    /// </summary>
    public partial class InstructionAttachmentsList : Window
    {
        Instruction instruction;
        public InstructionAttachmentsList(Instruction instruction)
        {
            InitializeComponent();
            this.instruction = instruction;
            LoadFiles();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Title = "لطفا فايل مورد نظر را انتخاب نماييد.",
            };
            if (dlg.ShowDialog(this) == true)
            {
                foreach (string file in dlg.FileNames)
                {
                    AddFile(file);
                    LoadFiles();
                }
            }
        }

        private void LoadFiles()
        {
            if (System.IO.Directory.Exists(instruction.StoragePath))
            {
                filesListView.ItemsSource = from f in System.IO.Directory.GetFiles(instruction.StoragePath)
                                            select new
                                            {
                                                FileName = System.IO.Path.GetFileName(f),
                                                Image = ShellIcon.Get(f)
                                            };

            }
        }

        private void AddFile(string sourcePath)
        {
            string destPath = string.Format("{0}\\{1}", instruction.StoragePath, System.IO.Path.GetFileName(sourcePath));
            if (!System.IO.Directory.Exists(instruction.StoragePath))
            {
                System.IO.Directory.CreateDirectory(instruction.StoragePath);
            }

            if (System.IO.File.Exists(destPath))
            {
                MessageBox.ShowError("فايل ديگری با اين نام موجود است.");
            }
            else
            {
                System.IO.File.Copy(sourcePath, destPath);
            }
        }

        private void filesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowFile();
        }

        private void ShowFile()
        {
            if (filesListView.SelectedItem != null)
            {
                Process.Start(instruction.StoragePath + "\\" + (string)filesListView.SelectedValue);
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowFile();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (filesListView.SelectedItem != null && MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes)
            {

                string file = instruction.StoragePath + "\\" + (string)filesListView.SelectedValue;
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }

                LoadFiles();
            }
        }


    }
}
