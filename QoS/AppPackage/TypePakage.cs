using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    /// <summary>
    /// Тип рандомного пакета
    /// </summary>
    enum ServiceClassName
    {
        Network_Control_1,
        Network_Control_2,
        Telephony,
        Multemedia_Conferencing,
        Multemedia_Streaming,
        LowLatency_Data,
        HightThroughput_Data,
        Standart
    }
}
