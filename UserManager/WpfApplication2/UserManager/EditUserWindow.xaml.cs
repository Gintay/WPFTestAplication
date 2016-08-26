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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window, iUserManager
    {
        Users EditedUser;
        Users EditorUser;
        string EditedUserCurrentLogin;
        public EditUserWindow(Users user, Users editor)
        {
            InitializeComponent();
            EditedUser = user;
            EditorUser = editor;
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
                    if (category.user_category_name.Length < skipCategory.Length ||
                        !category.user_category_name.Substring(0, skipCategory.Length).Equals(skipCategory))
                        if (EditorUser.user_category <= category.user_category_id)
                            cbCategory.Items.Add(category.user_category_name);
                }
            }
            else
            {
                MessageBox.Show("База категорій користувачів не була створена, або не заповнена!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
        /// <summary>
        /// Tries to fill YearsOfBirth combo
        /// </summary>
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

        //Window events processing
        private void pbOldPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbOldPassword.SelectAll();
        }

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
            //check current password
            if (!chbSuperAdmin.IsChecked.HasValue || chbSuperAdmin.IsChecked == false)
                if (!pbOldPassword.Password.Equals(EditedUser.user_pass))
                {
                    MessageBox.Show("Діючий пароль введено невірно", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            //check login
            if (String.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Ви не ввели жодного логіну!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var context = new MyBaseNameEntities();
            if (!tbLogin.Text.Equals(EditedUserCurrentLogin) && context.Users.Where(user => user.user_login == tbLogin.Text).Count() > 0)
            {
                MessageBox.Show("Користувач з таким логіном вже існує!", "Увага!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //check password and confirm password
            //if (String.IsNullOrEmpty(pbPassword.Password))
            //{
            //    var rez = MessageBox.Show("Ви не ввели жодного паролю!\nПароль потрібен для захисту аккаунту від сторонніх осіб.\nБажаєте продовжити створення без паролю?", "Увага!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //    if (rez == MessageBoxResult.No)
            //        return;
            //}else 
            if (!String.IsNullOrEmpty(pbPassword.Password) &&
                    !pbConfirmPassword.Password.Equals(pbPassword.Password))
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

        public Users Run()
        {
            if (EditedUser == null)
            {
                EditedUser = new UserManager.UserListWindow(EditorUser).Run();
                EditedUserCurrentLogin =
                    EditedUser != null ?
                    EditedUser.user_login :
                    null;
            }
            var rez = this.ShowDialog();
            if (rez.HasValue && rez == true)
            {
                EditedUser.user_login = tbLogin.Text;
                if (!String.IsNullOrEmpty(pbPassword.Password))
                    EditedUser.user_pass = pbPassword.Password;
                if (cbCategory.SelectedItem != null)
                    EditedUser.user_category = new MyBaseNameEntities().Users_category.Where(category => category.user_category_name.Equals(cbCategory.SelectedItem.ToString())).FirstOrDefault().user_category_id;
                //Save data to database
                var db = new MyBaseNameEntities();
                var original = db.Users.Find(EditedUser.user_id);
                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(EditedUser);
                    db.SaveChanges();
                }
            }
            else
                EditedUser = null;
            return EditedUser;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditedUser == null)
            {
                DialogResult = false;
                this.Close();
                return;
            }
            //imgAvatarImage = user.user_image;
            //cbYearOfBirth.SelectedValue = EditedUser.user_yearOfBirth;
            //tbName.Text = EditedUser.user_name;
            EditedUserCurrentLogin = EditedUser.user_login;
            tbLogin.Text = EditedUser.user_login;
            cbCategory.SelectedItem = EditedUser.Users_category.user_category_name;

            TryToFillCategories();
            TryToFillYearthOfBirth();
            chbSuperAdmin.IsChecked = false;
            if (EditorUser.user_category == new MyBaseNameEntities().Users_category.Where(cat => cat.user_category_name.Equals("SuperAdmin")).FirstOrDefault().user_category_id)
            {
                chbSuperAdmin.Opacity = 100.0;
                chbSuperAdmin.IsEnabled = true;
            }
            else
            {
                chbSuperAdmin.Opacity = 0.0;
                chbSuperAdmin.IsEnabled = false;
            }
        }

        private void chbSuperAdmin_Checked(object sender, RoutedEventArgs e)
        {
            pbOldPassword.IsEnabled = false;
        }

        private void chbSuperAdmin_Unchecked(object sender, RoutedEventArgs e)
        {
            pbOldPassword.IsEnabled = true;
        }


    }
}
