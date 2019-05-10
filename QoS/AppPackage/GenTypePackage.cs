using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    /// <summary>
    /// Генератор рандомного пакета из сервиса
    /// </summary>
    class GenTypePackage
    {
        Random random;       

        public GenTypePackage()
        {            
            random = new Random();
        }

        public ServiceClassName NextType()
        {
            int k = random.Next(8);
            switch (k)
            {
                case 0:
                    return ServiceClassName.Standart;
                case 1:
                    return ServiceClassName.HightThroughput_Data;
                case 2:
                    return ServiceClassName.LowLatency_Data;
                case 3:
                    return ServiceClassName.Multemedia_Streaming;
                case 4:
                    return ServiceClassName.Multemedia_Conferencing;
                case 5:
                    return ServiceClassName.Telephony;
                case 6:
                    return ServiceClassName.Network_Control_1;
                case 7:
                    return ServiceClassName.Network_Control_2;
                default:
                    throw new Exception();
            }
        }
    }
}
