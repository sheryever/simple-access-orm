using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Dynamic;
using System.Reflection;
using System.Threading;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SqlServer
{
#if !NET40
    /// <summary>
    /// Sql Server implementation for SimpleAccess.
    /// </summary>
    public partial class SqlSimpleAccess
    {


        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQueryAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
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
            return ExecuteNonQueryAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
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
            return ExecuteNonQueryAsync(commandText, commandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            int result;
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(SqlTransactionAsyncContext transactionContext, string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQueryAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null)
        {
            return ExecuteNonQueryAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> ExecuteNonQueryAsync(SqlTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteNonQueryAsync(transactionContext, commandText, commandType, BuildSqlParameters(paramObject));

        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> ExecuteNonQueryAsync(SqlTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand dbCommand = null;
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
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public Task<T> ExecuteScalarAsync<T>(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteScalarAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , sqlParameters);
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
                , BuildSqlParameters(paramObject));
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
                , BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public async Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteScalarAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , sqlParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null)
        {
            return ExecuteScalarAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public Task<T> ExecuteScalarAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText,
                    CommandType commandType, object paramObject = null)
        {
            return ExecuteScalarAsync<T>(transactionContext, commandText, commandType
                , BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public async Task<T> ExecuteScalarAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {

                dbCommand = CreateCommand(transactionContext, commandText, commandType, sqlParameters);

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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteReaderAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteReaderAsync(commandText, commandType, CommandBehavior.CloseConnection, sqlParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteReaderAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, commandBehavior, sqlParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> The TDbDataReader </returns>
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, BuildSqlParameters(paramObject));
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
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, commandType, BuildSqlParameters(paramObject));
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
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, CommandBehavior commandBehavior = CommandBehavior.CloseConnection, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, commandBehavior, BuildSqlParameters(paramObject));
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
        public Task<SqlDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, CommandBehavior commandBehavior = CommandBehavior.CloseConnection, object paramObject = null)
        {
            return ExecuteReaderAsync(commandText, commandType, commandBehavior, BuildSqlParameters(paramObject));
        }


        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public async Task<SqlDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;


                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                var result = await dbCommand.ExecuteReaderAsync(commandBehavior, cancellationToken);

                dbCommand.Parameters.Clear();
                return result;
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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteValuesAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(sqlParameters));
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
            return ExecuteValuesAsync<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
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
            return ExecuteValuesAsync<T>(commandText, commandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public async Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                using (var reader = await dbCommand.ExecuteReaderAsync(cancellationToken))
                {
                    return GetValues<T>(reader);
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> value </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null)
        {
            return ExecuteValuesAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> value </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValuesAsync<T>(transactionContext, commandText, commandType, BuildSqlParameters(paramObject));
        }
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public Task<IEnumerable<T>> ExecuteValuesAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText,
                                             params SqlParameter[] sqlParameters)
        {
            return ExecuteValuesAsync<T>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public async Task<IEnumerable<T>> ExecuteValuesAsync<T>(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {

                dbCommand = CreateCommand(transactionContext, commandText, commandType, sqlParameters);

                using (var reader = await dbCommand.ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false))
                {
                    return GetValues<T>(reader);
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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TEntity value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, sqlParameters);
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
                , propertyInfoDictionary, BuildSqlParameters(paramObject));
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
                , propertyInfoDictionary, BuildSqlParameters(paramObject));
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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public async Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SqlParameter[] sqlParameters) where TEntity : new()
        {
            SqlCommand dbCommand = null;
            try
            {

                dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
                var cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                using (var reader = await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                {
                    return reader.DataReaderToObjectList<TEntity>(fieldsToSkip, propertyInfoDictionary);
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, commandType
                , fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public async Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, sqlParameters);

                using (var reader = await dbCommand
                    .ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false))
                {
                    return reader.DataReaderToObjectList<TEntity>(fieldsToSkip, propertyInfoDictionary);
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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, sqlParameters);
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
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
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
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public async Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public Task<TEntity> ExecuteEntityAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntityAsync<TEntity>(transactionContext, commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public async Task<TEntity> ExecuteEntityAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, sqlParameters);

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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamicsAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
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
                BuildSqlParameters(paramObject));
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
                BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public async Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;

            try
            {
                dbCommand = CreateCommandForAsync(commandText, commandType, sqlParameters);
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                return GetDynamicSqlData(await dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false));

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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SqlTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamicsAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null,
            string fieldsToSkip = null)
        {
            return ExecuteDynamicsAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicsAsync(transactionContext, commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }



        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public async Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, sqlParameters);

                return GetDynamicSqlData(await dbCommand
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
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(string commandText, string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamicAsync(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
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
                BuildSqlParameters(paramObject));
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
                BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public async Task<dynamic> ExecuteDynamicAsync(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;

                await dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                var reader = await dbCommand.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken).ConfigureAwait(false);
                if (reader.Read())
                {
                    var result = SqlDataReaderToExpando(reader);
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(SqlTransactionAsyncContext transactionContext, string commandText, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamicAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// -<param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(SqlTransactionAsyncContext transactionContext, string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicAsync(transactionContext, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public Task<dynamic> ExecuteDynamicAsync(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
                object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamicAsync(transactionContext, commandText, commandType,
                fieldsToSkip, BuildSqlParameters(paramObject));
        }


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public async Task<dynamic> ExecuteDynamicAsync(SqlTransactionAsyncContext transactionContext, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            SqlDataReader reader = null;
            try
            {
                dbCommand = CreateCommand(transactionContext, commandText, commandType, sqlParameters);

                reader = await dbCommand.ExecuteReaderAsync(transactionContext.CancellationToken).ConfigureAwait(false);
                if (reader.Read())
                    return SqlDataReaderToExpando(reader);

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
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SqlCommand CreateCommandForAsync(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            var connection = new SqlConnection(_sqlConnection.ConnectionString);
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);

            return dbCommand;
        }

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SqlCommand CreateCommand(SqlTransactionAsyncContext transctionContext, string commandText, CommandType commandType
            , params SqlParameter[] sqlParameters)
        {
            var dbCommand = transctionContext.Connection.CreateCommand();
            dbCommand.Transaction = transctionContext.Transaction;
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);
            if (_sqlTransaction != null)
                dbCommand.Transaction = _sqlTransaction;

            return dbCommand;
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public Task<SqlTransactionAsyncContext> BeginTransactionAsync()
        {
            return BeginTransactionAsync(IsolationLevel.ReadCommitted, null);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(
            IsolationLevel isolationLevel)
        {
            return await BeginTransactionAsync(isolationLevel, null);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(
            string transactionName)
        {
            return await BeginTransactionAsync(IsolationLevel.ReadCommitted, transactionName);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(IsolationLevel isolationLevel, string transactionName)
        {
            var tokenSource = new CancellationTokenSource();


            var tranContext = new SqlTransactionAsyncContext(GetNewConnection(), tokenSource.Token);
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
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(SqlTransactionAsyncContext context)
        {
            return await BeginTransactionAsync(context, IsolationLevel.ReadCommitted, null);

        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(
            SqlTransactionAsyncContext context, IsolationLevel isolationLevel)
        {
            return await BeginTransactionAsync(context, isolationLevel, null);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(
            SqlTransactionAsyncContext context, string transactionName)
        {
            return await BeginTransactionAsync(context, IsolationLevel.ReadCommitted, transactionName);
        }

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        public async Task<SqlTransactionAsyncContext> BeginTransactionAsync(
            SqlTransactionAsyncContext context, IsolationLevel isolationLevel,
            string transactionName)
        {
            var tranContext = new SqlTransactionAsyncContext(context.Connection, context.CancellationToken);
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

        public void EndTransaction(SqlTransactionAsyncContext transaction,
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

    }
#endif 
}
