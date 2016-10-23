/**--------------------------------------------------------------------------------------------------
// file:	Repository\Repository.cs
//
// summary:	Implements the repository class
**/
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Dynamic;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Oracle.DbExtensions;
using SimpleAccess.Oracle.Entity;

//using SimpleAccess.Infrastructure.DbExtensions;

namespace SimpleAccess.Oracle.Repository
{
    /**--------------------------------------------------------------------------------------------------
    <summary> Repository. </summary>
    **/
    public class Repository : IRepository, IDisposable
    {
        private const string DefaultConnectionStringKey = "simpleAccess:oracleConnectionStringName";
        /// <summary>
        /// Default connection string.
        /// </summary>
        public static string DefaultConnectionString { get; set; }

        /**--------------------------------------------------------------------------------------------------
        <summary> The SQL connection. </summary>
        **/
        private OracleConnection _oracleConnection;

		/**--------------------------------------------------------------------------------------------------
		<summary> The SQL transaction. </summary>
		**/
		private OracleTransaction _oracleTransaction;

        #region Constructor

        /**--------------------------------------------------------------------------------------------------
        <summary> Constructor. </summary>
        
        <param name="sqlConnection"> The SQL connection. </param>
        **/
        public Repository(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
            DefaultConnectionString = oracleConnection.ConnectionString;
        }

