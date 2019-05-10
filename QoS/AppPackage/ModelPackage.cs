using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    enum ModelPackage
    {
        Best_Effort,    //никакой гарантии качества. Все равны.
        IntServ,        //гарантия качества для каждого потока. Резервирование ресурсов от источника до получателя.
        DiffServ        //нет никакого резервирования. Каждый узел сам определяет, как обеспечить нужное качество.
    }
}
