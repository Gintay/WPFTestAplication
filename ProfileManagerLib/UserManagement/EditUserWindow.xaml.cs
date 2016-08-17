using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProfileManagerLib
{/// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window,iManagerInterface
    {
        private int UserId;
        private string UserPassword;
        private bool isAdmin;
        public EditUserWindow(User user)
        {
            InitializeComponent();
            imgProfileImage.Source = user.GetImage().Source;
            tbLogin.Text = user.Login;
            if (user.Admin == true)
            {
                chbAdmin.IsChecked = true;
                isAdmin = true;
            }
            else
            {
                chbAdmin.IsChecked = false;
                chbAdmin.IsEnabled = false;
                isAdmin = false;
            }
            UserId = user.Id;
            //load current password
            UserPassword = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Either User object if changes were successfuly set or null otherwise</returns>
        public User Run()
        {
            User user;
            var rez = this.ShowDialog();
            if (rez == null ||
                rez == false ||
                !pbOldPassword.Password.Equals(UserPassword))
                user = null;
            else
            {
                user = new User();
                user.Id = UserId;
                user.Login = tbLogin.Text;
                user.SetImage(imgProfileImage);
                if (chbAdmin.IsChecked.HasValue && chbAdmin.IsChecked == true && isAdmin)
                {
                    user.Admin = true;
                }
                else
                {
                    user.Admin = false;
                }
                //set new password
                if (!String.IsNullOrEmpty(pbNewPassword.Password) &&
                    !String.IsNullOrEmpty(pbRepeatPassword.Password) &&
                    pbNewPassword.Password.Equals(pbRepeatPassword.Password))
                {
                    user.SetPassword(pbNewPassword.Password);
                }
            }
            return user;
        }

        private void bChange_Click(object sender, RoutedEventArgs e)
        {
            var diag = new OpenFileDialog();
            var rez = diag.ShowDialog();
            if (rez.HasValue)
            {
                if (rez == true)
                {
                    try
                    {
                        this.imgProfileImage.Source = new BitmapImage(new Uri(diag.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Load image failed", MessageBoxButton.OK, MessageBoxImage.Stop);
                        this.imgProfileImage.Source = new BitmapImage(new Uri(@"tshirt.jpeg"));
                    }
                }
            }
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
