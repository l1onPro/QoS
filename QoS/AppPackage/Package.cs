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

        public Priority priorityPackage { get; set; }
        
        int minSize;
        public int MinSize
        {
            get { return minSize; }
            set { if (value >= 0) minSize = value; else throw new ArgumentException(); }
        }

        public int maxSize;
        int MaxSize
        {
            get { return maxSize; }
            set { if (value >= 0) maxSize = value; else throw new ArgumentException(); }
        }

        public int Length { get; }         

        public Package(Priority priority, int min, int max, int length)
        {
            this.id = NexID();
            MinSize = min;
            MaxSize = max;
            Length = length;
        }

        private static int NexID() { return nextID++; }

        public override string ToString()
        {
            return "Package id: " + id + "\rminSize: " + minSize + "\rmaxSize: " + maxSize + "\rlength: " + Length + "\n\n";   
        }
    }
}
