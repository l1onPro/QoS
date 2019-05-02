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
        BaseClasses.Queue[] priorityQueues;
        const int count = 4; 

        public PriorServ()
        {
            priorityQueues = new BaseClasses.Queue[count];
            for (int i = 0; i < count; ++i)
            {
                priorityQueues[i] = new BaseClasses.Queue(max / 4);
            }
        }

        public bool AddPacket(BaseClasses.Packet p)
        {
            switch(p.pryority)
            {
                case BaseClasses.Priority.Low:
                    return priorityQueues[3].addPacket(p);
                case BaseClasses.Priority.Medium:
                    return priorityQueues[2].addPacket(p);
                case BaseClasses.Priority.High:
                    return priorityQueues[1].addPacket(p);
                case BaseClasses.Priority.Suprime:
                    return priorityQueues[0].addPacket(p);
                default:
                    return false;
            }
        }

        public void ProcessingPacket()
        {
            if (!IsEmpty())
            {
                BaseClasses.Packet p = GetPacket();
                
            }

        }

        public bool IsEmpty()
        {
            int count = 0;
            foreach (BaseClasses.Queue queue in priorityQueues)
            {
                count += queue.getCount();
            }
            return count < 1;
        }

        public BaseClasses.Packet GetPacket()
        {
            int i = 0;
            while (i < 3)
            {
                if (priorityQueues[i].getCount() > 0)
                {
                    return priorityQueues[i].getPacket();
                }
                else
                {
                    ++i;
                }
            }
            return priorityQueues[i].getPacket();
        }
    }
}
