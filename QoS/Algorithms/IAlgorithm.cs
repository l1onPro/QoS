﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.Algorithms
{
    interface IAlgorithm
    {
        bool AddPacket(BaseClasses.Packet p);
        void ProcessingPacket();
        BaseClasses.Packet GetPacket();
        bool IsEmpty();
    }
}