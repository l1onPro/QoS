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
        List<Package> allPackages { get; set; }
        GenTypePackage genType;
        DispatcherTimer timerGen;
        Random random;
        public string result = "";

        public GenPackage()
        {
            allPackages = new List<Package>();
            random = new Random();
            genType = new GenTypePackage();            
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
        private ModelPackage SetModelPackage(TypePakage type)
        {
            switch (type)
            {
                case TypePakage.Voice:
                    return ModelPackage.DiffServ;                 
                case TypePakage.Critically_important:
                    return ModelPackage.IntServ;
                case TypePakage.Transactions:
                    return ModelPackage.IntServ;
                case TypePakage.Non_guaranteed_Delivery:
                    return ModelPackage.Best_Effort;
                default:
                    throw new Exception();
            }
        }

        private void genNew(ModelPackage model)
        {
            Package newPackage = new Package(500, 1500, 40);
            newPackage.model = model;
            allPackages.Add(newPackage);
            result += "Add new Package: " + newPackage.ToString();
        }

        private void Move(object sender, EventArgs e)
        {
            if (GenerationNext())
            {
                TypePakage newType = genType.NextType();
                ModelPackage priority = SetModelPackage(newType);
                genNew(priority);
            }
        }
    }
}
