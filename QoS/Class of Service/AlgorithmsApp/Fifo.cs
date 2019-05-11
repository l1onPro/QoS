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
        /// First In, First Out.
        /// Пакеты уходят из очереди ровно в том порядке, в котором они туда попали
        /// </summary>
        public FIFO()
        {            
            queue = new Queuering(); 
        }  
      
        /// <summary>
        /// Проверяет, есть ли пакеты в очереди
        /// </summary>
        /// <returns></returns>
        public bool NotNULL()
        {            
            return queue.Count != 0;
        }

        /// <summary>
        /// Добавляет новый пакет в конеч очереди
        /// </summary>
        /// <param name="newPackage"></param>
        public void Add(Package newPackage)
        {
            queue.AddPackege(newPackage);
        }

        /// <summary>
        /// Удаляет пакет из начала очереди и возращает его
        /// </summary>
        /// <returns></returns>
        public Package GetPackage()
        {
            return queue.GetPackege();
        }
    }
}
