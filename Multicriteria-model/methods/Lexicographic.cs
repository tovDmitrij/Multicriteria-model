/* Лексикографическая оптимизация

Лексикографическая оптимизация основана на упорядочении критериев по их относительной важности.
Отбираются исходы, которые имеют максимальную оценку по важнейшему критерию (первому).
Если такой исход единственный, то его и считают оптимальным. 
Если же таких исходов несколько, то среди них отбирают те, которые имеют максимальную оценку по следующему (за важнейшим критерием) и т.д.

*/
using System;
using System.Collections.Generic;
using System.Windows;
namespace Multicriteria_model
{
    /// <summary>
    /// Лексикографическая оптимизация
    /// </summary>
    /// 
    internal sealed class Lexicographic<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<byte, Characteristics> criteria;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        /// <param name="criteria">Список <see cref="Characteristics"/> критериев и их <see cref="byte"/> порядок</param>
        public Lexicographic(List<T> products, SortedDictionary<byte, Characteristics> criteria)
        {
            this.products = products;
            this.criteria = criteria;
        }
        /// <summary>
        /// Лексикографическая оптимизация
        /// </summary>
        /// <returns>Список <see cref="T"/> товаров</returns>
        public List<T> Run()
        {
            List<T> productList = products;
            for (byte i = 1; i <= criteria.Count; i++)
            {
                try
                {
                    productList = productList.FindAll(criteria[i]);
                }
                catch(Exception ex)
                {
                    string exception = "Ошибка в лексикографической оптимизации:\n";
                    exception += productList is null ? "Список товаров пустой!" : $"{ex}";
                    MessageBox.Show($"{exception}");
                    return productList;
                }
            }
            return productList;
        }
    }
}