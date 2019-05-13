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
            queue.AddPackage(newPackage);
        }    

        public Queue<Package> GetPackages(int speed)
        {
            Queue<Package> packages = new Queue<Package>();

            int sum = 0;            
           
            while (NotNULL())
            {
                Package pack = queue.FirstPackage();
                sum += pack.Length;
                if (sum <= speed) packages.Enqueue(queue.GetPackage());
                else return packages;                
            }

            return packages;
        }
    }
}
