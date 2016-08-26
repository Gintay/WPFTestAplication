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
namespace BindingInterfaces
{
    /// <summary>
    /// Інтерфейс для байдингу категорії
    /// </summary>
    interface Category
    {
        /// <summary>
        /// Назва категорії
        /// </summary>
        string Name { set; get; }
        /// <summary>
        /// Картинка категорії
        /// </summary>
        Image image { set; get; }
    }

    interface Question
    {
        /// <summary>
        /// Запитання
        /// </summary>
        string QuestionString { set; get; }
        /// <summary>
        /// Відповіді
        /// </summary>
        List<string> Answers { set; get; }
    }
}
