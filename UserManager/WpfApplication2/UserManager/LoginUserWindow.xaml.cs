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

namespace Library.UserManager
{
    /// <summary>
    /// Interaction logic for LoginUserWindow.xaml
    /// </summary>
    public partial class LoginUserWindow : Window, iUserManager
    {
        Users LogedUser;

        public LoginUserWindow()
        {
            InitializeComponent();

            LogedUser = null;

            //input language detection setup
            lblKeyb.Content = InputLanguageManager.Current.CurrentInputLanguage.TwoLetterISOLanguageName.ToUpper();
            InputLanguageManager.Current.InputLanguageChanged += new System.Windows.Input.InputLanguageEventHandler((sender, e) => lblKeyb.Content = e.NewLanguage.TwoLetterISOLanguageName.ToUpper());
            //----

            //Caps Lock detection setup
            lblCaps.Content = Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled ?
               "Caps ON" :
               "Caps OFF";
            //----
        }


        /// <summary>
        /// Tries to login
        /// </summary>
        private void Login()
        {
            var context = new MyBaseNameEntities();
            var records = context.Users.Where(usr => usr.user_login == tbLogin.Text);
            if(records.Count()>0)
            {
                var record = records.FirstOrDefault();

                LogedUser = record.user_pass.Equals(pbPassword.Password) ?
                record :
                null;
            }
            if (LogedUser == null)
            {
                MessageBox.Show("Неправильно введений пароль або логін", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        //interface implementation
        public Users Run()
        {
            this.ShowDialog();
            return LogedUser;
        }


        //Events processing
        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            tbLogin.SelectAll();

        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.SelectAll();
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
            DialogResult = true;
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bLogin == null) return;
            if (String.IsNullOrEmpty(tbLogin.Text))
            {
                bLogin.IsEnabled = false;
            }
            else
            {
                bLogin.IsEnabled = true;
            }
        }

        private void wUserLogin_KeyDown(object sender, KeyEventArgs e)
        {
            lblCaps.Content = Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled ?
                "Caps ON" :
                "Caps OFF";
        }

        private void wUserLogin_MouseMove(object sender, MouseEventArgs e)
        {
            lblCaps.Content = Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled ?
                "Caps ON" :
                "Caps OFF";
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var langs = InputLanguageManager.Current.AvailableInputLanguages;
        //    foreach (System.Globalization.CultureInfo lang in langs)
        //    {
        //        if (!lang.Equals(InputLanguageManager.Current.CurrentInputLanguage))
        //        {
        //            InputLanguageManager.Current.CurrentInputLanguage = lang;
        //            break;
        //        }
        //    }
        //}
    }
}
