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

        /// <summary>
        /// Priority Queuing (приоритетная очередь)
        /// </summary>
        /// <param name="n">для 4 очередей</param>
        public PQ()
        {
            listQueue = new List<Queuering>(4);
        }

        public PQ(bool expansion)
        {
            listQueue = new List<Queuering>(8);
        }

        public void Add(Package newPackage)
        {
            throw new NotImplementedException();
        }    

        public Package GetPackage()
        {
            throw new NotImplementedException();
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
