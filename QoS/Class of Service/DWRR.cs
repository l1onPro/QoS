using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service
{
    class DWRRQueue //: IQueuering
    {
        int[] cash;
        Queuering[] queues;
        int count;
        int step;
        int maxLen = 40;

        public DWRRQueue(int cnt)
        {
            count = cnt;
            cash = new int[cnt];
            queues = new Queuering[cnt];
            for (int i = 0; i < count; ++i)
            {
                queues[i] = new Queuering(maxLen);
            }
        }

        public bool AddPackege(AppPackage.Package p)
        {
            return true;
        }
    }
}
