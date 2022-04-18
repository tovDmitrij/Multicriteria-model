using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicriteria_model
{
    class RAM: Product
    {
        private readonly string name;
        private readonly uint memory;
        private readonly uint frequency;
        private readonly uint price;
        public override string Name => name;
        public uint Memory => memory;
        public uint Frequency => frequency;
        public override uint Price => price;
        public RAM(string name, uint memory, uint frequency, uint price)
        {
            this.name = name;
            this.memory = memory;
            this.frequency = frequency;
            this.price = price;
        }
    }
}
