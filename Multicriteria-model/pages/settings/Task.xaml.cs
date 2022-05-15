using System.Windows;
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
        public Task()
        {
            InitializeComponent();
        }
        private void ProductTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            Server.Name = serverName.Text;
            switch (ProductType)
            {
                case "Покупка видеокарты":
                    criteria.Navigate(new criteria.PageVideocard(this));
                    break;
                case "Покупка процессора":
                    criteria.Navigate(new criteria.PageProcessor(this));
                    break;
                case "Покупка оперативной памяти":
                    criteria.Navigate(new criteria.PageRAM(this));
                    break;
                case "Покупка жесткого диска":
                    criteria.Navigate(new criteria.PageHDD(this));
                    break;
                case "Покупка монитора":
                    criteria.Navigate(new criteria.PageMonitor(this));
                    break;
                default:
                    return;
            }
        }
    }
}