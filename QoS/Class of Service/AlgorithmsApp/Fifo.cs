using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// Трафик обрабатывается одинаково — в одной очереди
    /// </summary>
    class FIFO : IAlgorithm
    {
        private Queuering queue;       

        /// <summary>
        /// Пакеты уходят из очереди ровно в том порядке, в котором они туда попали
        /// </summary>
        /// <param name="allPackage">Приходящие пакеты</param>
        public FIFO(List<Package> allPackage)
        {
            //работа алгоритма
            queue = new Queuering();
            foreach (Package package in allPackage)
            {
                queue.AddPackege(package);
            }
        }  
      
        public List<Queuering> getAllQueues()
        {
            List<Queuering> newList = new List<Queuering>();
            newList.Add(queue);
            return newList;            
        }
    }
}
