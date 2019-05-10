using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    /// <summary>
    /// Тип рандомного пакета
    /// </summary>
    enum TypePakage
    {
        /// <summary>
        /// голос
        /// </summary>
        Voice,

        /// <summary>
        /// критически важный 
        /// </summary>
        Critically_important,   
        
        /// <summary>
        /// Транзакции
        /// </summary>
        Transactions,

        /// <summary>
        /// Не гарантированная доставка
        /// </summary>
        Non_guaranteed_Delivery
    }
}
