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


//Документація
//Щоб добавити категорію в поле з категоріями, потрібно скористатись методом AddCategoryToCategoryList. Щоб розпочати тест при натискані на категорію, потрібно редагувати метод
//CategoryMouseClick. Щоб розпочати сам тест потрібно викликати метод OpenTest. Щоб додати запитання до тесту, потрібно виклакати метод AddQuestionToTest. Щоб завершити тест,та 
//перейти до головного меню, потрібно викликати метод CloseTest. Щоб змінити логіку при натисканні на відповідь, потрібно редагувати метод AnswerMouseClick. В методах CategoryMouseClick
//та AnswerMouseClick існує зміна, яка тримає в собі назву обраної категорії, або текст вибраної відповіді.
//Для оновлення профілю потрібно скористатись методом RefreshProfile


namespace WpfApplication2
{
    /// <summary>
    /// Делегат методу завантаження
    /// </summary>
    delegate void LoadDelegate();
    /// <summary>
    /// Показовий клас запитання
    /// </summary>
    class QuestionTMP : BindingInterfaces.Question
    {
        public string QuestionString { set; get; }
        public List<string> Answers { set; get; }
        public QuestionTMP()
        {
            Answers = new List<string>();
            QuestionString = "В якому фільмі знявся Аль Пачіно?";
            Answers.Add("Ворота в рай");
            Answers.Add("Відьма та відьмак показують фак");
            Answers.Add("Хресний батько");
            Answers.Add("Хресна мама");
            Answers.Add("Хресний дідо");
            Answers.Add("Хресний баба");
            Answers.Add("Гаррі Поттер");
            Answers.Add("Володар Перснів");

            Answers.Add("Стар Трек");

            Answers.Add("Зохан");

            Answers.Add("Дикі качки");
            Answers.Add("Івасик-Телесик");

            Answers.Add("Вечір на ранок");
            Answers.Add("Синій дим");
            Answers.Add("Білий дім");






        }
    }
    /// <summary>
    /// Показовий клас категорії
    /// </summary>
    class CategoryTMP : BindingInterfaces.Category
    {
        public string Name { set; get; }
        public Image image { set; get; }
        public CategoryTMP()
        {
            Name = "Показова категорія";
            image = new Image();
            image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\tmp.png"));
        }
    }


    public partial class MainWindow : Window
    {
        //Шлях до виконавчого файлу
        string exepath = Environment.CurrentDirectory;
        //Path to deffault image... just in case...
        string deffaultImagePath = @"\Images\tmp.png";
        //Aplication tracks loged in user
        Users LogedUser = null;

        public MainWindow()
        {
            InitializeComponent();
            //об'єкт делегату, в який присвоється метод завантаження
            LoadDelegate load = new LoadDelegate(LoadMethod);
            //Запуск делегату в новому потоці
            this.Dispatcher.BeginInvoke(load);
            //Показовий клас та метод категорії
            CategoryTMP c = new CategoryTMP();
            AddCategoryToCategoryList(c);

            TryToAdDeffaultUserCategories();
            TryToAddDeffaultUser();
           
        }

