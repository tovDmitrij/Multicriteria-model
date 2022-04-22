namespace Multicriteria_model
{
    internal sealed class Processor : Product, ICores, IFrequency
    {
        private readonly string name;
        private readonly uint cores;
        private readonly double frequency;
        private readonly uint price;
        public override string Name => name;
        public uint Cores => cores;
        public double Frequency => frequency;
        public override uint Price => price;
        /// <param name="name">Имя товара</param>
        /// <param name="cores">Количество ядер</param>
        /// <param name="frequency">Частота ядра</param>
        /// <param name="price">Цена</param>
        public Processor(string name, uint cores, double frequency, uint price)
        {
            this.name = name;
            this.cores = cores;
            this.frequency = frequency;
            this.price = price;
        }
    }
}