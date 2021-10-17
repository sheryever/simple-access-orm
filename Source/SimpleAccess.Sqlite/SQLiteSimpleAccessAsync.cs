using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Dynamic;
using System.Reflection;
using System.Threading;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Sql Server implementation for SimpleAccess.
    /// </summary>
    public partial class SQLiteSimpleAccess : ISQLiteSimpleAccess
    {
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteNonQueryAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }


        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(string commandText, object paramObject = null)
        {
            return ExecuteNonQueryAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType,
            object paramObject = null)
        {
            return ExecuteNonQueryAsync(commandText, commandType, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType,
            params SQLiteParameter[] SQLiteParameters)
        {
            int result;
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                result = await dbCommand.ExecuteNonQueryAsync(cancellationToken);
                dbCommand.Parameters.Clear();

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
            return result;
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. 
        /// </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteNonQueryAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null)
        {
            return ExecuteNonQueryAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(SQLiteTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteNonQueryAsync(transactionContext, commandText, commandType, BuildDbParameters(paramObject));

        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> ExecuteNonQueryAsync(SQLiteTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, params SQLiteParameter[] parameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, parameters);

                return await dbCommand.ExecuteNonQueryAsync(transactionContext.CancellationToken);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                dbCommand?.Parameters.Clear();

            }

        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public Task<T> ExecuteScalarAsync<T>(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteScalarAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(string commandText, object paramObject = null)
        {
            return ExecuteScalarAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteScalarAsync<T>(commandText, commandType
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public async Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;


                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);


                var result = await dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteScalarAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null)
        {
            return ExecuteScalarAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText,
                    CommandType commandType, object paramObject = null)
        {
            return ExecuteScalarAsync<T>(transactionContext, commandText, commandType
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public async Task<T> ExecuteScalarAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {

                dbCommand = CreateCommand(transactionContext, commandText, commandType, SQLiteParameters);

                var result = await dbCommand.ExecuteScalarAsync(transactionContext.CancellationToken).ConfigureAwait(false);

                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteReaderAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, CommandType commandType,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteReaderAsync(commandText, commandType, CommandBehavior.CloseConnection, SQLiteParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteReaderAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, commandBehavior, SQLiteParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, commandType, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, CommandBehavior commandBehavior = CommandBehavior.CloseConnection, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, commandBehavior, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, CommandBehavior commandBehavior = CommandBehavior.CloseConnection, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, commandType, commandBehavior, BuildDbParameters(paramObject));
        }


        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public async Task<SQLiteDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SQLiteParameter[] SQLiteParameters)
        {
            try
            {
                var dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;


                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                var result = await dbCommand.ExecuteReaderAsync(commandBehavior, cancellationToken);

                dbCommand.Parameters.Clear();
                return (SQLiteDataReader) result;
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }


        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteValuesAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }


        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, object paramObject = null)
        {
            return ExecuteValuesAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValuesAsync<T>(commandText, commandType, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public async Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                using (var reader = await dbCommand.ExecuteReaderAsync(cancellationToken))
                {
                    return GetValues<T>((SQLiteDataReader)reader);
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> value </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null)
        {
            return ExecuteValuesAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> value </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValuesAsync<T>(transactionContext, commandText, commandType, BuildDbParameters(paramObject));
        }
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText,
                                             params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteValuesAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public async Task<IEnumerable<T>> ExecuteValuesAsync<T>(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {

                dbCommand = CreateCommand(transactionContext, commandText, commandType, SQLiteParameters);

                using (var reader = await dbCommand.ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false))
                {
                    return GetValues<T>((SQLiteDataReader)reader);
                }

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();

                    dbCommand.ClearDbCommand();
                }

            }
        }


        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TEntity value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters)
            where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The {TEntity} value </returns>

        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, CommandType commandType, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(commandText, commandType, fieldsToSkip
                , propertyInfoDictionary, BuildDbParameters(paramObject));
        }


        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public async Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SQLiteParameter[] SQLiteParameters) where TEntity : new()
        {
            SQLiteCommand dbCommand = null;
            try
            {

                dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                using (var reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                {
                    return await reader.DataReaderToObjectListAsync<TEntity>(fieldsToSkip, propertyInfoDictionary);
                }

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();

                    dbCommand.ClearDbCommand();
                }

            }
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters)
            where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, commandType
                , fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public async Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters) where TEntity : new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, SQLiteParameters);

                using (var reader = await dbCommand
                    .ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false))
                {
                    return await reader.DataReaderToObjectListAsync<TEntity>(fieldsToSkip, propertyInfoDictionary);
                }

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.ClearDbCommand();
                }

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, object paramObject = null, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, CommandType commandType, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public async Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                using (var reader = await dbCommand
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken).ConfigureAwait(false))
                {
                    return reader.HasRows ? reader.DataReaderToObject<TEntity>(fieldsToSkip, propertyInfoDictionary) : null;
                }

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.ClearDbCommand();
                }

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(transactionContext, commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public async Task<TEntity> ExecuteEntityAsync<TEntity>(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, SQLiteParameters);

                using (var reader = await dbCommand.ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false))
                {
                    return reader.HasRows ? reader.DataReaderToObject<TEntity>(fieldsToSkip, propertyInfoDictionary) : null;
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {

                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.ClearDbCommand();
                }

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamicsAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        ///  
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///  
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        ///  
        ///  <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicsAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        ///  
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///  
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        ///  
        ///  <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, CommandType commandType, object paramObject = null,
            string fieldsToSkip = null)
        {
            return ExecuteDynamicsAsync(commandText, commandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public async Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;

            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                return await GetDynamicSqlDataAsync((SQLiteDataReader) await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false));

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                dbCommand.ClearDbCommand();

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamicsAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null,
            string fieldsToSkip = null)
        {
            return ExecuteDynamicsAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicsAsync(transactionContext, commandText, commandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }



        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public async Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, SQLiteParameters);

                return await GetDynamicSqlDataAsync((SQLiteDataReader) await dbCommand
                    .ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(string commandText, string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamicAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(string commandText, CommandType commandType, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicAsync(commandText, commandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public async Task<dynamic> ExecuteDynamicAsync(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                var reader = await dbCommand.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken).ConfigureAwait(false);
                if (await reader.ReadAsync())
                {
                    var result = SQLiteDataReaderToExpando((SQLiteDataReader) reader);
                    reader.Close();
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection.State != ConnectionState.Closed)
                    dbCommand.Connection.CloseSafely();

                dbCommand.ClearDbCommand();

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamicAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// -<param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
                object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicAsync(transactionContext, commandText, commandType,
                fieldsToSkip, BuildDbParameters(paramObject));
        }


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public async Task<dynamic> ExecuteDynamicAsync(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            SQLiteDataReader reader = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, SQLiteParameters);

                reader = (SQLiteDataReader) await dbCommand.ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false);
                if (await reader.ReadAsync())
                    return SQLiteDataReaderToExpando(reader);

                return null;
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                reader?.Close();
                dbCommand.ClearDbCommand();

            }
        }

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqliteParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SQLiteCommand CreateCommandForAsync(string commandText, CommandType commandType, params SQLiteParameter[] sqliteParameters)
        {
            var connection = new SQLiteConnection(DefaultConnectionString);
            SQLiteCommand dbCommand = new SQLiteCommand(connection);
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqliteParameters != null)
                dbCommand.Parameters.AddRange(sqliteParameters);

            return dbCommand;
        }

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="transactionContext"> The SQLiteTransactionAsyncContext. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SQLiteCommand CreateCommand(SQLiteTransactionAsyncContext transactionContext, string commandText, CommandType commandType
            , params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = new SQLiteCommand(transactionContext.Connection);
            dbCommand.Transaction = transactionContext.Transaction;
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (SQLiteParameters != null)
                dbCommand.Parameters.AddRange(SQLiteParameters);

            return dbCommand;
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public Task<SQLiteTransactionAsyncContext> BeginTransactionAsync()
        {
            return BeginTransactionAsync(IsolationLevel.ReadCommitted, null);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(
            IsolationLevel isolationLevel)
        {
            return await BeginTransactionAsync(isolationLevel, null);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(
            string transactionName)
        {
            return await BeginTransactionAsync(IsolationLevel.ReadCommitted, transactionName);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(IsolationLevel isolationLevel, string transactionName)
        {
            var tokenSource = new CancellationTokenSource();


            var tranContext = new SQLiteTransactionAsyncContext(GetNewConnection(), tokenSource.Token);
            if (string.IsNullOrEmpty(transactionName))
            {
                await tranContext.BeginTransactionAsync(isolationLevel);
            }
            else
            {
                await tranContext.BeginTransactionAsync(isolationLevel, transactionName);
            }
            return tranContext;
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(SQLiteTransactionAsyncContext context)
        {
            return await BeginTransactionAsync(context, IsolationLevel.ReadCommitted, null);

        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(
            SQLiteTransactionAsyncContext context, IsolationLevel isolationLevel)
        {
            return await BeginTransactionAsync(context, isolationLevel, null);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(
            SQLiteTransactionAsyncContext context, string transactionName)
        {
            return await BeginTransactionAsync(context, IsolationLevel.ReadCommitted, transactionName);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SQLiteTransactionAsyncContext> BeginTransactionAsync(
            SQLiteTransactionAsyncContext context, IsolationLevel isolationLevel,
            string transactionName)
        {
            var tranContext = new SQLiteTransactionAsyncContext(context.Connection, context.CancellationToken);
            if (string.IsNullOrEmpty(transactionName))
            {
                await tranContext.BeginTransactionAsync(isolationLevel);
            }
            else
            {
                await tranContext.BeginTransactionAsync(isolationLevel, transactionName);
            }
            return tranContext;
        }

        /// <summary> Close an open database transaction. </summary>
        /// 
        /// <param name="transaction">	  The SQL transaction. </param>
        /// <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        /// <param name="closeConnection">    (optional) the close connection. </param>

        public void EndTransaction(SQLiteTransactionAsyncContext transaction,
            bool transactionSucceed = true, bool closeConnection = true)
        {
            if (transactionSucceed)
            {
                transaction.Transaction.Commit();
            }
            else
            {
                transaction.Transaction.Rollback();
            }

            if (closeConnection)
            {
                transaction.Connection.CloseSafely();
                transaction.SetConnectionDisposable();
            }
        }

        /// <summary> Gets a object SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The object SQL data. </returns>
        public async Task<IList<dynamic>> GetDynamicSqlDataAsync(SQLiteDataReader reader)
        {
            var result = new List<dynamic>();

            while (await reader.ReadAsync())
            {
                result.Add(SQLiteDataReaderToExpando(reader));
            }
            return result;
        }
    }
}
