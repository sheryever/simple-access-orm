using System;
using System.Collections.Generic;
using System.Data;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleAccess.Core;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Represent the SqlTransactionAsyncContext and the implementation of IDbTransactionAsyncContext
    /// </summary>
    public class SqlTransactionAsyncContext : IDbTransactionAsyncContext<SqlConnection, SqlTransaction>
    {
        public string Name { get; private set; } = "";
        public CancellationToken CancellationToken { get; }
        public SqlConnection Connection { get; private set; }
        internal bool IsConnectionDisposable { get; set; }
        public SqlTransaction Transaction { get; private set; }

        internal SqlTransactionAsyncContext(SqlConnection connection, CancellationToken cancellationToken)
        {
            Connection = connection;
            CancellationToken = cancellationToken;
        }
        public async Task BeginTransactionAsync()
        {
            await Connection.OpenAsync(CancellationToken).ConfigureAwait(false);
            Transaction = Connection.BeginTransaction();
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            await Connection.OpenAsync(CancellationToken).ConfigureAwait(false);
            Transaction = Connection.BeginTransaction(isolationLevel);
        }

        public async Task BeginTransactionAsync(string transactionName)
        {
            Name = transactionName;
            await Connection.OpenAsync(CancellationToken).ConfigureAwait(false);
            Transaction = Connection.BeginTransaction(transactionName);
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel, string transactionName)
        {
            Name = transactionName;
            await Connection.OpenAsync(CancellationToken).ConfigureAwait(false);
            Transaction = Connection.BeginTransaction(isolationLevel, transactionName);
        }

        public void SetConnectionDisposable()
        {
            IsConnectionDisposable = true;
        }

        public void Dispose()
        {
            if (IsConnectionDisposable)
                Connection?.Dispose();

            Transaction?.Dispose();
        }
    }
}
