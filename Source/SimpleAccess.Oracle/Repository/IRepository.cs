using System.Collections.Generic;
using System.Data;

using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Entity;

namespace SimpleAccess.Oracle
{    
    /// <summary> IRepository. </summary>
    public interface IRepository
    {

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all TEntity in this
        /// collection.</returns>

        IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
                    where TEntity : new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameter"> The SQL parameter. </param>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        TEntity Get<TEntity>(OracleParameter oracleParameter, OracleTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        TEntity Get<TEntity>(dynamic paramObject, OracleTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        TEntity Get<TEntity>(long id, OracleTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class,new();

         
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		   The SQL. </param>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	 (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        dynamic Get(string sql, long id, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null);

        
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		    The SQL. </param>
        /// <param name="oracleParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        dynamic Get(string sql, OracleParameter oracleParameter, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null);

        
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		    The SQL. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        dynamic Get(string sql, dynamic paramObject, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null);


        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(params OracleParameter[] oracleParameters);

        
        /// <summary> Inserts the given dynamic object as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(dynamic paramObject);

        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters">Options for controlling the stored procedure. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters)
            where TEntity: class;

        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters">Options for controlling the stored procedure. </param>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(OracleTransaction transaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class;

        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// <param name="storedProcedureParameters">Options for controlling the stored procedure. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters, OracleTransaction transaction = null)
            where TEntity : class;

        
        /// <summary> Updates the given oracleParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(params OracleParameter[] oracleParameters)
            where TEntity : class;

        
        /// <summary> Updates the given dynamic object as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(dynamic paramObject)
            where TEntity : class;

        
        /// <summary> Updates the given oracleParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters">Options for controlling the stored procedure. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(StoredProcedureParameters storedProcedureParameters)
         where TEntity : class;

        
        /// <summary> Updates the given oracleParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// <param name="storedProcedureParameters">Options for controlling the stored procedure. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(OracleTransaction transaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class;

        
        /// <summary> Deletes the given ID. </summary>
        ///  
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(params OracleParameter[] oracleParameters)
            where TEntity : class;

        
        /// <summary> Deletes the given dynamic object as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(dynamic paramObject)
            where TEntity : class;

        
        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(long id, OracleTransaction transaction = null)
            where TEntity : class;

        
        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(OracleTransaction transaction, params OracleParameter[] oracleParameters)
            where TEntity : class;

        
        /// <summary> Soft delete. </summary>
		/// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
		/// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int SoftDelete<TEntity>(long id)
			where TEntity : class;

        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(string sql, CommandType commandType = CommandType.StoredProcedure, params OracleParameter[] oracleParameters);

        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null);
        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(OracleTransaction transaction, string sql
            , CommandType commandType = CommandType.StoredProcedure
            , params OracleParameter[] oracleParameters);

        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(OracleTransaction transaction, string sql
            , CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null);

        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The T value </returns>
        T ExecuteScalar<T>(string sql, CommandType commandType, params OracleParameter[] oracleParameters);
        
        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The T value </returns>
        
        T ExecuteScalar<T>(string sql, CommandType commandType, dynamic paramObject = null);

        
        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The T value </returns>
        
        T ExecuteScalar<T>(OracleTransaction transaction, string sql
            , CommandType commandType = CommandType.StoredProcedure
            , params OracleParameter[] oracleParameters);

        
        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The T value </returns>
        
        T ExecuteScalar<T>(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null);

        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> </returns>
        
        List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> piList = null,
                                             params OracleParameter[] oracleParameters)
            where TEntity : new();

        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> </returns>
        
        List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null,
                                     Dictionary<string, PropertyInfo> piList = null,
                                     dynamic paramObject = null)
            where TEntity : new();



        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> </returns>

        List<TEntity> ExecuteReader<TEntity>(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                             params OracleParameter[] oracleParameters)
            where TEntity : new();


        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> </returns>
        
        List<TEntity> ExecuteReader<TEntity>(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                     dynamic paramObject = null)
            where TEntity : new();


        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(string sql, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> piList = null,
                                             params OracleParameter[] oracleParameters)
            where TEntity : class, new();

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(string sql, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> piList = null,
                                             dynamic paramObject = null)
            where TEntity : class, new();

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(OracleTransaction transaction, string sql, CommandType commandType,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                             params OracleParameter[] oracleParameters)
            where TEntity : class, new();

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(OracleTransaction transaction, string sql, CommandType commandType,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                     dynamic paramObject = null)
            where TEntity : class, new();

        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        
        IList<dynamic> ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , params OracleParameter[] oracleParameters);


        
       /// <summary> Executes the reader operation. </summary>
       ///  
       /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
       ///  
       /// <param name="sql">			 The SQL. </param>
       /// <param name="commandType"> Type of the command. </param>
       /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
       /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
       /// <param name="paramObject"> The dynamic object as parameters. </param>
       ///  
       /// <returns> A list of dynamic. </returns>
       
        IList<dynamic> ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , dynamic paramObject = null);
        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        
        IList<dynamic> ExecuteReader(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                     params OracleParameter[] oracleParameters);
        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        
        IList<dynamic> ExecuteReader(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                             dynamic paramObject = null);


        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(string sql, CommandType commandType = CommandType.StoredProcedure
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , params OracleParameter[] oracleParameters);

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(string sql, CommandType commandType = CommandType.StoredProcedure
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , dynamic paramObject = null);

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                    string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                    params OracleParameter[] oracleParameters);
        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(OracleTransaction transaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                    string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                    dynamic paramObject = null);

        
        /// <summary> Begins a transaction. </summary>
        /// 
        /// <returns> . </returns>
        
        OracleTransaction BeginTrasaction();
        
        /// <summary> Gets the new connection. </summary>
        /// 
        /// <returns> The new connection. </returns>
        
        OracleConnection GetNewConnection();
        
        /// <summary> Ends a transaction. </summary>
        /// 
        /// <param name="transaction">	  The SQL transaction. </param>
        /// <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        /// <param name="closeConnection">    (optional) the close connection. </param>
        
        void EndTransaction(OracleTransaction transaction, bool transactionSucceed = true, bool closeConnection = true);
    }
}
