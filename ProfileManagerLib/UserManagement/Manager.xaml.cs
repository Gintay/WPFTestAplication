using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProfileManagerLib
{
    /// <summary>
    /// Interaction logic for ProfileManager.xaml
    /// </summary>
    public partial class ProfileManagerWindow : Window, iManagerInterface
    {
        //
        private Dictionary<string, User> Users;
        private User CurrentLogedUser;
        private int NextId;

        public ProfileManagerWindow(Dictionary<string, User> users, User currentLogedUser, int nextId)
        {
            InitializeComponent();
            Users = users;
            CurrentLogedUser = currentLogedUser;
            NextId = nextId;

            //Bind datagrid to Users
            dtgUsers.ItemsSource = Users.Values;
        }
        public User Run()
        {
            if (CurrentLogedUser == null)
            {
                this.Login();
            }
            else
            {
                var rez = this.ShowDialog();
            }

            return CurrentLogedUser;
        }
        /// <summary>
        /// Login user
        /// </summary>
        private void Login()
        {
            var diag = new Login();
            var rez = diag.Run();
            if (rez != null)
            {
                CurrentLogedUser = Users[rez.Login];
            }
            else
            {

            }
        }
        /// <summary>
        /// Add new User
        /// </summary>
        private void AddUser()
        {
            var addUser = new AddUserWindow();
            var newUser = addUser.Run();
            if (newUser == null) return;
            if (Users.Keys.Contains(newUser.Login))
                MessageBox.Show("Add new user failed: user with such login already exists!", "Add user failed!", MessageBoxButton.OK, MessageBoxImage.Stop);
            else
            {
                Users.Add(newUser.Login, newUser);
                //update database records
                Users[newUser.Login].SetPassword("");
                Users[newUser.Login].Id = NextId++;
                dtgUsers.ItemsSource = null;
                dtgUsers.ItemsSource = Users.Values;
            }
        }
        /// <summary>
        /// Edit selected user
        /// </summary>
        private void EditUser()
        {
            if (dtgUsers.SelectedIndex < 0) return;
            var user = Users[dtgUsers.Items[dtgUsers.SelectedIndex].ToString()];
            if (user != null)
            {
                var editDiag = new EditUserWindow(user);
                var rez = editDiag.Run();
                if (rez != null)
                {
                    Users.Remove(user.Login);
                    Users.Add(rez.Login, rez);
                    dtgUsers.ItemsSource = null;
                    dtgUsers.ItemsSource = Users.Values;
                }
            }
        }
        /// <summary>
        /// Remove selected user
        /// </summary>
        private void RemoveUser()
        {
            if (dtgUsers.SelectedIndex < 0) return;
            var user = dtgUsers.Items[dtgUsers.SelectedIndex];
            if (user != null)
            {
                Users.Remove(dtgUsers.SelectedItem.ToString());
                dtgUsers.ItemsSource = null;
                dtgUsers.ItemsSource = Users.Values;
            }
        }
        private void SelectUser()
        {
            if (dtgUsers.SelectedIndex < 0) return;
            var user = Users[dtgUsers.Items[dtgUsers.SelectedIndex].ToString()];
        }

        private void bNew_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            RemoveUser();
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            EditUser();
        }
    }
}
