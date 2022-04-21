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
        public GeneralizedCriterion(List<T> products)
        {
            this.products = products;
        }
        public List<T> Run()
        {
            return products;
        }
    }
}
