using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicriteria_model
{
    class HDD: Product
    {
        private readonly string name;
        private readonly uint memory;
        private readonly uint speed;
        private readonly uint price;
        public override string Name => name;
        public uint Memory => memory;
        public uint Speed => speed;
        public override uint Price => price;
        public HDD(string name, uint memory, uint speed, uint price)
        {
            this.name = name;
            this.memory = memory;
            this.speed = speed;
            this.price = price;
        }
    }
}
