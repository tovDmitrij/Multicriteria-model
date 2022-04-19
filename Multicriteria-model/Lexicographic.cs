using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Multicriteria_model
{
    class Lexicographic<T> where T : Product
    {
        private readonly List<T> products;
        private readonly Dictionary<byte, Characteristics> criteria;
        public Lexicographic(List<T> products, Dictionary<byte, Characteristics> criteria)
        {
            this.products = products;
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
                        for (byte i = 1; i <= criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria[i];
                            switch (currentCriteria)
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
                        }
                        return HDDList as List<T>;
                    } 
                    catch(Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        return null;
                    }
                case List<Videocard>:
                    try
                    {
                        List <Videocard> VideocardList = products as List<Videocard>;
                        for (byte i = 1; i <= criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria[i];
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    VideocardList = VideocardList.FindAll(productX => productX.Price == VideocardList.Min(productY => productY.Price));
                                    break;
                                case Characteristics.Frequency:
                                    VideocardList = VideocardList.FindAll(productX => productX.Frequency == VideocardList.Max(productY => productY.Frequency));
                                    break;
                                case Characteristics.Memory:
                                    VideocardList = VideocardList.FindAll(productX => productX.Memory == VideocardList.Max(productY => productY.Memory));
                                    break;
                            }
                        }
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
                        for (byte i = 1; i <= criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria[i];
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    RAMList = RAMList.FindAll(productX => productX.Price == RAMList.Min(productY => productY.Price));
                                    break;
                                case Characteristics.Frequency:
                                    RAMList = RAMList.FindAll(productX => productX.Frequency == RAMList.Max(productY => productY.Frequency));
                                    break;
                                case Characteristics.Memory:
                                    RAMList = RAMList.FindAll(productX => productX.Memory == RAMList.Max(productY => productY.Memory));
                                    break;
                            }
                        }
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
                        for (byte i = 1; i <= criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria[i];
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    ProcessorList = ProcessorList.FindAll(productX => productX.Price == ProcessorList.Min(productY => productY.Price));
                                    break;
                                case Characteristics.Frequency:
                                    ProcessorList = ProcessorList.FindAll(productX => productX.Frequency == ProcessorList.Max(productY => productY.Frequency));
                                    break;
                                case Characteristics.Cores:
                                    ProcessorList = ProcessorList.FindAll(productX => productX.Cores == ProcessorList.Max(productY => productY.Cores));
                                    break;
                            }
                        }
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
                        for (byte i = 1; i <= criteria.Count; i++)
                        {
                            Characteristics currentCriteria = criteria[i];
                            switch (currentCriteria)
                            {
                                case Characteristics.Price:
                                    MonitorList = MonitorList.FindAll(productX => productX.Price == MonitorList.Min(productY => productY.Price));
                                    break;
                                case Characteristics.Frequency:
                                    MonitorList = MonitorList.FindAll(productX => productX.Frequency == MonitorList.Max(productY => productY.Frequency));
                                    break;
                                case Characteristics.ScreenSize:
                                    MonitorList = MonitorList.FindAll(productX => productX.ScreenSize == MonitorList.Max(productY => productY.ScreenSize));
                                    break;
                            }
                        }
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