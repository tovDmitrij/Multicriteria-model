/* Указание нижних границ критериев

По всем критериям назначаются нижние границы.
Оптимальным при этом считается исход, удовлетворяющий всем нижним границам критериев.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model
{
    /// <summary>
    /// Указание нижних границ критериев
    /// </summary>
    internal sealed class LowerCriteriaBorders<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<Characteristics, double> criteria;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        /// <param name="criteria">Список <see cref="Characteristics"/> критериев и их <see cref="double"/> нижние границы</param>
        public LowerCriteriaBorders(List<T> products, SortedDictionary<Characteristics, double> criteria)
        {
            this.products = products;
            this.criteria = criteria;
        }
        /// <summary>
        /// Указание нижних границ критериев
        /// </summary>
        /// <returns>Список <see cref="T"/> товаров</returns>
        public List<T> Run()
        {
            List<T> productList = products;
            for (byte i = 0; i < criteria.Count; i++)
            {
                try
                {
                    productList = productList.FindAll(criteria.ElementAt(i).Key, criteria.ElementAt(i).Value);
                }
                catch (Exception ex)
                {
                    string exception = "Ошибка в субоптимизации:\n";
                    exception += productList is null ? "Список товаров пустой!" : $"{ex}";
                    MessageBox.Show($"{exception}");
                    return productList;
                }
            }
            return productList;
        }
    }
}