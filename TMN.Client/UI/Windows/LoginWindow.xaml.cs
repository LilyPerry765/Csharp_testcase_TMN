using System;
using System.Windows;

namespace TMN.UI.Windows
{

    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
            ShiftLabel.Text = Shift.GetShiftBasedOnTime().ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
            Application.Current.Shutdown();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (User.Login(txtUsername.Text.Trim(), txtPassword.Password))
            {
                DialogResult = true;
                this.Close();


            }
            else
            {
                MessageBox.Show("نام کاربری يا کلمه عبور نادرست است", "خطا", MessageBoxImage.Error);
                txtUsername.Focus();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Content.As<UIElement>().FadeOut();
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Content.As<UIElement>().FadeIn();
            }
        }

    }
}
