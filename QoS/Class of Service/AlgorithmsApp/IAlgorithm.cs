using QoS.Class_of_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    interface IAlgorithm
    {
        /// <summary>
        /// Получившиеся очереди
        /// </summary>
        /// <returns></returns>
        List<Queuering> getAllQueues();
    }
}
