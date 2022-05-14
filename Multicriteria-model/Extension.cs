using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model
{
    public static class Extensions
    {
        /// <summary>
        /// Возвращает список <see cref="T"/> товаров
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="productList">Список товаров</param>
        /// <param name="currentCriteria">Текущий критерий</param>
        /// <returns></returns>
        public static List<T> FindAll<T>(this List<T> productList, Characteristics currentCriteria)
        {
            try
            {
                return currentCriteria switch
                {
                    Characteristics.Price => productList.FindAll(productX => (productX as Product).Price == productList.Max(productY => (productY as Product).Price)),
                    Characteristics.Speed => productList.FindAll(productX => ((ISpeed)productX).Speed == productList.Max(productY => ((ISpeed)productY).Speed)),
                    Characteristics.Memory => productList.FindAll(productX => ((IMemory)productX).Memory == productList.Max(productY => ((IMemory)productY).Memory)),
                    Characteristics.Frequency => productList.FindAll(productX => ((IFrequency)productX).Frequency == productList.Max(productY => ((IFrequency)productY).Frequency)),
                    Characteristics.Cores => productList.FindAll(productX => ((ICores)productX).Cores == productList.Max(productY => ((ICores)productY).Cores)),
                    Characteristics.ScreenSize => productList.FindAll(productX => ((IScreenSize)productX).ScreenSize == productList.Max(productY => ((IScreenSize)productY).ScreenSize)),
                    _ => productList,
                };
            }
            catch (Exception ex)
            {
                string exception = "Ошибка при фильтрации:\n";
                exception += productList is null ? "Список товаров пустой!" : $"{ex}";
                MessageBox.Show($"{exception}");
                return productList;
            }
        }
        /// <summary>
        /// Возвращает список <see cref="T"/> товаров
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="productList">Список товаров</param>
        /// <param name="currentCriteria">Текущий критерий</param>
        /// <param name="border">Нижняя граница текущего критерия</param>
        /// <returns></returns>
        public static List<T> FindAll<T>(this List<T> productList, Characteristics currentCriteria, double border)
        {
            try
            {
                return currentCriteria switch
                {
                    Characteristics.Price => productList.FindAll(productX => (productX as Product).Price >= border),
                    Characteristics.Speed => productList.FindAll(productX => ((ISpeed)productX).Speed >= border),
                    Characteristics.Memory => productList.FindAll(productX => ((IMemory)productX).Memory >= border),
                    Characteristics.Frequency => productList.FindAll(productX => ((IFrequency)productX).Frequency >= border),
                    Characteristics.Cores => productList.FindAll(productX => ((ICores)productX).Cores >= border),
                    Characteristics.ScreenSize => productList.FindAll(productX => ((IScreenSize)productX).ScreenSize >= border),
                    _ => productList,
                };
            }
            catch (Exception ex)
            {
                string exception = "Ошибка при фильтрации:\n";
                exception += productList is null ? "Список товаров пустой!" : border == null ? $"Отсуствует нижняя граница {currentCriteria} критерия!" : $"{ex}";
                MessageBox.Show($"{exception}");
                return productList;
            }
        }
    }
}