using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QoS.AppPackage;

namespace QoS.Class_of_Service.AlgorithmsApp
{
    /// <summary>
    /// CBWFQ+LLQ — Low-Latency Queue
    /// </summary>
    class CBWFQ_LLQ : IAlgorithm
    {
        //Проверить правильность!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

        /// <summary>
        /// CBWFQ+LLQ — Low-Latency Queue
        /// </summary>
        public CBWFQ_LLQ()
        {
            listQueue = new List<Queuering>(4);
            weight = new int[3];
            curWeight = 0;
            num = 0;

            for (int i = 0; i < listQueue.Count; i++)
            {
                listQueue[i] = new Queuering();                
            }

            SetWeight();
        }

        /// <summary>
        /// Установка весов
        /// </summary>
        private void SetWeight()
        {
            weight = new int[3] { 12, 10, 5 };
        }

        public void Add(Package newPackage)
        {
            switch (newPackage.CoS)
            {
                case DSCPName.CS0:
                    listQueue[3].AddPackege(newPackage);
                    break;
                case DSCPName.AF1:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF2:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF3:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.AF4:
                    listQueue[2].AddPackege(newPackage);
                    break;
                case DSCPName.EF:
                    listQueue[1].AddPackege(newPackage);
                    break;
                case DSCPName.CS6:
                    listQueue[0].AddPackege(newPackage);
                    break;
                case DSCPName.CS7:
                    listQueue[0].AddPackege(newPackage);
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// 2 типа очереди. между ними работает PQ, внутри второго CBWFQ
        /// </summary>
        /// <returns></returns>
        public Package GetPackage()
        {
            //Если в LQ есть пакеты, отдает их (Алгоритм PQ)
            if (listQueue[0].Count != 0) return listQueue[0].GetPackege();

            //Алгоритм CBWFQ
            if (curWeight == 0)
            {
                //если  дошли до конца очереди, обнуляем
                if (num == listQueue.Count - 1) num = 0;
                num++;
            }
            for (int i = num; i < listQueue.Count; i++)
            {
                //если нет элементов в текущей очереди, обнуляем и тем самым переходим к следующей
                if (listQueue[i].Count == 0) curWeight = 0;
                else
                {
                    //если вес не достиг максимального, отправляем пакеты из текущей очереди
                    if (curWeight != weight[i]) return listQueue[i].GetPackege();
                    else curWeight = 0;
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
