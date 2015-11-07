using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Reflection;
using SimpleAccess.Core;

namespace SimpleAccess
{
    /// <summary>
    /// ISimpleCommand
    /// </summary>
    public interface ISimpleAccess<TDbConnection, TDbTransaction, TDbCommand, TDbParameter, TDbDataReader, TDataAdapter>
            where TDbConnection : DbConnection, new()
            where TDbTransaction : DbTransaction
            where TDbCommand : DbCommand, new()
            where TDbParameter : DbParameter, new()
            where TDbDataReader : DbDataReader
            where TDataAdapter : DataAdapter, new()    {


        SimpleAccessSettings DefaultSimpleAccessSettings { get; set; }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(string commandText, params TDbParameter[] parameters);

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="parameters">  Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int ExecuteNonQuery(string commandText, CommandType commandType, params TDbParameter[] parameters);

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
            , params TDbParameter[] parameters);


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
            , params TDbParameter[] parameters);

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
        T ExecuteScalar<T>(string commandText, params TDbParameter[] parameters);


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
        T ExecuteScalar<T>(string commandText, CommandType commandType, params TDbParameter[] parameters);

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
            , params TDbParameter[] parameters);

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
            , params TDbParameter[] parameters);

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
            params TDbParameter[] parameters);

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
            params TDbParameter[] parameters);

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{TEntity} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText">		The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="parameters"> Parmeters rquired to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                             params TDbParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{TEntity} from DataReader. </summary>
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
                                             params TDbParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{TEntity} from DataReader. </summary>
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

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{TEntity} from DataReader. </summary>
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
                                             params TDbParameter[] parameters)
            where TEntity : new();

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
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
                                             params TDbParameter[] parameters)
            where TEntity : new();

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{TEntity} from DataReader. </summary>
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
        /// <returns> The {TEntity} value </returns>
        IEnumerable<TEntity> ExecuteEntities<TEntity>(TDbTransaction transaction, string commandText,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
                                     dynamic paramObject = null)
            where TEntity : new();



        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{TEntity} from DataReader. </summary>
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
        /// <returns> The {TEntity} value </returns>
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
                                             params TDbParameter[] parameters)
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
                                             params TDbParameter[] parameters)
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
                                             params TDbParameter[] parameters)
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
                                             params TDbParameter[] parameters)
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
            , string fieldsToSkip = null, params TDbParameter[] parameters);

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
            , string fieldsToSkip = null, params TDbParameter[] parameters);

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
                                     string fieldsToSkip = null, params TDbParameter[] parameters);


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
            string fieldsToSkip = null , params TDbParameter[] parameters);


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
            , string fieldsToSkip = null, params TDbParameter[] parameters);


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
            , string fieldsToSkip = null, params TDbParameter[] parameters);


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
                                    string fieldsToSkip = null, params TDbParameter[] parameters);



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
                                    string fieldsToSkip = null, params TDbParameter[] parameters);



        /// <summary> Sends the CommandText to the Connection and builds a dynamic object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
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
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataTable FillSingle(string commandText);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet Fill(string commandText);

        /// <summary> Begins a transaction. </summary>
        /// 
        /// <returns> . </returns>
        TDbTransaction BeginTrasaction();
        

        /// <summary> Gets the new connection. </summary>
        /// 
        /// <returns> The new connection. </returns>
        
        TDbConnection GetNewConnection();

        /// <summary> Close the current open connection. </summary>
        void CloseCurrentDbConnection();


        /// <summary> Ends a transaction. </summary>
        /// 
        /// <param name="transaction">	  The SQL transaction. </param>
        /// <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        /// <param name="closeConnection">    (optional) the close connection. </param>

        void EndTransaction(TDbTransaction transaction, bool transactionSucceed = true, bool closeConnection = true);
    }
}
