using Multicriteria_model.pages.settings;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Input;
using System.Text.RegularExpressions;
namespace Multicriteria_model.pages.criteria
{
    /// <summary>
    /// Логика взаимодействия для RAM.xaml
    /// </summary>
    public partial class PageRAM : Page
    {
        private readonly Task parentPage;
        public PageRAM(Task parentPage)
        {
            InitializeComponent();
            this.parentPage = parentPage;
        }
        private void Run(object sender, RoutedEventArgs e)
        {
            Criteria<RAM> criteria = new Criteria<RAM>();
            List<List<string>> productStringList = criteria.Initialize();
            List<RAM> products = GetProducts(productStringList);
            if (products == null || products.Count == 0)
            {
                MessageBox.Show("База данных пустая!");
                return;
            }
            SortedDictionary<Characteristics, double> criteriaWithBorderList = new SortedDictionary<Characteristics, double>();
            SortedDictionary<byte, Characteristics> criteriaList = new SortedDictionary<byte, Characteristics>();
            try
            {
                criteriaWithBorderList.Add(Characteristics.Price, -1 * Convert.ToDouble(priceValue.Text));
                criteriaWithBorderList.Add(Characteristics.Frequency, Convert.ToDouble(frequencyValue.Text));
                criteriaWithBorderList.Add(Characteristics.Memory, Convert.ToDouble(memoryValue.Text));
                criteriaList.Add(Convert.ToByte(pricePriority.Text), Characteristics.Price);
                criteriaList.Add(Convert.ToByte(frequencyPriority.Text), Characteristics.Frequency);
                criteriaList.Add(Convert.ToByte(memoryPriority.Text), Characteristics.Memory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ОШИБКА ВВОДА ДАННЫХ:\n{ex}");
                return;
            }
            parentPage.results.Navigate(new Results(criteria.Run(
                products,
                criteriaWithBorderList,
                criteriaList
                )));
        }
        private static List<RAM> GetProducts(List<List<string>> productStringList)
        {
            List<RAM> productList = new List<RAM>();
            try
            {
                foreach (var item in productStringList)
                {
                    productList.Add(new RAM(item[0], Convert.ToUInt32(item[1]), Convert.ToUInt32(item[2]), Convert.ToInt32(item[3])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ОШИБКА:\n{ex}");
                return productList;
            }
            return productList;
        }
        private void PreviewValueInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PreviewPriorityInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-3]");
            e.Handled = regex.IsMatch(e.Text) || ((TextBox)sender).Text.Length >= 1;
        }
    }
}