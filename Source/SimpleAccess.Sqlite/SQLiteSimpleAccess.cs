using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Dynamic;
using System.Reflection;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Sql Server implementation for SimpleAccess.
    /// </summary>
    public partial class SQLiteSimpleAccess : ISQLiteSimpleAccess

    {
        private const string DefaultConnectionStringKey = "DefaultConnection";

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
        private readonly SQLiteConnection _SQLiteConnection;

        #region Constructors

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="SQLiteConnection"> The SQL connection. </param>

        public SQLiteSimpleAccess(SQLiteConnection SQLiteConnection)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings(CommandType.Text, new SimpleLogger());
            
            _SQLiteConnection = SQLiteConnection;
            DefaultConnectionString = _SQLiteConnection.ConnectionString;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="SQLiteConnection"> The SQL connection. </param>
        /// <param name="defaultCommandType"> The default command type for all queries </param>

        public SQLiteSimpleAccess(SQLiteConnection SQLiteConnection, CommandType defaultCommandType)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings(defaultCommandType);
            _SQLiteConnection = SQLiteConnection;
            DefaultConnectionString = _SQLiteConnection.ConnectionString;

        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="SQLiteConnection"> The SQL connection. </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>

        public SQLiteSimpleAccess(SQLiteConnection SQLiteConnection, SimpleAccessSettings defaultSimpleAccessSettings)
        {
            DefaultSimpleAccessSettings = new SimpleAccessSettings (defaultSimpleAccessSettings.DefaultCommandType, defaultSimpleAccessSettings.DefaultLogger);
            DefaultSimpleAccessSettings.DbCommandTimeout = defaultSimpleAccessSettings.DbCommandTimeout;

            _SQLiteConnection = SQLiteConnection;
            DefaultConnectionString = _SQLiteConnection.ConnectionString;

        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        public SQLiteSimpleAccess(string connection)
            : this(new SQLiteConnection(connection))
        {
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        /// <param name="defaultCommandType"> The default command type for all queries </param>
        public SQLiteSimpleAccess(string connection, CommandType defaultCommandType)
            : this(new SQLiteConnection(connection), defaultCommandType)
        {
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The ConnectionString Name from the config file or a complete ConnectionString . </param>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>
        public SQLiteSimpleAccess(string connection, SimpleAccessSettings defaultSimpleAccessSettings)
            : this(new SQLiteConnection(connection), defaultSimpleAccessSettings)
        {
        }

        /// <summary> Default constructor. </summary>
        public SQLiteSimpleAccess()
            : this(new SQLiteConnection(DefaultConnectionString))
        {
        }

        /// <summary> Default constructor. </summary>
        /// <param name="defaultCommandType"> The default command type for all queries </param>
        public SQLiteSimpleAccess(CommandType defaultCommandType)
            : this(new SQLiteConnection(DefaultConnectionString), defaultCommandType)
        {
        }


        /// <summary> Default constructor. </summary>
        /// <param name="defaultSimpleAccessSettings"> The default settings for simple access </param>
        public SQLiteSimpleAccess(SimpleAccessSettings defaultSimpleAccessSettings)
            : this(new SQLiteConnection(DefaultConnectionString), defaultSimpleAccessSettings)
        {
        }
        /// <summary>
        /// Static constructor to load default connection string from default configuration file
        /// </summary>
        static SQLiteSimpleAccess()
        {
#if NET452
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
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes the non query operation. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType,
            params SQLiteParameter[] SQLiteParameters)
        {
            int result;
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
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
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
            return ExecuteNonQuery(commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
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
            return ExecuteNonQuery(commandText, commandType, BuildDbParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. 
        /// </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SQLiteTransaction transaction, string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteNonQuery(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="parameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SQLiteTransaction SQLiteTransaction, string commandText,
            CommandType commandType, params SQLiteParameter[] parameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, parameters);
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
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SQLiteTransaction SQLiteTransaction, string commandText, object paramObject = null)
        {
            return ExecuteNonQuery(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
        }

        /// <summary> Executes a command text against the connection and returns the number of rows affected. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        public int ExecuteNonQuery(SQLiteTransaction SQLiteTransaction, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteNonQuery(SQLiteTransaction, commandText, commandType, BuildDbParameters(paramObject));

        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public T ExecuteScalar<T>(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteScalar<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(string commandText, CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
                dbCommand.Connection.Open();
                var result = dbCommand.ExecuteScalar();

                if (result == DBNull.Value) return default(T);

                return (T)Convert.ChangeType(result, typeof(T));
                //if (typeof(T).Name.IndexOf("Nullable") > -1)
                    

                //return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
                throw;
            }
            finally
            {
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
                , BuildDbParameters(paramObject));
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
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(SQLiteTransaction SQLiteTransaction, string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteScalar<T>(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public T ExecuteScalar<T>(SQLiteTransaction SQLiteTransaction, string commandText,
            CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, SQLiteParameters);
                dbCommand.Connection.OpenSafely();
                var result = dbCommand.ExecuteScalar();

                return (T)result;

                //return (T)Convert.ChangeType(result, typeof(T));
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
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(SQLiteTransaction SQLiteTransaction, string commandText, object paramObject = null)
        {
            return ExecuteScalar<T>(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// 
        /// <returns> The {T} value </returns>
        public T ExecuteScalar<T>(SQLiteTransaction SQLiteTransaction, string commandText,
            CommandType commandType, object paramObject = null)
        {
            return ExecuteScalar<T>(SQLiteTransaction, commandText, commandType
                , BuildDbParameters(paramObject));
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// <returns> The TDbDataReader </returns>
        public SQLiteDataReader ExecuteReader(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public SQLiteDataReader ExecuteReader(string commandText, CommandType commandType,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteReader(commandText, commandType, CommandBehavior.CloseConnection, SQLiteParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public SQLiteDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteReader(commandText, DefaultSimpleAccessSettings.DefaultCommandType, commandBehavior, SQLiteParameters);
        }

        /// <summary> Executes the commandText and return TDbDataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="commandBehavior"> The CommandBehavior of executing DbCommand</param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TDbDataReader </returns>
        public SQLiteDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior = CommandBehavior.CloseConnection,
            params SQLiteParameter[] SQLiteParameters)
        {
            try
            {
                var dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
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
        public SQLiteDataReader ExecuteReader(string commandText, object paramObject = null)
        {
            return ExecuteReader(commandText, BuildDbParameters(paramObject));
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
        public SQLiteDataReader ExecuteReader(string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteReader(commandText, commandType, BuildDbParameters(paramObject));
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
        public SQLiteDataReader ExecuteReader(string commandText, CommandBehavior commandBehavior, object paramObject = null)
        {
            return ExecuteReader(commandText, commandBehavior, BuildDbParameters(paramObject));
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
        public SQLiteDataReader ExecuteReader(string commandText, CommandType commandType, CommandBehavior commandBehavior, object paramObject = null)
        {
            return ExecuteReader(commandText, commandType, commandBehavior, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(string commandText, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteValues<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
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
            return ExecuteValues<T>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,  BuildDbParameters(paramObject));
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
            return ExecuteValues<T>(commandText, commandType, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{T}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        ///     
        /// <typeparam name="T"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(string commandText, CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
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
                if (dbCommand != null && _SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
        public IEnumerable<T> ExecuteValues<T>(SQLiteTransaction transaction, string commandText, object paramObject = null)
        {
            return ExecuteValues<T>(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, BuildDbParameters(paramObject));
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
        public IEnumerable<T> ExecuteValues<T>(SQLiteTransaction transaction, string commandText, CommandType commandType, object paramObject = null)
        {
            return ExecuteValues<T>(transaction, commandText, commandType, BuildDbParameters(paramObject));
        }
        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(SQLiteTransaction transaction, string commandText,
                                             params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteValues<T>(transaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{T}" /> </returns>
        public IEnumerable<T> ExecuteValues<T>(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
            params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, SQLiteParameters);
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
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The TEntity value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip
                , propertyInfoDictionary, SQLiteParameters);
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
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SQLiteParameter[] SQLiteParameters) where TEntity : new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
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
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
                , propertyInfoDictionary, BuildDbParameters(paramObject));
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
                , propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="DbException"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters)
            where TEntity : new()
        {
            return ExecuteEntities<TEntity>(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, SQLiteParameters);
        }

        /// <summary> Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Generic type parameter. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The {TEntity} value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText,
            CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters) where TEntity : new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, SQLiteParameters);
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
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText, object paramObject = null
            , string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType
                , fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a <see cref="IEnumerable{TEntity}" /> from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The <see cref="IEnumerable{TEntity}" /> value </returns>
        public IEnumerable<TEntity> ExecuteEntities<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText,
            CommandType commandType, object paramObject = null, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : new()
        {
            return ExecuteEntities<TEntity>(SQLiteTransaction, commandText, commandType
                , fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		 (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(string commandText, string fieldsToSkip = null
            , Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
            params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, SQLiteParameters);
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
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(string commandText, CommandType commandType, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow ))
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
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
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
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText, string fieldsToSkip = null,
            Dictionary<string, PropertyInfo> propertyInfoDictionary = null, params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null
            , params SQLiteParameter[] SQLiteParameters) where TEntity : class, new()
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, SQLiteParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
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
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText, object paramObject = null,
            string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null) where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a TEntity from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="propertyInfoDictionary">		  (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> The value of the entity. </returns>
        public TEntity ExecuteEntity<TEntity>(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null, Dictionary<string, PropertyInfo> propertyInfoDictionary = null)
            where TEntity : class, new()
        {
            return ExecuteEntity<TEntity>(SQLiteTransaction, commandText, commandType,
                fieldsToSkip, propertyInfoDictionary, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamics(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
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
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
                BuildDbParameters(paramObject));
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
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SQLiteTransaction SQLiteTransaction, string commandText, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamics(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, SQLiteParameters);
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
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SQLiteTransaction SQLiteTransaction, string commandText, object paramObject = null,
            string fieldsToSkip = null)
        {
            return ExecuteDynamics(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a IEnumerable{object} from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> A list of object. </returns>
        public IEnumerable<dynamic> ExecuteDynamics(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
            object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamics(SQLiteTransaction, commandText, commandType, fieldsToSkip,
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(string commandText, string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamic(commandText, DefaultSimpleAccessSettings.DefaultCommandType, fieldsToSkip,
                SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters"> Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(string commandText, CommandType commandType, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(commandText, commandType, SQLiteParameters);
                dbCommand.Connection.OpenSafely();
                var reader = dbCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    var result = SQLiteDataReaderToExpando(reader);
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
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
                BuildDbParameters(paramObject));
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
                BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SQLiteTransaction SQLiteTransaction, string commandText, string fieldsToSkip = null,
            params SQLiteParameter[] SQLiteParameters)
        {
            return ExecuteDynamic(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, SQLiteParameters);
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// <param name="SQLiteParameters">  Parameters required to execute CommandText. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
            string fieldsToSkip = null, params SQLiteParameter[] SQLiteParameters)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                dbCommand = CreateCommand(SQLiteTransaction, commandText, commandType, SQLiteParameters);
                dbCommand.Connection.OpenSafely();
                using (var reader = dbCommand.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return SQLiteDataReaderToExpando(reader);
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
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// -<param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SQLiteTransaction SQLiteTransaction, string commandText, object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(SQLiteTransaction, commandText, DefaultSimpleAccessSettings.DefaultCommandType,
                fieldsToSkip, BuildDbParameters(paramObject));
        }

        /// <summary> Sends the CommandText to the Connection and builds a anonymous object from DataReader. </summary>
        /// 
        /// <exception cref="Exception"> Thrown when an exception error condition occurs. </exception>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="paramObject"> The anonymous object as parameters. </param>
        /// <param name="fieldsToSkip"> (optional) the fields to skip. </param>
        /// 
        /// <returns> Result in a anonymous object. </returns>
        public dynamic ExecuteDynamic(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType,
                object paramObject = null, string fieldsToSkip = null)
        {
            return ExecuteDynamic(SQLiteTransaction, commandText, commandType,
                fieldsToSkip, BuildDbParameters(paramObject));
        }
        /// <summary>
        /// Execute the CommandText against connection and add or refresh rows in <see cref="DataTable"/>
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dataTable">A <see cref="DataTable"/> to fill with records and, if necessary, schema  </param>
        /// <returns></returns>
        public int Fill(string commandText, DataTable dataTable)
        {
            SQLiteCommand dbCommand = null;
            try
            {
                if (dataTable == null)
                    dataTable = new DataTable();
                dbCommand = new SQLiteCommand(commandText);
                var sqlDataAdapter = new SQLiteDataAdapter(dbCommand);
                return sqlDataAdapter.Fill(dataTable);

            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);

                throw;
            }
            finally
            {
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

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
            SQLiteCommand dbCommand = null;
            try
            {
                if (dataSet == null)
                    dataSet = new DataSet();

                dbCommand = new SQLiteCommand(commandText);

                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(dbCommand);
                return sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);

                throw;
            }
            finally
            {
                if (_SQLiteConnection.State != ConnectionState.Closed)
                    _SQLiteConnection.CloseSafely();

                dbCommand.ClearDbCommand();
            }
        }

        /// <summary> Gets the new connection. </summary>
        /// <returns> The new connection. </returns>
        public SQLiteConnection GetNewConnection()
        {
            return new SQLiteConnection(DefaultConnectionString);
            //return new SQLiteConnection(_SQLiteConnection.ConnectionString);
        }

        /// <summary> Close the current open connection. </summary>
        public void CloseDbConnection()
        {
            if (_SQLiteConnection != null)
                _SQLiteConnection.CloseSafely();
        }

        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SQLiteTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }
        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SQLiteTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_SQLiteConnection.State != ConnectionState.Open)
                _SQLiteConnection.Open();

            return _SQLiteConnection.BeginTransaction(isolationLevel);
        }
        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SQLiteTransaction BeginTransaction(string transactionName)
        {
            throw new NotSupportedException("SQLiteTransaction with transaction name are not support");
            //return BeginTransaction(IsolationLevel.ReadCommitted, transactionName);
        }
        /// <summary> Begins a transaction. </summary>
        /// <returns> . </returns>
        public SQLiteTransaction BeginTransaction(IsolationLevel isolationLevel, string transactionName)
        {
            throw new NotSupportedException("SQLiteTransaction with transaction name are not support");

            //if (_SQLiteConnection.State != ConnectionState.Open)
            //    _SQLiteConnection.Open();

            //if (string.IsNullOrEmpty(transactionName))
            //{
            //    return _SQLiteConnection.BeginTransaction(isolationLevel);
            //}

            //return _SQLiteConnection.BeginTransaction(isolationLevel, transactionName);
        }


        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SQLiteCommand CreateCommand(string commandText, CommandType commandType, params SQLiteParameter[] SQLiteParameters)
        {
            var dbCommand = new SQLiteCommand(commandText, _SQLiteConnection);
                //_SQLiteConnection.CreateCommand();
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            //dbCommand.CommandText = commandText;
            if (SQLiteParameters != null)
                dbCommand.Parameters.AddRange(SQLiteParameters);

            return dbCommand;
        }

        /// <summary> Ends a transaction. </summary>
        /// 
        /// <param name = "SQLiteTransaction" > The SQL transaction. </param>
        /// <param name = "transactionSucceed" > (optional)the transaction succeed. </param>
        /// <param name = "closeConnection" > (optional)the close connection. </param>
        public void EndTransaction(SQLiteTransaction SQLiteTransaction, bool transactionSucceed = true, bool closeConnection = true)
        {
            if (transactionSucceed)
            {
                SQLiteTransaction.Commit();
            }
            else
            {
                SQLiteTransaction.Rollback();
            }

            if (closeConnection)
            {
                _SQLiteConnection.CloseSafely();
            }
        }



        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        public SQLiteCommand CreateCommand(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType
            , params SQLiteParameter[] SQLiteParameters)
        {
            var dbCommand = _SQLiteConnection.CreateCommand();
            dbCommand.Transaction = SQLiteTransaction;
            dbCommand.CommandTimeout = DefaultSimpleAccessSettings.DbCommandTimeout;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            if (SQLiteParameters != null)
                dbCommand.Parameters.AddRange(SQLiteParameters);
           

            return dbCommand;
        }

        /// <summary> SQL data reader to <see cref="ExpandoObject"/>. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        public dynamic SQLiteDataReaderToExpando(SQLiteDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var value = reader[i];
                if (value != null)
                    expandoObject.Add(reader.GetName(i), value);
            }

            return expandoObject;
        }

        /// <summary> Gets a object SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The object SQL data. </returns>
        public IList<dynamic> GetDynamicSqlData(SQLiteDataReader reader)
        {
            var result = new List<dynamic>();
            
            while (reader.Read())
            {
                result.Add(SQLiteDataReaderToExpando(reader));
            }
            return result;
        }

        /// <summary> Gets a object SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The object SQL data. </returns>
        public IList<T> GetValues<T>(SQLiteDataReader reader)
        {
            var result = new List<T>();

            while (reader.Read())
            {
                result.Add((T)Convert.ChangeType(reader[0], typeof(T)));
            }
            return result;
        }

        /// <summary> Build SQLiteParameter Array from anonymous object. </summary>
        ///  <param name="paramObject"> The anonymous object as parameters. </param>
        /// <returns> SQLiteParameter[] object and if paramObject is null then return null </returns>
        public SQLiteParameter[] BuildDbParameters(object paramObject)
        {
            if (paramObject == null)
                return null;
            var SQLiteParameters = new List<SQLiteParameter>();
            SQLiteParameters.CreateSQLiteParametersFromObject(paramObject);

            return SQLiteParameters.ToArray();
        }

        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. </summary>
        public void Dispose()
        {

            if (_SQLiteConnection.State != ConnectionState.Closed)
                _SQLiteConnection.Close();

            DefaultSimpleAccessSettings = null;
        }

    }
}
