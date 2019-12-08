using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;

namespace SimpleAccess.Core
{
#if !NET40
    /// <summary>
    /// Represent the interface of SimpleAccess methods and it's implemented by SimpleAccess 
    /// </summary>
    public interface IDbTransactionAsyncContext<TDbConnection, TDbTransaction> : IDisposable
        where TDbConnection : IDbConnection
        where TDbTransaction: IDbTransaction
    {
        string Name { get; }
        TDbConnection Connection { get; }
        TDbTransaction Transaction { get; }

        void SetConnectionDisposable();
    }
#endif
}
