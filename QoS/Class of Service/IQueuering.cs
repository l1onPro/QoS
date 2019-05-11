using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service
{
    interface IQueuering
    {
        bool AddPackege(AppPackage.Package p);

        void ProcessingPackege();

        AppPackage.Package GetPackege();

        bool IsEmpty();

        void PrintQueues();


    }
}