        /// <summary>
        /// Завантаження картинок в фронтенді
        /// </summary>
        void LoadImages()
        {
            //Вивід картинки категорій в меню
            CategoryMenuImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\book.png"));
            //Вивід картинки профілю в меню
            ProfileMuneImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\profile.png"));
            //Вивід картинки налаштувань в меню
            SettingsMenuImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\settings.png"));

            //Вивід картинки Додати Тест
            AddTestButton.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\AddTest.png"));
            //Вивід картинки Редагувати Тест
            EditTestButtonImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\edit.png"));
            //Вивід картинки Видалити Тест
            DeleteTestButtonImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\delete.png"));
            //Вивід картинки Додати користувача
            AddUserButtonImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\adduser.png"));
            //Вивід картинки Редагувати користувача
            EditUserButtonImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\edituser.png"));
            //Вивід картинки Видалити користувача
            DeleteUserButtonImage.Source = new BitmapImage(new Uri(exepath + @"\Images\Menu\deletuser.png"));
        }
        /// <summary>
        /// Метод завантеження
        /// </summary>
        void LoadMethod()
        {
            LoadImages();
        }
        /// <summary>
        /// Метод Віталія. Щоб передати категорію, потрібно унаслідувати клас від інтерфейсу BindingInterfaces.Category
        /// Метод додає категорії в панель з категоріями в головному меню
        /// </summary>
        /// <param name="category">Категорія</param>
        void AddCategoryToCategoryList(BindingInterfaces.Category category)
        {
            //Метод Віталія

            //Основний бордер
            Border border = new Border();
            //Стиль для бордера
            border.Style = this.FindResource("CategoryBorder") as Style;
            //Панель в бордері
            StackPanel stack = new StackPanel();
            //Картинка в панелі, а саме картинка категорії
            Image img = category.image;
            img.Width = 130;
            img.Height = 100;
            img.Margin = new Thickness(0, 10, 0, 0);
            //Текст в панелі, а саме назва категорії
            TextBlock t = new TextBlock();
            //Налаштування текст
            t.TextWrapping = TextWrapping.Wrap;
            t.Text = category.Name;
            t.FontSize = 25;
            t.TextAlignment = TextAlignment.Center;
            //Добавлення дочерніх класів
            stack.Children.Add(img);
            stack.Children.Add(t);
            border.Child = stack;
            border.MouseDown += CategoryMouseClick;
            //border.MouseEnter += Grid_MouseEnter;
            CategoriesPanel.Children.Add(border);
        }
        /// <summary>
        /// Метод Віталія. Щоб передати запитання, потрібно унаслідувати клас від інтерфейсу BindingInterfaces.Question
        /// Метод заповнює сітку з запитанням та відповідями в самому тесті
        /// </summary>
        /// <param name="question">Question</param>
        void AddQuestionToTest(BindingInterfaces.Question question)
        {
            //Метод Віталія

            //Присвоєння запитання
            QuestionBlock.Text = question.QuestionString;
            //Очищення старих відповідей
            AnswersStack.Children.Clear();
            //Додавання відповідей до панелі
            foreach (var i in question.Answers)
            {
                Border bo = new Border();
                bo.Style = this.FindResource("AnswerStyle") as Style;
                bo.Background = Brushes.White;
                //Відповідь
                TextBlock t = new TextBlock();
                //Присвоєння стилю
                t.Style = this.FindResource("AnswerStyleText") as Style;
                //Присвоєння тексту відповіді
                t.Text = i;
                t.TextAlignment = TextAlignment.Center;
                bo.Child = t;
                bo.MouseDown += AnswerMouseClick;
                //Додавання до панелі
                AnswersStack.Children.Add(bo);
            }
        }
        /// <summary>
        /// Метод Віталія. Метод закриває тест, та повертає до головного меню
        /// </summary>
        void CloseTest()
        {
            TestGrid.Visibility = System.Windows.Visibility.Hidden;
            MainGrid.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// Метод Віталія. Щоб передати запитання, потрібно унаслідувати клас від інтерфейсу BindingInterfaces.Question
        /// Метод запускає тест, заповнивши його запитанням
        /// </summary>
        /// <param name="question">Question</param>
        void OpenTest(BindingInterfaces.Question question)
        {
            MainGrid.Visibility = System.Windows.Visibility.Hidden;
            AddQuestionToTest(question);
            TestGrid.Visibility = System.Windows.Visibility.Visible;

        }
        //Подія кнопки Закінчити тест
        private void FinishTest_Click(object sender, RoutedEventArgs e)
        {
            CloseTest();
        }
        /// <summary>
        /// Метод події натискання на категорію
        /// </summary>
        void CategoryClick(object sender, MouseButtonEventArgs e)
        {
            QuestionTMP b = new QuestionTMP();
            OpenTest(b);
        }
        /// <summary>
        /// Метод натискання на категорію
        /// </summary>
        private void CategoryMouseClick(object sender, MouseEventArgs e)
        {
            //Показовий клас та тест
            QuestionTMP b = new QuestionTMP();
            OpenTest(b);

            StackPanel stack = ((Border)sender).Child as StackPanel;
            TextBlock text = stack.Children[1] as TextBlock;

            //Назва категорії для вашого користування
            string name = text.Text;
        }
        /// <summary>
        /// Метод натискання на відповідь в тесті
        /// </summary>
        private void AnswerMouseClick(object sender, MouseEventArgs e)
        {
            TextBlock text = ((Border)sender).Child as TextBlock;
            if(((Border)sender).Background==Brushes.White)
            {
                ((Border)sender).Background = Brushes.LightGray;
            }
            else
            {
                ((Border)sender).Background = Brushes.White;
            }

            //Текст відповіді
            string answer = text.Text;

        }
        /// <summary>
        /// Поновленя інформації в профілі
        /// Edit by bigDrone: taken params redone to none since profile is refreshed only for currently loged-in user, logics updated accordingly
        /// </summary>
        void RefreshProfile()
        {
            //Check if Loged user is defined
            if (LogedUser == null)
            {
                //run login window
                var rezult = new UserManager.LoginUserWindow().Run();
                //if login was not success aplication is shutdown
                if(rezult==null)
                {
                    //LogedUser = LogWithGuest();
                    this.Close();
                    return;
                }
                else//Loged user is updated
                {
                    LogedUser = rezult;
                }
            }

            NameProfileTextBlock.Text = LogedUser.user_login;// LogedUser.user_name;
            YearProfileTextBlock.Text = "Рік не визначений";//LogedUser.user_birthYear;
            //try
            //{
            //    LogoImage.ImageSource = new BitmapImage(new Uri(exepath + LogedUser.user_image));
            //}
            //catch (Exception any)
            //{
            //    LogoImage.ImageSource = new BitmapImage(new Uri(exepath + deffaultImagePath));
            //}
            LogoImage.ImageSource = new BitmapImage(new Uri(exepath + deffaultImagePath));
        }
        /// <summary>
        /// bigDrone Method:
        /// Method that generates deffault user categories if there are none in User_categories set
        /// </summary>
        void TryToAdDeffaultUserCategories()
        {
            var context = new MyBaseNameEntities();
            var usr_cat_count = context.Users_category.Count();
            if(usr_cat_count<1)
            {
                var usr_cat_col = context.Set<Users_category>();

                //Add deffault categories set
                usr_cat_col.Add(new Users_category() { user_category_name = "SuperAdmin", admin = true });
                usr_cat_col.Add(new Users_category() { user_category_name = "Admin", admin = true });
                usr_cat_col.Add(new Users_category() { user_category_name = "User", admin = false });
                usr_cat_col.Add(new Users_category() { user_category_name = "ReservedForUser01", admin = false });
                usr_cat_col.Add(new Users_category() { user_category_name = "ReservedForUser02", admin = false });
                usr_cat_col.Add(new Users_category() { user_category_name = "ReservedForUser03", admin = false });
                usr_cat_col.Add(new Users_category() { user_category_name = "Guest", admin = false });

                context.SaveChanges();
            }
            
        }
        /// <summary>
        /// bigDrone Method:
        /// Method that generates deffault admin User if no users or no admins are in Users set
        /// </summary>
        void TryToAddDeffaultUser()
        {
            var context = new MyBaseNameEntities();
            var adminsCount = context.Users.Where(usr => usr.Users_category.admin == true).Count();
            var guestsCount = context.Users.Where(usr => usr.Users_category.user_category_name == "Guest").Count();
            var set = context.Set<Users>();
            if (adminsCount < 1)
            {
                //Define deffault admin
                Users user = new Users();
                //user.user_name = "Deffault Admin";
                user.user_login = "admin";
                //user.user_birthYear = DateTime.Today.Year.ToString();
                user.user_pass = "";
                user.user_category = context.Users_category.Where(cat => cat.admin == true).FirstOrDefault().user_category_id;
                //user.user_image = deffaultImagePath;
                set.Add(user);
            }
            if (guestsCount < 1)
            {
                Users guest = new Users();
                //guest.user_name = "Deffault Admin";
                guest.user_login = "guest";
                //guest.user_birthYear = DateTime.Today.Year.ToString();
                guest.user_pass = "";
                guest.user_category = context.Users_category.Where(cat => cat.user_category_name == "Guest").FirstOrDefault().user_category_id;
                //guest.user_image = deffaultImagePath;
                set.Add(guest);
            }
            context.SaveChanges();
        }
        

        //Event proccessing added by bigDrone
        /// <summary>
        /// When window is loaded, default signed profile is set to "Guest" and login window pops up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //run login window
            var rezult = new UserManager.LoginUserWindow().Run();
            //if login was not success aplication is loged under "Guest"
            LogedUser =
                rezult != null ?
                rezult :
                new MyBaseNameEntities().Users.Where(usr => usr.user_login.Equals("Guest")).FirstOrDefault();

            RefreshProfile();
            this.tbProfileTab.IsSelected = true;
        }

        //BigDrone:
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var diag = new UserManager.NewUserWindow(LogedUser);
            var rez = diag.Run();
            if (rez != null)
            {
                var context = new MyBaseNameEntities();
                context.Set<Users>().Add(rez);
                context.SaveChanges();
            }
            this.RefreshProfile();
        }
        //BigDrone: Closing program prompt
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Бажаєте закрити програму?", "Підтвердіть", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                e.Cancel = true;
        }

        //BigDrone:
        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new UserManager.EditUserWindow(null,LogedUser).Run();
        }

        //BigDrone:
        /// <summary>
        /// Change current profile (new login)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LogedUser = null;
            this.RefreshProfile();
        }
        //BigDrone
        /// <summary>
        /// Quit programm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}
