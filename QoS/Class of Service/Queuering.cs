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
        private int maxn;
      
        private Queue<Package> packets;
        private Mutex mtx = new Mutex();        
        Random random = new Random();

        public Queuering(int maxn)
        {
            this.maxn = maxn;
            packets = new Queue<AppPackage.Package>();
        }

        /// <summary>
        /// Процент заполненности очереди
        /// </summary>
        /// <returns></returns>
        private int CheckBuffer()
        {         
            //maxN - 100%
            //Count - ?%
            return packets.Count * 100 / maxn;
        }

        /// <summary>
        /// Weighted Random Early Detection
        /// true - отбрасывается пакет
        /// </summary>
        private bool WRED(Package package)
        {
            int occupancy = CheckBuffer();
            switch (package.color)
            {
                case GradColor.red:
                    if (occupancy <= 20) return false;                                          //ничего не отрбрасывается
                    if (occupancy > 20 && occupancy < 40) return random.NextDouble() < 0.2;     //отбрасывается 20%
                    return true;                                                                //все отбрасывается при выше 40%                    
                case GradColor.yellow:
                    if (occupancy <= 30) return false;                                          //ничего не отрбрасывается
                    if (occupancy > 30 && occupancy <= 50) return random.NextDouble() < 0.1;    //отбрасывается 10%
                    return true;                                                                //все отбрасывается при выше 50%                      
                case GradColor.green:
                    if (occupancy <= 50) return false;
                    if (occupancy > 50 && occupancy <= 55) return random.NextDouble() < 0.005;     //отбрасывается 0.5%
                    if (occupancy > 55 && occupancy <= 60) return random.NextDouble() < 0.010;     //отбрасывается 1.0%
                    if (occupancy > 60 && occupancy <= 65) return random.NextDouble() < 0.015;     //отбрасывается 1.5%
                    if (occupancy > 65 && occupancy <= 70) return random.NextDouble() < 0.020;     //отбрасывается 2.0%
                    if (occupancy > 70 && occupancy <= 75) return random.NextDouble() < 0.025;     //отбрасывается 2.5%
                    if (occupancy > 75 && occupancy <= 80) return random.NextDouble() < 0.030;     //отбрасывается 3.0%
                    if (occupancy > 80 && occupancy <= 85) return random.NextDouble() < 0.035;     //отбрасывается 3.5%
                    if (occupancy > 85 && occupancy <= 90) return random.NextDouble() < 0.040;     //отбрасывается 4.0%
                    if (occupancy > 90 && occupancy <= 95) return random.NextDouble() < 0.045;     //отбрасывается 4.5%
                    if (occupancy > 95 && occupancy < 100) return random.NextDouble() < 0.05;      //отбрасывается 5%
                    return true;                                                                   //все отбрасывается
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// отбрасыватель пакетов, если true - отбрасывает
        /// </summary>
        /// <param name="Occupancy">Заполненность</param>
        /// <returns></returns>
        private bool Garbage_collector_For_RED(int Occupancy)
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
        private bool RED()
        {            
            int cur = CheckBuffer();
            //отправка на проверку отсечения
            return Garbage_collector_For_RED(cur);
        }

        /// <summary>
        /// Отбрасывает прибывший пакет, если нет места. Если True - отбрасывает
        /// </summary>
        /// <returns></returns>
        private bool TailDrop()
        {
            return packets.Count >= maxn;            
        }

        /// <summary>
        /// Отбрасывает пакеты, которые находятся долго
        /// </summary>
        private void HeadDrop()
        {
            int maxWaitTiem = 40;       //максимальное время застоя
            var ordered = packets.OrderBy(e => e.ID);

            Queue<Package> newPackets = new Queue<Package>();

            //пройтись по всей очереди и посмотреть застаявшиеся пакеты, если такие есть, удалить
            foreach (var kv in ordered)
            {
                if (kv.TimeWait < maxWaitTiem) newPackets.Enqueue(kv);
            }

            packets = newPackets;           
        }

        /// <summary>
        /// добавляет пакет
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool AddPackege(Package p)
        {
            mtx.WaitOne();

            bool good = false;
            //в зависимости от очереди будет применяться свой отбрсыватель
            if (!TailDrop())
            {
                packets.Enqueue(p);
                good = true;
            }

            HeadDrop();

            mtx.ReleaseMutex();
            return good;
        }
     
        /// <summary>
        /// Удаляет пакет их начала очереди и удаляет его
        /// </summary>
        /// <returns></returns>
        public AppPackage.Package GetPackege()
        {
            mtx.WaitOne();
            AppPackage.Package p = packets.Dequeue();
            mtx.ReleaseMutex();
            return p;            
        }

        /// <summary>
        /// возвращает объект, находящийся в начале очереди, но не удаляет его
        /// </summary>
        /// <returns></returns>
        public AppPackage.Package FirstPackage()
        {
            mtx.WaitOne();
            AppPackage.Package p = packets.Peek();
            mtx.ReleaseMutex();
            return p; 
        }

        /// <summary>
        /// Текущее состоянии очереди
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return packets.Count;
        }

        public override string ToString()
        {
            string txt = "";
            foreach(AppPackage.Package p in packets)
            {
                txt += p.ToString();
            }
            return txt;
        }
    }
}
