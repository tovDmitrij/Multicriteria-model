using System;
using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    public static class Extensions
    {
        /// <summary>
        /// Фильтрация списка товаров по текущему критерию
        /// </summary>
        /// <param name="productList">Список товаров</param>
        /// <param name="currentCriteria">Текущий критерий</param>
        /// <returns>Список товаров</returns>
        public static Product[] FindAllWithCriteria(this Product[] productList, Characteristic currentCriteria)
        {
            try
            {
                return Array.FindAll(productList, productX => 
                    productX.Characteristics.Contains(currentCriteria) &&
                    Array.Find(productX.Characteristics, criteriaX => criteriaX.Name == currentCriteria.Name).Value ==
                    productList.Max(productY =>
                        Array.Find(productY.Characteristics, criteriaY => criteriaY.Name == currentCriteria.Name).Value));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при фильтрации по критерию:\n{ex.Message}");
            }
        }
        /// <summary>
        /// Фильтрация списка товаров по текущему критерию и его границе
        /// </summary>
        /// <param name="productList">Список товаров</param>
        /// <param name="currentCriteria">Текущий критерий</param>
        /// <param name="border">Граница критерия</param>
        /// <returns>Список товаров</returns>
        public static Product[] FindAllWithBorder(this Product[] productList, Characteristic currentCriteria)
        {
            try
            {
                return Array.FindAll(productList, productX =>
                    productX.Characteristics.Contains(currentCriteria) &&
                    Array.Find(productX.Characteristics, criteriaX => criteriaX.Name == currentCriteria.Name).Value >= currentCriteria.Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка с фильтрацией по границе:\n{ex.Message}");
            }
        }
        /// <summary>
        /// Нахождение характеристики в текущем товаре
        /// </summary>
        /// <param name="characteristicList">Список характеристик товара</param>
        /// <param name="currentCharacteristic">Текущая характеристика</param>
        /// <returns></returns>
        public static bool Contains(this IEnumerable<Characteristic> characteristicList, Characteristic currentCharacteristic)
        {
            foreach (Characteristic characteristic in characteristicList)
            {
                if (characteristic.Name == currentCharacteristic.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}