using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Multicriteria_model.pages.settings;

namespace Multicriteria_model
{
    public partial class Main : Window
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-R2N8P3B;Initial Catalog=DB_Goods;Integrated Security=True");
        public Main()
        {
            InitializeComponent();
            frame.Navigate(new pages.settings.Task());
            //Foo(typeof(HDD));
        }
        void Foo(Type productType)
        {
            string sql = $"select* from {productType}";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<List<string>> list = new List<List<string>>();
            while (reader.Read())
                list.Add(new List<string>() { $"{reader.GetValue(0)}", $"{reader.GetValue(1)}", $"{reader.GetValue(2)}", $"{reader.GetValue(3)}" });

            SortedDictionary<byte, Characteristics> criteria = new SortedDictionary<byte, Characteristics>();
            criteria.Add(1, Characteristics.Price);
            criteria.Add(2, Characteristics.Speed);
            criteria.Add(3, Characteristics.Memory);

            List<HDD> products;
            //switch (productType)
            //{
            //    case typeof(HDD):
            //        products = new List<HDD>();
            //        foreach (var k in list)
            //        {
            //            products.Add(new HDD(k[0], Convert.ToUInt32(k[1]), Convert.ToUInt32(k[2]), Convert.ToUInt32(k[3])));
            //        }
            //        Lexicographic<HDD> lx = new Lexicographic<HDD>(products, criteria);
            //        List<HDD> z = lx.Run();
            //        break;
            //    //case "Videocards":
            //        //products = new List<Videocard>();
            //        //foreach (var k in list)
            //        //{
            //        //    products.Add(new Videocard(k[0], Convert.ToUInt32(k[1]), Convert.ToUInt32(k[2]), Convert.ToUInt32(k[3])));
            //        //}
            //        //break;
            //}

            #region Лексикографическая оптимизация
            //SortedDictionary<byte, Characteristics> criteria = new SortedDictionary<byte, Characteristics>();
            //criteria.Add(1, Characteristics.Price);
            //criteria.Add(2, Characteristics.Speed);
            //criteria.Add(3, Characteristics.Memory);
            //Lexicographic<HDD> lx = new Lexicographic<HDD>(products, criteria);
            //List<HDD> z = lx.Run();
            #endregion

            #region Субоптимизация
            //SortedDictionary<KeyValuePair<double, double>, Characteristics> ddd = new SortedDictionary<KeyValuePair<double, double>, Characteristics>();
            //ddd.Add(new KeyValuePair<double, double>(2048, 4096), Characteristics.Memory);
            //ddd.Add(new KeyValuePair<double, double>(2048, 4096), Characteristics.Memory);
            //Suboptimization<HDD> sb = new Suboptimization<HDD>(pr, Characteristics.Price, ddd);
            //sb.Run();
            #endregion
        }
    }
}
