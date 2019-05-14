using System;
using System.Collections.Generic;
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
        static int maxSize = 40;
        /// <summary>
        /// Максимальный размер очереди
        /// </summary>
        public static int MaxSize
        {
            get { return maxSize; }
            set { if (value > 0) maxSize = value; }
        }

        static int speed = 20;
        public static int Speed
        {
            get { return speed; }
            set { if (value > 0) speed = value; }
        }

        static Byte bitSet = 3;

        /// <summary>
        /// Bit
        /// </summary>
        public static Byte BitSet
        {
            get { return bitSet; }
            set { if (value > 0) bitSet = value; }
        }

        static int waitTime = 200;  //мс
        public static int WaitTime
        {
            get { return waitTime; }
            set { if (value > 0) waitTime = value; }
        }
    }
}
