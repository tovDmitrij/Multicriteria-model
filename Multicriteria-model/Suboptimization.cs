using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model
{
/* Субоптимизация

Выделяется один из критериев, а по всем остальным критериям назначаются нижние границы.
Оптимальным при этом считается исход, максимизирующий выделенный критерий на множестве исходов,
оценки которых по остальным притерием не ниже назначенных.

*/
    class Suboptimization<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<double, Characteristics> criteria;
        private readonly Characteristics mainCriterion;
        public Suboptimization(List<T> products, Characteristics mainCriterion, SortedDictionary<double, Characteristics> criteria)
        {
            this.products = products;
            this.mainCriterion = mainCriterion;
            this.criteria = criteria;
        }
        public List<T>? Run()
        {
            try
            {
                switch (products)
                {
                    #region Жесткие диски
                    case List<HDD>:
                        List<HDD> HDDList = products as List<HDD>;
                        for (int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            double border = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    HDDList = HDDList.FindAll(productX => productX.Price <= border);
                                    break;
                                case Characteristics.Speed:
                                    HDDList = HDDList.FindAll(productX => productX.Speed >= border);
                                    break;
                                case Characteristics.Memory:
                                    HDDList = HDDList.FindAll(productX => productX.Memory >= border);
                                    break;
                            }
                        }
                        switch (mainCriterion)
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
                    #endregion

                    #region Оперативная память
                    case List<RAM>:
                        List<RAM> RAMList = products as List<RAM>;
                        for (int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            double border = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    RAMList = RAMList.FindAll(productX => productX.Price <= border);
                                    break;
                                case Characteristics.Memory:
                                    RAMList = RAMList.FindAll(productX => productX.Memory >= border);
                                    break;
                                case Characteristics.Frequency:
                                    RAMList = RAMList.FindAll(productX => productX.Frequency >= border);
                                    break;
                            }
                        }
                        switch (mainCriterion)
                        {
                            case Characteristics.Price:
                                RAMList = RAMList.FindAll(productX => productX.Price == RAMList.Min(productY => productY.Price));
                                break;
                            case Characteristics.Memory:
                                RAMList = RAMList.FindAll(productX => productX.Memory == RAMList.Max(productY => productY.Memory));
                                break;
                            case Characteristics.Frequency:
                                RAMList = RAMList.FindAll(productX => productX.Frequency == RAMList.Max(productY => productY.Frequency));
                                break;
                        }
                        return RAMList as List<T>;
                    #endregion

                    #region Видеокарты
                    case List<Videocard>:
                        List<Videocard> VCList = products as List<Videocard>;
                        for (int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            KeyValuePair<double, double> range = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    VCList = VCList.FindAll(productX => productX.Price >= range.Key && productX.Price <= range.Value);
                                    break;
                                case Characteristics.Memory:
                                    VCList = VCList.FindAll(productX => productX.Memory >= range.Key && productX.Memory <= range.Value);
                                    break;
                                case Characteristics.Frequency:
                                    VCList = VCList.FindAll(productX => productX.Frequency >= range.Key && productX.Frequency <= range.Value);
                                    break;
                            }
                        }
                        switch (mainCriterion)
                        {
                            case Characteristics.Price:
                                VCList = VCList.FindAll(productX => productX.Price == VCList.Min(productY => productY.Price));
                                break;
                            case Characteristics.Memory:
                                VCList = VCList.FindAll(productX => productX.Memory == VCList.Max(productY => productY.Memory));
                                break;
                            case Characteristics.Frequency:
                                VCList = VCList.FindAll(productX => productX.Frequency == VCList.Max(productY => productY.Frequency));
                                break;
                        }
                        return VCList as List<T>;
                    #endregion

                    #region Процессоры
                    case List<Processor>:
                        List<Processor> ProcessorList = products as List<Processor>;
                        for (int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            double border = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    ProcessorList = ProcessorList.FindAll(productX => productX.Price <= border);
                                    break;
                                case Characteristics.Cores:
                                    ProcessorList = ProcessorList.FindAll(productX => productX.Cores >= border);
                                    break;
                                case Characteristics.Frequency:
                                    ProcessorList = ProcessorList.FindAll(productX => productX.Frequency >= border);
                                    break;
                            }
                        }
                        switch (mainCriterion)
                        {
                            case Characteristics.Price:
                                ProcessorList = ProcessorList.FindAll(productX => productX.Price == ProcessorList.Min(productY => productY.Price));
                                break;
                            case Characteristics.Cores:
                                ProcessorList = ProcessorList.FindAll(productX => productX.Cores == ProcessorList.Max(productY => productY.Cores));
                                break;
                            case Characteristics.Frequency:
                                ProcessorList = ProcessorList.FindAll(productX => productX.Frequency == ProcessorList.Max(productY => productY.Frequency));
                                break;
                        }
                        return ProcessorList as List<T>;
                    #endregion

                    #region Мониторы
                    case List<Monitor>:
                        List<Monitor> MonitorList = products as List<Monitor>;
                        for (int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            double border = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    MonitorList = MonitorList.FindAll(productX => productX.Price <= border);
                                    break;
                                case Characteristics.ScreenSize:
                                    MonitorList = MonitorList.FindAll(productX => productX.ScreenSize >= border);
                                    break;
                                case Characteristics.Frequency:
                                    MonitorList = MonitorList.FindAll(productX => productX.Frequency >= border);
                                    break;
                            }
                        }
                        switch (mainCriterion)
                        {
                            case Characteristics.Price:
                                MonitorList = MonitorList.FindAll(productX => productX.Price == MonitorList.Min(productY => productY.Price));
                                break;
                            case Characteristics.ScreenSize:
                                MonitorList = MonitorList.FindAll(productX => productX.ScreenSize == MonitorList.Max(productY => productY.ScreenSize));
                                break;
                            case Characteristics.Frequency:
                                MonitorList = MonitorList.FindAll(productX => productX.Frequency == MonitorList.Max(productY => productY.Frequency));
                                break;
                        }
                        return MonitorList as List<T>;
                    #endregion

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
                return null;
            }
        }
    }
}