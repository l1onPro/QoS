using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    interface IAlgorithm
    {
        bool AddPackege(AppPackage.Package p);

        void ProcessingPackege();

        AppPackage.Package GetPackege();

        bool IsEmpty();

        void PrintQueues();
    }
}
