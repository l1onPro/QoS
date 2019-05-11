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
        public PQ(List<Package> allPackage)
        {

        }
        public List<Queuering> getAllQueues()
        {
            return listQueue;
        }
    }
}
