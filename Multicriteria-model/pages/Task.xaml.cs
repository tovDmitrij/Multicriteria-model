using System.Windows.Controls;
namespace Multicriteria_model
{
    /// <summary>
    /// Задача выбора
    /// </summary>
    public sealed partial class Task : Page
    {
        public Task()
        {
            InitializeComponent();
        }
        private void ProductTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            Server.Name = serverName.Text;
            Server.Database = dbName.Text;
            string productType = (this.productType.SelectedItem as ComboBoxItem).Uid;
            criteria.Navigate(new Criteria(this, productType));
        }
    }
}