using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace SimpleAccess.Core
{
    /// <summary>
    /// Represent the interface of SimpleAccess methods and it's implemented by SimpleAccess 
    /// </summary>
    public interface ISimpleAccess<TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TSqlBuilder>
            where TDbConnection : IDbConnection, new()
            where TDbTransaction : IDbTransaction
            where TDbCommand : IDbCommand, new()
            where TDataParameter : IDataParameter, new()
            where TDbDataReader : IDataReader
            where TSqlBuilder : ISqlBuilder<TDataParameter>, new()
    {


        /// <summary>
        /// Represent the default settings SimpleAccess <see cref="SimpleAccessSettings" />
        /// </summary>
        SimpleAccessSettings DefaultSimpleAccessSettings { get; set; }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(string commandText, params TDataParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(string commandText, CommandType commandType, params TDataParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">	The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(string commandText, dynamic paramObject = null);


        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">	The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(string commandText, CommandType commandType, dynamic paramObject = null);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(TDbTransaction transaction, string commandText
            , params TDataParameter[] parameters);


        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(TDbTransaction transaction, string commandText
            , CommandType commandType
            , params TDataParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(TDbTransaction transaction, string commandText
            , dynamic paramObject = null);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(TDbTransaction transaction, string commandText
            , CommandType commandType, dynamic paramObject = null);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        T ExecuteScalar<T>(string commandText, params TDataParameter[] parameters);


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        T ExecuteScalar<T>(string commandText, CommandType commandType, params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        T ExecuteScalar<T>(string commandText, dynamic paramObject = null);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        T ExecuteScalar<T>(string commandText, CommandType commandType, dynamic paramObject = null);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        T ExecuteScalar<T>(TDbTransaction transaction, string commandText
            , params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        T ExecuteScalar<T>(TDbTransaction transaction, string commandText
            , CommandType commandType
            , params TDataParameter[] parameters);

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        T ExecuteScalar<T>(TDbTransaction transaction, string commandText
            , dynamic paramObject = null);


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        T ExecuteScalar<T>(TDbTransaction transaction, string commandText, CommandType commandType
            , dynamic paramObject = null);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        TDbDataReader ExecuteReader(string commandText,
            params TDataParameter[] parameters);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        TDbDataReader ExecuteReader(string commandText, CommandType commandType,
            params TDataParameter[] parameters);

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        TDbDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior,
            params TDataParameter[] parameters);


        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        TDbDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior,
            params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The TEntity value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
                                     Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
                                     Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : new();


        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(TDbTransaction transaction, string commandText, 
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(TDbTransaction transaction, string commandText,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : new();



        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             dynamic paramObject = null)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             dynamic paramObject = null)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(TDbTransaction transaction, string commandText, string fieldsToSkip = null, 
                                            Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDataParameter[] parameters)
            where TEntity : class, new();




        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(TDbTransaction transaction, string commandText,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : class, new();


        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        TEntity ExecuteEntity<TEntity>(TDbTransaction transaction, string commandText, CommandType commandType,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : class, new();

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(string commandText
            , string fieldsToSkip = null, params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType
            , string fieldsToSkip = null, params TDataParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        ///  
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///  
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        ///  
        ///  <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(string commandText
            , string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        ///  
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///  
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        ///  
        ///  <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType
            , string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(TDbTransaction transaction, string commandText,
                                     string fieldsToSkip = null, params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(TDbTransaction transaction, string commandText, CommandType commandType,                                     
            string fieldsToSkip = null , params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(TDbTransaction transaction, string commandText,
                             string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        IEnumerable<dynamic> ExecuteDynamics(TDbTransaction transaction, string commandText, CommandType commandType,
                             string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(string commandText 
            , string fieldsToSkip = null, params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(string commandText, CommandType commandType
            , string fieldsToSkip = null, params TDataParameter[] parameters);


        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(string commandText
            , string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(string commandText, CommandType commandType
            , string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(TDbTransaction transaction, string commandText,
                                    string fieldsToSkip = null, params TDataParameter[] parameters);



        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(TDbTransaction transaction, string commandText, CommandType commandType,
                                    string fieldsToSkip = null, params TDataParameter[] parameters);



        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// -<param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(TDbTransaction transaction, string commandText,
                                    string fieldsToSkip = null, dynamic paramObject = null);


        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        dynamic ExecuteDynamic(TDbTransaction transaction, string commandText, CommandType commandType,
                                    string fieldsToSkip = null, dynamic paramObject = null);

        /// <summary>
        /// Execute commant text against connection and add or refresh rows in <see cref="DataTable"/>
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dataTable">A <see cref="DataTable"/> to fill with records and, if necessary, schema  </param>
        /// <returns></returns>
        int Fill(string commandText, DataTable dataTable);

        /// <summary>
        /// Execute commant text against connection and add or refresh rows in <see cref="DataSet"/>
        /// </summary>
        /// <param name="commandText">	The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="dataSet"> A <see cref="DataSet"/> to fill with records and, if necessary, schema  </param>
        /// <returns></returns>
        int Fill(string commandText, DataSet dataSet);

        /// <summary> Begins a database transaction. </summary>
        /// 
        /// <returns> . </returns>
        TDbTransaction BeginTrasaction();
        

        /// <summary> Gets the new connection. </summary>
        /// 
        /// <returns>  </returns>
        
        TDbConnection GetNewConnection();

        /// <summary> Close the current open connection. </summary>
        void CloseCurrentDbConnection();


        /// <summary> Close an open database transaction. </summary>
        /// 
        /// <param name="transaction">	  The SQL transaction. </param>
        /// <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        /// <param name="closeConnection">    (optional) the close connection. </param>

        void EndTransaction(TDbTransaction transaction, bool transactionSucceed = true, bool closeConnection = true);
    }
}
