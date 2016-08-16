using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProfileManagerLib
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private bool Admin;

        //private bool inputIsNotValid;
        public AddUserWindow()
        {
            InitializeComponent();

            tbLogin.BorderBrush = Brushes.Red;
            pbPassword.BorderBrush = Brushes.Red;
            pbRepeatPassword.BorderBrush = Brushes.Red;
            this.Admin = false;
            this.imgProfileImage.Source = new BitmapImage(new Uri(@"E:\Projects\TestProject\Big\ProfileManager\tshirt.jpeg"));
            //this.inputIsNotValid = true;
        }

        /// <summary>
        /// Add user to base
        /// </summary>
        /// <returns></returns>
        public User Run()
        {
            var user = new User();
            var rez = this.ShowDialog();
            if (rez.HasValue && rez == true)
            {
                user.Login = this.tbLogin.Text;
                user.SetPassword(this.pbPassword.Password);
                user.Admin = this.Admin;
                user.SetImage(this.imgProfileImage);
            }
            else user = null;
            return user;
        }


        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (InputIsValid())
            {
                if (this.chbAdmin.IsChecked.HasValue && (bool)this.chbAdmin.IsChecked)
                    this.Admin = true;
                DialogResult = true;
            }
            else
                MessageBox.Show("Login or password is not set correctly", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        private bool InputIsValid()
        {
            return (String.IsNullOrEmpty(tbLogin.Text) ||
                 String.IsNullOrEmpty(pbPassword.Password) ||
                 String.IsNullOrEmpty(pbRepeatPassword.Password) ||
                 !pbRepeatPassword.Password.Equals(pbPassword.Password)) ?
                 false :
                 true;
        }

        //Visual presentation of data input validation
        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (String.IsNullOrEmpty(tbLogin.Text))
            {
                tbLogin.BorderBrush = Brushes.Red;
            }
            else
            {
                tbLogin.BorderBrush = Brushes.Green;
            }

        }
        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(pbPassword.Password))
            {
                pbPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                pbPassword.BorderBrush = Brushes.Green;
            }

        }
        private void pbRepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(pbRepeatPassword.Password) || !pbRepeatPassword.Password.Equals(pbPassword.Password))
            {
                pbRepeatPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                pbRepeatPassword.BorderBrush = Brushes.Green;
            }
        }

        private void bPick_Click(object sender, RoutedEventArgs e)
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

    }
}
