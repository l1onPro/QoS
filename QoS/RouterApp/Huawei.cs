using QoS.AppPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.RouterApp
{
    /// <summary>
    /// На примере Huawei
    /// </summary>
    class Huawei
    {
        public Huawei()
        {

        }
        
        /// <summary>
        /// значения DSCP будут транслироваться в соответствующие им значения Service-Class
        /// </summary>
        /// <param name="DSCP"></param>
        public static PHB GetServiceClass(int DSCP)
        {
            if (DSCP >= 0 && DSCP <= 8) return PHB.DF;
            if (DSCP == 8) return PHB.AF1;
            if (DSCP == 9) return PHB.DF;
            if (DSCP == 10) return PHB.AF1;
            if (DSCP == 11) return PHB.DF;
            if (DSCP == 12) return PHB.AF1;
            if (DSCP == 13) return PHB.DF;
            if (DSCP == 14) return PHB.AF1;
            if (DSCP == 15) return PHB.DF;
            if (DSCP == 16) return PHB.AF2;
            if (DSCP == 17) return PHB.DF;
            if (DSCP == 18) return PHB.AF2;
            if (DSCP == 19) return PHB.DF;
            if (DSCP == 20) return PHB.AF2;
            if (DSCP == 21) return PHB.DF;
            if (DSCP == 22) return PHB.AF2;
            if (DSCP == 23) return PHB.DF;
            if (DSCP == 24) return PHB.AF3;
            if (DSCP == 25) return PHB.DF;
            if (DSCP == 26) return PHB.AF3;
            if (DSCP == 27) return PHB.DF;
            if (DSCP == 28) return PHB.AF3;
            if (DSCP == 29) return PHB.DF;
            if (DSCP == 30) return PHB.AF3;
            if (DSCP == 31) return PHB.DF;
            if (DSCP == 32) return PHB.AF4;
            if (DSCP == 33) return PHB.DF;
            if (DSCP == 34) return PHB.AF4;
            if (DSCP == 35) return PHB.DF;
            if (DSCP == 36) return PHB.AF4;
            if (DSCP == 37) return PHB.DF;
            if (DSCP == 38) return PHB.AF4;
            if (DSCP == 39) return PHB.DF;
            if (DSCP == 40) return PHB.EF;
            if (DSCP >= 41 && DSCP <= 45) return PHB.DF;
            if (DSCP == 46) return PHB.EF;
            if (DSCP == 47) return PHB.DF;
            if (DSCP == 48) return PHB.CS6;
            if (DSCP >= 49 && DSCP <= 55) return PHB.DF;
            if (DSCP == 56) return PHB.CS7;
            if (DSCP >= 57 && DSCP <= 63) return PHB.DF;

            throw new Exception();
        }

        /// <summary>
        /// значения DSCP будут транслироваться в соответствующие им Color
        /// </summary>
        /// <param name="DSCP"></param>
        public static GradColor GetColor(int DSCP)
        {
            if (DSCP >= 0 && DSCP <= 11) return GradColor.green;
            if (DSCP == 12) return GradColor.yellow;
            if (DSCP == 13) return GradColor.green;
            if (DSCP == 14) return GradColor.red;
            if (DSCP >= 15 && DSCP <= 19) return GradColor.green;
            if (DSCP == 20) return GradColor.yellow;
            if (DSCP == 21) return GradColor.green;
            if (DSCP == 22) return GradColor.red;
            if (DSCP >= 23 && DSCP <= 27) return GradColor.green;
            if (DSCP == 28) return GradColor.yellow;
            if (DSCP == 29) return GradColor.green;
            if (DSCP == 30) return GradColor.red;
            if (DSCP >= 31 && DSCP <= 35) return GradColor.green;
            if (DSCP == 36) return GradColor.yellow;
            if (DSCP == 37) return GradColor.green;
            if (DSCP == 38) return GradColor.red;
            if (DSCP >= 39 && DSCP <= 63) return GradColor.green;

            throw new Exception();
        }

        /// <summary>
        /// Маркировка пакетов на выходе
        /// </summary>
        /// <param name="secviceClass"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int getDSCP(PHB secviceClass, GradColor color)
        {
            switch (secviceClass)
            {
                case PHB.DF:
                    return 0;                    
                case PHB.AF1:
                    switch (color)
                    {
                        case GradColor.red:
                            return 14;                            
                        case GradColor.yellow:
                            return 12;
                        case GradColor.green:
                            return 10;
                        default:
                            throw new Exception();
                    }                    
                case PHB.AF2:
                    switch (color)
                    {
                        case GradColor.red:
                            return 22;
                        case GradColor.yellow:
                            return 20;
                        case GradColor.green:
                            return 18;
                        default:
                            throw new Exception();
                    }
                case PHB.AF3:
                    switch (color)
                    {
                        case GradColor.red:
                            return 30;
                        case GradColor.yellow:
                            return 28;
                        case GradColor.green:
                            return 26;
                        default:
                            throw new Exception();
                    }
                case PHB.AF4:
                    switch (color)
                    {
                        case GradColor.red:
                            return 38;
                        case GradColor.yellow:
                            return 36;
                        case GradColor.green:
                            return 34;
                        default:
                            throw new Exception();
                    }
                case PHB.EF:
                    return 46;
                case PHB.CS6:
                    return 48;
                case PHB.CS7:
                    return 56;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Маркировка пакетов
        /// </summary>
        /// <param name="secviceClass"></param>
        /// <returns></returns>
        public static int GetIPPrecedence(PHB secviceClass)
        {
            switch (secviceClass)
            {
                case PHB.DF:
                    return 0;
                case PHB.AF1:
                    return 1; 
                case PHB.AF2:
                    return 2;
                case PHB.AF3:
                    return 3;
                case PHB.AF4:
                    return 4;
                case PHB.EF:
                    return 5;
                case PHB.CS6:
                    return 6;
                case PHB.CS7:
                    return 7;
                default:
                    throw new Exception(); 
            }
        }

        /// <summary>
        /// Маркировка пакета на входе по ip
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        public static PHB GetServiceClassByIp(int Ip)
        {
            if (Ip == 0) return PHB.DF;
            if (Ip == 1) return PHB.AF1;
            if (Ip == 2) return PHB.AF2;
            if (Ip == 3) return PHB.AF3;
            if (Ip == 4) return PHB.AF4;
            if (Ip == 5) return PHB.EF;
            if (Ip == 6) return PHB.CS6;
            if (Ip == 7) return PHB.CS7;

            throw new Exception();
        }

        /// <summary>
        /// Маркировка пакетов по цвету по ip
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        public static GradColor GetColorByIp(int Ip)
        {
            return GradColor.green;
        }
    }

    
}
