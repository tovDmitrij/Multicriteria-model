using System;
namespace Multicriteria_model
{
    /// <summary>
    /// Субоптимизация
    /// </summary>
    internal sealed class Suboptimization
    {
        private readonly Product[] _products;
        private readonly Characteristic[] _criteria;
        private readonly Characteristic _mainCriterion;
        /// <summary>
        /// Субоптимизация
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="mainCriterion">Главный критерий</param>
        /// <param name="criteria">Список критериев и их нижние границы</param>
        public Suboptimization(Product[] products, Characteristic mainCriterion, Characteristic[] criteria)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в субоптимизации:\nОтсутствует список товаров!");
            _mainCriterion = mainCriterion;
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria),
                "Ошибка в субоптимизации:\nОтсутствует список критериев!");
        }
        /// <summary>
        /// Субоптимизация
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
                    throw new Exception($"Ошибка в субоптимизации:\n{ex.Message}");
                }
            }
            return productList.FindAllWithCriteria(_mainCriterion);
        }
    }
}