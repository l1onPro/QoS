using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.BaseClasses
{
  
    enum Priority
    {
        Low,
        Medium,
        High,
        Suprime
    }
    struct Packet
    {
        bool type;
        int lenghtTitle;
        public Priority pryority;
    }
}
