using System.Collections.Generic;

namespace Multicriteria_model
{
    abstract class Product
    {
        public abstract string Name { get; }
        public abstract int Price { get; }
        public abstract string Print { get; }
    }
}