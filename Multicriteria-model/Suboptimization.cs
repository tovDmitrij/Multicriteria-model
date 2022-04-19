using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Multicriteria_model
{
    class Suboptimization<T> where T : Product
    {
        private readonly List<T> products;
        private readonly Dictionary<KeyValuePair<double, double>, Characteristics> criteria;
        private readonly Characteristics mainCriteria;
        public Suboptimization(List<T> products, Characteristics mainCriteria, Dictionary<KeyValuePair<double, double>, Characteristics> criteria)
        {
            this.products = products;
            this.mainCriteria = mainCriteria;
            this.criteria = criteria;
        }
        public List<T>? Run()
        {
            switch (products)
            {
                case List<HDD>:
                    try
                    {
                        List<HDD> HDDList = products as List<HDD>;
                        for(int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            KeyValuePair<double,double> range = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    HDDList = HDDList.FindAll(productX => productX.Price >= range.Key && productX.Price <= range.Value);
                                    break;
                                case Characteristics.Speed:
                                    HDDList = HDDList.FindAll(productX => productX.Speed >= range.Key && productX.Speed <= range.Value);
                                    break;
                                case Characteristics.Memory:
                                    HDDList = HDDList.FindAll(productX => productX.Memory >= range.Key && productX.Memory <= range.Value);
                                    break;
                            }
                        }
                        switch (mainCriteria)
                        {
                            case Characteristics.Price:
                                HDDList = HDDList.FindAll(productX => productX.Price == HDDList.Min(productY => productY.Price));
                                break;
                            case Characteristics.Speed:
                                HDDList = HDDList.FindAll(productX => productX.Speed == HDDList.Max(productY => productY.Speed));
                                break;
                            case Characteristics.Memory:
                                HDDList = HDDList.FindAll(productX => productX.Memory == HDDList.Max(productY => productY.Memory));
                                break;
                        }
                        return HDDList as List<T>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        return null;
                    }
                case List<Videocard>:
                    try
                    {
                        List<Videocard> VideocardList = products as List<Videocard>;

                        return VideocardList as List<T>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        return null;
                    }
                case List<RAM>:
                    try
                    {
                        List<RAM> RAMList = products as List<RAM>;

                        return RAMList as List<T>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        return null;
                    }
                case List<Processor>:
                    try
                    {
                        List<Processor> ProcessorList = products as List<Processor>;

                        return ProcessorList as List<T>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        return null;
                    }
                case List<Monitor>:
                    try
                    {
                        List<Monitor> MonitorList = products as List<Monitor>;

                        return MonitorList as List<T>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        return null;
                    }
                default:
                    return null;
            }
        }
    }
}
