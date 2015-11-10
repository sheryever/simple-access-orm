using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess
{
    /// <summary>
    /// Defines the DbParameters type to be generated
    /// </summary>
    public enum ParametersType
    {
        /// <summary>
        /// Uses for Insert parameters type, Specially for out parameters
        /// </summary>
        Insert,
        /// <summary>
        /// Uses for Update parameters type. Just normal parameters
        /// </summary>
        Update
    }
}
