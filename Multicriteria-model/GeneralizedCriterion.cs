using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model
{
/* Обобщенный критерий

Процедура, которая "синтезирует" набор оценок по заданным

*/
    class GeneralizedCriterion<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<Characteristics, double> weights;
        public GeneralizedCriterion(List<T> products, SortedDictionary<Characteristics, double> weights)
        {
            this.products = products;
            this.weights = weights;
        }
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
            try
            {
                switch (products)
                {
                    #region Жесткие диски
                    case List<HDD>:
                        List<HDD> HDDList = products as List<HDD>;
                        for(int i = 0; i < products.Count; i++)
                        {
                            for (int j = 0; j < weights.Count; j++)
                            {
                                tableSumm[i, j] = HDDList[i].Memory;
                                //switch (weights.ElementAt(j).Key)
                                //{
                                //    case Characteristics.Price:
                                //        tableSumm[i, j] = HDDList[i].Price * weights.ElementAt(j).Value;
                                //        break;
                                //    case Characteristics.Memory:
                                //        tableSumm[i, j] = HDDList[i].Memory * weights.ElementAt(j).Value;
                                //        break;
                                //    case Characteristics.Speed:
                                //        tableSumm[i,j]
                                //}
                            }
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
                return null;
            }
            return summ;
        }
    }
}
