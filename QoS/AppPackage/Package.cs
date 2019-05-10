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
        static int nextID = 1;
        public DSCPName Name{ get; set; }
        public ModelPackage model { get; set; }

        public Priority priorityPackage { get; set; }
       

        int length;
        public int Length
        {
            get { return length; }
            set { if (value >= 0) length = value; else throw new ArgumentException(); }
        }        
       
        /// <summary>
        /// Создание пакета
        /// </summary>
        /// <param name="length">Килло байт</param>
        public Package(int length)
        {
            this.id = NexID();            
            Length = length;
        }

        private static int NexID() { return nextID++; }

        public override string ToString()
        {
            return "id: " + id + " length: " + Length + "к/б\n";   
        }
    }
}
