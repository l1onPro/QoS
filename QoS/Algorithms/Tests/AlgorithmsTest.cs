using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class AlgorithmsTest
    {
        private FIFO fifo;
        private PBDWRR priorServ;

        public AlgorithmsTest()
        {
            fifo = new FIFO();
            priorServ = new PBDWRR();
        }

        public void Testing()
        {

        }
    }
}
