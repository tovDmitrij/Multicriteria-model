using System;
namespace Multicriteria_model
{
    internal sealed class Monitor: Product, IScreenSize, IFrequency
    {
        private readonly string name;
        private readonly uint screenSize_X;
        private readonly uint screenSize_Y;
        private readonly uint frequency;
        private readonly int price;
        public override string Name => name;
        public uint ScreenSize => screenSize_X * screenSize_Y;
        public double Frequency => frequency;
        public override int Price => price;
        /// <param name="name">Имя товара</param>
        /// <param name="screenSize">Размер экрана</param>
        /// <param name="frequency">Частота обновления экрана</param>
        /// <param name="price">Цена</param>
        public Monitor(string name, string screenSize, uint frequency, int price)
        {
            this.name = name;
            string[] str = screenSize.Split('x');
            screenSize_X = Convert.ToUInt32(str[0]);
            screenSize_Y = Convert.ToUInt32(str[1]);
            this.frequency = frequency;
            this.price = price;
        }
        /// <summary>
        /// Вывод характеристик монитора
        /// </summary>
        public override string Print => $"Монитор {name} {screenSize_X}x{screenSize_Y}@{frequency} Гц {Math.Abs(price)} ₽";

    }
}