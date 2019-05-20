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

        static int typeFrequencyGenPack = 10;
        public static int TypeFrequencyGenPack
        {
            get { return typeFrequencyGenPack; }
            set
            {
                if (value == 0) typeFrequencyGenPack = 30;
                else if (value == 1) typeFrequencyGenPack = 20;
                else if (value == 2) typeFrequencyGenPack = 10;
                else throw new Exception();
            }
        }

        /// <summary>
        /// Номер алгоритма
        /// </summary>
        public static int numAlg { get; set; }       

        /// <summary>
        /// Максимальный размер пакета в байтах
        /// </summary>
        public static int MaxSizePackage { get; } = 1500;

        /// <summary>
        /// Минимальный размер пакета в байтах
        /// </summary>
        public static int MinSizePackage { get; } = 100;

        /// <summary>
        /// Максимальный размер очереди (в байтах)
        /// </summary>
        public static int MaxConstSizeQueuering { get; } = 10000;
        /// <summary>
        /// Минимальный размер очереди (в байтах)
        /// </summary>
        public static int MinConstSizeQueuering { get; } = 3000;

        //по умолчанию 10000 (в байтах)
        static int curSizeQueuering = MaxConstSizeQueuering;
        /// <summary>
        /// Текущий максимальный размер очереди (в байтах)
        /// </summary>
        public static int CurSizeQueuering
        {
            get { return curSizeQueuering; }
            set { if (value >= MinConstSizeQueuering && value <= MaxConstSizeQueuering) curSizeQueuering = value; else throw new Exception(); }
        }

        /// <summary>
        /// Максимально допустимая скорость пропуская (в байтах)
        /// </summary>
        public static int MaxConstSpeed { get; } = 15000;
        /// <summary>
        /// Минимально допустимая скорость пропускаие (в байтах)
        /// </summary>
        public static int MinConstSpeed { get; } = 3000;

        //по умолчанию 10000
        static int curSpeed = MaxConstSpeed;
        /// <summary>
        /// Текущий максимальный размер очереди
        /// </summary>
        public static int CurSpeed
        {
            get { return curSpeed; }
            set { if (value >= MinConstSpeed && value <= MaxConstSpeed) curSpeed = value; else throw new Exception(); }
        }

        static int sizePaint = 50;
        public static int SizePaint
        {
            get { return sizePaint; }
            set
            {
                if (value == 0) sizePaint = 50;
                else if (value == 1) sizePaint = 40;
                else if(value == 2) sizePaint = 30;
                else throw new Exception();
            }
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
        /// Текущее время работы моделирования
        /// </summary>
        static int timeWork;
        public static int TimeWork
        {
            get { return waitTime; }
            set { if (value > 0) waitTime = value; }
        }
    }
}
