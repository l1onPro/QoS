using System;
using System.Collections.Generic;
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
        //Проверить правильность!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case DSCPName.CS0:                   
                    listQueue[3].AddPackege(newPackage);
                    break;
                case DSCPName.AF1:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF2:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF3:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF4:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.EF:
                    listQueue[1].AddPackege(newPackage);
                    break;
                case DSCPName.CS6:
                    listQueue[0].AddPackege(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackege(newPackage);
                    break;
                default:
                    throw new Exception();
            }
        }

        private double FindWeight(Package package)
        {
            return (package.Length / Setting.Speed) / (1.0 + package.IP_Precedence);
        }

        /// <summary>
        /// Рассчитывает веса пакетов и возвращает тот, у кого вес меньше
        /// </summary>
        /// <returns></returns>
        public Package GetPackage()
        {
            //вычисляет, пакет из какой очереди «быстрее»
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

            if (num != -1) return listQueue[num].GetPackege();
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
    }
}
