using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.AppPackage
{
    /// <summary>
    /// генерератор пакетов
    /// </summary>
    class GenPackage
    {        
        GenTypePackage genType;       
        Random random;
        public string result = "";
      
        public GenPackage()
        {            
            random = new Random();
            genType = new GenTypePackage();            
        }          

        /// <summary>
        /// разделение пакетов на модели
        /// </summary>
        /// <param name="type">Тип пакета</param>
        /// <returns></returns>
        private DSCPName SetPHBForPackage(ServiceClassName type)
        {
            switch (type)
            {
                case ServiceClassName.Network_Control_2:
                    return DSCPName.CS7;
                case ServiceClassName.Network_Control_1:
                    return DSCPName.CS6;
                case ServiceClassName.Telephony:
                    return DSCPName.EF;
                case ServiceClassName.Multemedia_Conferencing:
                    return DSCPName.AF4;
                case ServiceClassName.Multemedia_Streaming:
                    return DSCPName.AF3;
                case ServiceClassName.LowLatency_Data:
                    return DSCPName.AF2;
                case ServiceClassName.HightThroughput_Data:
                    return DSCPName.AF1;
                case ServiceClassName.Standart:
                    return DSCPName.CS0;                    
                default:
                    throw new Exception();
            }            
        }       

        public Package New()
        {
            //создается новый рандомный пакет
            ServiceClassName newPackege = genType.NextType();
            //Задается маркером
            DSCPName CoS = SetPHBForPackage(newPackege);            
            //Создается пакет
            return new Package(CoS, random.Next(5, 100));  
        }
    }
}
