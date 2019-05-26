using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using QoS.AppPackage;
using System.Windows.Threading;

namespace QoS.Class_of_Service
{
    class Queuering
    {
        public int ID { get; }

        public static int nextID = 0;

        private static int NexID() { return nextID++; }


        private int maxLength;
        public int MaxLength
        {
            get { return maxLength; }
            set { if (value >= Setting.MinConstSizeQueuering && value <= Setting.MaxConstSizeQueuering) maxLength = value; else throw new Exception(); }
        }
        public int CurLength { get; set; }


        private Queue<Package> packets;
        private Mutex mtx = new Mutex();        
        Random random = new Random();

        public Queuering()
        {
            this.ID = NexID();
            this.maxLength = Setting.CurSizeQueuering;
            CurLength = 0;
            packets = new Queue<Package>();
        }

        public Queuering(int maxLength)
        {
            this.ID = NexID();
            CurLength = 0;
            this.maxLength = maxLength;
            packets = new Queue<Package>();
        }

        public Queue<Package> GetAllPackages()
        {
            return packets;
        }

        /// <summary>
        /// Процент заполненности очереди
        /// </summary>
        /// <returns></returns>
        private int CheckBuffer()
        {
            //maxLength - 100%
            //curLength - ?%
            return CurLength * 100 / maxLength;
        }

