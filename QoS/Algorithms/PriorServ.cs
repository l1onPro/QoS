using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class PriorServ : IAlgorithm
    {
        const int max = 40;
        Queues.Queuering[] priorityQueues;
        const int count = 4; 

        public PriorServ()
        {
            priorityQueues = new Queues.Queuering[count];
            for (int i = 0; i < count; ++i)
            {
                priorityQueues[i] = new Queues.Queuering(max / 4);
            }
        }

        public bool AddPacket(AppPackage.Package p)
        {
            switch(p.priorityPackage)
            {
                case AppPackage.Priority.Low:
                    return priorityQueues[3].AddPackege(p);
                case AppPackage.Priority.Medium:
                    return priorityQueues[2].AddPackege(p);
                case AppPackage.Priority.High:
                    return priorityQueues[1].AddPackege(p);
                case AppPackage.Priority.Suprime:
                    return priorityQueues[0].AddPackege(p);
                default:
                    return false;
            }
            
        }

        public void ProcessingPackege()
        {
            if (!IsEmpty())
            {
                AppPackage.Package p = GetPackege();
                
            }

        }

        public bool IsEmpty()
        {
            int count = 0;
            foreach (Queues.Queuering queue in priorityQueues)
            {
                count += queue.GetCount();
            }
            return count < 1;
        }

        public AppPackage.Package GetPackege()
        {
            int i = 0;
            while (i < 3)
            {
                if (priorityQueues[i].GetCount() > 0)
                {
                    return priorityQueues[i].GetPackege();
                }
                else
                {
                    ++i;
                }
            }
            return priorityQueues[i].GetPackege();
        }

        public void PrintQueues()
        {
            foreach (Queues.Queuering q in priorityQueues)
            {
                q.WritePackeges();
            }
            Console.WriteLine();
        }
    }
}
