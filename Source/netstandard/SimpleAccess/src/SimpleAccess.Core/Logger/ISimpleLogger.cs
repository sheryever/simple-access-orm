using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAccess.Core.Logger
{
    /// <summary>
    /// Represent the default methods required by SimpleAccess to log the Exception
    /// </summary>
    public interface ISimpleLogger
    {
        /// <summary>
        /// Log the exception when the any exception occur in SimpleAccess
        /// </summary>
        /// <param name="exception"></param>
        void LogException(Exception exception);
    }
}