        /// <summary>
        /// Weighted Random Early Detection
        /// true - отбрасывается пакет
        /// </summary>
        private bool WRED(Package package)
        {
            int occupancy = CheckBuffer();
            switch (package.Color)
            {
                case GradColor.red:
                    if (occupancy <= 20) return false;                                          //ничего не отрбрасывается
                    if (occupancy > 20 && occupancy <= 25) return random.NextDouble() < 0.05;   //отбрасывается 5%
                    if (occupancy > 25 && occupancy <= 30) return random.NextDouble() < 0.10;   //отбрасывается 10%
                    if (occupancy > 30 && occupancy <= 35) return random.NextDouble() < 0.15;   //отбрасывается 15%
                    if (occupancy > 35 && occupancy <= 40) return random.NextDouble() < 0.20;   //отбрасывается 20%
                    return true;                                                                //все отбрасывается при выше 40%                    
                case GradColor.yellow:
                    if (occupancy <= 30) return false;                                          //ничего не отрбрасывается
                    if (occupancy > 30 && occupancy <= 35) return random.NextDouble() < 0.025;  //отбрасывается 2,5%
                    if (occupancy > 35 && occupancy <= 40) return random.NextDouble() < 0.05;   //отбрасывается 5,0%
                    if (occupancy > 40 && occupancy <= 45) return random.NextDouble() < 0.075;  //отбрасывается 7,5%
                    if (occupancy > 45 && occupancy <= 50) return random.NextDouble() < 0.1;    //отбрасывается 10%                   
                    return true;                                                                //все отбрасывается при выше 50%                      
                case GradColor.green:
                    if (occupancy <= 50) return false;
                    if (occupancy > 50 && occupancy <= 55) return random.NextDouble() < 0.005;  //отбрасывается 0.5%
                    if (occupancy > 55 && occupancy <= 60) return random.NextDouble() < 0.010;  //отбрасывается 1.0%
                    if (occupancy > 60 && occupancy <= 65) return random.NextDouble() < 0.015;  //отбрасывается 1.5%
                    if (occupancy > 65 && occupancy <= 70) return random.NextDouble() < 0.020;  //отбрасывается 2.0%
                    if (occupancy > 70 && occupancy <= 75) return random.NextDouble() < 0.025;  //отбрасывается 2.5%
                    if (occupancy > 75 && occupancy <= 80) return random.NextDouble() < 0.030;  //отбрасывается 3.0%
                    if (occupancy > 80 && occupancy <= 85) return random.NextDouble() < 0.035;  //отбрасывается 3.5%
                    if (occupancy > 85 && occupancy <= 90) return random.NextDouble() < 0.040;  //отбрасывается 4.0%
                    if (occupancy > 90 && occupancy <= 95) return random.NextDouble() < 0.045;  //отбрасывается 4.5%
                    if (occupancy > 95 && occupancy < 100) return random.NextDouble() < 0.05;   //отбрасывается 5%
                    return true;                                                                //все отбрасывается
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
        public bool TailDrop(int length)
        {
            return CurLength + length > maxLength;           
        }

        /// <summary>
        /// Отбрасывает первый в очереди пакет, который находится долго
        /// </summary>
        private void HeadDrop()
        {
            //максимальное время застоя 
            if (NOTNULL())
            {
                if (FirstPackage().TimeWait > Setting.WaitTime)
                    packets.Dequeue();
            }            
        }

        /// <summary>
        /// добавляет пакет
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool AddPackage(Package p)
        {
            mtx.WaitOne();
            //только Tail Drop
            if (p.CoS == PHB.EF || p.CoS == PHB.CS6 || p.CoS == PHB.CS7)
            {
                if (!TailDrop(p.Length))
                {
                    packets.Enqueue(p);
                    CurLength += p.Length;
                    mtx.ReleaseMutex();
                    return true;
                }
            }
            //применяется WRED
            if (p.CoS == PHB.AF1 || p.CoS == PHB.AF2 || p.CoS == PHB.AF3 || p.CoS == PHB.AF4)
            {
                if (!WRED(p) && !TailDrop(p.Length))
                {
                    packets.Enqueue(p);
                    CurLength += p.Length;
                    mtx.ReleaseMutex();
                    return true;
                }
            }
            //применяется Tail Drop и Head Drop
            if (p.CoS == PHB.DF)
            {
                //HeadDrop();
                if (!TailDrop(p.Length))
                {
                    packets.Enqueue(p);
                    CurLength += p.Length;
                    mtx.ReleaseMutex();
                    return true;
                }                
            }
            mtx.ReleaseMutex();
            return false;
        }
        public bool AddPackage(Package p, bool type)
        {
            if (!TailDrop(p.Length))
            {
                packets.Enqueue(p);
                CurLength += p.Length;                
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет пакет из начала очереди и возращает его
        /// </summary>
        /// <returns></returns>
        public Package GetPackage()
        {
            mtx.WaitOne();
            if (NOTNULL())
            {
                Package p = packets.Dequeue();
                CurLength -= p.Length;
                mtx.ReleaseMutex();
                return p;
            }
            mtx.ReleaseMutex();
            return null;
        }

        /// <summary>
        /// возвращает объект, находящийся в начале очереди, но не удаляет его
        /// </summary>
        /// <returns></returns>
        public Package FirstPackage()
        {
            mtx.WaitOne();
            if (NOTNULL())
            {
                Package p = packets.Peek();
                mtx.ReleaseMutex();
                return p;
            }
            mtx.ReleaseMutex();
            return null;
        }

        /// <summary>
        /// Текущее состоянии очереди
        /// </summary>
        /// <returns></returns>
        public int Count
        {
            get { mtx.WaitOne(); int count = packets.Count; mtx.ReleaseMutex(); return count; }
        }

        /// <summary>
        /// Есть ли элементы в очереди
        /// </summary>
        /// <returns></returns>
        public bool NOTNULL()
        {
            return Count != 0;
        }

        public override string ToString()
        {
            mtx.WaitOne();
            string txt = info() + "\n";
            foreach (AppPackage.Package p in packets)
            {
                txt += p.ToString() + "\n";
            }
            mtx.ReleaseMutex();
            return txt;
        }

        public string PrintToFile()
        {
            mtx.WaitOne();
            string txt = info() + Environment.NewLine;
            foreach (AppPackage.Package p in packets)
            {
                txt += p.ToString() + Environment.NewLine;
            }
            mtx.ReleaseMutex();
            return txt;
        }

        public string info()
        {
            return "Queuering id: " + ID + "  length: " + CurLength + "  max length: " + maxLength;            
        }

        public void Clear()
        {
            CurLength = 0;
            packets = new Queue<Package>();
        }
    }
}
