using QoS.Class_of_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    class PackageTest
    {
        int dscp;
        /// <summary>
        /// значение DSCP (Per-Hop Behavior)
        /// </summary>
        public int DSCP
        {
            get { return dscp; }
            set { if (value >= 0 && value < 64) dscp = value; else throw new ArgumentException(); }
        }

        int length;
        /// <summary>
        /// Размер пакета в байтах
        /// </summary>
        public int Length
        {
            get { return length; }
            set { if (value >= Setting.MinSizePackage && value <= Setting.MaxSizePackage) { length = value; } else throw new ArgumentException(); }
        }

        /// <summary>
        /// задержка
        /// </summary>
        public int Delay { get; set; }  

        public PackageTest(int dSCP, int length, int delay)
        {
            DSCP = dSCP;
            Length = length;
            Delay = delay;
        }
    }
}
