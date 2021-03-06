﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;
using QoS.RouterApp;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// CBWFQ+LLQ — Low-Latency Queue
    /// </summary>
    class CBWFQ_LLQ : IAlgorithm
    {        
        private List<Queuering> listQueue;
        /// <summary>
        /// массив весов
        /// </summary>
        private int[] weight;
        /// <summary>

        /// <summary>
        /// CBWFQ+LLQ — Low-Latency Queue. 2 типа очереди. между ними работает PQ, внутри второго CBWFQ
        /// </summary>
        public CBWFQ_LLQ()
        {            
            weight = new int[3];

            int Count = 4;
            listQueue = new List<Queuering>();

            for (int i = 0; i < Count; i++)
            {
                listQueue.Add(new Queuering());
            }

            SetWeight();
        }

        /// <summary>
        /// Установка весов
        /// </summary>
        private void SetWeight()
        {
            //в процентах
            weight = new int[3] { 50, 30, 20 };
        }

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case PHB.DF:
                    listQueue[3].AddPackage(newPackage);
                    break;
                case PHB.AF1:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF2:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF3:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.AF4:
                    listQueue[2].AddPackage(newPackage);
                    break;
                case PHB.EF:
                    listQueue[1].AddPackage(newPackage);
                    break;
                case PHB.CS6:
                    listQueue[0].AddPackage(newPackage);
                    break;
                case PHB.CS7:
                    listQueue[0].AddPackage(newPackage);
                    break;
                default:
                    throw new Exception();
            }
           
        }

        /// <summary>
        /// Вывод в файл
        /// </summary>
        private void PrintToFile()
        {
            SettingFile.PrintToFileListQueuering(listQueue);
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
        public List<Queuering> GetQueueringPackages()
        {
            if (NotNULL())
            {
                return listQueue;
            }
            return null;
        }
        public int CountQueuering()
        {
            return listQueue.Count;
        }
        public Queue<Package> GetPackages(int speed)
        {
            PrintToFile();
            Queue<Package> packages = new Queue<Package>();

            //LQ
            int curSpeed = 0;

            while (listQueue[0].NOTNULL())
            {                
                Package pack = FirstPackage(0);
                curSpeed += pack.Length;
                if (curSpeed <= speed) packages.Enqueue(GetPackage(0));
                else { curSpeed -= pack.Length; break; }
            }

            //CBWFQ
            for (int i = 1; i < listQueue.Count; i++)
            {
                int MaxLength = FindLengthForQueue(i - 1, curSpeed);
                int sum = 0;

                while (listQueue[i].NOTNULL())
                {
                    Package pack = FirstPackage(i);
                    sum += pack.Length;
                    if (sum <= speed) packages.Enqueue(GetPackage(i));
                    else break;
                }
            }

            if (packages.Count != 0)
                return packages;
            else return null;
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
