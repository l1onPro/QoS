using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    class RR : IAlgorithm
    {
        private List<Queuering> listQueue;
        private int num;
        /// <summary>
        /// Round-Robin
        /// </summary>
        public RR()
        {
            listQueue = new List<Queuering>(4);
            num = 0;
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

        /// <summary>
        /// возвращает одинаковое кол-во пакетов каждой очереди
        /// </summary>
        /// <returns></returns>
        public Package GetPackage()
        {
            if (num == listQueue.Count) num = 0;
            for (int i = num; i < listQueue.Count; i++)
            {
                num++;
                if (listQueue[i].Count != 0) return listQueue[i].GetPackege();
            }
            return null;
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
