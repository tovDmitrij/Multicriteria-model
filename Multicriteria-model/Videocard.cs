using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicriteria_model
{
    class Videocard: Product, IMemory, IFrequency
    {
        private readonly string name;
        private readonly uint videoMemory;
        private readonly uint frequency;
        private readonly uint price;
        public override string Name => name;
        public uint Memory => videoMemory;
        public double Frequency => frequency;
        public override uint Price => price;
        public Videocard(string name, uint videoMemory, uint frequency, uint price)
        {
            this.name = name;
            this.videoMemory = videoMemory;
            this.frequency = frequency;
            this.price = price;
        }
    }
}
