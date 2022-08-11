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
        private readonly Product[] _products;
        private readonly Characteristic[] _criteria;
        /// <summary>
        /// Указание нижних границ критериев
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="criteria">Список  критериев и их нижние границы</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LowerCriteriaBorders(Product[] products, Characteristic[] criteria)
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
        public Product[] Run()
        {
            Product[] productList = _products;
            for (byte i = 0; i < _criteria.Length; i++)
            {
                try
                {
                    productList = productList.FindAllWithBorder(_criteria[i]);
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