using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SimpleAccess.Core.Logger
{

    //TODO: Implement using ILogger and ILoggerFactory
    /// <summary>
    /// Default implementation of ISimpleLoger, It uses the <see cref="Debug"/> to log the <see cref="Exception"/>.
    /// </summary>
    public class SimpleLogger : ISimpleLogger
    {
        /// <summary>
        /// Log the <see cref="Exception"/> using <see cref="Debug"/>.
        /// </summary>
        /// <param name="exception"> <see cref="Exception"/> thrown from the methods</param>
        public void LogException(Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }
    }
}
