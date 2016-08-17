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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, iManagerInterface
    {
        private User LogedUser;
        /// <summary>
        /// Constructor for login window object
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evaluates login attempt result
        /// </summary>
        private void Evaluate()
        {
            // this part of code is selfexplanatory
            // it pops an error message if login is usuccessful
            if (String.IsNullOrEmpty(this.tbLogin.Text) ||
               /*String.IsNullOrEmpty(this.pbPassword.Password) ||*/
               isNotValid(this.tbLogin.Text, this.pbPassword.Password))
            {
                MessageBox.Show("Wrong username or password!", "Login failed!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.DialogResult = false;
            }
            // Setting DialogResult invokes 
            else
                this.DialogResult = true;
        }

        /// <summary>
        /// This method compares login and password with those provided by connection string
        /// </summary>
        /// <param name="login">login string</param>
        /// <param name="password">password string</param>
        /// <returns>true if comparison failed, otherwise false</returns>
        private bool isNotValid(string login, string password)
        {
            bool isNotValid;
            if (User.HasConnectionStringDefined())
            {
                // connect to database
                // validate login
                // validate password
                // update isNotValid to false if login and password are ok
                isNotValid = true;
            }
            else isNotValid = false;
            return isNotValid;
        }

        /// <summary>
        /// Method for Login button clicked
        /// </summary>
        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            // evaluate login atempt
            this.Evaluate();
        }

        /// <summary>
        /// Default dialog init method
        /// </summary>
        /// <returns>Loged User or null if login usuccessfull</returns>
        public User Run()
        {
            var rez = this.ShowDialog();
            if (rez.HasValue && rez == true)
            {
                LogedUser = new User();
                LogedUser.Login = tbLogin.Text;
            }
            else LogedUser = null;
            return LogedUser;
        }

        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            tbLogin.SelectAll();
        }

        private void tbLogin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            tbLogin.SelectAll();
        }

        private void tbLogin_GotMouseCapture(object sender, MouseEventArgs e)
        {
            tbLogin.SelectAll();
        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.SelectAll();
        }

        private void pbPassword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            pbPassword.SelectAll();
        }

        private void pbPassword_GotMouseCapture(object sender, MouseEventArgs e)
        {
            pbPassword.SelectAll();
        }

    }
}
