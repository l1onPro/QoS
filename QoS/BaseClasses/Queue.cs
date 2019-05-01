using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QoS.BaseClasses
{
    class Queue
    {
        private int maxn;
        private Queue<Packet> packets;

        public Queue(int maxn)
        {
            this.maxn = maxn;
        }

        public bool addPacket(Packet p)
        {
            if (packets.Count < maxn)
            {
                packets.Enqueue(p);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Packet getPacket()
        {
            return packets.Dequeue();
            
        }

        public int getCount()
        {
            return packets.Count;
        }
    }
}
