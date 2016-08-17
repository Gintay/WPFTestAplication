using System.Windows.Controls;

namespace ProfileManagerLib
{
    public class User
    {
        private static string ConnectionString = null;

        public int Id { set; get; }
        public string Login { set; get; }
        public bool Admin { set; get; }
        public string BirthYear { set; get; }
        public string Name { set; get; }
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
        /// <summary>
        /// Cheks User.ConnectionString property
        /// </summary>
        /// <returns>true if connection string is defined, false otherwise if connection string is null</returns>
        public static bool HasConnectionStringDefined()
        {
            return ConnectionString == null ?
                    false :
                    true;
        }

    }
}
