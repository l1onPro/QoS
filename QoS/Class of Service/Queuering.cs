﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using QoS.AppPackage;
using System.Windows.Threading;

namespace QoS.Queues
{
    class Queuering
    {
        /// <summary>
        /// Что это такое?
        /// </summary>
        private int maxn;

        private Queue<Package> packets;
        private Mutex mtx = new Mutex();        
        Random random = new Random();

        public Queuering(int maxn)
        {
            this.maxn = maxn;
            packets = new Queue<AppPackage.Package>();
        }

        private void WRED()
        {

        }

        /// <summary>
        /// отбрасыватель пакетов, если true - отбрасывает
        /// </summary>
        /// <param name="Occupancy">Заполненность</param>
        /// <returns></returns>
        private bool Garbage_collector(int Occupancy)
        {
            if (Occupancy <= 80) return false;                                              //не отбрасывает пакеты
            if (Occupancy > 80 && Occupancy <= 82) return random.NextDouble() < 0.1;        //
            if (Occupancy > 82 && Occupancy <= 84) return random.NextDouble() < 0.2;        //
            if (Occupancy > 84 && Occupancy <= 86) return random.NextDouble() < 0.3;        //
            if (Occupancy > 86 && Occupancy <= 88) return random.NextDouble() < 0.4;        //
            if (Occupancy > 88 && Occupancy <= 90) return random.NextDouble() < 0.45;       //
            if (Occupancy > 90 && Occupancy <= 92) return random.NextDouble() < 0.5;        //обтрасывает 50% случайным образом

            if (Occupancy > 92 && Occupancy <= 94) return random.NextDouble() < 0.6;        //
            if (Occupancy > 94 && Occupancy <= 96) return random.NextDouble() < 0.7;        //
            if (Occupancy > 96 && Occupancy <= 98) return random.NextDouble() < 0.8;        //
            if (Occupancy > 98 && Occupancy < 100) return random.NextDouble() < 0.95;       //

            return true;                                                                    //типа TailDrop
        }

        /// <summary>
        /// Random Early Detection
        /// </summary>
        private void RED()
        {
            //проверка очереди на сколько заполнена 

            //отправка на проверку отсечения
            Garbage_collector(23);
        }

        private bool TailDrop()
        {
            return packets.Count < maxn ? true : false;            
        }

        private void HeadDrop()
        {
            //пройтись по всей очереди и посмотреть застаявшиеся пакеты, если такие есть, удалить
        }

        public bool AddPackege(Package p)
        {
            mtx.WaitOne();
            bool good;      
            
            //в зависимости от очереди будет применяться свой отбрсыватель
            good = TailDrop();

            if (good) packets.Enqueue(p);

            mtx.ReleaseMutex();
            return good;
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
