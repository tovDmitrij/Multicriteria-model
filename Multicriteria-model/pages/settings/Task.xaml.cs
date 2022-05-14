using System.Collections.Generic;
using System.Windows.Controls;
namespace Multicriteria_model.pages.settings
{
    /// <summary>
    /// Задача выбора
    /// </summary>
    public sealed partial class Task : Page
    {
        /// <summary>
        /// Получить выбранный тип товара
        /// </summary>
        public string ProductType => (productType.SelectedItem as ComboBoxItem).Content.ToString();
        /// <summary>
        /// Страница для вывода результата
        /// </summary>
        public object ResultFrame
        {
            set
            {
                //results.Navigate(new Results());
            }
        }
        /// <summary>
        /// Задача выбора
        /// </summary>
        public Task()
        {
            InitializeComponent();
        }
        private void productType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ProductType)
            {
                case "Покупка видеокарты":
                    criteria.Navigate(new criteria.Videocard());
                    break;
                case "Покупка процессора":
                    criteria.Navigate(new criteria.Processor());
                    break;
                case "Покупка оперативной памяти":
                    criteria.Navigate(new criteria.RAM());
                    break;
                case "Покупка жесткого диска":
                    criteria.Navigate(new criteria.pageHDD(this));
                    break;
                case "Покупка монитора":
                    criteria.Navigate(new criteria.Monitor());
                    break;
                default:
                    return;
            }
        }
    }
}
