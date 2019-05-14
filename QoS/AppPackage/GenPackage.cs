using QoS.Class_of_Service;
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

        private int GetDSCP()
        {
            return random.Next(64);
        }        

        public Package New()
        {
            //создается новый рандомный пакет
            //ServiceClassName newPackege = genType.NextType();   
            
            int DSCP = GetDSCP();
            int length = random.Next(Setting.MinSizePackage, Setting.MaxSizePackage);

            //Создается пакет
            return new Package(DSCP);  
        }
    }
}
