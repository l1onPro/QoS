using QoS.Class_of_Service;
using QoS.RouterApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.AppPackage
{
    /// <summary>
    /// Генерератор пакетов
    /// </summary>
    class GenPackage
    {  
        Random random; 

        public GenPackage()
        {            
            random = new Random();            
        }

        /// <summary>
        /// Установить размер пакета по цвету
        /// </summary>
        /// <param name="color">Зеленый, желтый, красный</param>
        /// <returns></returns>
        public int GetLength(GradColor color)
        {
            switch (color)
            {
                case GradColor.red:
                    return random.Next(1001, 1500);
                case GradColor.yellow:
                    return random.Next(501, 1000);
                case GradColor.green:
                    return random.Next(100, 500);
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Получить рандомный DSCP
        /// </summary>
        /// <returns></returns>
        private int GetDSCP()
        {
            return random.Next(64);
        }

        /// <summary>
        /// Cоздается рандомный новый пакет
        /// </summary>
        /// <returns></returns>
        public Package New()
        {            
            int DSCP = GetDSCP();

            GradColor color = Huawei.GetColor(DSCP);

            int length = GetLength(color);

            //Создается пакет
            return New(DSCP, length);  
        }

        /// <summary>
        /// Cоздается новый пакет
        /// </summary>
        /// <param name="DSCP">id DSCP</param>
        /// <param name="length">Длина</param>
        /// <returns></returns>
        public Package New(int DSCP, int length)
        { 
            //Создается пакет
            return new Package(DSCP, length);
        }
    }
}
