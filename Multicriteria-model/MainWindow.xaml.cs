﻿using System;
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

namespace Multicriteria_model
{
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-R2N8P3B;Initial Catalog=DB_Goods;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
            Foo();
        }
        void Foo()
        {
            string sql = "select* from HDD";
            //string sql = "select* from Videocards";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<List<string>> list = new List<List<string>>();
            while (reader.Read())
                list.Add(new List<string>() { $"{reader.GetValue(0)}", $"{reader.GetValue(1)}", $"{reader.GetValue(2)}", $"{reader.GetValue(3)}" });
            //Имя память частота цена
            List<HDD> pr = new List<HDD>();
            foreach (var k in list)
            {
                pr.Add(new HDD(k[0], Convert.ToUInt32(k[1]), Convert.ToUInt32(k[2]), Convert.ToUInt32(k[3])));
            }

            //List<Videocard> pr = new List<Videocard>();
            //foreach (var k in list)
            //{
            //    pr.Add(new Videocard(k[0], Convert.ToUInt32(k[1]), Convert.ToUInt32(k[2]), Convert.ToUInt32(k[3])));
            //}

            #region Лексикографическая оптимизация
            /*
            Dictionary<byte, Characteristics> ddd = new Dictionary<byte, Characteristics>();
            ddd.Add(1, Сharacteristics.Price);
            ddd.Add(2, Сharacteristics.Speed);
            ddd.Add(3, Сharacteristics.Memory);
            Lexicographic<Videocard> lx = new Lexicographic<Videocard>(pr, ddd);
            lx.Run();
            */
            #endregion

            #region Субоптимизация
            Dictionary<KeyValuePair<double, double>, Characteristics> ddd = new Dictionary<KeyValuePair<double, double>, Characteristics>();
            ddd.Add(new KeyValuePair<double, double>(2048, 4096), Characteristics.Memory);
            Suboptimization<HDD> sb = new Suboptimization<HDD>(pr, Characteristics.Price, ddd);
            sb.Run();
            #endregion
        }
    }
}
