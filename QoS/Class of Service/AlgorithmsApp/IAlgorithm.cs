using QoS.AppPackage;
using QoS.Class_of_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    interface IAlgorithm
    {
        /// <summary>
        /// Добавить пакет в алгоритм
        /// </summary>
        /// <param name="newPackage"></param>
        void Add(Package newPackage);

        /// <summary>
        /// получить результирующий пакет
        /// </summary>
        /// <returns></returns>
        Package GetPackage();

        /// <summary>
        /// Есть ли пакеты в очередях
        /// </summary>
        /// <returns></returns>
        bool NotNULL();
    }
}
