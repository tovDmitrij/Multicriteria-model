using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicriteria_model
{
    interface IMemory { uint Memory { get; } }
    interface IFrequency { double Frequency { get; } }
    interface ISpeed { uint Speed { get; } }
    interface ICores { uint Cores { get; } }
    interface IScreenSize { uint ScreenSize { get; } }
}
