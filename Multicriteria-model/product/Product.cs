using System;
using System.Collections;
using System.Collections.Generic;
namespace Multicriteria_model
{
    /// <summary>
    /// Товар
    /// </summary>
    public struct Product
    {
        private readonly Characteristic[] _characteristics;
        /// <summary>
        /// Список характеристик товара
        /// </summary>
        public Characteristic[] Characteristics => _characteristics;
        /// <summary>
        /// Товар
        /// </summary>
        /// <param name="characteristics">Список характеристик товара</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Product(Characteristic[] characteristics)
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
                result += $"{currentChar.Name}: {(currentChar.Value is IFormattable ? Math.Abs(currentChar.Value) : currentChar.Value)};\n";
            }
            return result;
        }
    }
}