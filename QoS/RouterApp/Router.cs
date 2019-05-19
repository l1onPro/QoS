using QoS.AppPackage;
using QoS.Class_of_Service;
using QoS.Class_of_Service.AlgorithmsApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace QoS.RouterApp
{
    class Router
    {        
        String filename = @"GenerationPackage.txt";
        String fileNameForResultQueue = @"ResultQueue.txt";

        MyGraphica Graphica;
        Mutex mtx = new Mutex();
        /// <summary>
        /// частота обновление сек
        /// </summary>
        readonly int frequencyUpdate = 1;

        /// <summary>
        /// Таймер генератора пакетов
        /// </summary>
        DispatcherTimer timerGenPack;

        /// <summary>
        /// Таймер алгоритма
        /// </summary>
        DispatcherTimer timerAlg;       

        /// <summary>
        /// Генератор пакета
        /// </summary>
        GenPackage genPackage;

        /// <summary>
        /// Алготимы диспетчера
        /// </summary>
        IAlgorithm algorithm;    

        Random random = new Random();

        /// <summary>
        /// Маркировка пакетов
        /// </summary>
        Classification classification;

        /// <summary>
        /// Результирующий трафик 
        /// </summary>
        Queuering resultPackage;

        SettingFile settingFile;

        public Router(int numAlgorithm, Canvas paint)
        {
            genPackage = new GenPackage();
            classification = new Classification();

            settingFile = new SettingFile();
            Graphica = new MyGraphica(paint);

            resultPackage = new Queuering();
            SetAlg(numAlgorithm);
        }

        public Router()
        {
            genPackage = new GenPackage();
            classification = new Classification();

            settingFile = new SettingFile();

            resultPackage = new Queuering();
            algorithm = new PQ(false);            
        }

        private void SetAlg(int num)
        {
            if (num == 0) algorithm = new FIFO();
            else if (num == 1) algorithm = new PQ(false);
            else if (num == 2) algorithm = new PQ(true);
            else if (num == 3) algorithm = new WFQ();
            else if (num == 4) algorithm = new CBWFQ();
            else if (num == 5) algorithm = new CBWFQ_LLQ();
            else if (num == 6) algorithm = new DWRR();
            else throw new Exception();            
        }

        //с вероятностью 50%
        private bool GenerationNext()
        {
            return random.NextDouble() <= 0.5;
        }

        private void WorkTime(object sender, EventArgs e)
        {
            Setting.TimeWork += 1;
        }

        public void Addpackage(object sender, EventArgs e)
        {
            if (GenerationNext())
            {
                //получили пакет
                Package package = genPackage.New();

                //отправили на маркировку
                classification.ClassificationPackage(package);

                String path = Setting.Path + "\\" + Setting.Directory + "\\" + filename;

                //вывели в файл инфу
                File.AppendAllText(path, package.ToString() + Environment.NewLine);
                //Нарисовали

                //добавить пакет в алгоритм
                algorithm.Add(package);
            }
        }

        /// <summary>
        /// Управление перегрузками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Congestion_Management(object sender, EventArgs e)
        {
            Queue<Package> packages = algorithm.GetPackages(Setting.Speed * 1000000);

            PrintToFile(packages);

            SetResultQueue(packages);
        }

        /// <summary>
        /// Сформировать последнюю часть результирующей очереди
        /// </summary>
        /// <param name="packages">Пакеты, полученные из алгоритма</param>
        private void SetResultQueue(Queue<Package> packages)
        {
            foreach (Package pack in packages)
            {
                //если нет места
                GetPlace(pack.Length);

                //добавить пакет
                resultPackage.AddPackage(pack, true);
            }
        }

        /// <summary>
        /// Очищает очередь для пакета
        /// </summary>
        /// <returns></returns>
        private bool GetPlace(int length)
        {            
            while (resultPackage.TailDrop(length))
            {
                //очистить нужное место
                resultPackage.GetPackage();
            }
            return true;
        }

        /// <summary>
        /// Вывод получившей очереди из алгоритма в файл
        /// </summary>
        private void PrintToFile(Queue<Package> resultPackage)
        {
            String path = Setting.Path + "\\" + Setting.Directory + "\\" + fileNameForResultQueue;
            foreach (Package package in resultPackage)
            {
                File.AppendAllText(path, package.ToString() + Environment.NewLine);
            }
        }       

        public List<Queuering> GetQueueringPackages()
        {
            List<Queuering> queuerings = algorithm.GetQueueringPackages();
            if (queuerings != null)
            {
                return queuerings;
            }
            return null;
        }

        /// <summary>
        /// Запуск генератора пакетов, обновление : 1 мс
        /// </summary>
        private void StartTimerGenPackage()
        {
            timerGenPack = new DispatcherTimer();
            //за 1 сек - 3 пакета
            timerGenPack.Interval = new TimeSpan(0, 0, 0, 0, frequencyUpdate * 1000 / 5);
            timerGenPack.Tick += new EventHandler(Addpackage);
            timerGenPack.Tick += new EventHandler(UpdatePicter);
            timerGenPack.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        private void StopTimerAlg()
        {
            timerAlg.Stop();
        }

        /// <summary>
        /// Запуск алгоритма, обновление : 1 мс
        /// </summary>
        private void StartTimerAlg()
        {
            timerAlg = new DispatcherTimer();
            timerAlg.Interval = new TimeSpan(0, 0, 0, frequencyUpdate);
            timerAlg.Tick += new EventHandler(Congestion_Management);
            timerAlg.Tick += new EventHandler(UpdatePicter);
            //timerAlg.Tick += new EventHandler(WorkTime);
            timerAlg.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        private void StopTimerGenPackage()
        {
            timerGenPack.Stop();
        }

        /// <summary>
        /// Запуск таймеров
        /// </summary>
        public void Start()
        {          
            StartTimerGenPackage();
            StartTimerAlg();
        }

        /// <summary>
        /// Остановка таймеров
        /// </summary>
        public void Stop()
        {
            StopTimerGenPackage();
            StopTimerAlg();
        }

        /// <summary>
        /// Обновление экрана отрисовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdatePicter(object sender, EventArgs e)
        {
            mtx.WaitOne();

            Graphica.Clear();

            //Начертить очереди
            int count = algorithm.CountQueuering();
            Graphica.PaintLineQueues(count);

            //Начертить содержимое очередей
            List<Queuering> list = algorithm.GetQueueringPackages();
            if (list != null)
                Graphica.PaintQueues(list);

            //Начертить содержимое результирующей очереди
            if (resultPackage != null)
                Graphica.PaintResultQueue(resultPackage.GetAllPackages());
            
            mtx.ReleaseMutex();
        }      
    }
}
