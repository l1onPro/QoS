using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service.AlgorithmsApp
{
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

            this.expansion = expansion;
        }

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case DSCPName.CS0:
                    if (expansion) listQueue[7].AddPackege(newPackage);
                    else listQueue[3].AddPackege(newPackage);
                    break;
                case DSCPName.AF1:
                    if (expansion) listQueue[6].AddPackege(newPackage);
                    else listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF2:
                    if (expansion) listQueue[5].AddPackege(newPackage);
                    else listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF3:
                    if (expansion) listQueue[4].AddPackege(newPackage);
                    else listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF4:
                    if (expansion) listQueue[3].AddPackege(newPackage);
                    else listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.EF:
                    if (expansion) listQueue[2].AddPackege(newPackage);
                    else listQueue[1].AddPackege(newPackage);
                    break;
                case DSCPName.CS6:
                    if (expansion) listQueue[1].AddPackege(newPackage);
                    else listQueue[0].AddPackege(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackege(newPackage);
                    break;
                default:
                    break;
            }
        }    

        public Package GetPackage()
        {
            foreach (Queuering queue in listQueue)
            {
                if (queue.Count != 0) return queue.GetPackege();
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
