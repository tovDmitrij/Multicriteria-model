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
        private readonly List<Product> _products;
        private readonly SortedDictionary<Characteristic, double> _weights;
        /// <summary>
        /// Обобщённый критерий
        /// </summary>
        /// <param name="products">Список товаров</param>
        /// <param name="weights">Список весов критериев</param>
        public GeneralizedCriterion(List<Product> products, SortedDictionary<Characteristic, double> weights)
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
        public List<Product> Run()
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
            return newProductList;
        }
        private double[] GenCriterion()
        {
            double[] summ = new double[_products.Count];
            for(int i = 0; i < _products.Count; i++)
            {
                for (int j = 0; j < _weights.Count; j++)
                {
                    summ[i] += Calculate(_weights.ElementAt(j).Key, _products, _products[i]) * _weights.ElementAt(j).Value;
                }
            }
            return summ;
        }
        private double Calculate(Characteristic currentCriteria, List<Product> productList, Product currentProduct)
        {
            double maxValue = productList.Max(
                productY => productY.Characteristics.Find(
                    criteriaY => criteriaY.Name == currentCriteria.Name).Value);
            return currentCriteria.Value / maxValue;
        }
    }
}