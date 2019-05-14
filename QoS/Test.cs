using QoS.AppPackage;
using QoS.RouterApp;
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
        Router router;

        public Test()
        {
            router = new Router();
        }

        public string getResult()
        {
            return "1";
        }
    }
}
