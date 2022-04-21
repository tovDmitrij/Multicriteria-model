using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Multicriteria_model
{
    class ParetoOptimum<T> where T : Product
    {
        private readonly List<T> products;
        public ParetoOptimum(List<T> products)
        {
            this.products = products;
        }
        public List<T>? Run()
        {
            int[] summ = ParetoArray();
            List<T> newList = products;
            for(int i = 0; i < summ.Length; i++)
                if (summ[i] == summ.Max())
                    newList.Add(products[i]);
            return newList;
        }
        private int[]? ParetoArray()
        {
            int[,] paretoArray = new int[products.Count, products.Count];
            int[] summ = new int[products.Count];
            switch (products)
            {
                #region Жесткие диски
                case List<HDD>:
                    List<HDD> HDDList = products as List<HDD>();
                    for (int i = 0; i < products.Count; i++)
                        for (int j = 0; j < products.Count; j++)
                            paretoArray[i, j] = HDDList[j].Price < HDDList[i].Price || 
                                HDDList[j].Speed > HDDList[i].Speed || 
                                HDDList[j].Memory > HDDList[i].Memory ? 1 : 0;
                    break;
                #endregion

                #region Оперативная память
                case List<RAM>:
                    List<RAM> RAMList = products as List<RAM>();
                    for (int i = 0; i < products.Count; i++)
                        for (int j = 0; j < products.Count; j++)
                            paretoArray[i, j] = RAMList[j].Price < RAMList[i].Price || 
                                RAMList[j].Frequency > RAMList[i].Frequency || 
                                RAMList[j].Memory > RAMList[i].Memory ? 1 : 0;
                    break;
                #endregion

                #region Видеокарты
                case List<Videocard>:
                    List<Videocard> VideocardList = products as List<Videocard>();
                    for (int i = 0; i < products.Count; i++)
                        for (int j = 0; j < products.Count; j++)
                            paretoArray[i, j] = VideocardList[j].Price < VideocardList[i].Price || 
                                VideocardList[j].Frequency > VideocardList[i].Frequency || 
                                VideocardList[j].Memory > VideocardList[i].Memory ? 1 : 0;
                    break;
                #endregion

                #region Процессоры
                case List<Processor>:
                    List<Processor> ProcessorList = products as List<Processor>();
                    for (int i = 0; i < products.Count; i++)
                        for (int j = 0; j < products.Count; j++)
                            paretoArray[i, j] = ProcessorList[j].Price < ProcessorList[i].Price ||
                                ProcessorList[j].Frequency > ProcessorList[i].Frequency ||
                                ProcessorList[j].Cores > ProcessorList[i].Cores ? 1 : 0;
                    break;
                #endregion

                #region Мониторы
                case List<Monitor>:
                    List<Monitor> MonitorList = products as List<Monitor>();
                    for (int i = 0; i < products.Count; i++)
                        for (int j = 0; j < products.Count; j++)
                            paretoArray[i, j] = MonitorList[j].Price < MonitorList[i].Price ||
                                MonitorList[j].Frequency > MonitorList[i].Frequency ||
                                MonitorList[j].ScreenSize > MonitorList[i].ScreenSize ? 1 : 0;
                    break;
                #endregion

                default:
                    return null;
            }
            for (int i = 0; i < products.Count; i++)
            {
                for(int j = 0; j <= products.Count; j++)
                {
                    summ[i] += paretoArray[i, j];
                }
            }
            return summ;
        }
    }
}