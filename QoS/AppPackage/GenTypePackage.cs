using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    class GenTypePackage
    {
        Random random;       

        public GenTypePackage()
        {            
            random = new Random(4);
        }

        public TypePakage NextType()
        {
            int k = random.Next(4);
            switch (k)
            {
                case 0:
                    return TypePakage.Voice;
                case 1:
                    return TypePakage.Critically_important;
                case 2:
                    return TypePakage.Transactions;
                case 3:
                    return TypePakage.Non_guaranteed_Delivery;
                default:
                    throw new Exception();                    
            }
        }
    }
}
