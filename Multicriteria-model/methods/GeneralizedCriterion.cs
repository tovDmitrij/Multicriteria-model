/* Обобщенный критерий

Процедура, которая "синтезирует" набор оценок по заданным критериям

*/
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Обобщенный критерий
    /// </summary>
    internal sealed class GeneralizedCriterion<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<Characteristics, double> weights;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        /// <param name="weights">Список <see cref="SortedDictionary"/> весов критериев</param>
        public GeneralizedCriterion(List<T> products, SortedDictionary<Characteristics, double> weights)
        {
            this.products = products;
            this.weights = weights;
        }
        /// <summary>
        /// Обобщенный критерий
        /// </summary>
        /// <returns>Список <see cref="T"/> товаров</returns>
        public List<T> Run()
        {
            double[] summ = GenCriterion();
            List<T> newProductList = products;
            for (int i = 0; i < summ.Length; i++)
            {
                if (summ[i] == summ.Max())
                {
                    newProductList.Add(products[i]);
                }
            }
            return newProductList;
        }
        private double[] GenCriterion()
        {
            double[] summ = new double[products.Count];
            for(int i = 0; i < products.Count; i++)
            {
                for (int j = 0; j < weights.Count; j++)
                {
                    summ[i] += Calculate(weights.ElementAt(j).Key, weights.ElementAt(j).Value, products, products[i]);
                }
            }
            return summ;
        }
        private double Calculate(Characteristics currentCriteria, double currentWeight , List<T> productList, T currentProduct)
        {
            return currentCriteria switch
            {
                Characteristics.Price => currentProduct.Price / productList.Max(productX => (currentProduct.Price)) * currentWeight,
                Characteristics.Memory => ((IMemory)currentProduct).Memory / productList.Max(productX => ((IMemory)currentProduct).Memory),
                Characteristics.Speed => ((ISpeed)currentProduct).Speed / productList.Max(productX => ((ISpeed)currentProduct).Speed),
                Characteristics.Frequency => ((IFrequency)currentProduct).Frequency / productList.Max(productX => ((IFrequency)currentProduct).Frequency),
                Characteristics.Cores => ((ICores)currentProduct).Cores / productList.Max(productX => ((ICores)currentProduct).Cores),
                Characteristics.ScreenSize => ((IScreenSize)currentProduct).ScreenSize / productList.Max(productX => ((IScreenSize)currentProduct).ScreenSize),
                _ => 0.0,
            };
        }
    }
}