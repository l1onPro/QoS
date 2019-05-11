using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QoS.Class_of_Service
{
    class Queuering
    {
        private int maxn;
        private Queue<AppPackage.Package> packets;
        private Mutex mtx = new Mutex();

        public Queuering(int maxn)
        {
            this.maxn = maxn;
            packets = new Queue<AppPackage.Package>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool AddPackege(AppPackage.Package p)
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

        public AppPackage.Package GetPackege()
        {
            mtx.WaitOne();
            AppPackage.Package p = packets.Dequeue();
            mtx.ReleaseMutex();
            return p;
            
        }

        public AppPackage.Package FirstPackage()
        {
            mtx.WaitOne();
            AppPackage.Package p = packets.Peek();
            mtx.ReleaseMutex();
            return p; 
        }

        public int GetCount()
        {
            return packets.Count;
        }

        public void WritePackeges()
        {
            foreach(AppPackage.Package p in packets)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
