using QoS.Class_of_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.AppPackage
{
    class Package
    {
        public int ID { get; }

        private static int nextID = 1;
                
        int dscp;
        /// <summary>
        /// значение DSCP
        /// </summary>
        public int DSCP
        {
            get { return dscp; }
            set { if (value >= 0 && value < 64) dscp = value; else throw new ArgumentException(); }
        }

        /// <summary>
        /// Class of Service
        /// </summary>
        public PHB CoS { get; set; }

        /// <summary>
        /// Время ожидания пакета
        /// </summary>
        public int TimeWait { get; set; }

        /// <summary>
        /// Цвет
        /// </summary>
        public GradColor Color { get; set; }

        int lengthInBit;
        /// <summary>
        /// Размер пакета в битах
        /// </summary>
        public int LengthInBit
        {
            get { return lengthInBit; }
            set { if (value >= 0) lengthInBit = value; else throw new ArgumentException(); }
        }

        private void SetBit(int Byte)
        {
            LengthInBit = Byte * 8;
        }

        int length;
        /// <summary>
        /// Размер пакета в байтах
        /// </summary>
        public int Length
        {
            get { return length; }
            set { if (value >= 0) { length = value; SetBit(length); } else throw new ArgumentException(); }
        }

        public int IP_Precedence { get; set; }

        /// <summary>
        /// Таймер жизни пакета
        /// </summary>
        DispatcherTimer timer;

        /// <summary>
        /// Запуск генератора
        /// </summary>
        private void StartTimerGenPackage()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, Setting.Millisecond);
            timer.Tick += new EventHandler(TimeWork);
            timer.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        public void StopTimerGenPackage()
        {
            timer.Stop();
        }

        private void TimeWork(object sender, EventArgs e)
        {
            TimeWait += 1; //в мс
        }

        /// <summary>
        /// Создание пакета
        /// </summary>
        /// <param name="DSCP"></param>
        public Package(int DSCP)
        {
            this.ID = NexID();
            this.DSCP = DSCP;

            TimeWait = 0;
            StartTimerGenPackage();
        }

        public Package(int DSCP, int length)
        {
            this.ID = NexID();
            this.DSCP = DSCP;
            Length = length;

            TimeWait = 0;
            StartTimerGenPackage();
        }

        private static int NexID() { return nextID++; }

        public override string ToString()
        {
            return "id: " + ID + "  DSCP: " + DSCP + "  CoS: " + CoS + "  Color: " + Color + "  Length: " + Length + " байт" + "  Length: " + LengthInBit + " Бит" + "  IP_Precedence: " + IP_Precedence;   
        }        
    }
}
