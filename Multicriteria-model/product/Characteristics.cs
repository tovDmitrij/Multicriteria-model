using System;
namespace Multicriteria_model
{
    /// <summary>
    /// Характеристика товара
    /// </summary>
    public struct Characteristic
    {
        private readonly string _name;
        private readonly dynamic _value;
        /// <summary>
        /// Наименование характеристики
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Значение характеристики
        /// </summary>
        public dynamic Value => _value;
        /// <summary>
        /// Характеристика товара
        /// </summary>
        /// <param name="name">Наименование характеристики</param>
        /// <param name="value">Значение характеристики</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Characteristic(string name, dynamic value)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name),
                "Ошибка при создании характеристики:\nОтсутствует наименование хар-ки!");
            _value = value ?? throw new ArgumentNullException(nameof(value),
                "Ошибка при создании характеристики:\nОтсутствует значение хар-ки");
        }
    }
}