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
        /// Наименование БД
        /// </summary>
        public static string Database { get; set; }
        /// <summary>
        /// Подключение к БД и получение из него товаров
        /// </summary>
        /// <param name="productType">Тип товара</param>
        /// <returns>Список товаров</returns>
        public static Product[] GetProducts(string productType)
        {
            #region Подключение к БД
            SqlConnection sqlConnection;
            try
            {
                if (Name == "" || Database == "")
                {
                    throw new System.Exception();
                }
                sqlConnection = new SqlConnection(@$"Data Source={Name};Initial Catalog={Database};Integrated Security=True");
                sqlConnection.Open();
            }
            catch
            {
                throw new System.Exception("Ошибка при подключении к серверу:\nОтсутствует сервер или БД с таким наименованием!");
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
                throw new System.Exception($"Ошибка при подключении к серверу:\nНеправильно составлен SQL-запрос:\n{sql}");
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
                productList.Add(new Product(characteristics.ToArray()));
            }
            return productList.ToArray();
            #endregion
        }
    }
}