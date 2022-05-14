using System;

namespace Multicriteria_model
{
    internal sealed class RAM : Product, IMemory, IFrequency
    {
        private readonly string name;
        private readonly uint memory;
        private readonly uint frequency;
        private readonly int price;
        public override string Name => name;
        public uint Memory => memory;
        public double Frequency => frequency;
        public override int Price => price;
        /// <param name="name">Имя товара</param>
        /// <param name="memory">Количество памяти</param>
        /// <param name="frequency">Частота памяти</param>
        /// <param name="price">Цена</param>
        public RAM(string name, uint memory, uint frequency, int price)
        {
            this.name = name;
            this.memory = memory;
            this.frequency = frequency;
            this.price = price;
        }
        /// <summary>
        /// Вывод характеристик оперативной памяти
        /// </summary>
        public override string Print => $"Оперативная память {name} {memory} ГБ {frequency} МГц {Math.Abs(price)} ₽";

    }
}