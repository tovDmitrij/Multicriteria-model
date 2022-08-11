using System;
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Обобщённый критерий
    /// </summary>
    internal sealed class GeneralizedCriterion
    {
        private readonly Product[] _products;
        private readonly Characteristic[] _weights;
        /// <summary>
        /// Обобщённый критерий
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="weights">Список весов критериев</param>
        public GeneralizedCriterion(Product[] products, Characteristic[] weights)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в обобщённом критерии:\nОтсутствует список товаров!");
            _weights = weights ?? throw new ArgumentNullException(nameof(weights),
                "Ошибка в обобщённом критерии:\nОтсутствует список весов!");
        }
        /// <summary>
        /// Обобщённый критерий
        /// </summary>
        /// <returns>Список товаров</returns>
        public Product[] Run()
        {
            double[] summ = GenCriterion();
            List<Product> newProductList = new();
            for (int i = 0; i < summ.Length; i++)
            {
                if (summ[i] == summ.Max())
                {
                    newProductList.Add(_products[i]);
                }
            }
            return newProductList.ToArray();
        }
        private double[] GenCriterion()
        {
            double[] summ = new double[_products.Length];
            for (int i = 0; i < _products.Length; i++)
            {
                for (int j = 0; j < _weights.Length; j++)
                {
                    summ[i] += Calculate(_weights[j]) * _weights.ElementAt(j).Value;
                }
            }
            return summ;
        }
        private double Calculate(Characteristic currentCriteria)
        {
            double maxValue = _products.Max(
                productY => Array.Find(
                    productY.Characteristics, criteriaY => criteriaY.Name == currentCriteria.Name).Value);
            return currentCriteria.Value / maxValue;
        }
    }
}