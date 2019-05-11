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
    /// <summary>
    /// Управление перегрузками
    /// </summary>
    class Congestion_Management
    {
        /// <summary>
        /// Алготимы диспетчера
        /// </summary>
        IAlgorithm algorithm;

        DispatcherTimer timer;

        public Congestion_Management()
        {

        }        
    }
}
