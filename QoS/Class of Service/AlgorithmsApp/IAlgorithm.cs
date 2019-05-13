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
        /// Добавляет новый пакет в конец очереди
        /// </summary>
        /// <param name="newPackage">Новый пакет</param>
        void Add(Package newPackage);

        /// <summary>
        ///  Получить результирующую очередь за 1 цикл
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <returns></returns>
        Queue<Package> GetPackage(int speed);

        /// <summary>
        /// Есть ли пакеты в очередях
        /// </summary>
        /// <returns></returns>
        bool NotNULL();
    }
}
