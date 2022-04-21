using System.Collections.Generic;
using System.Linq;
namespace Multicriteria_model
{
    /* Указание нижних границ критериев

    По всем критериям назначаются нижние границы.
    Оптимальным при этом считается исход, удовлетворяющий всем нижним границам критериев.

    */
    class LowerCriteriaBorders<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<double, Characteristics> criteria;
        public LowerCriteriaBorders(List<T> products, SortedDictionary<double, Characteristics> criteria)
        {
            this.products = products;
            this.criteria = criteria;
        }
        public List<T> Run()
        {
            List<T> productList = products;
            for (byte i = 0; i < criteria.Count; i++)
            {
                Characteristics currentCriteria = criteria.ElementAt(i).Value;
                double border = criteria.ElementAt(i).Key;
                switch (currentCriteria)
                {
                    case Characteristics.Price:
                        productList = productList.FindAll(productX => productX.Price <= border);
                        break;
                    case Characteristics.Speed:
                        if (productList is ISpeed productSpeed)
                            productList = productList.FindAll(productX => productSpeed.Speed >= border);
                        break;
                    case Characteristics.Memory:
                        if (productList is IMemory productMemory)
                            productList = productList.FindAll(productX => productMemory.Memory >= border);
                        break;
                    case Characteristics.Frequency:
                        if (productList is IFrequency productFrequency)
                            productList = productList.FindAll(productX => productFrequency.Frequency >= border);
                        break;
                    case Characteristics.Cores:
                        if (productList is ICores productCores)
                            productList = productList.FindAll(productX => productCores.Cores >= border);
                        break;
                    case Characteristics.ScreenSize:
                        if (productList is IScreenSize productIScreenSize)
                            productList = productList.FindAll(productX => productIScreenSize.ScreenSize >= border);
                        break;
                    default:
                        break;
                }
            }
            return productList;
        }
    }
}