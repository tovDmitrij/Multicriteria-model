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
            List<T> newProductList = new List<T>();
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
                    summ[i] += Calculate(weights.ElementAt(j).Key, products, products[i]) * weights.ElementAt(j).Value;
                }
            }
            return summ;
        }
        private double Calculate(Characteristics currentCriteria, List<T> productList, T currentProduct)
        {
            switch (currentCriteria)
            {
                case (Characteristics.Price):
                    double tmpPrice = productList.Max(productX => (productX.Price));
                    return tmpPrice / currentProduct.Price;
                case (Characteristics.Memory):
                    double tmpMemory = productList.Max(productX => ((IMemory)productX).Memory);
                    return ((IMemory)currentProduct).Memory / tmpMemory;
                case (Characteristics.Speed):
                    double tmpSpeed = productList.Max(productX => ((ISpeed)productX).Speed);
                    return ((ISpeed)currentProduct).Speed / tmpSpeed;
                case (Characteristics.Frequency):
                    double tmpFrequency = productList.Max(productX => ((IFrequency)productX).Frequency);
                    return ((IFrequency)currentProduct).Frequency / tmpFrequency;
                case (Characteristics.Cores):
                    double tmpCores = productList.Max(productX => ((ICores)productX).Cores);
                    return ((ICores)currentProduct).Cores / tmpCores;
                case (Characteristics.ScreenSize):
                    double tmpScreenSize = productList.Max(productX => ((IScreenSize)productX).ScreenSize);
                    return ((IScreenSize)currentProduct).ScreenSize / tmpScreenSize;
                default: 
                    return 0.0;
            }
        }
    }
}