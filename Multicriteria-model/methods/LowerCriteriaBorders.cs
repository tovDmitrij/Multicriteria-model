using System;
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Указание нижних границ критериев
    /// </summary>
    internal sealed class LowerCriteriaBorders
    {
        private readonly List<Product> _products;
        private readonly SortedDictionary<Characteristic, double> _criteria;
        /// <summary>
        /// Указание нижних границ критериев
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="criteria">Список  критериев и их нижние границы</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LowerCriteriaBorders(List<Product> products, SortedDictionary<Characteristic, double> criteria)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в указании нижних границ критериев:\nОтсутствует список товаров!");
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria),
                "Ошибка в указании нижних границ критериев:\nОтсутствует список критериев!");
        }
        /// <summary>
        /// Указание нижних границ критериев
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
                    throw new Exception($"Ошибка в указании нижних границ критериев:\n{ex.Message}");
                }
            }
            return productList;
        }
    }
}