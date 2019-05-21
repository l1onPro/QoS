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
        MyGraphica Graphica;
        Mutex mtx = new Mutex();      

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
        /// Результирующий трафик 
        /// </summary>
        Queuering resultPackage;
        /// <summary>
        /// Генерирующийся трафик 
        /// </summary>
        Queuering StartPackage;

        SettingFile settingFile;

        /// <summary>
        /// Указывает, что запускаем пример
        /// </summary>
        public bool ItExample { get; set; } = false;

        /// <summary>
        /// Имитация роутера
        /// </summary>
        /// <param name="numAlgorithm">Номер алгоритма</param>
        /// <param name="paint">Место отрисовки</param>
        public Router(int numAlgorithm, Canvas paint)
        {
            genPackage = new GenPackage(); 
            settingFile = new SettingFile();
            Graphica = new MyGraphica(paint);

            resultPackage = new Queuering(Setting.MaxConstSizeQueuering);
            StartPackage = new Queuering(Setting.MaxConstSizeQueuering);
            SetAlg(numAlgorithm);
        }

        /// <summary>
        /// Имитация роутера
        /// </summary>
        /// <param name="paint">Место отрисовки</param>
        public Router(Canvas paint)
        {
            genPackage = new GenPackage(); 
            settingFile = new SettingFile();
            Graphica = new MyGraphica(paint);

            resultPackage = new Queuering(Setting.MaxConstSizeQueuering);
            StartPackage = new Queuering(Setting.MaxConstSizeQueuering);
            algorithm = new PQ(false);            
        }

        /// <summary>
        /// Установить алгоритм
        /// </summary>
        /// <param name="num"></param>
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

        /// <summary>
        /// Имитация работы программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTime(object sender, EventArgs e)
        {
            Setting.TimeWork += 1;
        }

        /// <summary>
        /// Сформировать стартовую очередь
        /// </summary>
        /// <param name="packages">Сгенерированные пакеты</param>
        private void SetStartQueue(Package package)
        {
            //если нет места
            GetPlace(package.Length, StartPackage);

            //добавить пакет
            StartPackage.AddPackage(package, true);
        }

        public void Addpackage(object sender, EventArgs e)
        {
            //если тест, то заполнять готовыми пакетами
            if (ItExample)
            {

            }
            else if (GenerationNext())
            {
                //получили пакет
                Package package = genPackage.New();

                //отправили на маркировку
                Classification.ClassificationPackage(package);

                //вывели в файл инфу
                SettingFile.PrintToFile(SettingFile.pathForGenerationPackage, package.ToString());

                SetStartQueue(package);

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
            Queue<Package> packages = algorithm.GetPackages(Setting.CurSpeed);

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
                GetPlace(pack.Length, resultPackage);

                //добавить пакет
                resultPackage.AddPackage(pack, true);
            }
        }

        /// <summary>
        /// Очищает очередь для пакета
        /// </summary>
        /// <returns></returns>
        private bool GetPlace(int length, Queuering Packages)
        {            
            while (Packages.TailDrop(length))
            {
                //очистить нужное место
                Packages.GetPackage();
            }
            return true;
        }

        /// <summary>
        /// Вывод получившей очереди из алгоритма в файл
        /// </summary>
        private void PrintToFile(Queue<Package> resultPackage)
        {           
            foreach (Package package in resultPackage)
            {
                SettingFile.PrintToFile(SettingFile.pathForQueuerings, package.ToString());               
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
            timerGenPack.Interval = new TimeSpan(0, 0, 0, 0, Setting.frequencyUpdate * 1000 / Setting.TypeFrequencyGenPack);
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
            timerAlg.Interval = new TimeSpan(0, 0, 0, Setting.frequencyUpdate);
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

            //Начертить границы очередей
            int count = algorithm.CountQueuering();
            Graphica.PaintLineQueues(count);

            //начертить начальную очередь
            if (StartPackage.NOTNULL())
                Graphica.PaintStartQueue(StartPackage.GetAllPackages());

            //Начертить содержимое очередей
            List<Queuering> list = algorithm.GetQueueringPackages();
            if (list != null)
                Graphica.PaintQueues(list);

            //Начертить содержимое результирующей очереди
            if (resultPackage.NOTNULL())
                Graphica.PaintResultQueue(resultPackage.GetAllPackages());
            
            mtx.ReleaseMutex();
        }      
    }
}
