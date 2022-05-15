using System;
namespace Multicriteria_model
{
    internal sealed class Videocard : Product, IMemory, IFrequency
    {
        private readonly string name;
        private readonly uint videoMemory;
        private readonly uint frequency;
        private readonly int price;
        public override string Name => name;
        public uint Memory => videoMemory;
        public double Frequency => frequency;
        public override int Price => price;
        /// <param name="name">Имя товара</param>
        /// <param name="videoMemory">Количество видеопамяти</param>
        /// <param name="frequency">Частота памяти</param>
        /// <param name="price">Цена</param>
        public Videocard(string name, uint videoMemory, uint frequency, int price)
        {
            this.name = name;
            this.videoMemory = videoMemory;
            this.frequency = frequency;
            this.price = price;
        }
        /// <summary>
        /// Вывод характеристик видеокарты
        /// </summary>
        public override string Print => $"Видеокарта {name} {videoMemory} ГБ {frequency} МГц {Math.Abs(price)} ₽";
    }
}