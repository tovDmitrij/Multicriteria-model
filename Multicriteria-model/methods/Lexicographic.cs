using System;
using System.Collections.Generic;
namespace Multicriteria_model
{
    /// <summary>
    /// Лексикографическая оптимизация
    /// </summary>
    internal sealed class Lexicographic
    {
        private readonly Product[] _products;
        private readonly SortedDictionary<int, Characteristic> _criteria;
        /// <summary>
        /// Лексикографическая оптимизация
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="criteria">Список критериев и их порядок</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Lexicographic(Product[] products, SortedDictionary<int, Characteristic> criteria)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в лексикографической оптимизации:\nОтсутствует список товаров!");
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria),
                "Ошибка в лексикографической оптимизации:\nОтсутствует список критериев!");
        }
        /// <summary>
        /// Лексикографическая оптимизация
        /// </summary>
        /// <returns>Список товаров</returns>
        public Product[] Run()
        {
            Product[] productList = _products;
            for (int i = 1; i <= _criteria.Count; i++)
            {
                try
                {
                    productList = productList.FindAllWithCriteria(_criteria[i]);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка в лексикографической оптимизации:\n{ex.Message}");
                }
            }
            return productList;
        }
    }
}