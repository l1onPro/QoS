using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    class CBWFQ : IAlgorithm
    {
        private List<Queuering> listQueue;
        private int[] weight;

        /// <summary>
        /// Class-Based WFQ
        /// </summary>
        public CBWFQ()
        {
            listQueue = new List<Queuering>(8);
            weight = new int[8];
            SetWeight();
        }

        private void SetWeight()
        {
            weight = new int[8] { 56, 48, 46, 34, 26, 18, 10, 0 };
        }

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case DSCPName.CS0:
                    listQueue[7].AddPackege(newPackage);                    
                    break;
                case DSCPName.AF1:
                    listQueue[6].AddPackege(newPackage);
                    break;
                case DSCPName.AF2:
                    listQueue[5].AddPackege(newPackage);
                    break;
                case DSCPName.AF3:
                    listQueue[4].AddPackege(newPackage);
                    break;
                case DSCPName.AF4:
                    listQueue[3].AddPackege(newPackage);
                    break;
                case DSCPName.EF:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.CS6:
                    listQueue[1].AddPackege(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackege(newPackage);
                    break;
                default:
                    throw new Exception();
            }
        }

        public Package GetPackage()
        {
            throw new NotImplementedException();
        }

        public bool NotNULL()
        {
            throw new NotImplementedException();
        }
    }
}
