using QoS.AppPackage;
using System;
using System.Collections.Generic;
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
                case DSCPName.CS0:
                    if (expansion) listQueue[7].AddPackage(newPackage);
                    else listQueue[3].AddPackage(newPackage);
                    break;
                case DSCPName.AF1:
                    if (expansion) listQueue[6].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.AF2:
                    if (expansion) listQueue[5].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.AF3:
                    if (expansion) listQueue[4].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.AF4:
                    if (expansion) listQueue[3].AddPackage(newPackage);
                    else listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.EF:
                    if (expansion) listQueue[2].AddPackage(newPackage);
                    else listQueue[1].AddPackage(newPackage);
                    break;
                case DSCPName.CS6:
                    if (expansion) listQueue[1].AddPackage(newPackage);
                    else listQueue[0].AddPackage(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackage(newPackage);
                    break;
                default:
                    throw new Exception();
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
        /// Возращает пакет приоритетной очереди, удаляет его
        /// </summary>
        /// <returns></returns>
        private Package GetPackage(int num)
        {
            if (num != -1) return listQueue[num].GetPackage();
            else return null;
        }

        /// <summary>
        /// Возращает пакет приоритетной очереди, не удаляет его
        /// </summary>
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
