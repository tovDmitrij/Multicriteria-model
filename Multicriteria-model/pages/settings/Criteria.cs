using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model.pages.settings
{
    /// <summary>
    /// Страница с критериями
    /// </summary>
    public class Criteria<T> where T : Product
    {
        public List<List<string>> Initialize()
        {
            return Server.DBConnection(typeof(T));
        }
        public List<string> Run(List<T> products, SortedDictionary<Characteristics, double> criteriaWithBorderList, SortedDictionary<byte, Characteristics> criteriaList)
        {
            SortedDictionary<byte, Characteristics> criteria = CriteriaWithOrders(criteriaList);
            SortedDictionary<Characteristics, double> criteriaWithWeights = CriteriaWithWeights(criteria);

            SortedDictionary<byte, Characteristics> tmp1 = criteriaList;
            SortedDictionary<Characteristics, double> tmp2 = new();
            foreach(var item in tmp1)
            {
                if (criteriaWithBorderList.ContainsKey(item.Value) && item.Key != 1)
                {
                    tmp2.Add(item.Value, criteriaWithBorderList[item.Value]);
                }
            }

            SortedDictionary<Characteristics, double> criteriaWithBorder1 = CriteriaWithBorder(tmp2);
            SortedDictionary<Characteristics, double> criteriaWithBorder2 = CriteriaWithBorder(criteriaWithBorderList);

            //SortedDictionary<Characteristics, double> criteriaWithBorder = CriteriaWithBorder(criteriaWithBorderList);
            if (products == null || products.Count == 0)
            {
                MessageBox.Show("База данных пустая!");
                return default;
            }
            string lexicographicResult = Lexicographic(products, criteria);
            string subResult = Suboptimization(products, criteria, criteriaWithBorder1);
            string lcbResult = LowerCriteriaBorders(products, criteriaWithBorder2);
            string gcResult = GeneralizedCriterion(products, criteriaWithWeights);
            string poResult = ParetoOptimum(products);
            return new List<string>() { lexicographicResult, subResult, lcbResult, gcResult, poResult };
        }
        /// <summary>
        /// Вызывает лексикографическую оптимизацию
        /// </summary>
        /// <typeparam name="T">Тип товара</typeparam>
        /// <param name="products">Список товаров</param>
        /// <param name="criteria">Список критериев</param>
        /// <returns>Результат оптимизации</returns>
        private string Lexicographic(List<T> products, SortedDictionary<byte, Characteristics> criteria)
        {
            List<T> lexicographisResultList = new Lexicographic<T>(products, criteria).Run();
            string lexicographicResult = "";
            foreach (var item in lexicographisResultList)
            {
                lexicographicResult += $"{item.Print}\n";
            }
            return lexicographicResult;
        }
        /// <summary>
        /// Вызывает субоптимизацию
        /// </summary>
        /// <typeparam name="T">Тип товара</typeparam>
        /// <param name="products">Список товаров</param>
        /// <param name="criteria">Список критериев</param>
        /// <param name="criteriaWithBorder">Список критериев с нижними границами</param>
        /// <returns>Результат оптимизации</returns>
        private string Suboptimization(List<T> products, SortedDictionary<byte, Characteristics> criteria, SortedDictionary<Characteristics, double> criteriaWithBorder)
        {
            List<T> suboptimizationResultList = new Suboptimization<T>(products, criteria.First().Value, criteriaWithBorder).Run();
            string subResult = "";
            foreach (var item in suboptimizationResultList)
            {
                subResult += $"{item.Print}\n";
            }
            return subResult;
        }
        /// <summary>
        /// Вызывает оптимизацию "Указание нижних границ критериев
        /// </summary>
        /// <typeparam name="T">Тип товара</typeparam>
        /// <param name="products">Список товаров</param>        
        /// <param name="criteriaWithBorder">Список критериев с нижними границами</param>        
        /// <returns>Результат оптимизации</returns>
        private string LowerCriteriaBorders(List<T> products, SortedDictionary<Characteristics, double> criteriaWithBorder)
        {
            List<T> lcbResultList = new LowerCriteriaBorders<T>(products, criteriaWithBorder).Run();
            string lcbResult = "";
            foreach (var item in lcbResultList)
            {
                lcbResult += $"{item.Print}\n";
            }
            return lcbResult;
        }
        /// <summary>
        /// Вызывает оптимизацию "Обобщённый критерий"
        /// </summary>
        /// <typeparam name="T">Тип товара</typeparam>
        /// <param name="products">Список товаров</param>
        /// <param name="criteriaWithWeights">Список критериев с весами</param>
        /// <returns></returns>
        private string GeneralizedCriterion(List<T> products, SortedDictionary<Characteristics, double> criteriaWithWeights)
        {
            List<T> gcResultList = new GeneralizedCriterion<T>(products, criteriaWithWeights).Run();
            string gcResult = "";
            foreach (var item in gcResultList)
            {
                gcResult += $"{item.Print}\n";
            }
            return gcResult;
        }
        /// <summary>
        /// вызывает оптимизацию "Парето оптимум"
        /// </summary>
        /// <typeparam name="T">Тип товара</typeparam>
        /// <param name="products">Список товаров</param>
        /// <returns>Результат оптимизации</returns>
        private string ParetoOptimum(List<T> products)
        {
            List<T> poResultList = new ParetoOptimum<T>(products).Run();
            string poResult = "";
            foreach (var item in poResultList)
            {
                poResult += $"{item.Print}\n";
            }
            return poResult;
        }
        /// <summary>
        /// Список критериев и их порядок
        /// </summary>
        /// <returns>Возвращает список критериев и их порядок</returns>
        private SortedDictionary<byte, Characteristics> CriteriaWithOrders(SortedDictionary<byte, Characteristics> criteriaList)
        {
            SortedDictionary<byte, Characteristics> criteria = new SortedDictionary<byte, Characteristics>();
            foreach (var item in criteriaList)
            {
                criteria.Add(item.Key, item.Value);
            }
            return criteria;
        }
        /// <summary>
        /// Список критериев с указанными нижними границами
        /// </summary>
        /// <returns>Возвращает список критериев с указанными нижними границами</returns>
        private SortedDictionary<Characteristics, double> CriteriaWithBorder(SortedDictionary<Characteristics, double> criteriaWithBorderList)
        {
            SortedDictionary<Characteristics, double> criteria = new SortedDictionary<Characteristics, double>();
            foreach (var item in criteriaWithBorderList)
            {
                criteria.Add(item.Key, item.Value);
            }
            return criteria;
        }
        /// <summary>
        /// Список критериев и их веса
        /// </summary>
        /// <param name="criteriaList">Возвращает список критериев и их веса</param>
        /// <returns></returns>
        private SortedDictionary<Characteristics, double> CriteriaWithWeights(SortedDictionary<byte, Characteristics> criteriaList)
        {
            SortedDictionary<Characteristics, double> criteriaWeights = new SortedDictionary<Characteristics, double>();
            double weight = 0.6;
            int count = 1;
            foreach (var item in criteriaList)
            {
                double currentWeight = weight / count;
                criteriaWeights.Add(item.Value, currentWeight);
                count++;
            }
            return criteriaWeights;
        }
    }
}