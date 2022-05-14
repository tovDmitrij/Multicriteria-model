using Multicriteria_model.pages.settings;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System;
using System.Linq;

namespace Multicriteria_model.pages.criteria
{
    /// <summary>
    /// Страница с критериями для <see cref="pageHDD"/>
    /// </summary>
    public partial class pageHDD : Page
    {
        private readonly Task parentPage;
        /// <summary>
        /// Страница с критериями для <see cref="pageHDD"/>
        /// </summary>
        /// <param name="parentPage">Ссылка на страницу с задачей</param>
        public pageHDD(Task parentPage)
        {
            InitializeComponent();
            this.parentPage = parentPage;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Run(object sender, RoutedEventArgs e)
        {
            List<List<string>> list = DBConnection();
            List<HDD> products = GetProducts(list);

            #region Лексикографическая оптимизация
            SortedDictionary<byte, Characteristics> criteria = Criteria();
            List<HDD> lexicographisResultList = new Lexicographic<HDD>(products, criteria).Run();
            string lexicographicResult = "\tЛексикографическая оптимизация:\n";
            foreach (var item in lexicographisResultList)
            {
                lexicographicResult += $"{item.Print}\n";
            }
            #endregion

            #region Субоптимизация
            SortedDictionary<Characteristics, double> criteriaWithBorder = CriteriaWithBorder();
            List<HDD> suboptimizationResultList = new Suboptimization<HDD>(products, criteria.First().Value, criteriaWithBorder).Run();
            string subResult = "\tСубоптимизация:\n";
            foreach (var item in suboptimizationResultList)
            {
                subResult += $"{item.Print}\n";
            }
            #endregion

            #region Указание нижних границ критериев
            List<HDD> lcbResultList = new LowerCriteriaBorders<HDD>(products, criteriaWithBorder).Run();
            string lcbResult = "\tУказание нижних границ критериев:\n";
            foreach (var item in lcbResultList)
            {
                lcbResult += $"{item.Print}\n";
            }
            #endregion

            #region Обобщенный критерий
            SortedDictionary<Characteristics, double> criteriaWithWeights = CriteriaWithWeights(criteria);
            List<HDD> gcResultList = new GeneralizedCriterion<HDD>(products, criteriaWithWeights).Run();
            string gcResult = "\tОбобщенный критерий:\n";
            foreach (var item in gcResultList)
            {
                gcResult += $"{item.Print}\n";
            }
            #endregion

            #region Формирование множества Паретто
            List<HDD> poResultList = new ParetoOptimum<HDD>(products).Run();
            string poResult = "\tФормирование множества Паретто\n";
            foreach (var item in poResultList)
            {
                poResult += $"{item.Print}\n";
            }
            #endregion

            parentPage.results.Navigate(new Results(
                "*краткий результат*",
                $"{lexicographicResult}\n" +
                $"{subResult}\n" +
                $"{lcbResult}\n" +
                $"{gcResult}\n" +
                $"{poResult}"));
        }
        private List<List<string>> DBConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-R2N8P3B;Initial Catalog=DB_Goods;Integrated Security=True");
            string sql = $"select* from HDD";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<List<string>> list = new List<List<string>>();
            while (reader.Read())
                list.Add(new List<string>() { $"{reader.GetValue(0)}", $"{reader.GetValue(1)}", $"{reader.GetValue(2)}", $"{reader.GetValue(3)}" });
            return list;
        }
        private SortedDictionary<byte, Characteristics> Criteria()
        {
            SortedDictionary<byte, Characteristics> criteria = new SortedDictionary<byte, Characteristics>();
            criteria.Add(Convert.ToByte(pricePriority.Text), Characteristics.Price);
            criteria.Add(Convert.ToByte(speedPriority.Text), Characteristics.Speed);
            criteria.Add(Convert.ToByte(memoryPriority.Text), Characteristics.Memory);
            return criteria;
        }
        private SortedDictionary<Characteristics, double> CriteriaWithBorder()
        {
            SortedDictionary<Characteristics, double> criteria = new SortedDictionary<Characteristics, double>();
            criteria.Add(Characteristics.Price, -1 * Convert.ToDouble(priceValue.Text));
            criteria.Add(Characteristics.Speed, Convert.ToDouble(speedValue.Text));
            criteria.Add(Characteristics.Memory, Convert.ToDouble(memoryValue.Text));
            return criteria;
        }
        private SortedDictionary<Characteristics, double> CriteriaWithWeights(SortedDictionary<byte, Characteristics> criteriaList)
        {
            SortedDictionary<Characteristics, double> criteriaWeights = new SortedDictionary<Characteristics, double>();
            double weight = 1;
            int count = 1;
            foreach(var item in criteriaList)
            {
                double currentWeight = weight / count;
                criteriaWeights.Add(item.Value, currentWeight);
                count++;
            }
            return criteriaWeights;
        }
        private List<HDD> GetProducts(List<List<string>> list)
        {
            List<HDD> products = new List<HDD>();
            foreach (var k in list)
            {
                products.Add(new HDD(k[0], Convert.ToUInt32(k[1]), Convert.ToUInt32(k[2]), Convert.ToInt32(k[3])));
            }
            return products;
        }
    }
}