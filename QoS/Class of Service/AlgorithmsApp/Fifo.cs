using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// First In, First Out.
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
      
        public bool NotNULL()
        {            
            return queue.Count != 0;
        }
        
        public void Add(Package newPackage)
        {
            queue.AddPackege(newPackage);
        }    

        public Queue<Package> GetPackage(int speed)
        {
            Queue<Package> packages = new Queue<Package>();

            int sum = 0;
            Package pack;

            pack = queue.FirstPackage();
            while (pack != null)
            {
                if (sum + pack.Length <= speed) packages.Enqueue(pack);
                else return packages;
                pack = queue.FirstPackage();
            }

            return packages;
        }
    }
}
