using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleAccess.Core;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Represent the SQLiteTransactionAsyncContext and the implementation of IDbTransactionAsyncContext
    /// </summary>
    public class SQLiteTransactionAsyncContext : IDbTransactionAsyncContext<SQLiteConnection, SQLiteTransaction>
    {
        public string Name 
        { 
            get
            {
                throw new NotSupportedException("SQLiteTransaction with transaction name are not support");
            }
            private set {
                throw new NotSupportedException("SQLiteTransaction with transaction name are not support");
            }
        } 
        public CancellationToken CancellationToken { get; }
        public SQLiteConnection Connection { get; private set; }
        internal bool IsConnectionDisposable { get; set; }
        public SQLiteTransaction Transaction { get; private set; }

        internal SQLiteTransactionAsyncContext(SQLiteConnection connection, CancellationToken cancellationToken)
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
            throw new NotSupportedException("SQLiteTransaction with transaction name are not support");

            //Name = transactionName;
            //await Connection.OpenAsync(CancellationToken).ConfigureAwait(false);
            //Transaction = Connection.BeginTransaction(transactionName);
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel, string transactionName)
        {
            throw new NotSupportedException("SQLiteTransaction with transaction name are not support");

            //Name = transactionName;
            //await Connection.OpenAsync(CancellationToken).ConfigureAwait(false);
            //Transaction = Connection.BeginTransaction(isolationLevel, transactionName);
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
