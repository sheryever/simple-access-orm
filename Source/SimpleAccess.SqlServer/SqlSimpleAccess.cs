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
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Sql Server implementation for SimpleAccess.
    /// </summary>
    public partial class SqlSimpleAccess : ISqlSimpleAccess

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

        //private SqlTransaction _sqlTransaction;


        #region Constructors

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>

        public SqlSimpleAccess(SqlConnection sqlConnection)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings
            {
                DefaultCommandType = CommandType.Text,
                DefaultLogger = new SimpleLogger()
            };
            _sqlConnection = sqlConnection;

        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>
        /// <param name="defaultCommandType"> The default command type for all queries </param>

        public SqlSimpleAccess(SqlConnection sqlConnection, CommandType defaultCommandType)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings(defaultCommandType);
            _sqlConnection = sqlConnection;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>

        public SqlSimpleAccess(SqlConnection sqlConnection, SimpleAccessSettings defaultSimpleAccessSettings)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings (defaultSimpleAccessSettings.DefaultCommandType, defaultSimpleAccessSettings.DefaultLogger);
            DefaultSimpleAccessSettings.DbCommandTimeout = defaultSimpleAccessSettings.DbCommandTimeout;

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
#if NET40
            var connectionStringName = ConfigurationManager.AppSettings[DefaultConnectionStringKey];
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings != null)
            {
                DefaultConnectionString = connectionStringSettings.ConnectionString;
            }
#endif
        }
