using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Multicriteria_model
{
    class LowerCriteriaBorders<T> where T : Product
    {
        private readonly List<T> products;
        private readonly SortedDictionary<double, Characteristics> criteria;
        public LowerCriteriaBorders(List<T> products, SortedDictionary<double, Characteristics> criteria)
        {
            this.products = products;
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
                        return RAMList as List<T>;
                    #endregion

                    #region Видеокарты
                    case List<Videocard>:
                        List<Videocard> VCList = products as List<Videocard>;
                        for (int i = 0; i < criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria.ElementAt(i).Value;
                            double border = criteria.ElementAt(i).Key;
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    VCList = VCList.FindAll(productX => productX.Price  <= border);
                                    break;
                                case Characteristics.Memory:
                                    VCList = VCList.FindAll(productX => productX.Memory >= border);
                                    break;
                                case Characteristics.Frequency:
                                    VCList = VCList.FindAll(productX => productX.Frequency >= border);
                                    break;
                            }
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
