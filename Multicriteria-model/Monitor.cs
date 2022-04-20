using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicriteria_model
{
    class Monitor: Product, IComparable<Monitor>, IScreenSize, IFrequency
    {
        private readonly string name;
        private readonly uint screenSize_X;
        private readonly uint screenSize_Y;
        private readonly uint frequency;
        private readonly uint price;
        public override string Name => name;
        public uint ScreenSize => screenSize_X * screenSize_Y;
        public double Frequency => frequency;
        public override uint Price => price;
        public Monitor(string name, string screenSize, uint frequency, uint price)
        {
            this.name = name;
            string[] str = screenSize.Split('x');
            screenSize_X = Convert.ToUInt32(str[0]);
            screenSize_Y = Convert.ToUInt32(str[1]);
            this.frequency = frequency;
            this.price = price;
        }
        public int CompareTo(Monitor other)
        {
            return screenSize_X * screenSize_Y < other.screenSize_X * other.screenSize_Y ? -1
                :
                screenSize_X * screenSize_Y > other.screenSize_X * other.screenSize_Y ? 1
                :
                0;
        }
    }
}
