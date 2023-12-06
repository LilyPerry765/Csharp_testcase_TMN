using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for InstructionImageWindow.xaml
    /// </summary>
    public partial class InstructionImageWindow : Window
    {
        private TMNModelDataContext db;
        private bool shouldSave = false;

        public InstructionImageWindow(Instruction instruction, TMNModelDataContext db)
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
            DataContext = instruction;
            this.db = db;
            if (instruction.Image != null)
            {
                Stream strm = new MemoryStream(instruction.Image.ToArray());
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = strm;
                bmp.EndInit();
                img.Source = bmp;
            }
        }


        public bool ShouldSave
        {
            get
            {
                return shouldSave;
            }
            set
            {
                shouldSave = value;
                btnSave.IsEnabled = shouldSave;
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Images|*.png;*.jpg;*.bmp"
            };
            if (dlg.ShowDialog(this) ?? false)
            {
                Stream strm = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = strm;
                bmp.EndInit();
                img.Source = bmp;
                ShouldSave = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ShouldSave)
            {
                switch (MessageBox.Show("تغييرات اعمال شده ذخيره شود؟", "ذخيره", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
                {
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.OK:
                    case MessageBoxResult.Yes:
                        Save();
                        break;
                    case MessageBoxResult.None:
                    case MessageBoxResult.Cancel:
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes)
            {
                img.Source = null;
                ShouldSave = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Close();
        }

        private void Save()
        {
            (DataContext as Instruction).Image = (System.Data.Linq.Binary)Converters.BinaryToImageConverter.Instance.ConvertBack(img.Source, null, null, null);
            db.SubmitChanges();
            ShouldSave = false;
        }

    }
}
