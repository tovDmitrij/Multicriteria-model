using Multicriteria_model.pages.settings;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
namespace Multicriteria_model.pages.criteria
{
    /// <summary>
    /// Логика взаимодействия для Monitor.xaml
    /// </summary>
    public partial class PageMonitor : Page
    {
        private readonly Task parentPage;
        public PageMonitor(Task parentPage)
        {
            InitializeComponent();
            this.parentPage = parentPage;
        }
        private void Run(object sender, RoutedEventArgs e)
        {
            Criteria<Monitor> criteria = new Criteria<Monitor>();
            List<List<string>> productStringList = criteria.Initialize();
            List<Monitor> products = GetProducts(productStringList);
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
                criteriaWithBorderList.Add(Characteristics.ScreenSize, Convert.ToDouble(
                    Convert.ToDouble((screenSizeValue.Text.Split('x'))[0]) * Convert.ToDouble((screenSizeValue.Text.Split('x'))[1])
                    ));
                criteriaList.Add(Convert.ToByte(pricePriority.Text), Characteristics.Price);
                criteriaList.Add(Convert.ToByte(frequencyPriority.Text), Characteristics.Frequency);
                criteriaList.Add(Convert.ToByte(screenSizePriority.Text), Characteristics.ScreenSize);
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
        private static List<Monitor> GetProducts(List<List<string>> productStringList)
        {
            List<Monitor> productList = new List<Monitor>();
            try
            {
                foreach (var item in productStringList)
                {
                    productList.Add(new Monitor(item[0], item[1].ToString(), Convert.ToUInt32(item[2]), Convert.ToInt32(item[3])));
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
        private void PreviewValueScreenSizeInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9, x]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}