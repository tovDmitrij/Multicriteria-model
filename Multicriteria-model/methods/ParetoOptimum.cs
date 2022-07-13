using System;
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Формирование множества Парето
    /// </summary>
    internal sealed class ParetoOptimum
    {
        private readonly List<Product> _products;
        /// <summary>
        /// Формирование множества Парето
        /// </summary>
        /// <param name="products">Список товаров</param>
        public ParetoOptimum(List<Product> products)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в формировании множества Парето:\nОтсутствует список товаров!");
        }
        /// <summary>
        /// Формирование множества Парето
        /// </summary>
        /// <returns>Список товаров</returns>
        public List<Product> Run()
        {
            int[] summ = ParetoArray();
            List<Product> newList = new();
            for(int i = 0; i < summ.Length; i++)
            {
                if (summ[i] == summ.Max())
                {
                        newList.Add(_products[i]);
                }
            }
            return newList;
        }
        private int[] ParetoArray()
        {
            int[,] paretoArray = new int[_products.Count, _products.Count];
            int[] summ = new int[_products.Count];
            List<Product> productList = _products;
            for (int i = 0; i < productList.Count; i++)
            {
                for (int j = 0; j < summ.Length; j++)
                {
                    foreach (var currentChar in productList[i].Characteristics)
                    {
                        paretoArray[j, i] += currentChar.Value > productList[j].Characteristics.Find(
                            itemX => itemX.Name == currentChar.Name) ? 1 : 0;
                    }
                    summ[i] += paretoArray[j, i] > 1 ? 1 : 0;
                }
            }
            return summ;
        }
    }
}