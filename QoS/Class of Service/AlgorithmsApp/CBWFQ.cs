using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// Class-Based WFQ
    /// </summary>
    class CBWFQ : IAlgorithm
    {
        /// <summary>
        /// Список очередей
        /// </summary>
        private List<Queuering> listQueue;

        /// <summary>
        /// массив весов
        /// </summary>
        private int[] weight;

        /*
         * для этих классов 
         * определяется «вес» и пакеты их очередей обслуживаются, соразмерно весу 
         * (больше вес — больше пакетов из этой очереди уйдёт в единицу времени)
         */

        /// <summary>
        /// Class-Based WFQ
        /// </summary>
        public CBWFQ()
        {
            listQueue = new List<Queuering>(8);           

            for (int i = 0; i < listQueue.Count; i++)
            {
                listQueue[i] = new Queuering();
            }

            //Назначается классам вес.
            SetWeight();
        }

        /// <summary>
        /// Установка весов
        /// </summary>
        private void SetWeight()
        {           
            //задается в %
            weight = new int[8] { 20, 20, 15, 10, 10, 10, 10, 5 };            
        }

        /// <summary>
        /// Добавить пакет в соответствующую очередь
        /// </summary>
        /// <param name="newPackage"></param>
        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case DSCPName.CS0:
                    listQueue[7].AddPackage(newPackage);                    
                    break;
                case DSCPName.AF1:
                    listQueue[6].AddPackage(newPackage);
                    break;
                case DSCPName.AF2:
                    listQueue[5].AddPackage(newPackage);
                    break;
                case DSCPName.AF3:
                    listQueue[4].AddPackage(newPackage);
                    break;
                case DSCPName.AF4:
                    listQueue[3].AddPackage(newPackage);
                    break;
                case DSCPName.EF:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.CS6:
                    listQueue[1].AddPackage(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackage(newPackage);
                    break;
                default:
                    throw new Exception();
            }
        }

        public bool NotNULL()
        {
            foreach (Queuering queue in listQueue)
            {
                if (queue.Count != 0) return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает первый пакет выбранной очереди и удаляет из учереди
        /// </summary>
        /// <param name="num">Номер очереди</param>
        /// <returns></returns>
        private Package GetPackage(int num)
        {
            if (num != -1) return listQueue[num].GetPackage();
            else return null;
        }

        /// <summary>
        /// Возвращает первый пакет выбранной очереди и не удаляет из очереди
        /// </summary>
        /// <param name="num">Номер очереди</param>
        /// <returns></returns>
        private Package FirstPackage(int num)
        {
            if (num != -1) return listQueue[num].FirstPackage();
            else return null;
        }

        /// <summary>
        /// Вычисляем сколько доступно длины для i очереди
        /// </summary>
        /// <param name="i">Номер очереди</param>
        /// <param name="speed">Скорость пропускания</param>
        /// <returns></returns>
        private int FindLengthForQueue(int i, int speed)
        {
            /*
             * speed - 100%
             * ? - weight[i] %
             */
            return speed * weight[i] / 100;
        }

        public Queue<Package> GetPackages(int speed)
        {
            Queue<Package> packages = new Queue<Package>();

            for (int i = 0; i < listQueue.Count; i++)
            {
                int MaxLength = FindLengthForQueue(i, speed);
                int sum = 0;

                while(listQueue[i].NOTNULL())
                {
                    Package pack = FirstPackage(i);
                    sum += pack.Length;
                    if (sum <= speed) packages.Enqueue(GetPackage(i));
                    else break;
                }
            }

            return packages;
        }
    }
}
