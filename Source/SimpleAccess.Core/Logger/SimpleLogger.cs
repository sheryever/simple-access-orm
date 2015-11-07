using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SimpleAccess.Core.Logger
{
    public class SimpleLogger : ISimpleLogger
    {
        public void LogException(Exception exception)
        {
            Trace.Write(exception.Message);
        }
    }
}
