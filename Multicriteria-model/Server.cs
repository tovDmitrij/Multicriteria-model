using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Multicriteria_model
{
    /// <summary>
    /// Сервер с БД товаров (MSSQL)
    /// </summary>
    internal static class Server
    {
        /// <summary>
        /// Наименование сервера
        /// </summary>
        public static string Name { get; set; }
        /// <summary>
        /// Подключение к БД и получение из него товаров
        /// </summary>
        /// <param name="productType">Тип товара</param>
        /// <returns>Список товаров</returns>
        public static List<Product> GetProducts(string productType)
        {
            #region Подключение к БД

            string DataBase = "DB_Goods";
            if (Name == "" || Name == null)
            {
                throw new ArgumentNullException(nameof(Name), 
                    "Наименование сервера пустое!");
            }
            SqlConnection sqlConnection;
            try
            {
                sqlConnection = new SqlConnection(@$"Data Source={Name};Initial Catalog={DataBase};Integrated Security=True");
                sqlConnection.Open();
            }
            catch
            {
                throw new System.Exception($"Сервер с наименованием {Name} не найден!");
            }

            #endregion
            #region Изъятие из БД записей

            SqlDataReader reader;
            string sql = $"select* from {productType}";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                reader = cmd.ExecuteReader();
            }
            catch
            {
                throw new System.Exception($"Неправильно составлен SQL-запрос!\n{sql}");
            }

            #endregion
            #region Преобразование записей в список товаров

            List<Product> productList = new();
            while (reader.Read())
            {
                List<Characteristic> characteristics = new();
                int index = 0;
                while (true)
                {
                    try
                    {
                        characteristics.Add(new Characteristic(reader.GetName(index), reader.GetValue(index)));
                        index++;
                    }
                    catch
                    {
                        break;
                    }
                }
                productList.Add(new Product(characteristics));
            }
            return productList;

            #endregion
        }
    }
}