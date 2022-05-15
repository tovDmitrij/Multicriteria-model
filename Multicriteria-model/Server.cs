using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
namespace Multicriteria_model
{
    /// <summary>
    /// Сервер с БД товаров
    /// </summary>
    internal static class Server
    {
        /// <summary>
        /// Название сервера
        /// </summary>
        public static string Name { get; set; }
        /// <summary>
        /// Подключение к БД и получение из него товаров
        /// </summary>
        /// <param name="productType">Тип товара</param>
        /// <returns>Список товаров</returns>
        public static List<List<string>> DBConnection(Type type)
        {
            SqlConnection sqlConnection;
            string sql = $"select* from {type.Name}";
            try
            {
                if (Name == null || Name == "")
                    throw new Exception();
                sqlConnection = new SqlConnection(@$"Data Source={Name};Initial Catalog=DB_Goods;Integrated Security=True");
                sqlConnection.Open();
            }
            catch
            {
                MessageBox.Show("Сервер не найден!");
                return null;
            }
            SqlDataReader reader;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                reader = cmd.ExecuteReader();
            }
            catch
            {
                MessageBox.Show("Неправильно составлен SQL-запрос!");
                return null;
            }
            List<List<string>> productStringList = new List<List<string>>();
            while (reader.Read())
            {
                productStringList.Add(new List<string>() { $"{reader.GetValue(0)}", $"{reader.GetValue(1)}", $"{reader.GetValue(2)}", $"{reader.GetValue(3)}" });
            }
            return productStringList;
        }
    }
}