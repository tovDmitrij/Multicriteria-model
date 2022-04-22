/* Субоптимизация

Выделяется один из критериев, а по всем остальным критериям назначаются нижние границы.
Оптимальным при этом считается исход, максимизирующий выделенный критерий на множестве исходов,
оценки которых по остальным притерием не ниже назначенных.

*/
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Субоптимизация
    /// </summary>
    internal sealed class Suboptimization<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<double, Characteristics> criteria;
        private readonly Characteristics mainCriterion;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        /// <param name="mainCriterion">Главный <see cref="Characteristics"/> критерий</param>
        /// <param name="criteria">Список <see cref="Characteristics"/> критериев и их <see cref="double"/> нижние границы</param>
        public Suboptimization(List<T> products, Characteristics mainCriterion, SortedDictionary<double, Characteristics> criteria)
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
                Characteristics currentCriteria = criteria.ElementAt(i).Value;
                double border = criteria.ElementAt(i).Key;
                switch (currentCriteria)
                {
                    case Characteristics.Price:
                        productList = productList.FindAll(productX => productX.Price <= border);
                        break;
                    case Characteristics.Speed:
                        if (productList is ISpeed productSpeed)
                            productList = productList.FindAll(productX => productSpeed.Speed >= border);
                        break;
                    case Characteristics.Memory:
                        if (productList is IMemory productMemory)
                            productList = productList.FindAll(productX => productMemory.Memory >= border);
                        break;
                    case Characteristics.Frequency:
                        if (productList is IFrequency productFrequency)
                            productList = productList.FindAll(productX => productFrequency.Frequency >= border);
                        break;
                    case Characteristics.Cores:
                        if (productList is ICores productCores)
                            productList = productList.FindAll(productX => productCores.Cores >= border);
                        break;
                    case Characteristics.ScreenSize:
                        if (productList is IScreenSize productIScreenSize)
                            productList = productList.FindAll(productX => productIScreenSize.ScreenSize >= border);
                        break;
                    default:
                        break;
                }
            }
            switch (mainCriterion)
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
            return productList;
        }
    }
}