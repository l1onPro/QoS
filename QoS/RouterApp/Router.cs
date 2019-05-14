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
        /// <summary>
        /// Генератор пакета
        /// </summary>
        GenPackage genPackage;

        /// <summary>
        /// Таймер генератора пакетов
        /// </summary>
        DispatcherTimer timerGenPack;

        Random random = new Random();

        /// <summary>
        /// Маркировка пакетов
        /// </summary>
        Classification classification;

        public Router()
        {
            DeleteDirectory();
            CreateDirectory();

            genPackage = new GenPackage();
            classification = new Classification();

            StartTimerGenPackage();
        }

        private void DeleteDirectory()
        {
            string dirName = Setting.Path + "\\" + Setting.Directory;

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                dirInfo.Delete(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Setting.Path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(Setting.Directory);

            //FileInfo fileInf = new FileInfo(Setting.Path + Setting.Directory + filename);
            //fileInf.Create();
        }

        /// <summary>
        /// Запуск генератора, обновление : 1 мс
        /// </summary>
        public void StartTimerGenPackage()
        {
            timerGenPack = new DispatcherTimer();
            timerGenPack.Interval = new TimeSpan(0, 0, 0, 0, Setting.Millisecond);
            timerGenPack.Tick += new EventHandler(WorkTime);
            timerGenPack.Tick += new EventHandler(Addpackage);
            timerGenPack.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        public void StopTimerGenPackage()
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
            }
        }
    }
}
