﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// Deficit Weighted Round Robin
    /// </summary>
    class DWRR : IAlgorithm
    {
        /// <summary>
        /// Список очередей
        /// </summary>
        private List<Queuering> listQueue;

        /// <summary>
        /// 
        /// </summary>
        private int[] kvant;

        private int[] kreditResidue;

        /// <summary>
        /// Deficit Weighted Round Robin
        /// </summary>
        public DWRR()
        {
            listQueue = new List<Queuering>(4);

            for (int i = 0; i < listQueue.Count; i++)
            {
                listQueue[i] = new Queuering();
            }

            SetKvantForAllQueue();
            SetResidueKredit();
        }

        /// <summary>
        /// Установить кванты для всех очередей.
        /// </summary>
        private void SetKvantForAllQueue()
        {
            kvant = new int[4] { 1600, 1400, 1200, 1000 };           
        }

        /// <summary>
        /// Обнулять остаток для кредитных линий
        /// </summary>
        private void SetResidueKredit()
        {
            kreditResidue = new int[4] { 0, 0, 0, 0 };
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

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case DSCPName.CS0:
                    listQueue[3].AddPackage(newPackage);
                    break;
                case DSCPName.AF1:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.AF2:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.AF3:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.AF4:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case DSCPName.EF:
                    listQueue[1].AddPackage(newPackage);
                    break;
                case DSCPName.CS6:
                    listQueue[0].AddPackage(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackage(newPackage);
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Добавить квант в каждую очередь
        /// </summary>
        private void AddKvantForKreditLine()
        {
            for (int i = 0; i < listQueue.Count; i++)
            {
                kreditResidue[i] += kvant[i];
            }
        }

        public Queue<Package> GetPackages(int speed)
        {
            Queue<Package> packages = new Queue<Package>();

            AddKvantForKreditLine();

            for (int i = 0; i < listQueue.Count; i++)
            {
                //пока есть пакеты в очереди
                while (listQueue[i].NOTNULL())
                {
                    Package pack = FirstPackage(i);

                    //Пока хватает кредита 
                    if (kreditResidue[i] - pack.Length >= 0)
                    {
                        packages.Enqueue(GetPackage(i));
                        kreditResidue[i] -= pack.Length;
                    }                    
                    else break;
                }
            }

            return packages;
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