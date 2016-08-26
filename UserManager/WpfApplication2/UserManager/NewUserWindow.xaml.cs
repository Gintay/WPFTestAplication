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

namespace Library.UserManager
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window, iUserManager
    {
        Users CreatedUser { set; get; }
        Users LogedUser;

        public NewUserWindow(Users logedUser)
        {
            InitializeComponent();
            pbPassword.BorderThickness = new Thickness(2.0);
            LogedUser = logedUser;
        }

        /// <summary>
        /// inplements iUserManager interface method Run
        /// </summary>
        /// <returns>Created user object if successfuly such is created, or null if otherwise</returns>
        public Users Run()
        {
            if (LogedUser.Users_category.admin != true)
            {
                MessageBox.Show(LogedUser.user_login + " не має права створювати користувачів", "Порушення доступу", MessageBoxButton.OK, MessageBoxImage.Stop);
                return null;
            }
            var rez = this.ShowDialog();
            if (rez.HasValue && rez == true)
            {
                var context = new MyBaseNameEntities();
                CreatedUser = new Users()
                {
                    user_login = tbLogin.Text,
                    user_pass = pbPassword.Password,
                    user_category = context.Users_category.Where(category=>category.user_category_name == cbCategory.SelectedItem.ToString()).FirstOrDefault().user_category_id
                };
            }
            else
                CreatedUser = null;

            return CreatedUser;
        }

        private void TryToFillYearthOfBirth()
        {
            int Range = 100;
            int maxValue = DateTime.Today.Year;
            int minValue = maxValue - Range;
            int deffaultValueIndex = (int)((maxValue - minValue) / 2);

            for (int year = minValue; year < maxValue + 1; year++)
                cbYearOfBirth.Items.Add(year);

            if (cbYearOfBirth.Items.Count < 1)
            {
                MessageBox.Show("Очевидно, що щось пішло не так...", "Невідома помилка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            cbYearOfBirth.SelectedIndex = deffaultValueIndex;
        }
        /// <summary>
        /// Tries to fill categories combo from users_categories
        /// </summary>
        private void TryToFillCategories()
        {
            var context = new MyBaseNameEntities();
            var categories = context.Set<Users_category>();
            if (categories != null && categories.Count() > 0)
            {
                string skipCategory = "ReservedForUser";
                foreach (Users_category category in categories)
                {
                    //Skip any ReservedForUser category from list
                    if (category.user_category_name.Length<skipCategory.Length                             ||
                        !category.user_category_name.Substring(0, skipCategory.Length).Equals(skipCategory)  )
                        if(LogedUser.user_category <= category.user_category_id)
                            cbCategory.Items.Add(category.user_category_name);
                }
            }
            else
            {
                MessageBox.Show("База категорій користувачів не була створена, або не заповнена!","Помилка!",MessageBoxButton.OK,MessageBoxImage.Error);
                this.Close();
            }
        }


        //Window events processing
        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            tbLogin.SelectAll();
        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.SelectAll();
            pbPassword.SelectionBrush = pbPassword.BorderBrush = pbConfirmPassword.Password.Equals(pbPassword.Password) ?
                Brushes.GreenYellow ://if yes
                Brushes.Red;//if no 
        }

        private void pbConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbConfirmPassword.SelectAll();
            pbPassword.SelectionBrush = pbPassword.BorderBrush = pbConfirmPassword.Password.Equals(pbPassword.Password) ?
                Brushes.GreenYellow ://if yes
                Brushes.Red;//if no 
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbName.SelectAll();
        }

        private void pbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pbPassword.SelectionBrush = pbPassword.BorderBrush = pbConfirmPassword.Password.Equals(pbPassword.Password) ?
                Brushes.GreenYellow ://if yes
                Brushes.Red;//if no 
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Create user button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCreateUser_Click(object sender, RoutedEventArgs e)
        {
            //check login
            if (String.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Ви не ввели жодного логіну!","Помилка!",MessageBoxButton.OK,MessageBoxImage.Stop);
                return;
            }
            var context = new MyBaseNameEntities();
            if(context.Users.Where(user=>user.user_login == tbLogin.Text).Count()>0)
            {
                MessageBox.Show("Користувач з таким логіном вже існує!", "Увага!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //check password and confirm password
            if(String.IsNullOrEmpty(pbPassword.Password))
            {
                var rez = MessageBox.Show("Ви не ввели жодного паролю!\nПароль потрібен для захисту аккаунту від сторонніх осіб.\nБажаєте продовжити створення без паролю?", "Увага!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (rez == MessageBoxResult.No)
                    return;
            }
            else if(String.IsNullOrEmpty(pbConfirmPassword.Password)        ||
                    !pbConfirmPassword.Password.Equals(pbPassword.Password) )
            {
                MessageBox.Show("Пароль і його підтвердження не співпадають!", "Увага!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (cbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Ви не обрали категрію!", "Увага!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
            this.Close();
        }


        /// <summary>
        /// Select image button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelectImage_Click(object sender, RoutedEventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.TIFF;*.PNG;*.GIF)|*.BMP;*.JPG;*.JPEG;*.TIFF;*.PNG;*.GIF|All files (*.*)|*.*";
            var rez = diag.ShowDialog();
            if (rez.HasValue && rez == true)
            {
                imgAvatarImage.Source = new BitmapImage(new Uri(diag.FileName));
            }
        }

        private void wNewUser_Loaded(object sender, RoutedEventArgs e)
        {
            //Fill yearOfBirth combo
            TryToFillYearthOfBirth();

            //Fill categories combo
            TryToFillCategories();
        }

    }
}
