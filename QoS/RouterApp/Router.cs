using QoS.AppPackage;
using QoS.Class_of_Service;
using QoS.Class_of_Service.AlgorithmsApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.RouterApp
{
    class Router
    {
        String filename = @"GenerationPackage.txt";
        String fileNameForResultQueue = @"ResultQueue.txt";
        /// <summary>
        /// Генератор пакета
        /// </summary>
        GenPackage genPackage;

        /// <summary>
        /// Алготимы диспетчера
        /// </summary>
        IAlgorithm algorithm;

        /// <summary>
        /// Таймер генератора пакетов
        /// </summary>
        DispatcherTimer timerGenPack;
        
        /// <summary>
        /// Таймер алгоритма
        /// </summary>
        DispatcherTimer timerAlg;       

        Random random = new Random();

        /// <summary>
        /// Маркировка пакетов
        /// </summary>
        Classification classification;

        /// <summary>
        /// Результирующий трафик 
        /// </summary>
        Queue<Package> resultPackage;


        SettingFile settingFile;
        public Router()
        {       
            genPackage = new GenPackage();
            classification = new Classification();
            settingFile = new SettingFile();

            settingFile.DeleteDirectory();
            settingFile.CreateDirectory();

            algorithm = new FIFO();
            resultPackage = new Queue<Package>();

            StartTimerGenPackage();
            StartTimerAlg();
        }    

        /// <summary>
        /// Запуск генератора, обновление : 1 мс
        /// </summary>
        private void StartTimerGenPackage()
        {
            timerGenPack = new DispatcherTimer();
            timerGenPack.Interval = new TimeSpan(0, 0, 0, 0, Setting.Millisecond);
            timerGenPack.Tick += new EventHandler(Addpackage);
            timerGenPack.Tick += new EventHandler(WorkTime);            
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
        /// Запуск генератора, обновление : 1 мс
        /// </summary>
        private void StartTimerAlg()
        {
            timerAlg = new DispatcherTimer();
            timerAlg.Interval = new TimeSpan(0, 0, 0, 0, Setting.Millisecond);
            timerAlg.Tick += new EventHandler(Congestion_Management);           
            timerAlg.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        private void StopTimerGenPackage()
        {
            timerGenPack.Stop();
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

        private void Addpackage(object sender, EventArgs e)
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

            foreach (Package pack in packages)
            {
                resultPackage.Enqueue(pack);
            }

            PrintToFile();
            resultPackage.Clear();
        }

        /// <summary>
        /// Вывод результирующей очереди в файл
        /// </summary>
        private void PrintToFile()
        {
            String path = Setting.Path + "\\" + Setting.Directory + "\\" + fileNameForResultQueue;
            foreach (Package package in resultPackage)
            {
                File.AppendAllText(path, package.ToString() + Environment.NewLine);
            }
        }
    }
}
