using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    class AlgorithmsTest
    {
        private Fifo fifo;
        private PriorServ priorServ;

        public AlgorithmsTest()
        {
            fifo = new Fifo();
            priorServ = new PriorServ();
        }

        public void Testing()
        {

        }
    }
}
