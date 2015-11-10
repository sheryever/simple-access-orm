using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SimpleAccess.Core.Logger
{
    /// <summary>
    /// Default implementaion of ISimpleLoger, It uses the <see cref="Trace"/> to log the <see cref="Exception"/>.
    /// </summary>
    public class SimpleLogger : ISimpleLogger
    {
        /// <summary>
        /// Log the <see cref="Exception"/> using <see cref="Trace"/>.
        /// </summary>
        /// <param name="exception"> <see cref="Exception"/> thrown from the methods</param>
        public void LogException(Exception exception)
        {
            Trace.Write(exception.Message);
        }
    }
}
