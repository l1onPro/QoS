using QoS.AppPackage;
using QoS.Class_of_Service.AlgorithmsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service
{
    /// <summary>
    /// Управление перегрузками
    /// </summary>
    class Congestion_Management
    {
        /// <summary>
        /// Алготимы диспетчера
        /// </summary>
        IAlgorithm algorithm;

        /// <summary>
        /// Пакеты, которые пришли для обработки
        /// </summary>
        List<Package> allPackages;

        public Congestion_Management()
        {

        }

        
    }
}
