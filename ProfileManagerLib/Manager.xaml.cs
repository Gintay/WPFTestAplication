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
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        //
        private Dictionary<string, User> Users;
        int nextId = 0;
        public Manager()
        {
            InitializeComponent();

            Users = new Dictionary<string, User>();
            var user = new User() { Id = nextId++, Login = "Jarik", Admin = true };//for test purpose
            user.SetImage(new Image() { Source = new BitmapImage(new Uri(@"E:\Projects\TestProject\Big\ProfileManager\tshirt.jpeg")) });//for test purpose
            Users.Add(user.Login, user);//for test purpose
            dtgUsers.ItemsSource = Users.Values;
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
                Users[newUser.Login].Id = nextId++;
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

    public class User
    {
        private static string ConnectionString;

        public int Id { set; get; }
        public string Login { set; get; }
        public bool Admin { set; get; }
        private string Password = "";
        private Image ProfileImage;
        public void SetPassword(string value)
        {
            Password = value;
        }
        public string GetPassword()
        {
            return Password;
        }
        public override string ToString()
        {
            return this.Login;
        }

        public void SetImage(Image value)
        {
            ProfileImage = value;
        }
        public Image GetImage()
        {
            return ProfileImage;
        }
        public void SetConnection(string con)
        {
            ConnectionString = con;
        }

    }
}
