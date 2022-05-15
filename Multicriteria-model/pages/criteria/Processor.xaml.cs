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
    /// Логика взаимодействия для Processor.xaml
    /// </summary>
    public partial class PageProcessor : Page
    {
        private readonly Task parentPage;
        public PageProcessor(Task parentPage)
        {
            InitializeComponent();
            this.parentPage = parentPage;
        }
        private void Run(object sender, RoutedEventArgs e)
        {
            Criteria<Processor> criteria = new Criteria<Processor>();
            List<List<string>> productStringList = criteria.Initialize();
            List<Processor> products = GetProducts(productStringList);
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
                criteriaWithBorderList.Add(Characteristics.Cores, Convert.ToDouble(coresValue.Text));
                criteriaList.Add(Convert.ToByte(pricePriority.Text), Characteristics.Price);
                criteriaList.Add(Convert.ToByte(frequencyPriority.Text), Characteristics.Frequency);
                criteriaList.Add(Convert.ToByte(coresPriority.Text), Characteristics.Cores);
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
        private static List<Processor> GetProducts(List<List<string>> productStringList)
        {
            List<Processor> productList = new List<Processor>();
            try
            {
                foreach (var item in productStringList)
                {
                    productList.Add(new Processor(item[0], Convert.ToUInt32(item[1]), Convert.ToDouble(item[2]), Convert.ToInt32(item[3])));
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
