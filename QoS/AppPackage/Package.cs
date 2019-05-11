using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    class Package
    {
        int id;
        public int ID { get { return id; } }

        static int nextID = 1;
        public DSCPName CoS { get; set; }    
        public Priority priorityPackage { get; set; }
        public int TimeWait { get; set; }
        public GradColor color { get; set; }
        int length;
        public int Length
        {
            get { return length; }
            set { if (value >= 0) length = value; else throw new ArgumentException(); }
        }        
        
        public int IP_Precedence { get; set; }

        /// <summary>
        /// Создание пакета
        /// </summary>
        /// <param name="length">Килло байт</param>
        public Package(DSCPName coS, int length)
        {
            this.id = NexID();
            CoS = coS;
            Length = length;

            SetIPPrecedence();
        }

        private static int NexID() { return nextID++; }

        public override string ToString()
        {
            return "id: " + id + " length: " + Length + "к/б CoS: " + CoS + "\n";   
        }

        private void SetIPPrecedence()
        {
            switch (CoS)
            {
                case DSCPName.CS0:
                    IP_Precedence = 0;
                    break;
                case DSCPName.AF1:
                    IP_Precedence = 1;
                    break;
                case DSCPName.AF2:
                    IP_Precedence = 2;
                    break;
                case DSCPName.AF3:
                    IP_Precedence = 3;
                    break;
                case DSCPName.AF4:
                    IP_Precedence = 4;
                    break;
                case DSCPName.EF:
                    IP_Precedence = 5;
                    break;
                case DSCPName.CS6:
                    IP_Precedence = 6;
                    break;
                case DSCPName.CS7:
                    IP_Precedence = 7;
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
