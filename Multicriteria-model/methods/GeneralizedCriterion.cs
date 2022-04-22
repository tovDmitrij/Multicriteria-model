/* Обобщенный критерий

Процедура, которая "синтезирует" набор оценок по заданным

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
            double[] summ = genCriterion();
            List<T> newList = products;
            for (int i = 0; i < summ.Length; i++)
                if (summ[i] == summ.Max())
                    newList.Add(products[i]);
            return newList;
        }
        private double[] genCriterion()
        {
            double[,] tableSumm = new double[products.Count, weights.Count];
            double[] summ = new double[products.Count];
            List<T> productList = products;
            for(int i = 0; i < products.Count; i++)
            {
                for (int j = 0; j < weights.Count; j++)
                {
                    switch (weights.ElementAt(j).Key)
                    {
                        case Characteristics.Price:
                            if (productList[i] is Product productPrice)
                                tableSumm[i, j] = productList.Min(productX => productPrice.Price) / productPrice.Price;
                            break;
                        case Characteristics.Memory:
                            if(productList[i] is IMemory productMemory)
                                tableSumm[i, j] = productMemory.Memory / productList.Max(productX => productMemory.Memory);
                            break;
                        case Characteristics.Speed:
                            if (productList[i] is ISpeed productSpeed)
                                tableSumm[i, j] = productSpeed.Speed / productList.Max(productX => productSpeed.Speed);
                            break;
                        case Characteristics.Frequency:
                            if (productList[i] is IFrequency productFrequency)
                                tableSumm[i, j] = productFrequency.Frequency / productList.Max(productX => productFrequency.Frequency);
                            break;
                        case Characteristics.Cores:
                            if (productList[i] is ICores productCores)
                                tableSumm[i, j] = productCores.Cores / productList.Max(productX => productCores.Cores);
                            break;
                        case Characteristics.ScreenSize:
                            if (productList[i] is IScreenSize productScreenSize)
                                tableSumm[i, j] = productScreenSize.ScreenSize / productList.Max(productX => productScreenSize.ScreenSize);
                            break;
                    }
                    summ[i] += tableSumm[i, j] * weights.ElementAt(j).Value;
                }
            }
            return summ;
        }
    }
}