using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.RouterApp
{
    class GenerationPackage
    {
        GenPackage genPackage;
        /// <summary>
        /// Таймер генератора пакетов
        /// </summary>
        DispatcherTimer timerGenPack;

        /// <summary>
        /// Сгенерировался ли пакет
        /// </summary>
        bool gen;
        Random random;

        Package package;

        public GenerationPackage()
        {
            genPackage = new GenPackage();
            random = new Random();
        }

        /// <summary>
        /// Запуск генератора
        /// </summary>
        public void StartTimer()
        {
            timerGenPack = new DispatcherTimer();
            timerGenPack.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timerGenPack.Tick += new EventHandler(Move);
            timerGenPack.Start();
        }

        /// <summary>
        /// остановка генератора
        /// </summary>
        public void StopTimer()
        {
            timerGenPack.Stop();
        }

        //с вероятностью 50%
        private bool GenerationNext()
        {
            return random.NextDouble() <= 0.5;
        }

        private GradColor GetColor()
        {
            int k = random.Next(3);
            switch (k)
            {
                case 0:
                    return GradColor.green;
                case 1:
                    return GradColor.yellow;
                case 2:
                    return GradColor.red;
                default:
                    throw new Exception();
            }
        }

        public void CheckTypePackage()
        {
            if (package.CoS == DSCPName.AF1
                || package.CoS == DSCPName.AF2
                || package.CoS == DSCPName.AF3
                || package.CoS == DSCPName.AF4)
                package.color = GetColor();
            else
                package.color = GradColor.green;
        }

        private void Move(object sender, EventArgs e)
        {
            if (GenerationNext())
            {
                package = genPackage.New();
                CheckTypePackage();
                gen = true;
            }
        }

        public Package GetPackage()
        {
            gen = false;
            return package;
        }
    }
}
