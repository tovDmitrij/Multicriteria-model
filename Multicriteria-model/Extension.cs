using System;
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    public static class Extensions
    {
        /// <summary>
        /// Возвращает список товаров
        /// </summary>
        /// <param name="productList">Список товаров</param>
        /// <param name="currentCriteria">Текущий критерий</param>
        public static List<Product> FindAll(this List<Product> productList, Characteristic currentCriteria)
        {
            try
            {
                return productList.FindAll(productX => 
                    productX.Characteristics.Contains(currentCriteria) &&
                    productX.Characteristics.Find(criteriaX => criteriaX.Name == currentCriteria.Name).Value ==
                    productList.Max(productY =>
                        productY.Characteristics.Find(criteriaY => criteriaY.Name == currentCriteria.Name).Value));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при фильтрации по критерию:\n{ex.Message}");
            }
        }
        /// <summary>
        /// Возвращает список товаров
        /// </summary>
        /// <param name="productList">Список товаров</param>
        /// <param name="currentCriteria">Текущий критерий</param>
        /// <param name="border">Нижняя граница текущего критерия</param>
        public static List<Product> FindAll(this List<Product> productList, Characteristic currentCriteria, double border)
        {
            try
            {
                return productList.FindAll(productX =>
                    productX.Characteristics.Contains(currentCriteria) &&
                    productX.Characteristics.Find(criteriaX => criteriaX.Name == currentCriteria.Name).Value >= border);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка с фильтрацией по границе:\n{ex.Message}");
            }
        }
    }
}