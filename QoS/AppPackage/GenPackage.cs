using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.AppPackage
{
    /// <summary>
    /// генерератор пакетов
    /// </summary>
    class GenPackage
    {
        /// <summary>
        /// получение всех сгенерированных пакетов
        /// </summary>
        List<Package> allPackages;
        GenTypePackage genType;
        DispatcherTimer timerGen;
        Random random;
        public string result = "";

        /// <summary>
        /// после того, как получили пакеты, список обнуляется
        /// </summary>
        /// <returns></returns>
        public List<Package> getAllPackage()
        {
            List<Package> getList = allPackages;
            allPackages.Clear();
            return allPackages;
        }

        public GenPackage()
        {
            allPackages = new List<Package>();
            random = new Random();
            genType = new GenTypePackage();
            StartTimer();
        }     
        
        /// <summary>
        /// Запуск генератора
        /// </summary>
        public void StartTimer()
        {
            timerGen = new DispatcherTimer();
            timerGen.Interval = new TimeSpan(0,0,0,0,20);
            timerGen.Tick += new EventHandler(Move);
            timerGen.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        public void StopTimer()
        {
            timerGen.Stop();
        }

        //с вероятностью 50%
        private bool GenerationNext()
        {
            return random.NextDouble() <= 0.5;
        }

        /// <summary>
        /// разделение пакетов на модели
        /// </summary>
        /// <param name="type">Тип пакета</param>
        /// <returns></returns>
        private DSCPName SetPHBForPackage(ServiceClassName type)
        {
            switch (type)
            {
                case ServiceClassName.Network_Control_2:
                    return DSCPName.CS7;
                case ServiceClassName.Network_Control_1:
                    return DSCPName.CS6;
                case ServiceClassName.Telephony:
                    return DSCPName.EF;
                case ServiceClassName.Multemedia_Conferencing:
                    return DSCPName.AF4;
                case ServiceClassName.Multemedia_Streaming:
                    return DSCPName.AF3;
                case ServiceClassName.LowLatency_Data:
                    return DSCPName.AF2;
                case ServiceClassName.HightThroughput_Data:
                    return DSCPName.AF1;
                case ServiceClassName.Standart:
                    return DSCPName.CS0;                    
                default:
                    throw new Exception();
            }            
        }

        private void addNew()
        {
            //создается новый рандомный пакет
            ServiceClassName newPackege = genType.NextType();
            //Задается маркером
            DSCPName CoS = SetPHBForPackage(newPackege);
            //Создается пакет
            Package newPackage = new Package(CoS, random.Next(5, 100));
            //добавляется ко всем
            allPackages.Add(newPackage);
            result += "New package: " + newPackage.ToString();
        }

        private void Move(object sender, EventArgs e)
        {
            if (GenerationNext())
            {                               
                addNew();
            }
        }
    }
}
