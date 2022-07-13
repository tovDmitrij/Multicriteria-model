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
    /// Страница с критериями для <see cref="PageHDD"/>
    /// </summary>
    public partial class PageHDD : Page
    {
        private readonly Task parentPage;
        /// <summary>
        /// Страница с критериями для <see cref="PageHDD"/>
        /// </summary>
        /// <param name="parentPage">Ссылка на страницу с задачей</param>
        public PageHDD(Task parentPage)
        {
            InitializeComponent();
            this.parentPage = parentPage;
        }
        private void Run(object sender, RoutedEventArgs e)
        {
            Criteria<HDD> criteria = new Criteria<HDD>();
            List<List<string>> productStringList = criteria.Initialize();
            List<HDD> products = GetProducts(productStringList);
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
                criteriaWithBorderList.Add(Characteristics.Speed, Convert.ToDouble(speedValue.Text));
                criteriaWithBorderList.Add(Characteristics.Memory, Convert.ToDouble(memoryValue.Text));
                criteriaList.Add(Convert.ToByte(pricePriority.Text), Characteristics.Price);
                criteriaList.Add(Convert.ToByte(speedPriority.Text), Characteristics.Speed);
                criteriaList.Add(Convert.ToByte(memoryPriority.Text), Characteristics.Memory);
            }
            catch (System.Exception ex)
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
        private static List<HDD> GetProducts(List<List<string>> productStringList)
        {
            List<HDD> productList = new List<HDD>();
            try
            {
                foreach (var item in productStringList)
                {
                    productList.Add(new HDD(item[0], Convert.ToUInt32(item[1]), Convert.ToUInt32(item[2]), Convert.ToInt32(item[3])));
                }
            }
            catch(System.Exception ex)
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