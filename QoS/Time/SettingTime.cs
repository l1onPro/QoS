using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Time
{
    public static class SettingTime
    {
        static int millisecond;
        public static int Millisecond
        {
            get { return millisecond; }
            set { if (value > 0) millisecond = value; }
        }
    }
}
