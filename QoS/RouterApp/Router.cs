using QoS.Algorithms;
using QoS.AppPackage;
using QoS.Class_of_Service.AlgorithmsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.RouterApp
{
    class Router
    {       
        /// <summary>
        /// Алготимы диспетчера
        /// </summary>
        IAlgorithm algorithm;

        /// <summary>
        /// Пакеты, которые пришли для обработки
        /// </summary>
        List<Package> allPackages;

        DispatcherTimer timer;
        public Router()
        {

        }   


    }
}