        /// <summary>
        /// Constructor takes connection name.
        /// </summary>
        /// <param name="connectionStringName">The connection string.</param>
        public Repository(string connectionStringName)
        {
            var connectionStringSettings = RepositorySetting.LoadConnectionStringSettingsFromConfigurationFile(connectionStringName, true);
            DefaultConnectionString = connectionStringSettings.ConnectionString;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Default constructor. </summary>
        **/
        public Repository()
            : this(new OracleConnection(DefaultConnectionString))
        {
        }

        static Repository()
        {
            var connectionStringName = ConfigurationManager.AppSettings[DefaultConnectionStringKey];
            var connectionStringSettings = RepositorySetting.LoadConnectionStringSettingsFromConfigurationFile(connectionStringName);
            if (connectionStringSettings != null)
            {
                DefaultConnectionString = connectionStringSettings.ConnectionString;
            }
        }
        #endregion

        /**--------------------------------------------------------------------------------------------------
        <summary> Begins a transaction. </summary>
        
        <returns> . </returns>
        **/
        public OracleTransaction BeginTrasaction()
        {
            if (_oracleConnection.State != ConnectionState.Open)
                _oracleConnection.Open();
            _oracleTransaction = _oracleConnection.BeginTransaction();

			return _oracleTransaction;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets the new connection. </summary>
        
        <returns> The new connection. </returns>
        **/
        public OracleConnection GetNewConnection()
        {
            return new OracleConnection(DefaultConnectionString);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Enumerates get all in this collection. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="fieldToSkip"> (optional) the field to skip. </param>
        <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> An enumerator that allows for each to be used to process get all <TEntity> in this
        collection. </returns>
        **/
        public virtual IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof (TEntity));
            string queryString = string.Format("{0}_GetAll", entityInfo.Name);
            return ExecuteReader<TEntity>(queryString, CommandType.StoredProcedure, fieldToSkip, piList);
        }

        //public TEntity Get<TEntity>(long id, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
        //    where TEntity : new()
        //{
        //    return Get<TEntity>(new OracleParameter("@id", id), fieldToSkip, piList);
        //}

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="id">		   The identifier. </param>
        <param name="transaction"> (optional) the transaction. </param>
        <param name="fieldToSkip"> (optional) the field to skip. </param>
        <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> . </returns>
        **/
        public TEntity Get<TEntity>(long id, OracleTransaction transaction = null, string fieldToSkip = null,
                                    Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new()
        {
            return Get<TEntity>(new OracleParameter("@id", id), transaction, fieldToSkip, piList);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="oracleParameter"> The SQL parameter. </param>
        <param name="transaction">  (optional) the transaction. </param>
        <param name="fieldToSkip">  (optional) the field to skip. </param>
        <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> . </returns>
        **/
        public TEntity Get<TEntity>(OracleParameter oracleParameter, OracleTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var queryString = string.Format("{0}_GetById", entityInfo.Name);

            if (transaction == null)
                return ExecuteReaderSingle<TEntity>(queryString, CommandType.StoredProcedure, fieldToSkip, piList, oracleParameter);
            else
                return ExecuteReaderSingle<TEntity>(transaction, queryString, CommandType.StoredProcedure, fieldToSkip, piList, oracleParameter);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="paramObject"> The dynamic object as parameters. </param>
        <param name="transaction">  (optional) the transaction. </param>
        <param name="fieldToSkip">  (optional) the field to skip. </param>
        <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> . </returns>
        **/
        public TEntity Get<TEntity>(dynamic paramObject, OracleTransaction transaction = null, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var queryString = string.Format("{0}_GetById", entityInfo.Name);

            if (transaction == null)
                return ExecuteReaderSingle<TEntity>(queryString, CommandType.StoredProcedure, fieldToSkip, piList, paramObject);
            else
                return ExecuteReaderSingle<TEntity>(transaction, queryString, CommandType.StoredProcedure, fieldToSkip, piList, paramObject);
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Gets. </summary>
        
        <param name="sql">		   The SQL. </param>
        <param name="id">		   The identifier. </param>
        <param name="fieldToSkip"> (optional) the field to skip. </param>
        <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> . </returns>
        **/
        public dynamic Get(string sql, long id, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
        {
            return Get(sql, new OracleParameter("@id", id), fieldToSkip, piList);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets. </summary>
        
        <param name="sql">		    The SQL. </param>
        <param name="oracleParameter"> The SQL parameter. </param>
        <param name="fieldToSkip">  (optional) the field to skip. </param>
        <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> . </returns>
        **/
        public dynamic Get(string sql, OracleParameter oracleParameter, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
        {
            return ExecuteReaderSingle(sql, CommandType.StoredProcedure, fieldToSkip, piList, oracleParameter);
            
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets. </summary>
        
        <param name="sql">		    The SQL. </param>
        <param name="paramObject"> The dynamic object as parameters. </param>
        <param name="fieldToSkip">  (optional) the field to skip. </param>
        <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        
        <returns> . </returns>
        **/
        public dynamic Get(string sql, dynamic paramObject, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
        {

            return ExecuteReaderSingle(sql, CommandType.StoredProcedure, fieldToSkip, piList, paramObject);

        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Inserts the given SQL parameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public int Insert<TEntity>(params OracleParameter[] sqlParameters)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Insert", entityInfo.Name);

            return ExecuteNonQuery(queryString, CommandType.StoredProcedure, sqlParameters);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Inserts the given dynamic object as OracleParameter names and values. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public int Insert<TEntity>(dynamic paramObject)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Insert", entityInfo.Name);

            return ExecuteNonQuery(queryString, CommandType.StoredProcedure, BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Inserts the given SQL parameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        
        <returns> . </returns>
        **/
        public int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters)
            where TEntity: class
        {
            OracleParameter[] sqlParameters = storedProcedureParameters.GetSpParameters(ParametersType.Insert);
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("[dbo].{0}_Insert", entityInfo.Name);

            var result = ExecuteNonQuery(queryString, CommandType.StoredProcedure, sqlParameters);

            storedProcedureParameters.LoadOutParametersProperties();
            storedProcedureParameters.ClearSpParameters();

            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Inserts the given SQL parameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        <param name="sqlTransaction">			 The SQL transaction. </param>
        
        <returns> . </returns>
        **/
        public int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters, OracleTransaction sqlTransaction = null)
            where TEntity : class
        {
            OracleParameter[] sqlParameters = storedProcedureParameters.GetSpParameters(ParametersType.Insert);
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Insert", entityInfo.Name);

            var result = ExecuteNonQuery(sqlTransaction, queryString, CommandType.StoredProcedure, sqlParameters);

            storedProcedureParameters.LoadOutParametersProperties();
            storedProcedureParameters.ClearSpParameters();

            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Inserts the given SQL parameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction">			 The SQL transaction. </param>
        <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        
        <returns> . </returns>
        **/
        public int Insert<TEntity>(OracleTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class
        {
            OracleParameter[] sqlParameters = storedProcedureParameters.GetSpParameters(ParametersType.Insert);
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Insert", entityInfo.Name);

            var result = ExecuteNonQuery(sqlTransaction, queryString, CommandType.StoredProcedure, sqlParameters);

            storedProcedureParameters.LoadOutParametersProperties();
            storedProcedureParameters.ClearSpParameters();

            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Updates the given sqlParameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public int Update<TEntity>(params OracleParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var queryString = string.Format("{0}_Update", entityInfo.Name);
            return ExecuteNonQuery(queryString, CommandType.StoredProcedure, sqlParameters);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Updates the given dynamic object as OracleParameter names and values. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="paramObject"> The dynamic object as parameters. </param>        
        <returns> . </returns>
        **/
        public int Update<TEntity>(dynamic paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Update", entityInfo.Name);
            return ExecuteNonQuery(queryString, CommandType.StoredProcedure, BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Updates the given sqlParameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        
        <returns> . </returns>
        **/
        public int Update<TEntity>(StoredProcedureParameters storedProcedureParameters)
            where TEntity : class 
        {
            var sqlParameters = storedProcedureParameters.GetSpParameters(ParametersType.Update);

            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Update", entityInfo.Name);
            var result = ExecuteNonQuery(queryString, CommandType.StoredProcedure, sqlParameters);

            storedProcedureParameters.ClearSpParameters();

            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Updates the given sqlParameters. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction">			 The SQL transaction. </param>
        <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        
        <returns> . </returns>
        **/
        public int Update<TEntity>(OracleTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class
        {
            var sqlParameters = storedProcedureParameters.GetSpParameters(ParametersType.Update);

            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var queryString = string.Format("{0}_Update", entityInfo.Name);
            var result = ExecuteNonQuery(sqlTransaction, queryString, CommandType.StoredProcedure, sqlParameters);

            storedProcedureParameters.ClearSpParameters();

            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Deletes the given ID. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="id"> The identifier. </param>
        <param name="sqlTransaction">			 The SQL transaction. </param>

        <returns> . </returns>
        **/
        public int Delete<TEntity>(long id, OracleTransaction sqlTransaction = null)
            where TEntity : IEntity
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var queryString = string.Format("{0}_Delete", entityInfo.Name);
			var result = sqlTransaction != null
				? ExecuteNonQuery(sqlTransaction, queryString, CommandType.StoredProcedure,
					new OracleParameter("@id", id))
				: ExecuteNonQuery(queryString, CommandType.StoredProcedure, new OracleParameter("@id", id));
			return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Deletes the given ID. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public virtual int Delete<TEntity>(params OracleParameter[] sqlParameters)
            where TEntity : IEntity
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string queryString = string.Format("{0}_Delete", entityInfo.Name);

            return ExecuteNonQuery(queryString, CommandType.StoredProcedure, sqlParameters);
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Deletes the given dynamic object as OracleParameter names and values. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public virtual int Delete<TEntity>(dynamic paramObject)
            where TEntity : IEntity
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var queryString = string.Format("{0}_Delete", entityInfo.Name);

            return ExecuteNonQuery(queryString, CommandType.StoredProcedure, BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Deletes the given ID. </summary>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public virtual int Delete<TEntity>(OracleTransaction sqlTransaction, params OracleParameter[] sqlParameters)
            where TEntity : IEntity
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var queryString = string.Format("{0}_Delete", entityInfo.Name);

            return ExecuteNonQuery(sqlTransaction, queryString, CommandType.StoredProcedure, sqlParameters);
        }

		/**--------------------------------------------------------------------------------------------------
		<summary> Soft delete. </summary>
		
		<typeparam name="TEntity"> Type of the entity. </typeparam>
		<param name="id"> The identifier. </param>
		
		<returns> . </returns>
		**/
		public int SoftDelete<TEntity>(long id)
			where TEntity : IEntity
		{
			//var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
			var queryString = string.Format("{0}_SoftDelete", entityInfo.Name);

			return ExecuteNonQuery(queryString, CommandType.StoredProcedure, new OracleParameter("@id", id));
		}

        /**--------------------------------------------------------------------------------------------------
       <summary> Executes the non query operation. </summary>
        
       <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
       <param name="sqlTransaction"> The SQL transaction. </param>
       <param name="sql">			  The SQL. </param>
       <param name="commandType">    Type of the command. </param>
        <param name="paramObject"> The dynamic object as parameters. </param>
        
       <returns> . </returns>
       **/
        public int ExecuteNonQuery(OracleTransaction sqlTransaction, string sql, CommandType commandType, dynamic paramObject)
        {
            return ExecuteNonQuery(sqlTransaction, sql, commandType, BuildSqlParameters(paramObject));

        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the non query operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public int ExecuteNonQuery(OracleTransaction sqlTransaction, string sql, CommandType commandType, params OracleParameter[] sqlParameters)
        {
            int result;
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, sql, commandType, sqlParameters);
				dbCommand.Connection.OpenSafely();
                result = dbCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the non query operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="paramObject"> The dynamic object as parameters. </param>        
        <returns> . </returns>
        **/
        public int ExecuteNonQuery(string sql, CommandType commandType, dynamic paramObject)
        {
            return ExecuteNonQuery(sql, commandType, BuildSqlParameters(paramObject));
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the non query operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public int ExecuteNonQuery(string sql, CommandType commandType, params OracleParameter[] sqlParameters)
        {
            int result;
            try
            {
                var dbCommand = CreateCommand(sql, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                result = dbCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
				if (_oracleTransaction == null && _oracleConnection.State != System.Data.ConnectionState.Closed)
                    _oracleConnection.CloseSafely();
            }
            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the scalar operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="T"> Generic type parameter. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public T ExecuteScalar<T>(OracleTransaction sqlTransaction, string sql, CommandType commandType, dynamic paramObject)
        {
            return ExecuteScalar<T>(sqlTransaction, sql, commandType, BuildSqlParameters(paramObject));
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the scalar operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="T"> Generic type parameter. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public T ExecuteScalar<T>(OracleTransaction sqlTransaction, string sql, CommandType commandType, params OracleParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, sql, commandType, sqlParameters);
				dbCommand.Connection.OpenSafely();
                var result = dbCommand.ExecuteScalar();

                return (T) Convert.ChangeType(result, typeof (T));
            }
            catch (Exception)
            {
                throw;
            }
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the scalar operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="T"> Generic type parameter. </typeparam>
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public T ExecuteScalar<T>(string sql, CommandType commandType, dynamic paramObject)
        {
            return ExecuteScalar<T>(sql, commandType, BuildSqlParameters(paramObject));

        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the scalar operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="T"> Generic type parameter. </typeparam>
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public T ExecuteScalar<T>(string sql, CommandType commandType, params OracleParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sql, commandType, sqlParameters);
                dbCommand.Connection.Open();
                var result = dbCommand.ExecuteScalar();

                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
				if (_oracleTransaction == null && _oracleConnection.State != System.Data.ConnectionState.Closed)
                    _oracleConnection.CloseSafely();
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , dynamic paramObject = null)
            where TEntity : new()
        {
            return ExecuteReader<TEntity>(sql, commandType
                , fieldsToSkip, piList
                , BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , params OracleParameter[] sqlParameters)
            where TEntity : new()
        {
            try
            {
                var dbCommand = CreateCommand(sql, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
				using (var reader = dbCommand.ExecuteReader())
				{
					return reader.DataReaderToObjectList<TEntity>(fieldsToSkip, piList);
				}
                //return dbCommand.ExecuteReader().DataReaderToObjectList<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
				if (_oracleTransaction == null && _oracleConnection.State != System.Data.ConnectionState.Closed)
                    _oracleConnection.CloseSafely();
            }
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public List<TEntity> ExecuteReader<TEntity>(OracleTransaction sqlTransaction, string sql
            , CommandType commandType, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)
            where TEntity : new()
        {
            return ExecuteReader<TEntity>(sqlTransaction, sql, commandType
                , fieldsToSkip, piList
                , BuildSqlParameters(paramObject));
        }
        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public List<TEntity> ExecuteReader<TEntity>(OracleTransaction sqlTransaction, string sql
            , CommandType commandType, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> piList = null, params OracleParameter[] sqlParameters)
            where TEntity : new()
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, sql, commandType, sqlParameters);
				dbCommand.Connection.OpenSafely();
				using (var reader = dbCommand.ExecuteReader())
				{
					return reader.DataReaderToObjectList<TEntity>(fieldsToSkip, piList);
				}
                //return dbCommand.ExecuteReader().DataReaderToObjectList<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public TEntity ExecuteReaderSingle<TEntity>(string sql, CommandType commandType
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , dynamic paramObject = null)
            where TEntity : class , new()
        {
            return ExecuteReaderSingle(sql, commandType, fieldsToSkip, piList, BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public TEntity ExecuteReaderSingle<TEntity>(string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params OracleParameter[] sqlParameters)
            where TEntity : class , new()
        {
            try
            {
                var dbCommand = CreateCommand(sql, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
				using (var reader = dbCommand.ExecuteReader())
				{
				    return reader.HasRows ? reader.DataReaderToObject<TEntity>(fieldsToSkip, piList) : null;
				}
                //return dbCommand.ExecuteReader().DataReaderToObject<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_oracleTransaction == null && _oracleConnection.State != System.Data.ConnectionState.Closed)
                    _oracleConnection.CloseSafely();
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public TEntity ExecuteReaderSingle<TEntity>(OracleTransaction sqlTransaction, string sql
            , CommandType commandType, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)
            where TEntity : class, new()
        {
            return ExecuteReaderSingle<TEntity>(sqlTransaction, sql, commandType, fieldsToSkip, piList, BuildSqlParameters(paramObject));

        }
        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <typeparam name="TEntity"> Type of the entity. </typeparam>
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public TEntity ExecuteReaderSingle<TEntity>(OracleTransaction sqlTransaction, string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params OracleParameter[] sqlParameters)
            where TEntity : class, new()
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, sql, commandType, sqlParameters);
				dbCommand.Connection.OpenSafely();
				using (var reader = dbCommand.ExecuteReader())
				{
                    return reader.HasRows ? reader.DataReaderToObject<TEntity>(fieldsToSkip, piList) : null;
                }
                //return dbCommand.ExecuteReader().DataReaderToObject<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> A list of. </returns>
        **/
        public IList<dynamic> ExecuteReader(OracleTransaction sqlTransaction, string sql
            , CommandType commandType, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)
        {
            return ExecuteReader(sqlTransaction, sql, commandType, fieldsToSkip, piList
                , BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> A list of. </returns>
        **/
        public IList<dynamic> ExecuteReader(OracleTransaction sqlTransaction, string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params OracleParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, sql, commandType, sqlParameters);
				dbCommand.Connection.OpenSafely();
                return GetDynamicSqlData(dbCommand.ExecuteReader());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /**--------------------------------------------------------------------------------------------------
       <summary> Executes the reader operation. </summary>
        
       <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
       <param name="sql">			 The SQL. </param>
       <param name="commandType">   Type of the command. </param>
       <param name="fieldsToSkip">  (optional) the fields to skip. </param>
       <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
       <returns> A list of. </returns>
       **/
        public IList<dynamic> ExecuteReader(string sql, CommandType commandType
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , dynamic paramObject = null)
        {
            return ExecuteReader(sql, commandType, fieldsToSkip, piList
                , BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> A list of. </returns>
        **/
        public IList<dynamic> ExecuteReader(string sql, CommandType commandType
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , params OracleParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sql, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                return GetDynamicSqlData(dbCommand.ExecuteReader());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
				if (_oracleTransaction == null && _oracleConnection.State != System.Data.ConnectionState.Closed)
                    _oracleConnection.CloseSafely();
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public dynamic ExecuteReaderSingle(string sql, CommandType commandType
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null
            , dynamic paramObject = null)
        {


            return ExecuteReaderSingle(sql, commandType, fieldsToSkip, piList
                , BuildSqlParameters(paramObject));
 
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sql">			 The SQL. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="fieldsToSkip">  (optional) the fields to skip. </param>
        <param name="piList">		 (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public dynamic ExecuteReaderSingle(string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params OracleParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sql, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader();
                if (reader.Read())
                {
                    var result = SqlDataReaderToExpando(reader);
                    reader.Close();
                    return result;
                }
                    
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
				if (_oracleTransaction == null && _oracleConnection.State != System.Data.ConnectionState.Closed)
                    _oracleConnection.CloseSafely();
            }
        }


        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
         <param name="paramObject"> The dynamic object as parameters. </param>
        
        <returns> . </returns>
        **/
        public dynamic ExecuteReaderSingle(OracleTransaction sqlTransaction, string sql
            , CommandType commandType, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)
        {
            return ExecuteReaderSingle(sqlTransaction, sql, commandType, fieldsToSkip, piList
                , BuildSqlParameters(paramObject));
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Executes the reader single operation. </summary>
        
        <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="sql">			  The SQL. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="fieldsToSkip">   (optional) the fields to skip. </param>
        <param name="piList">		  (optional) dictionary of property name and PropertyInfo object. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> . </returns>
        **/
        public dynamic ExecuteReaderSingle(OracleTransaction sqlTransaction, string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params OracleParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, sql, commandType, sqlParameters);
				dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader();
                if (reader.Read())
                    return SqlDataReaderToExpando(reader);
                
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Ends a transaction. </summary>
        
        <param name="sqlTransaction">	  The SQL transaction. </param>
        <param name="transactionSucceed"> (optional) the transaction succeed. </param>
        <param name="closeConnection">    (optional) the close connection. </param>
        **/
        public void EndTransaction(OracleTransaction sqlTransaction, bool transactionSucceed = true, bool closeConnection = true)
        {
            if (transactionSucceed)
            {
                sqlTransaction.Commit();
            }
            else
            {
                sqlTransaction.Rollback();
            }

            if (closeConnection)
            {
                _oracleConnection.CloseSafely();
            }
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Creates a command. </summary>
        
        <param name="queryString">   The query string. </param>
        <param name="commandType">   Type of the command. </param>
        <param name="sqlParameters"> Options for controlling the SQL. </param>
        
        <returns> The new command. </returns>
        **/
        private OracleCommand CreateCommand(string queryString, CommandType commandType, params OracleParameter[] sqlParameters)
        {
            var dbCommand = _oracleConnection.CreateCommand();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = queryString;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);

			if (_oracleTransaction != null)
				dbCommand.Transaction = _oracleTransaction;

			return dbCommand;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Creates a command. </summary>
        
        <param name="sqlTransaction"> The SQL transaction. </param>
        <param name="queryString">    The query string. </param>
        <param name="commandType">    Type of the command. </param>
        <param name="sqlParameters">  Options for controlling the SQL. </param>
        
        <returns> The new command. </returns>
        **/
        private OracleCommand CreateCommand(OracleTransaction sqlTransaction, string queryString, CommandType commandType, params OracleParameter[] sqlParameters)
        {
            var dbCommand = _oracleConnection.CreateCommand();
            dbCommand.Transaction = sqlTransaction;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = queryString;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);
			if (_oracleTransaction != null)
				dbCommand.Transaction = _oracleTransaction;

            return dbCommand;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> SQL data reader to expando. </summary>
        
        <param name="reader"> The reader. </param>
        
        <returns> . </returns>
        **/
        private dynamic SqlDataReaderToExpando(OracleDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
                expandoObject.Add(reader.GetName(i), reader[i]);

            return expandoObject;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Gets a dynamic SQL data. </summary>
        
        <param name="reader"> The reader. </param>
        
        <returns> The dynamic SQL data. </returns>
        **/
        private IList<dynamic> GetDynamicSqlData(OracleDataReader reader)
        {
            var result = new List<dynamic>();
            
            while (reader.Read())
            {
                result.Add(SqlDataReaderToExpando(reader));
            }
            return result;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Build OracleParameter Array from dynamic object. </summary>
         <param name="paramObject"> The dynamic object as parameters. </param>
        <returns> OracleParameter[] object and if paramObject is null then return null </returns>
        **/
        private static OracleParameter[] BuildSqlParameters(dynamic paramObject)
        {
            if (paramObject == null)
                return null;
            var sqlParameters = new List<OracleParameter>();
            sqlParameters.CreateOracleParametersFromDynamic(paramObject as Object);
            
            return sqlParameters.ToArray();
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        unmanaged resources. </summary>
        **/
        public void Dispose()
        {
			if (_oracleTransaction != null)
				_oracleTransaction.Dispose();

            if (_oracleConnection.State != ConnectionState.Closed)
                _oracleConnection.Close();
        }

    }

}
