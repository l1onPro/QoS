using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class FIFO : IAlgorithm
    {
        private Queues.Queuering queue;
        const int max = 40;
        
        public FIFO()
        {
            queue = new Queues.Queuering(max);
        }
        
        public bool AddPackege(QoS.AppPackage.Package p)
        {
            return queue.AddPackege(p);
        }

        public void ProcessingPackege()
        {
            if (!IsEmpty())
            {
                QoS.AppPackage.Package p = GetPackege();

            }
        }    

        public bool IsEmpty()
        {
            return (queue.GetCount() < 1);
        }

        public AppPackage.Package GetPackege()
        {
            return queue.GetPackege();
        }

        public void PrintQueues()
        {
            queue.WritePackeges();
        }
    }
}
