using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAccess.Core
{
    public interface ISimpleLogger
    {
        void LogException(Exception exception);
    }
}
