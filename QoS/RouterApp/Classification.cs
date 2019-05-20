using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.RouterApp
{
    class Classification
    {   
        public Classification()
        {
                 
        }

        public static Package ClassificationPackage(Package newPackage)
        {
            Package package = newPackage;

            //по значение полей DSCP
            package.CoS = Huawei.GetServiceClass(package.DSCP);

            package.Color = Huawei.GetColor(package.DSCP);

            package.IP_Precedence = Huawei.GetIPPrecedence(package.CoS);       
            
            return package;
        }   
    }
}
