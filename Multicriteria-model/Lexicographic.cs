/* Лексикографическая оптимизация

Лексикографическая оптимизация основана на упорядочении критериев по их относительной важности.
Отбираются исходы, которые имеют максимальную оценку по важнейшему критерию (первому).
Если такой исход единственный, то его и считают оптимальным. 
Если же таких исходов несколько, то среди них отбирают те, которые имеют максимальную оценку по следующему (за важнейшим критерием) и т.д.

*/

using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Лексикографическая оптимизация
    /// </summary>
    internal sealed class Lexicographic<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<byte, Characteristics> criteria;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        /// <param name="criteria">Список <see cref="Characteristics"/> критериев</param>
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
                Characteristics currentCriteria = criteria[i];
                switch (currentCriteria)
                {
                    case Characteristics.Price:
                        productList = productList.FindAll(productX => productX.Price == productList.Min(productY => productY.Price));
                        break;
                    case Characteristics.Speed:
                        if (productList is ISpeed productSpeed)
                            productList = productList.FindAll(productX => productSpeed.Speed == productList.Max(productY => productSpeed.Speed));
                        break;
                    case Characteristics.Memory:
                        if (productList is IMemory productMemory)
                            productList = productList.FindAll(productX => productMemory.Memory == productList.Max(productY => productMemory.Memory));
                        break;
                    case Characteristics.Frequency:
                        if (productList is IFrequency productFrequency)
                            productList = productList.FindAll(productX => productFrequency.Frequency == productList.Max(productY => productFrequency.Frequency));
                        break;
                    case Characteristics.Cores:
                        if (productList is ICores productCores)
                            productList = productList.FindAll(productX => productCores.Cores == productList.Max(productY => productCores.Cores));
                        break;
                    case Characteristics.ScreenSize:
                        if (productList is IScreenSize productIScreenSize)
                            productList = productList.FindAll(productX => productIScreenSize.ScreenSize == productList.Max(productY => productIScreenSize.ScreenSize));
                        break;
                    default:
                        break;
                }
            }
            return productList;
        }
    }
}