﻿using System;
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

        /// <summary>
        /// текущий вес
        /// </summary>
        private int curWeight; 

        /// <summary>
        /// номер очереди
        /// </summary>
        private int num;

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
            weight = new int[8];
            curWeight = -1;
            num = -1;

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
                    listQueue[7].AddPackege(newPackage);                    
                    break;
                case DSCPName.AF1:
                    listQueue[6].AddPackege(newPackage);
                    break;
                case DSCPName.AF2:
                    listQueue[5].AddPackege(newPackage);
                    break;
                case DSCPName.AF3:
                    listQueue[4].AddPackege(newPackage);
                    break;
                case DSCPName.AF4:
                    listQueue[3].AddPackege(newPackage);
                    break;
                case DSCPName.EF:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.CS6:
                    listQueue[1].AddPackege(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackege(newPackage);
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        ///  Возращает из каждой очереди пакетов = весу
        /// </summary>
        /// <returns></returns>
        public Package GetPackage()
        {
            /////////Переписать///////////////////
            if (curWeight == -1)
            {
                //если  дошли до конца очереди, обнуляем
                if (num == listQueue.Count - 1) num = -1;
                num++;                                
            }

            for (int i = num; i < listQueue.Count; i++)
            {
                //если нет элементов в текущей очереди, обнуляем и тем самым переходим к следующей
                if (listQueue[i].Count == 0) curWeight = -1;
                else
                {
                    //если вес не достиг максимального, отправляем пакеты из текущей очереди
                    if (curWeight != weight[i]) return listQueue[i].GetPackege();
                    else curWeight = -1;
                }
            }
            return null;
        }

        public bool NotNULL()
        {
            foreach (Queuering queue in listQueue)
            {
                if (queue.Count != 0) return true;
            }
            return false;
        }
    }
}
