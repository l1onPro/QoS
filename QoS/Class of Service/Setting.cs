using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service
{
    /// <summary>
    /// Глобальная настройка параметров
    /// </summary>
    public static class Setting
    {
        /// <summary>
        /// частота обновление сек
        /// </summary>
        public static int frequencyUpdate { get; }  = 1;

        public static int numAlg { get; set; }

        public static int Move { get; } = 60;

        /// <summary>
        /// Цикл обновления потоков (1 мс)
        /// </summary>
        public static int Millisecond { get; } = 100;

        /// <summary>
        /// Максимальный размер пакета в байтах
        /// </summary>
        public static int MaxSizePackage { get; } = 1500;

        /// <summary>
        /// Минимальный размер пакета в байтах
        /// </summary>
        public static int MinSizePackage { get; } = 46;

        /// <summary>
        /// в байтах
        /// </summary>
        static int maxSizeQueuering = 10000;
        /// <summary>
        /// Максимальный размер очереди
        /// </summary>
        public static int MaxSizeQueuering
        {
            get { return maxSizeQueuering; }
            set { if (value > 0) maxSizeQueuering = value; }
        }       
               
        static int speed = 100;
        /// <summary>
        /// Скорость интерфейса (мегабит в секунду)
        /// </summary>
        public static int Speed
        {
            get { return speed; }
            set { if (value > 0) speed = value; }
        }     

        /// <summary>
        /// Максимальная задержка пакета
        /// </summary>
        static int waitTime = 200;  //мс
        public static int WaitTime
        {
            get { return waitTime; }
            set { if (value > 0) waitTime = value; }
        }

        /// <summary>
        /// Текущее время работы
        /// </summary>
        static int timeWork;
        public static int TimeWork
        {
            get { return waitTime; }
            set { if (value > 0) waitTime = value; }
        }

        
    }
}
