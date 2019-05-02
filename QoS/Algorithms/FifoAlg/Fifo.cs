using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algoritms.FifoAlg
{
    class Fifo : IAlgorithm
    {
        private BaseClasses.Queue queue;

        public Fifo()
        {
            queue = new BaseClasses.Queue(100);
        }

        public bool AddPacket(QoS.BaseClasses.Packet p)
        {
            return queue.addPacket(p);
        }

        public void ProcessingPacket()
        {
            if (queue.getCount() > 0)
            {
                QoS.BaseClasses.Packet p = GetPacket();

            }
        }    

        public BaseClasses.Packet GetPacket()
        {
            return queue.getPacket();
        }
    }
}
