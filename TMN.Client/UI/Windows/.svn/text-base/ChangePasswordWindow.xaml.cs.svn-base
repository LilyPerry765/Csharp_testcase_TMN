using System.Windows;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (User.Current.Password == currentPasswordBox.Password)
            {
                if (newPasswordBox.Password == repeatPasswordBox.Password)
                {
                    User.Current.ChangePassword(newPasswordBox.Password);
                    MessageBox.ShowInfo("تغيير رمز با موفقيت انجام شد.", "تغيير رمز");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.ShowError("رمز جديد با تکرار آن مطابفت ندارد.");
                }
            }
            else
            {
                MessageBox.ShowError("رمز فعلی عبور نادرست است.");
            }
        }
    }
}
