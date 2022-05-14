/* Субоптимизация

Выделяется один из критериев, а по всем остальным критериям назначаются нижние границы.
Оптимальным при этом считается исход, максимизирующий выделенный критерий на множестве исходов,
оценки которых по остальным притерием не ниже назначенных.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model
{
    /// <summary>
    /// Субоптимизация
    /// </summary>
    internal sealed class Suboptimization<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<Characteristics, double> criteria;
        private readonly Characteristics mainCriterion;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        /// <param name="mainCriterion">Главный <see cref="Characteristics"/> критерий</param>
        /// <param name="criteria">Список <see cref="Characteristics"/> критериев и их <see cref="double"/> нижние границы</param>
        public Suboptimization(List<T> products, Characteristics mainCriterion, SortedDictionary<Characteristics, double> criteria)
        {
            this.products = products;
            this.mainCriterion = mainCriterion;
            this.criteria = criteria;
        }
        /// <summary>
        /// Субоптимизация
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
                catch(Exception ex)
                {
                    string exception = "Ошибка в субоптимизации:\n";
                    exception += productList is null ? "Список товаров пустой!" : $"{ex}";
                    MessageBox.Show($"{exception}");
                    return productList;
                }
            }
            return productList.FindAll(mainCriterion);
        }
    }
}