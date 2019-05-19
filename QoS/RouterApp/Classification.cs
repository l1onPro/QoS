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
        Random random;
        /// <summary>
        /// Таблица классификаций на основе Huawei
        /// </summary>
        Huawei huawei;
        public Classification()
        {
            random = new Random();
            huawei = new Huawei();
        }

        public Package ClassificationPackage(Package newPackage)
        {
            Package package = newPackage;

            //по значение полей DSCP
            package.CoS = huawei.GetServiceClass(package.DSCP);
            package.Color = huawei.GetColor(package.DSCP);
            package.IP_Precedence = huawei.GetIPPrecedence(package.CoS);

            //установка длины в соответстии с цветом очереди
            package.Length = GetLength(package.Color);

            return package;
        }

        /// <summary>
        /// Установить размер пакета по цвету
        /// </summary>
        /// <param name="color">Зеленый, желтый, красный</param>
        /// <returns></returns>
        public int GetLength(GradColor color)
        {
            switch (color)
            {
                case GradColor.red:
                    return random.Next(1001, 1500);
                case GradColor.yellow:
                    return random.Next(501, 1000);
                case GradColor.green:
                    return random.Next(100, 500);
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// разделение пакетов на модели
        /// </summary>
        /// <param name="type">Тип пакета</param>
        /// <returns></returns>
        private PHB SetPHBForPackage(ServiceClassName type)
        {
            switch (type)
            {
                case ServiceClassName.Network_Control_2:
                    return PHB.CS7;
                case ServiceClassName.Network_Control_1:
                    return PHB.CS6;
                case ServiceClassName.Telephony:
                    return PHB.EF;
                case ServiceClassName.Multemedia_Conferencing:
                    return PHB.AF4;
                case ServiceClassName.Multemedia_Streaming:
                    return PHB.AF3;
                case ServiceClassName.LowLatency_Data:
                    return PHB.AF2;
                case ServiceClassName.HightThroughput_Data:
                    return PHB.AF1;
                case ServiceClassName.Standart:
                    return PHB.DF;
                default:
                    throw new Exception();
            }
        }        
    }
}
