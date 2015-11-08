using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;
using SimpleAccess.DbExtensions;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Sql Server implementaion for SimpleAccess.
    /// </summary>
    public class SqlSimpleAccess : ISqlSimpleAccess
    {
        private const string DefaultConnectionStringKey = "simpleAccess:sqlConnectionStringName";

        /// <summary>
        /// Default connection string.
        /// </summary>
        public static string DefaultConnectionString { get; set; }

        /// <summary>
        /// SimpleLogger to log exception
        /// </summary>
        public ISimpleLogger SimpleLogger { get { return DefaultSimpleAccessSettings.DefaultLogger; } }

        /// <summary>
        /// Default settings for simple access
        /// </summary>
        public SimpleAccessSettings DefaultSimpleAccessSettings { get; set; }

        /// <summary> The SQL connection. </summary>
        private readonly SqlConnection _sqlConnection;

		/// <summary> The SQL transaction. </summary>

        private SqlTransaction _sqlTransaction;


        #region Constructors

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>

        public SqlSimpleAccess(SqlConnection sqlConnection)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings
            {
                DefaultCommandType = CommandType.Text, DefaultLogger = new SimpleLogger()
            };
            _sqlConnection = sqlConnection;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>
        /// <param name="defaultCommandType"> The default command type for all queries </param>

        public SqlSimpleAccess(SqlConnection sqlConnection, CommandType defaultCommandType)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings (defaultCommandType );
            _sqlConnection = sqlConnection;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>

        public SqlSimpleAccess(SqlConnection sqlConnection, SimpleAccessSettings defaultSimpleAccessSettings)
        {
            DefaultSimpleAccessSettings = defaultSimpleAccessSettings;
            _sqlConnection = sqlConnection;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        public SqlSimpleAccess(string connection)
            : this(new SqlConnection(SimpleAccessSettings.GetProperConnectionString(connection)))
        {
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        /// <param name="defaultCommandType"> The default command type for all queries </param>
        public SqlSimpleAccess(string connection, CommandType defaultCommandType)
            : this(new SqlConnection(SimpleAccessSettings.GetProperConnectionString(connection)), defaultCommandType)
        {
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>
        public SqlSimpleAccess(string connection, SimpleAccessSettings defaultSimpleAccessSettings)
            : this(new SqlConnection(SimpleAccessSettings.GetProperConnectionString(connection)), defaultSimpleAccessSettings)
        {
        }

        /// <summary> Default constructor. </summary>
        public SqlSimpleAccess()
            : this(new SqlConnection(DefaultConnectionString))
        {
        }

        /// <summary> Default constructor. </summary>
        /// <param name="defaultCommandType"> The default command type for all queries </param>
        public SqlSimpleAccess(CommandType defaultCommandType)
            : this(new SqlConnection(DefaultConnectionString), defaultCommandType)
        {
        }


        /// <summary> Default constructor. </summary>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>
        public SqlSimpleAccess(SimpleAccessSettings defaultSimpleAccessSettings)
            : this(new SqlConnection(DefaultConnectionString), defaultSimpleAccessSettings)
        {
        }
        /// <summary>
        /// Static constructor to load default connection string from default configuration file
        /// </summary>
        static SqlSimpleAccess()
        {
            var connectionStringName = ConfigurationManager.AppSettings[DefaultConnectionStringKey];
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings != null)
            {
                DefaultConnectionString = connectionStringSettings.ConnectionString;
            }
        }
        #endregion


        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int ExecuteNonQuery(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);

        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int ExecuteNonQuery(string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            int result;
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                result = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();
            }
            return result;
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int ExecuteNonQuery(string commandText, dynamic paramObject = null)
        {
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText">			The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType,
            dynamic paramObject = null)
        {
            return ExecuteNonQuery(commandText, commandType, BuildSqlParameters(paramObject));
        }

        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQuery(sqlTransaction, commandText,  DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, params SqlParameter[] sqlParameters)
        {
            int result;
            try
            {
                var sqlCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                sqlCommand.Connection.OpenSafely();
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            return result;

        }

        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText, dynamic paramObject = null)
        {
            return ExecuteNonQuery(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
        }

        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, dynamic paramObject = null)
        {
            return ExecuteNonQuery(sqlTransaction, commandText, commandType, BuildSqlParameters(paramObject));

        }

        public T ExecuteScalar<T>(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteScalar<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , sqlParameters);
        }

        public T ExecuteScalar<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.Open();
                var result = dbCommand.ExecuteScalar();

                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();
            }
        }

        public T ExecuteScalar<T>(string commandText, dynamic paramObject = null)
        {
            return ExecuteScalar<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildSqlParameters(paramObject));
        }

        public T ExecuteScalar<T>(string commandText, CommandType commandType, dynamic paramObject = null)
        {
            return ExecuteScalar<T>(commandText, commandType
                , BuildSqlParameters(paramObject));
        }

        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , sqlParameters);
        }

        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                var result = dbCommand.ExecuteScalar();

                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText, dynamic paramObject = null)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildSqlParameters(paramObject));
        }

        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, dynamic paramObject = null)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, commandType
                , BuildSqlParameters(paramObject));
        }


        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                var result = dbCommand.ExecuteReader();
                return result;
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) 
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, sqlParameters);
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SqlParameter[] sqlParameters) where TEntity : new()
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
                {
                    return reader.DataReaderToObjectList<TEntity>(fieldsToSkip, propertyInfoDictionary);
                }
                //return dbCommand.ExecuteReader().DataReaderToObjectList<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();
            }
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null) 
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip
                , propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : new()
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
                {
                    return reader.DataReaderToObjectList<TEntity>(fieldsToSkip, propertyInfoDictionary);
                }
                //return dbCommand.ExecuteReader().DataReaderToObjectList<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, commandType
                , fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        public TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.HasRows ? reader.DataReaderToObject<TEntity>(fieldsToSkip, propertyInfoDictionary) : null;
                }
                //return dbCommand.ExecuteReader().DataReaderToObject<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();
            }
        }

        public TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            dynamic paramObject = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
                {
                    return reader.HasRows ? reader.DataReaderToObject<TEntity>(fieldsToSkip, propertyInfoDictionary) : null;
                }
                //return dbCommand.ExecuteReader().DataReaderToObject<TEntity>(fieldsToSkip, piList);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null, dynamic paramObject = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        public IEnumerable<dynamic> ExecuteDynamics(string commandText, string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamics(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
        }

        public IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                return GetDynamicSqlData(dbCommand.ExecuteReader());
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();
            }
        }

        public IEnumerable<dynamic> ExecuteDynamics(string commandText, string fieldsToSkip = null, dynamic paramObject = null)
        {
            return ExecuteDynamics(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        public IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType, string fieldsToSkip = null,
            dynamic paramObject = null)
        {
            return ExecuteDynamics(commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamics(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
        }

        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                return GetDynamicSqlData(dbCommand.ExecuteReader());
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            dynamic paramObject = null)
        {
            return ExecuteDynamics(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, dynamic paramObject = null)
        {
            return ExecuteDynamics(sqlTransaction, commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        public dynamic ExecuteDynamic(string commandText, string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamic(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
        }

        public dynamic ExecuteDynamic(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow);
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
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();
            }
        }

        public dynamic ExecuteDynamic(string commandText, string fieldsToSkip = null, dynamic paramObject = null)
        {
            return ExecuteDynamic(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        public dynamic ExecuteDynamic(string commandText, CommandType commandType, string fieldsToSkip = null,
            dynamic paramObject = null)
        {
            return ExecuteDynamic(commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamic(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, sqlParameters);
        }

        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader();
                if (reader.Read())
                    return SqlDataReaderToExpando(reader);

                return null;
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
        }

        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            dynamic paramObject = null)
        {
            return ExecuteDynamic(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, BuildSqlParameters(paramObject));
        }

        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, dynamic paramObject = null)
        {
            return ExecuteDynamic(sqlTransaction, commandText, commandType,
                fieldsToSkip, BuildSqlParameters(paramObject));
        }

        public int Fill(string commandText, DataTable dataTable)
        {
            try
            {
                if (dataTable == null)
                    dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(commandText));
                return sqlDataAdapter.Fill(dataTable);

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);

                throw;
            }
        }

        public int Fill(string commandText, DataSet dataSet)
        {
            try
            {
                if (dataSet == null)
                    dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(commandText));
                return sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);

                throw;
            }
        }

        /// <summary> Gets the new connection. </summary>
        /// <returns> The new connection. </returns>
        public SqlConnection GetNewConnection()
        {
            return new SqlConnection(DefaultConnectionString);
        }

        /// <summary> Close the current open connection. </summary>

        public void CloseCurrentDbConnection()
        {
            if (_sqlConnection != null)
                _sqlConnection.CloseSafely();
        }

        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SqlTransaction BeginTrasaction()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();
            //_sqlTransaction = _sqlConnection.BeginTransaction();

            //return _sqlTransaction;
            return _sqlConnection.BeginTransaction();
        }

        /// <summary> Ends a transaction. </summary>
        /// 
        /// <param name = "sqlTransaction" > The SQL transaction. </param>
        /// <param name = "transactionSucceed" > (optional)the transaction succeed. </param>
        /// <param name = "closeConnection" > (optional)the close connection. </param>
        public void EndTransaction(SqlTransaction sqlTransaction, bool transactionSucceed = true, bool closeConnection = true)
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
                _sqlConnection.CloseSafely();
            }
        }


        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText">   The query string. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SqlCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            var dbCommand = _sqlConnection.CreateCommand();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);

            if (_sqlTransaction != null)
                dbCommand.Transaction = _sqlTransaction;

            return dbCommand;
        }

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText">    The query string. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SqlCommand CreateCommand(SqlTransaction sqlTransaction, string commandText, CommandType commandType
            , params SqlParameter[] sqlParameters)
        {
            var dbCommand = _sqlConnection.CreateCommand();
            dbCommand.Transaction = sqlTransaction;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);
            if (_sqlTransaction != null)
                dbCommand.Transaction = _sqlTransaction;

            return dbCommand;
        }

        /// <summary> SQL data reader to expando. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        public dynamic SqlDataReaderToExpando(SqlDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var value = reader[i];
                expandoObject.Add(reader.GetName(i), value == DBNull.Value ? null : value);
            }


            return expandoObject;
        }

        /// <summary> Gets a dynamic SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The dynamic SQL data. </returns>
        public IList<dynamic> GetDynamicSqlData(SqlDataReader reader)
        {
            var result = new List<dynamic>();

            while (reader.Read())
            {
                result.Add(SqlDataReaderToExpando(reader));
            }
            return result;
        }

        /// <summary> Build SqlParameter Array from dynamic object. </summary>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// <returns> SqlParameter[] object and if paramObject is null then return null </returns>
        public SqlParameter[] BuildSqlParameters(dynamic paramObject)
        {
            if (paramObject == null)
                return null;
            var sqlParameters = new List<SqlParameter>();
            sqlParameters.CreateSqlParametersFromDynamic(paramObject as Object);

            return sqlParameters.ToArray();
        }


        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. </summary>
        public void Dispose()
        {
            if (_sqlTransaction != null)
                _sqlTransaction.Dispose();

            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();

            DefaultSimpleAccessSettings = null;
        }
    }
}
