using System;
using System.Collections.Generic;
namespace Multicriteria_model
{
    /// <summary>
    /// Товар
    /// </summary>
    public sealed class Product
    {
        private readonly List<Characteristic> _characteristics;
        /// <summary>
        /// Список характеристик товара
        /// </summary>
        public List<Characteristic> Characteristics => _characteristics;
        /// <summary>
        /// Товар
        /// </summary>
        /// <param name="characteristics">Список характеристик товара</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Product(List<Characteristic> characteristics)
        {
            _characteristics = characteristics ?? 
                throw new ArgumentNullException(nameof(characteristics),
                "Ошибка при создании товара:\nОтсутствует список характеристик!");
        }
        public override string ToString()
        {
            string result = "";
            foreach (var currentChar in _characteristics)
            {
                result += $"{currentChar.Name}: {currentChar.Value}; ";
            }
            return result;
        }
    }
}