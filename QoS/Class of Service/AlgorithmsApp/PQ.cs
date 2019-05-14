using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    // <summary>
    /// Priority Queuing (приоритетная очередь).
    /// </summary>
    class PQ : IAlgorithm
    {
        private List<Queuering> listQueue;
        private bool expansion;
        /// <summary>
        /// Priority Queuing (приоритетная очередь).
        /// 8 очередей
        /// </summary>
        /// <param name="expansion">1 (true) - Улучшенная очередь</param>
        public PQ(bool expansion)
        {
            if (expansion) 
                listQueue = new List<Queuering>(8);
            else
                listQueue = new List<Queuering>(4);

            for (int i = 0; i < listQueue.Count; i++)
            {
                listQueue[i] = new Queuering();
            }

            this.expansion = expansion;
        }

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case PHB.DF:
                    if (expansion) listQueue[7].AddPackage(newPackage);
                    else listQueue[3].AddPackage(newPackage);
                    break;
                case PHB.AF1:
                    if (expansion) listQueue[6].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF2:
                    if (expansion) listQueue[5].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF3:
                    if (expansion) listQueue[4].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF4:
                    if (expansion) listQueue[3].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.EF:
                    if (expansion) listQueue[2].AddPackage(newPackage);
                    else listQueue[1].AddPackage(newPackage);
                    break;
                case PHB.CS6:
                    if (expansion) listQueue[1].AddPackage(newPackage);
                    else listQueue[0].AddPackage(newPackage);
                    break;
                case PHB.CS7:
                    listQueue[0].AddPackage(newPackage);
                    break;
                default:
                    throw new Exception();
            }
            PrintToFile();
        }

        /// <summary>
        /// Вывод в файл
        /// </summary>
        private void PrintToFile()
        {
            String path = Setting.Path + "\\" + Setting.Directory + "\\" + Setting.FileNameQueuering;

            foreach (Queuering queue in listQueue)
            {
                File.AppendAllText(path, queue.PrintToFile() + Environment.NewLine);
            }            
        }

        public bool NotNULL()
        {
            foreach (Queuering queue in listQueue)
            {
                if (queue.Count != 0) return true;
            }
            return false;
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

        private int FindNumQueue()
        {
            for (int i = 0; i < listQueue.Count; i++)
            {
                if (listQueue[i].Count != 0) return i;
            }
            return -1;
        }

        public Queue<Package> GetPackages(int speed)
        {
            Queue<Package> packages = new Queue<Package>();

            int sum = 0;           
            
            while (NotNULL())
            {
                int num = FindNumQueue();
                Package pack = FirstPackage(num);
                sum += pack.Length;
                if (sum <= speed) packages.Enqueue(GetPackage(num));
                else return packages;                
            }

            return packages;
        }
    }
}
