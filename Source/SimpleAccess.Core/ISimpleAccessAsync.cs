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
    public interface ISimpleAccessAsync<TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TSqlBuilder>
            where TDbConnection : IDbConnection, new()
            where TDbTransaction : IDbTransaction
            where TDbCommand : IDbCommand, new()
            where TDataParameter : IDataParameter, new()
            where TDbDataReader : IDataReader
            where TSqlBuilder : ISqlBuilder<TDataParameter>, new()
    {


        /*
        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(string commandText, params TDataParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType, params TDataParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(string commandText, object paramObject = null);


        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType, object paramObject = null);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(TDbTransaction transaction, string commandText
            , params TDataParameter[] parameters);


        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(TDbTransaction transaction, string commandText
            , CommandType commandType
            , params TDataParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(TDbTransaction transaction, string commandText
            , object paramObject = null);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> ExecuteNonQueryAsync(TDbTransaction transaction, string commandText
            , CommandType commandType, object paramObject = null);
*/
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        Task<T> ExecuteScalarAsync<T>(string commandText, params TDataParameter[] parameters);


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType, params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        Task<T> ExecuteScalarAsync<T>(string commandText, object paramObject = null);

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
        Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType, object paramObject = null);
        
        /*
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        Task<T> ExecuteScalarAsync<T>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, string commandText
            , params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        Task<T> ExecuteScalarAsync<T>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, string commandText
            , CommandType commandType
            , params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        Task<T> ExecuteScalarAsync<T>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, string commandText
            , object paramObject = null);


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        Task<T> ExecuteScalarAsync<T>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, string commandText, CommandType commandType
            , object paramObject = null);
        /*(
        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText,
            params TDataParameter[] parameters);
            /*
        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, CommandType commandType,
            params TDataParameter[] parameters);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, CommandBehavior commandBehavior,
            params TDataParameter[] parameters);


        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, CommandBehavior commandBehavior,
            params TDataParameter[] parameters);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, object paramObject = null);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, object paramObject = null);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, CommandBehavior commandBehavior, object paramObject = null);


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
        Task<TDbDataReader> ExecuteReaderAsync(string commandText, CommandType commandType, CommandBehavior commandBehavior, object paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, CommandType commandType, params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, object paramObject = null);

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
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(string commandText, CommandType commandType, object paramObject = null);


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(TDbTransaction transaction, string commandText, params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(TDbTransaction transaction, string commandText, CommandType commandType,
                                             params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> value </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(TDbTransaction transaction, string commandText, object paramObject = null);



        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> value </returns>
        Task<IEnumerable<T>> ExecuteValuesAsync<T>(TDbTransaction transaction, string commandText,
            CommandType commandType, object paramObject = null);

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, object paramObject = null, string fieldsToSkip = null,
                                     Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new();

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
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(string commandText, CommandType commandType, object paramObject = null,
                            string fieldsToSkip = null,Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new();


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(TDbTransaction transaction, string commandText, 
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(TDbTransaction transaction, string commandText, object paramObject = null,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new();



        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> </returns>
        Task<IEnumerable<TEntity>> ExecuteEntitiesAsync<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                     object paramObject = null, string fieldsToSkip = null, 
                                     Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();

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
        Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, object paramObject = null, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new();

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
        Task<TEntity> ExecuteEntityAsync<TEntity>(string commandText, CommandType commandType, object paramObject = null, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        Task<TEntity> ExecuteEntityAsync<TEntity>(TDbTransaction transaction, string commandText, string fieldsToSkip = null, 
                                            Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        Task<TEntity> ExecuteEntityAsync<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();




        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        Task<TEntity> ExecuteEntityAsync<TEntity>(TDbTransaction transaction, string commandText, object paramObject = null,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
                                     )
            where TEntity : class, new();


        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        Task<TEntity> ExecuteEntityAsync<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType, object paramObject = null,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText
            , string fieldsToSkip = null, params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, CommandType commandType
            , string fieldsToSkip = null, params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        ///  
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///  
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        ///  
        ///  <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, object paramObject = null, string fieldsToSkip = null);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        ///  
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///  
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        ///  
        ///  <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(string commandText, CommandType commandType, object paramObject = null
            , string fieldsToSkip = null);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(TDbTransaction transaction, string commandText,
                                     string fieldsToSkip = null, params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(TDbTransaction transaction, string commandText, CommandType commandType,                                     
            string fieldsToSkip = null , params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(TDbTransaction transaction, string commandText, object paramObject = null,
                             string fieldsToSkip = null);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        Task<IEnumerable<dynamic>> ExecuteDynamicsAsync(TDbTransaction transaction, string commandText, CommandType commandType,
                             object paramObject = null, string fieldsToSkip = null);


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(string commandText 
            , string fieldsToSkip = null, params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(string commandText, CommandType commandType
            , string fieldsToSkip = null, params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(string commandText, object paramObject = null
            , string fieldsToSkip = null);


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
        Task<dynamic> ExecuteDynamicAsync(string commandText, CommandType commandType, object paramObject = null,
                string fieldsToSkip = null);


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(TDbTransaction transaction, string commandText,
                                    string fieldsToSkip = null, params TDataParameter[] parameters);



        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="parameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(TDbTransaction transaction, string commandText, CommandType commandType,
                                    string fieldsToSkip = null, params TDataParameter[] parameters);



        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(TDbTransaction transaction, string commandText,
                                    object paramObject = null, string fieldsToSkip = null);


        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        Task<dynamic> ExecuteDynamicAsync(TDbTransaction transaction, string commandText, CommandType commandType,
                                    object paramObject = null, string fieldsToSkip = null );
                                    */

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync();

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(IsolationLevel isolationLevel);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(string transactionName);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(IsolationLevel isolationLevel, string transactionName);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(IDbTransactionAsyncContext<TDbConnection, TDbTransaction> context);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(IDbTransactionAsyncContext<TDbConnection, TDbTransaction> context,IsolationLevel isolationLevel);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(IDbTransactionAsyncContext<TDbConnection, TDbTransaction> context, string transactionName);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> IDbTransactionAsyncContext&lt;TDbConnection, TDbTransaction&gt; </returns>
        Task<IDbTransactionAsyncContext<TDbConnection, TDbTransaction>> BeginTransactionAsync(IDbTransactionAsyncContext<TDbConnection, TDbTransaction> context, IsolationLevel isolationLevel, string transactionName);

        /// <summary> Close an open database transaction. </summary>
        /// 
        /// <param name="transaction">	  The SQL transaction. </param>
        /// <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        /// <param name="closeConnection">    (optional) the close connection. </param>

        void EndTransaction(IDbTransactionAsyncContext<TDbConnection, TDbTransaction> transaction, bool transactionSucceed = true, bool closeConnection = true);
    }
#endif
}
