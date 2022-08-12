using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
namespace Multicriteria_model
{
    /// <summary>
    /// Страница с критериями
    /// </summary>
    public partial class Criteria : Page
    {
        private readonly Task _parentPage;
        private readonly string _productType;
        private readonly Product[] _productList;
        /// <summary>
        /// Страница с критериями
        /// </summary>
        /// <param name="parentPage">Родительская страница</param>
        /// <param name="productType">Тип товара</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Criteria(Task parentPage, string productType)
        {
            InitializeComponent();
            _parentPage = parentPage ?? throw new ArgumentNullException(nameof(_parentPage),
                "Ошибка при создании страницы с критериями:\nОтсутствует ссылка на родительскую страницу!");
            _productType = productType ?? throw new ArgumentNullException(nameof(_productType),
                "Ошибка при создании страницы с критериями:\nОтсутствует тип товара!");
            _productList = Server.GetProducts(_productType);
            CreateCriteriaList();
        }
        private void CreateCriteriaList()
        {
            try
            {
                foreach (var criteria in _productList[0].Characteristics)
                {
                    if (criteria.Value is IFormattable)
                    {
                        criteriaListBox.Items.Add(new CriteriaElement(criteria.Name, criteria.Value));
                    }
                }
            }
            catch(Exception ex)
            {
                if (_productList.Length == 0)
                {
                    throw new ArgumentNullException("Ошибка при создании критериев:\nСписок товаров оказался пустым!");
                }
                else
                {
                    throw new Exception($"{ex}");
                }
            }
        }
        private void Run(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] results = new string[5];

            #region Лексикографическая оптимизация
            SortedDictionary<int, Characteristic> lexOptim = new();
            foreach (CriteriaElement criteria in criteriaListBox.Items)
            {
                double value = criteria.Upper ? Convert.ToDouble(criteria.Value) : Convert.ToDouble(criteria.Value) * -1.0;
                lexOptim.Add((int)criteria.Priority, new Characteristic(criteria.Name, value));
            }
            Lexicographic lexicographic = new(_productList, lexOptim);
            foreach (var item in lexicographic.Run())
            {
                results[0] += item.ToString();
            }
            #endregion

            #region Субоптимизация
            Characteristic[] subOptim = new Characteristic[criteriaListBox.Items.Count - 1];
            Characteristic mainCriterion = new();
            int index = 0;
            foreach (CriteriaElement criteria in criteriaListBox.Items)
            {
                double value = criteria.Upper ? Convert.ToDouble(criteria.Value) : Convert.ToDouble(criteria.Value) * -1.0;
                if (criteria.Priority == 1)
                {
                    mainCriterion = new Characteristic(criteria.Name, value);
                }
                else
                {
                    subOptim[index] = new Characteristic(criteria.Name, value);
                    index++;
                }
            }
            Suboptimization suboptimization = new(_productList, mainCriterion, subOptim);
            var z = suboptimization.Run();
            foreach (var item in suboptimization.Run())
            {
                results[1] += item.ToString();
            }
            #endregion

            #region Указание нижних границ критериев
            Characteristic[] lcbOptim = new Characteristic[subOptim.Length + 1];
            for (int i = 0; i < subOptim.Length; i++)
            {
                lcbOptim[i] = subOptim[i];
            }
            lcbOptim[subOptim.Length] = mainCriterion;
            LowerCriteriaBorders lowerCriteriaBorders = new(_productList, lcbOptim);
            foreach (var item in lowerCriteriaBorders.Run())
            {
                results[2] += item.ToString();
            }
            #endregion

            #region Обобщённый критерий
            GeneralizedCriterion generalizedCriterion = new(_productList, CriteriaWithWeights(lexOptim));
            foreach (var item in generalizedCriterion.Run())
            {
                results[3] += item.ToString();
            }
            #endregion

            #region Формирование множества Парето
            ParetoOptimum paretoOptimum = new(_productList);
            foreach (var item in paretoOptimum.Run())
            {
                results[4] += item.ToString();
            }
            #endregion

            _parentPage.results.Navigate(new Results(results));
        }
        /// <summary>
        /// Расчёт весов для текущего списка критериев
        /// </summary>
        /// <param name="criteriaList">Словарь с ключом - порядком и значением - характеристикой</param>
        /// <returns>Cписок критериев и их веса</returns>
        private Characteristic[] CriteriaWithWeights(SortedDictionary<int, Characteristic> criteriaList)
        {
            Characteristic[] criteriaWeights = new Characteristic[criteriaList.Count];
            double weight = 0.6;
            int count = 1;
            foreach (var item in criteriaList)
            {
                double currentWeight = weight / count;
                criteriaWeights[count - 1] = new Characteristic(criteriaList.ElementAt(count - 1).Value.Name, currentWeight);
                count++;
            }
            return criteriaWeights;
        }
    }
}