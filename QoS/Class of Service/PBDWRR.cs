using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class PBDWRR : IAlgorithm
    {
        const int max = 40;
        FIFO[] pQueues;
        DWRR[] dwQueues;
        const int count = 4; 

        public PBDWRR()
        {
            pQueues = new FIFO[2];
            for (int i = 0; i < count; ++i)
            {
                pQueues[i] = new FIFO();
            }
            dwQueues = new DWRR[3];
        }

        public bool AddPackege(AppPackage.Package p)
        {
            switch(p.priorityPackage)
            {
                case AppPackage.Priority.
                    return pQueues[0].AddPackege(p);
                case AppPackage.Priority.Medium:
                    return pQueues[1].AddPackege(p);
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