#endregion


        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
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
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            int result;
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                result = dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
            return result;
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, object paramObject = null)
        {
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
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
        public int ExecuteNonQuery(string commandText, CommandType commandType,
            object paramObject = null)
        {
            return ExecuteNonQuery(commandText, commandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. 
        /// </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SqlTransaction transaction, string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQuery(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, parameters);
                dbCommand.Connection.OpenSafely();
                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                dbCommand.Parameters.Clear();

            }

        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText, object paramObject = null)
        {
            return ExecuteNonQuery(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteNonQuery(sqlTransaction, commandText, commandType, BuildSqlParameters(paramObject));

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
        public T ExecuteScalar<T>(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteScalar<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , sqlParameters);
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
        public T ExecuteScalar<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
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
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
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
        public T ExecuteScalar<T>(string commandText, object paramObject = null)
        {
            return ExecuteScalar<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
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
        public T ExecuteScalar<T>(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteScalar<T>(commandText, commandType
                , BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , sqlParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
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

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText, object paramObject = null)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, commandType
                , BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters"> Parameters required to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
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
        public SqlDataReader ExecuteReader(string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteReader(commandText, commandType, CommandBehavior.CloseConnection, sqlParameters);
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
        public SqlDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, commandBehavior, sqlParameters);
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
        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SqlParameter[] sqlParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                var result = dbCommand.ExecuteReader(commandBehavior);
                dbCommand.Parameters.Clear();
                return result;
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
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> The TDbDataReader </returns>
        public SqlDataReader ExecuteReader(string commandText, object paramObject = null)
        {
            return ExecuteReader(commandText, BuildSqlParameters(paramObject));
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
        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteReader(commandText, commandType, BuildSqlParameters(paramObject));
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
        public SqlDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior, object paramObject = null)
        {
            return ExecuteReader(commandText, commandBehavior, BuildSqlParameters(paramObject));
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
        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior, object paramObject = null)
        {
            return ExecuteReader(commandText, commandType, commandBehavior, BuildSqlParameters(paramObject));
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
        public IEnumerable<T> ExecuteValues<T>(string commandText, params SqlParameter[] sqlParameters)
        {
            return ExecuteValues<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
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
        public IEnumerable<T> ExecuteValues<T>(string commandText, object paramObject = null)
        {
            return ExecuteValues<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,  BuildSqlParameters(paramObject));
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
        public IEnumerable<T> ExecuteValues<T>(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValues<T>(commandText, commandType, BuildSqlParameters(paramObject));
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
        public IEnumerable<T> ExecuteValues<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
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
                if (dbCommand != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
        }

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
        public IEnumerable<T> ExecuteValues<T>(SqlTransaction transaction, string commandText, object paramObject = null)
        {
            return ExecuteValues<T>(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildSqlParameters(paramObject));
        }

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
        public IEnumerable<T> ExecuteValues<T>(SqlTransaction transaction, string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValues<T>(transaction, commandText, commandType, BuildSqlParameters(paramObject));
        }
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(SqlTransaction transaction, string commandText,
                                             params SqlParameter[] sqlParameters)
        {
            return ExecuteValues<T>(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, sqlParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
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
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, sqlParameters);
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
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SqlParameter[] sqlParameters) where TEntity : new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
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
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.Parameters.Clear();

                dbCommand.ClearDbCommand();
            }
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

        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
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
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip
                , propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
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
                dbCommand.Parameters.Clear();

                dbCommand.ClearDbCommand();

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText, object paramObject = null
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SqlTransaction sqlTransaction, string commandText,
            CommandType commandType, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, commandType
                , fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
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
        public TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, sqlParameters);
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
        public TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
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
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.Parameters.Clear();
                dbCommand.ClearDbCommand();

            }
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
        public TEntity ExecuteEntity<TEntity>(string commandText, object paramObject = null, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
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
        public TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
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
            finally
            {

                dbCommand.ClearDbCommand();

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildSqlParameters(paramObject));
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
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamics(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
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
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
                {
                    return GetDynamicSqlData(reader);
                }

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();

            }
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
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamics(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
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
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType, object paramObject = null,
            string fieldsToSkip = null)
        {
            return ExecuteDynamics(commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamics(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
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
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, object paramObject = null,
            string fieldsToSkip = null)
        {
            return ExecuteDynamics(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamics(sqlTransaction, commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
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
        public dynamic ExecuteDynamic(string commandText, string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamic(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                sqlParameters);
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
        public dynamic ExecuteDynamic(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, sqlParameters);
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
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();

            }
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
        public dynamic ExecuteDynamic(string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
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
        public dynamic ExecuteDynamic(string commandText, CommandType commandType, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(commandText, commandType, fieldsToSkip,
                BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDynamic(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, sqlParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="sqlParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SqlParameter[] sqlParameters)
        {
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, sqlParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                        return SqlDataReaderToExpando(reader);
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

                dbCommand.ClearDbCommand();

            }
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// -<param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, BuildSqlParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SqlTransaction sqlTransaction, string commandText, CommandType commandType,
                object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(sqlTransaction, commandText, commandType,
                fieldsToSkip, BuildSqlParameters(paramObject));
        }
        /// <summary>
        /// Execute the CommandText against connection and add or refresh rows in <see cref="DataTable"/>
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dataTable">A <see cref="DataTable"/> to fill with records and, if necessary, schema  </param>
        /// <returns></returns>
        public int Fill(string commandText, DataTable dataTable)
        {
            SqlCommand dbCommand = null;
            try
            {
                if (dataTable == null)
                    dataTable = new DataTable();
                dbCommand = new SqlCommand(commandText);
                var sqlDataAdapter = new SqlDataAdapter(dbCommand);
                return sqlDataAdapter.Fill(dataTable);

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);

                throw;
            }
            finally
            {
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary>
        /// Execute the CommandText against connection and add or refresh rows in <see cref="DataSet"/>
        /// </summary>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="dataSet"> A <see cref="DataSet"/> to fill with records and, if necessary, schema  </param>
        /// <returns></returns>
        public int Fill(string commandText, DataSet dataSet)
        {
            SqlCommand dbCommand = null;
            try
            {
                if (dataSet == null)
                    dataSet = new DataSet();

                dbCommand = new SqlCommand(commandText);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(dbCommand);
                return sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);

                throw;
            }
            finally
            {
                if (_sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary> Gets the new connection. </summary>
        /// <returns> The new connection. </returns>
        public SqlConnection GetNewConnection()
        {
            //return new SqlConnection(DefaultConnectionString);
            return new SqlConnection(_sqlConnection.ConnectionString);
        }

        /// <summary> Close the current open connection. </summary>
        public void CloseDbConnection()
        {
            if (_sqlConnection != null)
                _sqlConnection.CloseSafely();
        }

        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SqlTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted, null);
        }
        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SqlTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginTransaction(isolationLevel, null);

        }
        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SqlTransaction BeginTransaction(string transactionName)
        {
            return BeginTransaction(IsolationLevel.ReadCommitted, transactionName);
        }
        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SqlTransaction BeginTransaction(IsolationLevel isolationLevel, string transactionName)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            if (string.IsNullOrEmpty(transactionName))
            {
                return _sqlConnection.BeginTransaction(isolationLevel);
            }

            return _sqlConnection.BeginTransaction(isolationLevel, transactionName);
        }


        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SqlCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            var dbCommand = _sqlConnection.CreateCommand();
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);


            return dbCommand;
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
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SqlCommand CreateCommand(SqlTransaction sqlTransaction, string commandText, CommandType commandType
            , params SqlParameter[] sqlParameters)
        {
            var dbCommand = _sqlConnection.CreateCommand();
            dbCommand.Transaction = sqlTransaction;
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (sqlParameters != null)
                dbCommand.Parameters.AddRange(sqlParameters);
           

            return dbCommand;
        }

        /// <summary> SQL data reader to <see cref="ExpandoObject"/>. </summary>
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

        /// <summary> Gets a object SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The object SQL data. </returns>
        public IList<dynamic> GetDynamicSqlData(SqlDataReader reader)
        {
            var result = new List<dynamic>();

            while (reader.Read())
            {
                result.Add(SqlDataReaderToExpando(reader));
            }
            return result;
        }

        /// <summary> Gets a object SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The object SQL data. </returns>
        public IList<T> GetValues<T>(SqlDataReader reader)
        {
            var result = new List<T>();

            while (reader.Read())
            {
                result.Add((T)Convert.ChangeType(reader[0], typeof(T)));
            }
            return result;
        }

        /// <summary> Build SqlParameter Array from anonymous object. </summary>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> SqlParameter[] object and if paramObject is null then return null </returns>
        public SqlParameter[] BuildSqlParameters(object paramObject)
        {
            if (paramObject == null)
                return null;
            var sqlParameters = new List<SqlParameter>();
            sqlParameters.CreateSqlParametersFromObject(paramObject);

            return sqlParameters.ToArray();
        }

        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. </summary>
        public void Dispose()
        {

            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();

            DefaultSimpleAccessSettings = null;
        }

    }
}
