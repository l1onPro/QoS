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

        public AllMarker Marker { get; set; }
        
        public Priority priorityPackage { get; set; }
        public ModelPackage model { get; set; }

        int size;
        public int Size
        {
            get { return size; }
            set { if (value >= 0) size = value; else throw new ArgumentException(); }
        }
        
        public int Length { get; }         

        /// <summary>
        /// Создание пакета
        /// </summary>
        /// <param name="size">Размер в битах</param>
        /// <param name="length">длина</param>
        public Package(int size, int length)
        {
            this.id = NexID();
            Size = size;
            Length = length;
        }

        private static int NexID() { return nextID++; }

        public override string ToString()
        {
            return "Package id: " + id + "\rSize: " + Size + "\rlength: " + Length + "\n";   
        }
    }
}
