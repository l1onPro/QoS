using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.AppPackage
{
    /// <summary>
    /// Per-Hop Behavior
    /// </summary>
    enum PHB
    {
        /// <summary>
        /// Default Forwardin
        /// </summary>
        DF,     //или BE, или CS0

        /// <summary>
        /// Assured Forwarding
        /// </summary>
        AF1,

        /// <summary>
        /// Assured Forwarding
        /// </summary>
        AF2,

        /// <summary>
        /// Assured Forwarding
        /// </summary>
        AF3,

        /// <summary>
        /// Assured Forwarding
        /// </summary>
        AF4,

        /// <summary>
        /// Expedited Forwarding
        /// </summary>
        EF,

        /// <summary>
        /// Class Selector
        /// </summary>
        CS6,

        /// <summary>
        /// Class Selector
        /// </summary>
        CS7
    }   
}
