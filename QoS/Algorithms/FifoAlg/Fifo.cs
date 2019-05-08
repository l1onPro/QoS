using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class Fifo : IAlgorithm
    {
        private BaseClasses.Queue queue;
        const int max = 40;
        
        public Fifo()
        {
            queue = new BaseClasses.Queue(max);
        }
        
        public bool AddPacket(QoS.BaseClasses.Packet p)
        {
            return queue.AddPacket(p);
        }

        public void ProcessingPacket()
        {
            if (!IsEmpty())
            {
                QoS.BaseClasses.Packet p = GetPacket();

            }
        }    

        public bool IsEmpty()
        {
            return (queue.GetCount() < 1);
        }

        public BaseClasses.Packet GetPacket()
        {
            return queue.GetPacket();
        }

        public void PrintQueues()
        {
            queue.WritePackets();
        }
    }
}
