using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS
{
    class Test
    {
        //Отсюда запускать все свои тесты
        GenPackage package;

        public Test()
        {
            package = new GenPackage();
        }

        public string getResult()
        {
            return package.result;
        }
    }
}
