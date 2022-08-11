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
        private readonly Product[] _products;
        /// <summary>
        /// Формирование множества Парето
        /// </summary>
        /// <param name="products">Список товаров</param>
        public ParetoOptimum(Product[] products)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products),
                "Ошибка в формировании множества Парето:\nОтсутствует список товаров!");
        }
        /// <summary>
        /// Формирование множества Парето
        /// </summary>
        /// <returns>Список товаров</returns>
        public Product[] Run()
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
            return newList.ToArray();
        }
        private int[] ParetoArray()
        {
            int[,] paretoArray = new int[_products.Length, _products.Length];
            int[] summ = new int[_products.Length];
            Product[] productList = _products;
            for (int i = 0; i < productList.Length; i++)
            {
                for (int j = 0; j < summ.Length; j++)
                {
                    foreach (var currentChar in productList[i].Characteristics)
                    {
                        if (currentChar.Value is IFormattable)
                        {
                            paretoArray[j, i] += currentChar.Value > Array.Find(productList[j].Characteristics,
                                itemX => itemX.Name == currentChar.Name).Value ? 1 : 0;
                        }
                    }
                    summ[i] += paretoArray[j, i] > 1 ? 1 : 0;
                }
            }
            return summ;
        }
    }
}