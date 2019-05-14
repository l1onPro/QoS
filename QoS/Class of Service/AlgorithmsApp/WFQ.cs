using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// Weighted Fair Queuing
    /// </summary>
    class WFQ : IAlgorithm
    {        
        private List<Queuering> listQueue;

        /// <summary>
        /// Weighted Fair Queuing
        /// </summary>
        public WFQ()
        {
            listQueue = new List<Queuering>(4);

            for (int i = 0; i < listQueue.Count; i++)
            {
                listQueue[i] = new Queuering();
            }
        }

        /// <summary>
        /// Добавить пакет в свою очередь. Всего 4 класса очереди
        /// </summary>
        /// <param name="newPackage"></param>
        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case PHB.DF:                   
                    listQueue[3].AddPackage(newPackage);
                    break;
                case PHB.AF1:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF2:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF3:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF4:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.EF:
                    listQueue[1].AddPackage(newPackage);
                    break;
                case PHB.CS6:
                    listQueue[0].AddPackage(newPackage);
                    break;
                case PHB.CS7:
                    listQueue[0].AddPackage(newPackage);
                    break;
                default:
                    throw new Exception();
            }
            
        }

        /// <summary>
        /// Вывод в файл
        /// </summary>
        private void PrintToFile()
        {
            String path = Setting.Path + "\\" + Setting.Directory + "\\" + Setting.FileNameQueuering;

            foreach (Queuering queue in listQueue)
            {
                File.AppendAllText(path, queue.ID + ":" + Environment.NewLine);
                if (queue.NOTNULL())
                {
                    File.AppendAllText(path, queue.PrintToFile() + Environment.NewLine);
                }
            }

            File.AppendAllText(path, "------------------------");
            File.AppendAllText(path, Environment.NewLine);
        }

        /// <summary>
        /// Рассчитывает вес пакета
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private double FindWeight(Package package)
        {
            return (package.Length / Setting.Speed) / (1.0 + package.IP_Precedence);
        }

        /// <summary>
        /// Вычисляет, пакет из какой очереди «быстрее»
        /// </summary>
        /// <returns></returns>
        private int FindNumMinWeight()
        {            
            //номер очереди
            int num = -1;
            //вес
            double weight = 100;

            for (int i = 0; i < listQueue.Count; i++)
            {
                if (listQueue[0].Count != 0)
                {
                    double newWeight = FindWeight(listQueue[i].FirstPackage());
                    if (newWeight < weight)
                    {
                        weight = newWeight;
                        num = i;
                    }
                }
            }
            return num;
        }

        /// <summary>
        /// Возвращает первый пакет выбранной очереди и удаляет из учереди
        /// </summary>
        /// <param name="num">Номер очереди</param>
        /// <returns></returns>
        private Package GetPackage(int num)
        {
            if (num != -1) return listQueue[num].GetPackage();
            else return null;
        }

        /// <summary>       
        /// Возвращает первый пакет выбранной очереди и не удаляет из очереди
        /// </summary>
        /// <param name="num">Номер очереди</param>
        /// <returns></returns>
        private Package FirstPackage(int num)
        {
            if (num != -1) return listQueue[num].FirstPackage();
            else return null;
        }

        public bool NotNULL()
        {
            foreach (Queuering queue in listQueue)
            {
                if (queue.Count != 0) return true;
            }
            return false;
        }

        public Queue<Package> GetPackages(int speed)
        {
            Queue<Package> packages = new Queue<Package>();

            int sum = 0;

            while (NotNULL())
            {
                int num = FindNumMinWeight();
                Package pack = FirstPackage(num);
                sum += pack.Length;
                if (sum <= speed) packages.Enqueue(GetPackage(num));
                else return packages;
            }

            return packages;
        }
    }
}
