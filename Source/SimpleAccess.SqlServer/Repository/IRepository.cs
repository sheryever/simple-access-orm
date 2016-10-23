using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SimpleAccess.Entity;

namespace SimpleAccess.SqlServer
{    
    /// <summary> IRepository. </summary>
    public interface IRepository
    {

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all TEntity in this
        /// collection.</returns>

        IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
                    where TEntity : new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        TEntity Get<TEntity>(SqlParameter sqlParameter, SqlTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        TEntity Get<TEntity>(dynamic paramObject, SqlTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        TEntity Get<TEntity>(long id, SqlTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class,new();

         
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		   The SQL. </param>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        dynamic Get(string sql, long id, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null);

        
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		    The SQL. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        dynamic Get(string sql, SqlParameter sqlParameter, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null);

        
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		    The SQL. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        
        dynamic Get(string sql, dynamic paramObject, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null);


        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(params SqlParameter[] sqlParameters);

        
        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(dynamic paramObject);

        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters)
            where TEntity: class;

        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(SqlTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class;

        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> . </returns>
        
        int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters, SqlTransaction sqlTransaction = null)
            where TEntity : class;

        
        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        
        /// <summary> Updates the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(dynamic paramObject)
            where TEntity : class;

        
        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(StoredProcedureParameters storedProcedureParameters)
         where TEntity : class;

        
        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Update<TEntity>(SqlTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class;

        
        /// <summary> Deletes the given ID. </summary>
        ///  
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        
        /// <summary> Deletes the given dynamic object as SqlParameter names and values. </summary>
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
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(long id, SqlTransaction sqlTransaction = null)
            where TEntity : class;

        
        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int Delete<TEntity>(SqlTransaction sqlTransaction, params SqlParameter[] sqlParameters)
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
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] sqlParameters);

        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null);
        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(SqlTransaction sqlTransaction, string sql
            , CommandType commandType = CommandType.StoredProcedure
            , params SqlParameter[] sqlParameters);

        
        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        
        int ExecuteNonQuery(SqlTransaction sqlTransaction, string sql
            , CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null);

        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> The T value </returns>
        T ExecuteScalar<T>(string sql, CommandType commandType, params SqlParameter[] sqlParameters);
        
        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The T value </returns>
        
        T ExecuteScalar<T>(string sql, CommandType commandType, dynamic paramObject = null);

        
        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> The T value </returns>
        
        T ExecuteScalar<T>(SqlTransaction sqlTransaction, string sql
            , CommandType commandType = CommandType.StoredProcedure
            , params SqlParameter[] sqlParameters);

        
        /// <summary> Executes the scalar operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The T value </returns>
        
        T ExecuteScalar<T>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null);

        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> </returns>
        
        List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> piList = null,
                                             params SqlParameter[] sqlParameters)
            where TEntity : new();

        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
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
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> </returns>

        List<TEntity> ExecuteReader<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                             params SqlParameter[] sqlParameters)
            where TEntity : new();


        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> </returns>
        
        List<TEntity> ExecuteReader<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                     dynamic paramObject = null)
            where TEntity : new();


        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(string sql, CommandType commandType, string fieldsToSkip = null,
                                             Dictionary<string, PropertyInfo> piList = null,
                                             params SqlParameter[] sqlParameters)
            where TEntity : class, new();

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
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
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType,
                                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                             params SqlParameter[] sqlParameters)
            where TEntity : class, new();

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        
        TEntity ExecuteReaderSingle<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                     dynamic paramObject = null)
            where TEntity : class, new();

        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        
        IList<dynamic> ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , params SqlParameter[] sqlParameters);


        
       /// <summary> Executes the reader operation. </summary>
       ///  
       /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
       ///  
       /// <param name="sql">			 The SQL. </param>
       /// <param name="commandType">   Type of the command. </param>
       /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
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
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        
        IList<dynamic> ExecuteReader(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                     string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                     params SqlParameter[] sqlParameters);
        
        /// <summary> Executes the reader operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> A list of dynamic. </returns>
        
        IList<dynamic> ExecuteReader(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                             string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                             dynamic paramObject = null);


        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        /// <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(string sql, CommandType commandType = CommandType.StoredProcedure
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , params SqlParameter[] sqlParameters);

        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sql">			 The SQL. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="fieldsToSkip">  (optional) the fields to skip. </param>
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
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                    string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                    params SqlParameter[] sqlParameters);
        
        /// <summary> Executes the reader single operation. </summary>
        /// 
        /// <exception cref="System.Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sql">			  The SQL. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        /// <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Result in a dynamic object. </returns>
        
        dynamic ExecuteReaderSingle(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure,
                                    string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null,
                                    dynamic paramObject = null);

        
        /// <summary> Begins a transaction. </summary>
        /// 
        /// <returns> . </returns>
        
        SqlTransaction BeginTrasaction();
        
        /// <summary> Gets the new connection. </summary>
        /// 
        /// <returns> The new connection. </returns>
        
        SqlConnection GetNewConnection();
        
        /// <summary> Ends a transaction. </summary>
        /// 
        /// <param name="sqlTransaction">	  The SQL transaction. </param>
        /// <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        /// <param name="closeConnection">    (optional) the close connection. </param>
        
        void EndTransaction(SqlTransaction sqlTransaction, bool transactionSucceed = true, bool closeConnection = true);
    }
}
