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
    /// Interaction logic for RemoveUserWindow.xaml
    /// </summary>
    public partial class RemoveUserWindow : Window
    {
        Users LogedUser;

        public RemoveUserWindow(Users logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
        }

        /// <summary>
        /// implements Run of iUserManager interface
        /// Tries to emove selected user from database
        /// </summary>
        /// <returns>User selected for removal or null</returns>
        public Users Run()
        {
            
            if(LogedUser.Users_category.admin!=true)
            {
                MessageBox.Show(LogedUser.user_login + " не має права видаляти користувачів", "Порушення доступу", MessageBoxButton.OK, MessageBoxImage.Stop);
                return null;
            }
            var DeletedUser = new UserManager.UserListWindow(LogedUser).Run();
            if (MessageBox.Show("Ви дійсно бажаєте видалити " + DeletedUser.user_login + " з бази даних користувачів?", "Підтвердіть видалення!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                var db = new MyBaseNameEntities();
                db.Set<Users>().Remove(DeletedUser);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Користувач " + DeletedUser.user_login + " не був видалений з бази даних користувачів","Видалення відмінено",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            return DeletedUser;
        }
    }
}
