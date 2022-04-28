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
using Multicriteria_model.pages.criteria;

namespace Multicriteria_model.pages.settings
{
    public partial class Task : Page
    {
        public string ProductType => (productType.SelectedItem as ComboBoxItem).Content.ToString();
        public object Results
        {
            set
            {
                results.Navigate(value);
            }
        }
        public Task()
        {
            InitializeComponent();
            results.Navigate(new pages.Results());

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
                default:
                    return;
            }
        }
    }
}
