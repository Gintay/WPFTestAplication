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
    /// Interaction logic for UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window,iUserManager
    {
        Users LogedUser;
        public UserListWindow(Users user)
        {
            InitializeComponent();
            LogedUser = user;
            //lbUsersList.SelectionMode = SelectionMode.Single;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = new MyBaseNameEntities();
            var users = context.Set<Users>();
            if (users != null && users.Count() > 0)
            {
                foreach(Users user in users)
                {
                    if(LogedUser.user_category <= user.user_category)
                        lbUsersList.Items.Add(user.user_login);
                }                
            }
        }
        /// <summary>
        /// Implements iUserManager interface method Run
        /// </summary>
        /// <returns></returns>
        public Users Run()
        {
            Users selectedUser = null;
            var rez = this.ShowDialog();
            if (rez.HasValue && rez != null && lbUsersList.SelectedItems.Count == 1)
            {
                var context = new MyBaseNameEntities();
                selectedUser = context.Users.Where(user => user.user_login == lbUsersList.SelectedItem.ToString()).FirstOrDefault();
            }
            else
                selectedUser = null;
            return selectedUser;
        }


        private void lbUsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btSelect.IsEnabled = lbUsersList.SelectedItems.Count == 1 ?
                true :
                false;
        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }



    }
}
