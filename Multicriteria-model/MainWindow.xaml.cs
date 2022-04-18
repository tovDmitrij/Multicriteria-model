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
        List<List<string>> list = new List<List<string>>();
        void Foo()
        {
            Product m1 = new Monitor("Acer Nitro EI292CURPbmiipx черный", "2560x1080", 100, 25999);
            Product m2 = new Monitor("Acer Nitro VG252QPbmiipx черный", "1920x1080", 144, 20999);
            if (m1 is Monitor product1 && m2 is Monitor product2)
                if (product1.CompareTo(product2) == 0)
                    return;

            string sql = "select* from HDD";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
                list.Add(new List<string>() { $"{reader.GetValue(0)}", $"{reader.GetValue(1)}" });
        }
    }
}
