using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.AppPackage
{
    class GenPackage
    {
        List<Package> allPackages;
        GenTypePackage genType;
        DispatcherTimer timerGen;
        Random random;
        public string result = "";

        public GenPackage()
        {
            allPackages = new List<Package>();
            random = new Random();
            genType = new GenTypePackage();
            StartTimer();
        }
        
        private void StartTimer()
        {
            timerGen = new DispatcherTimer();
            timerGen.Interval = new TimeSpan(0,0,0,0,20);
            timerGen.Tick += new EventHandler(Move);
            timerGen.Start();
        }

        private void StopTimer()
        {
            timerGen.Stop();
        }

        //с вероятностью 50%
        private bool GenerationNext()
        {
            return random.NextDouble() <= 0.5;
        }

        private Priority SetPriorityPackage(TypePakage type)
        {
            switch (type)
            {
                case TypePakage.Voice:
                    return Priority.Suprime;                    
                case TypePakage.Critically_important:
                    return Priority.High;
                case TypePakage.Transactions:
                    return Priority.Medium;
                case TypePakage.Non_guaranteed_Delivery:
                    return Priority.Low;
                default:
                    throw new Exception();
            }
        }

        private void genNew(Priority priority)
        {
            Package newPackage = new Package(priority, 500, 1500, 40);
            allPackages.Add(newPackage);
            result += "Add new Package: " + newPackage.ToString();
        }

        private void Move(object sender, EventArgs e)
        {
            if (GenerationNext())
            {
                TypePakage newType = genType.NextType();
                Priority priority = SetPriorityPackage(newType);
                genNew(priority);
            }
        }
    }
}
