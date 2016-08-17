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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProfileManagerLib
{
    /// <summary>
    /// Interaction logic for ProfileManager.xaml
    /// </summary>
    public partial class ProfileManager : Window, iManagerInterface
    {
        private Dictionary<string, User> Users;
        private User CurrentLogedUser = null;
        private int NextId;
        private static ProfileManager Instance;

        private ProfileManager()
        {
            InitializeComponent();
            Instance = null;
            //init users collection
            Users = new Dictionary<string, User>();
            AddDeffaultAdmin();
            NextId = 1;
        }

        
        public static ProfileManager GetInstance()
        {
            return Instance == null ?
                    Instance = new ProfileManager() :
                    Instance;
        }

        public User Run()
        {
            UpdateUsers();
            iManagerInterface diag;
            if (CurrentLogedUser == null)
            {
                diag = new Login();
            }
            else
            {
                diag = new ProfileManagerWindow(Users, CurrentLogedUser, NextId);
            }
            var rez = diag.Run();

            return rez == null ?
                null:
                CurrentLogedUser = Users[rez.Login];
        }

        private void AddDeffaultAdmin()
        {
            var user = new User() { Id = 0, Login = "Admin", Admin = true };
            user.SetImage(new Image() { Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\tmp.png")) });
            user.BirthYear = "1956";
            user.Name = "Антон Гвидон";
            Users.Add(user.Login, user);
        }

        //This method is invoked anytime manager runs
        private void UpdateUsers()
        {
            //Update Users from database
        }
    }
}
