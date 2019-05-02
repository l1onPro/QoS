using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QoS.BaseClasses
{
    class Queue
    {
        private int maxn;
        private Queue<Packet> packets;
        private static Mutex mtx = new Mutex();

        public Queue(int maxn)
        {
            this.maxn = maxn;
        }

        public bool AddPacket(Packet p)
        {
            mtx.WaitOne();
            bool f;
            if (packets.Count < maxn)
            {
                packets.Enqueue(p);
                f = true;
            }
            else
            {
                f = false;
            }
            mtx.ReleaseMutex();
            return f;
        }

        public Packet GetPacket()
        {
            mtx.WaitOne();
            Packet p = packets.Dequeue();
            mtx.ReleaseMutex();
            return p;
            
        }

        public int GetCount()
        {
            return packets.Count;
        }
    }
}
