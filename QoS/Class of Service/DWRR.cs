using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class DWRR : IAlgorithm
    {
        int[] cash;
        Queues.Queuering[] queues;
        int count;
        int step;
        int maxLen = 40;

        public DWRR(int cnt)
        {
            count = cnt;
            cash = new int[cnt];
            queues = new Queues.Queuering[cnt];
            for (int i = 0; i < count; ++i)
            {
                queues[i] = new Queues.Queuering(maxLen);
            }
        }

        public bool AddPackege(AppPackage.Package p)
        {
            ;
        }
    }
}
