using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.Oracle
{
    /// <summary>
    /// SimpleAccess implementation for Oracle.
    /// </summary>
    public class OracleSimpleAccess : IOracleSimpleAccess
    {
        private const string DefaultConnectionStringKey = "simpleAccess:oracleConnectionStringName";

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
        private readonly OracleConnection _sqlConnection;

		/// <summary> The SQL transaction. </summary>

        private OracleTransaction _sqlTransaction;


        #region Constructors

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>

        public OracleSimpleAccess(OracleConnection sqlConnection)
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

        public OracleSimpleAccess(OracleConnection sqlConnection, CommandType defaultCommandType)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings (defaultCommandType );
            _sqlConnection = sqlConnection;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlConnection"> The SQL connection. </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>

        public OracleSimpleAccess(OracleConnection sqlConnection, SimpleAccessSettings defaultSimpleAccessSettings)
        {
            DefaultSimpleAccessSettings = defaultSimpleAccessSettings;
            _sqlConnection = sqlConnection;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        public OracleSimpleAccess(string connection)
            : this(new OracleConnection(SimpleAccessSettings.GetProperConnectionString(connection)))
        {
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        /// <param name="defaultCommandType"> The default command type for all queries </param>
        public OracleSimpleAccess(string connection, CommandType defaultCommandType)
            : this(new OracleConnection(SimpleAccessSettings.GetProperConnectionString(connection)), defaultCommandType)
        {
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>
        public OracleSimpleAccess(string connection, SimpleAccessSettings defaultSimpleAccessSettings)
            : this(new OracleConnection(SimpleAccessSettings.GetProperConnectionString(connection)), defaultSimpleAccessSettings)
        {
        }

        /// <summary> Default constructor. </summary>
        public OracleSimpleAccess()
            : this(new OracleConnection(DefaultConnectionString))
        {
        }

        /// <summary> Default constructor. </summary>
        /// <param name="defaultCommandType"> The default command type for all queries </param>
        public OracleSimpleAccess(CommandType defaultCommandType)
            : this(new OracleConnection(DefaultConnectionString), defaultCommandType)
        {
        }


        /// <summary> Default constructor. </summary>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>
        public OracleSimpleAccess(SimpleAccessSettings defaultSimpleAccessSettings)
            : this(new OracleConnection(DefaultConnectionString), defaultSimpleAccessSettings)
        {
        }
        /// <summary>
        /// Static constructor to load default connection string from default configuration file
        /// </summary>
        static OracleSimpleAccess()
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
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, params OracleParameter[] oracleParameters)
        {
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, oracleParameters);

        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType,
            params OracleParameter[] oracleParameters)
        {
            int result;
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildOracleParameters(paramObject));
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
            return ExecuteNonQuery(commandText, commandType, BuildOracleParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. 
        /// </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(OracleTransaction transaction, string commandText, params OracleParameter[] oracleParameters)
        {
            return ExecuteNonQuery(transaction, commandText,  DefaultSimpleAccessSettings.DefaultCommandType, oracleParameters);
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
        public int ExecuteNonQuery(OracleTransaction sqlTransaction, string commandText,
            CommandType commandType, params OracleParameter[] parameters)
        {
            int result;
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, parameters);
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
                dbCommand.ClearDbCommand();
            }
            return result;

        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(OracleTransaction sqlTransaction, string commandText, object paramObject = null)
        {
            return ExecuteNonQuery(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildOracleParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(OracleTransaction sqlTransaction, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteNonQuery(sqlTransaction, commandText, commandType, BuildOracleParameters(paramObject));

        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public T ExecuteScalar<T>(string commandText, params OracleParameter[] oracleParameters)
        {
            return ExecuteScalar<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , oracleParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(string commandText, CommandType commandType, params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
                , BuildOracleParameters(paramObject));
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
                , BuildOracleParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(OracleTransaction sqlTransaction, string commandText, params OracleParameter[] oracleParameters)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , oracleParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public T ExecuteScalar<T>(OracleTransaction sqlTransaction, string commandText,
            CommandType commandType, params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, oracleParameters);
                dbCommand.Connection.OpenSafely();
                var result = dbCommand.ExecuteScalar();

                return (T) Convert.ChangeType(result, typeof (T));
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
        public T ExecuteScalar<T>(OracleTransaction sqlTransaction, string commandText, object paramObject = null)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildOracleParameters(paramObject));
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
        public T ExecuteScalar<T>(OracleTransaction sqlTransaction, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteScalar<T>(sqlTransaction, commandText, commandType
                , BuildOracleParameters(paramObject));
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        public OracleDataReader ExecuteReader(string commandText, params OracleParameter[] oracleParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, oracleParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public OracleDataReader ExecuteReader(string commandText, CommandType commandType,
            params OracleParameter[] oracleParameters)
        {
            return ExecuteReader(commandText, commandType, CommandBehavior.Default, oracleParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public OracleDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior,
            params OracleParameter[] oracleParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, commandBehavior, oracleParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public OracleDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior,
            params OracleParameter[] oracleParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
        public OracleDataReader ExecuteReader(string commandText, object paramObject = null)
        {
            return ExecuteReader(commandText, BuildOracleParameters(paramObject));
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
        public OracleDataReader ExecuteReader(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteReader(commandText, commandType, BuildOracleParameters(paramObject));
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
        public OracleDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior, object paramObject = null)
        {
            return ExecuteReader(commandText, commandBehavior, BuildOracleParameters(paramObject));
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
        public OracleDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior, object paramObject = null)
        {
            return ExecuteReader(commandText, commandType, commandBehavior, BuildOracleParameters(paramObject));
        }



        
        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(string commandText, params OracleParameter[] oracleParameters)
        {
            return ExecuteValues<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildOracleParameters(oracleParameters));
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
            return ExecuteValues<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,  BuildOracleParameters(paramObject));
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
            return ExecuteValues<T>(commandText, commandType, BuildOracleParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(string commandText, CommandType commandType, params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
                if (_sqlTransaction == null && _sqlConnection.State != ConnectionState.Closed)
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
        public IEnumerable<T> ExecuteValues<T>(OracleTransaction transaction, string commandText, object paramObject = null)
        {
            return ExecuteValues<T>(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildOracleParameters(paramObject));
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
        public IEnumerable<T> ExecuteValues<T>(OracleTransaction transaction, string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValues<T>(transaction, commandText, commandType, BuildOracleParameters(paramObject));
        }
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(OracleTransaction transaction, string commandText,
                                             params OracleParameter[] oracleParameters)
        {
            return ExecuteValues<T>(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, oracleParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="oracleTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(OracleTransaction oracleTransaction, string commandText, CommandType commandType,
            params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(oracleTransaction, commandText, commandType, oracleParameters);
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

                dbCommand.ClearDbCommand();

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
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TEntity value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params OracleParameter[] oracleParameters) 
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, oracleParameters);
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
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params OracleParameter[] oracleParameters) where TEntity : new()
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
                , propertyInfoDictionary, BuildOracleParameters(paramObject));
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
                , propertyInfoDictionary, BuildOracleParameters(paramObject));
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
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(OracleTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params OracleParameter[] oracleParameters)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, oracleParameters);
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
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(OracleTransaction sqlTransaction, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params OracleParameter[] oracleParameters) where TEntity : new()
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, oracleParameters);
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
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(OracleTransaction sqlTransaction, string commandText, object paramObject = null
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, BuildOracleParameters(paramObject));
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
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(OracleTransaction sqlTransaction, string commandText,
            CommandType commandType, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(sqlTransaction, commandText, commandType
                , fieldsToSkip, propertyInfoDictionary, BuildOracleParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            params OracleParameter[] oracleParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, oracleParameters);
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
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params OracleParameter[] oracleParameters) where TEntity : class, new()
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
                fieldsToSkip, propertyInfoDictionary, BuildOracleParameters(paramObject));
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
                fieldsToSkip, propertyInfoDictionary, BuildOracleParameters(paramObject));
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
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(OracleTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params OracleParameter[] oracleParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, oracleParameters);
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
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(OracleTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params OracleParameter[] oracleParameters) where TEntity : class, new()
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, oracleParameters);
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
        public TEntity ExecuteEntity<TEntity>(OracleTransaction sqlTransaction, string commandText, object paramObject = null, 
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildOracleParameters(paramObject));
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
        public TEntity ExecuteEntity<TEntity>(OracleTransaction sqlTransaction, string commandText, CommandType commandType, 
            object paramObject = null, string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) 
            where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(sqlTransaction, commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildOracleParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, string fieldsToSkip = null, params OracleParameter[] oracleParameters)
        {
            return ExecuteDynamics(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                oracleParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType, string fieldsToSkip = null,
            params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
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
                BuildOracleParameters(paramObject));
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
                BuildOracleParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(OracleTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            params OracleParameter[] oracleParameters)
        {
            return ExecuteDynamics(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                oracleParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(OracleTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, oracleParameters);
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
                dbCommand.ClearDbCommand();

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
        public IEnumerable<dynamic> ExecuteDynamics(OracleTransaction sqlTransaction, string commandText, object paramObject = null, 
            string fieldsToSkip = null)
        {
            return ExecuteDynamics(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildOracleParameters(paramObject));
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
        public IEnumerable<dynamic> ExecuteDynamics(OracleTransaction sqlTransaction, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamics(sqlTransaction, commandText, commandType, fieldsToSkip,
                BuildOracleParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(string commandText, string fieldsToSkip = null, params OracleParameter[] oracleParameters)
        {
            return ExecuteDynamic(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                oracleParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(string commandText, CommandType commandType, string fieldsToSkip = null,
            params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, oracleParameters);
                dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    var result = OracleDataReaderToExpando(reader);
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
                BuildOracleParameters(paramObject));
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
                BuildOracleParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(OracleTransaction sqlTransaction, string commandText, string fieldsToSkip = null,
            params OracleParameter[] oracleParameters)
        {
            return ExecuteDynamic(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, oracleParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="oracleParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(OracleTransaction sqlTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params OracleParameter[] oracleParameters)
        {
            OracleCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(sqlTransaction, commandText, commandType, oracleParameters);
                dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader();
                if (reader.Read())
                    return OracleDataReaderToExpando(reader);

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
        public dynamic ExecuteDynamic(OracleTransaction sqlTransaction, string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(sqlTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, BuildOracleParameters(paramObject));
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
        public dynamic ExecuteDynamic(OracleTransaction sqlTransaction, string commandText, CommandType commandType,
                object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(sqlTransaction, commandText, commandType,
                fieldsToSkip, BuildOracleParameters(paramObject));
        }
        /// <summary>
        /// Execute the CommandText against connection and add or refresh rows in <see cref="DataTable"/>
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dataTable">A <see cref="DataTable"/> to fill with records and, if necessary, schema  </param>
        /// <returns></returns>
        public int Fill(string commandText, DataTable dataTable)
        {
            OracleCommand dbCommand = null;
            try
            {
                if (dataTable == null)
                    dataTable = new DataTable();
                dbCommand = new OracleCommand(commandText);
                var oracleDataAdapter = new OracleDataAdapter(dbCommand);
                return oracleDataAdapter.Fill(dataTable);

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
            OracleCommand dbCommand = null;
            try
            {
                if (dataSet == null)
                    dataSet = new DataSet();

                dbCommand = new OracleCommand(commandText);

                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(dbCommand);
                return oracleDataAdapter.Fill(dataSet);
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

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary> Gets the new connection. </summary>
        /// <returns> The new connection. </returns>
        public OracleConnection GetNewConnection()
        {
            return new OracleConnection(_sqlConnection.DataSource);
        }

        /// <summary> Close the current open connection. </summary>
        public void CloseDbConnection()
        {
            if (_sqlConnection != null)
                _sqlConnection.CloseSafely();
        }

        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public OracleTransaction BeginTrasaction()
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
                _sqlConnection.CloseSafely();
            }
        }


        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public OracleCommand CreateCommand(string commandText, CommandType commandType, params OracleParameter[] oracleParameters)
        {
            var dbCommand = _sqlConnection.CreateCommand();
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (oracleParameters != null)
                dbCommand.Parameters.AddRange(oracleParameters);

            if (_sqlTransaction != null)
                dbCommand.Transaction = _sqlTransaction;

            return dbCommand;
        }

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public OracleCommand CreateCommand(OracleTransaction sqlTransaction, string commandText, CommandType commandType
            , params OracleParameter[] oracleParameters)
        {
            var dbCommand = _sqlConnection.CreateCommand();
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.Transaction = sqlTransaction;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (oracleParameters != null)
                dbCommand.Parameters.AddRange(oracleParameters);
            if (_sqlTransaction != null)
                dbCommand.Transaction = _sqlTransaction;

            return dbCommand;
        }

        /// <summary> Gets a object SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The object SQL data. </returns>
        public IList<T> GetValues<T>(OracleDataReader reader)
        {
            var result = new List<T>();

            while (reader.Read())
            {
                result.Add((T)Convert.ChangeType(reader[0], typeof(T)));
            }
            return result;
        }

        /// <summary> SQL data reader to <see cref="ExpandoObject"/>. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        public dynamic OracleDataReaderToExpando(OracleDataReader reader)
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
        public IList<dynamic> GetDynamicSqlData(OracleDataReader reader)
        {
            var result = new List<dynamic>();

            while (reader.Read())
            {
                result.Add(OracleDataReaderToExpando(reader));
            }
            return result;
        }

        /// <summary> Build OracleParameter Array from anonymous object. </summary>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> OracleParameter[] object and if paramObject is null then return null </returns>
        public OracleParameter[] BuildOracleParameters(object paramObject)
        {
            if (paramObject == null)
                return null;
            var oracleParameters = new List<OracleParameter>();
            oracleParameters.CreateOracleParametersFromObject(paramObject);

            return oracleParameters.ToArray();
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
