using System.Collections.Generic;
using System.Linq;
using System;
namespace Multicriteria_model
{
    /// <summary>
    /// Субоптимизация
    /// </summary>
    internal sealed class Suboptimization
    {
        private readonly List<Product> _products;
        private readonly SortedDictionary<Characteristic, double> _criteria;
        private readonly Characteristic _mainCriterion;
        /// <summary>
        /// Субоптимизация
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="mainCriterion">Главный критерий</param>
        /// <param name="criteria">Список критериев и их нижние границы</param>
        public Suboptimization(List<Product> products, Characteristic mainCriterion, SortedDictionary<Characteristic, double> criteria)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в субоптимизации:\nОтсутствует список товаров!");
            _mainCriterion = mainCriterion ?? throw new ArgumentNullException(nameof(mainCriterion),
                "Ошибка в субоптимизации:\nНе был задан главный критерий!");
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria),
                "Ошибка в субоптимизации:\nОтсутствует список критериев!");
        }
        /// <summary>
        /// Субоптимизация
        /// </summary>
        /// <returns>Список товаров</returns>
        public List<Product> Run()
        {
            List<Product> productList = _products;
            for (byte i = 0; i < _criteria.Count; i++)
            {
                try
                {
                    productList = productList.FindAll(_criteria.ElementAt(i).Key, _criteria.ElementAt(i).Value);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка в субоптимизации:\n{ex.Message}");
                }
            }
            return productList.FindAll(_mainCriterion);
        }
    }
}