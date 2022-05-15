/* Формирование множества Паретто

По заданному массиву альтернатив находится множество [i,j] Парето-оптимальных исходов (ParetoArray()),
из которого выбор оптимального исхода определяется как максимальная j-я сумма Парето-оптимального исхода
для текущей альтернативы.

*/
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /// <summary>
    /// Формирование множества Паретто
    /// </summary>
    internal sealed class ParetoOptimum<T> where T : Product
    {
        private readonly List<T> products;
        /// <param name="products">Список <see cref="T"/> товаров</param>
        public ParetoOptimum(List<T> products)
        {
            this.products = products;
        }
        /// <summary>
        /// Формирование множества Паретто
        /// </summary>
        /// <returns>Список <see cref="T"/> товаров</returns>
        public List<T> Run()
        {
            int[] summ = ParetoArray();
            List<T> newList = new List<T>();
            for(int i = 0; i < summ.Length; i++)
            {
                if (summ[i] == summ.Max())
                {
                        newList.Add(products[i]);
                }
            }
            return newList;
        }
        private int[] ParetoArray()
        {
            int[,] paretoArray = new int[products.Count, products.Count];
            int[] summ = new int[products.Count];
            List<T> productList = products;
            for (int i = 0; i < productList.Count; i++)
            {
                for (int j = 0; j < summ.Length; j++)
                {
                    if (productList[i] is ISpeed productFirstSpeed && productList[j] is ISpeed productSecondSpeed)
                    {
                        paretoArray[i, j] += productFirstSpeed.Speed > productSecondSpeed.Speed ? 1 : 0;
                    }
                    if (productList[i] is IMemory productFirstMemory && productList[j] is IMemory productSecondMemory)
                    {
                        paretoArray[i, j] += productFirstMemory.Memory > productSecondMemory.Memory ? 1 : 0;
                    }
                    if (productList[i] is IFrequency productFirstFrequency && productList[j] is IFrequency productSecondFrequency)
                    { 
                        paretoArray[i, j] += productFirstFrequency.Frequency > productSecondFrequency.Frequency ? 1 : 0;
                    }
                    if (productList[i] is ICores productFirstCores && productList[j] is ICores productSecondCores)
                    {
                        paretoArray[i, j] += productFirstCores.Cores > productSecondCores.Cores ? 1 : 0;
                    }
                    if (productList[i] is IScreenSize productFirstScreenSize && productList[j] is IScreenSize productSecondScreenSize)
                    { 
                        paretoArray[i, j] += productFirstScreenSize.ScreenSize > productSecondScreenSize.ScreenSize ? 1 : 0;
                    }
                    if (productList[i] is Product productFirst && productList[j] is Product productSecond)
                    {
                        paretoArray[i, j] += productFirst.Price < productSecond.Price ? 1 : 0;
                    }
                    summ[i] += paretoArray[i, j] > 1 ? 1 : paretoArray[i, j];
                }
            }
            return summ;
        }
    }
}