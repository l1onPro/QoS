using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service
{
    class PBDWRR : IQueuering
    {
        const int max = 40;
        FIFO[] pQueues;
        DWRRQueue[] dwQueues;
        const int count = 4; 

        public PBDWRR()
        {
            pQueues = new FIFO[2];
            for (int i = 0; i < count; ++i)
            {
                pQueues[i] = new FIFO();
            }
            dwQueues = new DWRRQueue[3];
        }

        public bool AddPackege(AppPackage.Package p)
        {
            switch(p.priorityPackage)
            {
                case AppPackage.Priority.Low:
                    return pQueues[0].AddPackege(p);
                case AppPackage.Priority.Medium:
                    return pQueues[1].AddPackege(p);
                case AppPackage.Priority.High:
                    return pQueues[1].AddPackege(p);
                case AppPackage.Priority.Suprime:
                    return pQueues[0].AddPackege(p);
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
            bool flag = true;
            foreach (FIFO queue in pQueues)
            {
                flag = flag & !queue.IsEmpty();
            }
            return flag;
        }

        public AppPackage.Package GetPackege()
        {
            return pQueues[0].GetPackege();
        }

        public void PrintQueues()
        {
            
        }
    }
}
