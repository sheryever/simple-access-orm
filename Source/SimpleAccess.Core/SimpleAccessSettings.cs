using System;
using System.Configuration;
using System.Data;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.Core
{
    /// <summary>
    /// Used for setting up the default setting of SimpleAccess.
    /// </summary>
    public class SimpleAccessSettings
    {

        /// <summary>
        /// Check for the connection is a connectionString name from the config or 
        /// a complete database connetion string and return the connnection string.
        /// </summary>
        /// <param name="connection"> The connectionString Name from the config or a complete ConnectionString.</param>
        /// <returns></returns>
        public static string GetProperConnectionString(string connection)
        {
            if (connection.IndexOf(";") > 0)
            {
                return connection;

            }

            return LoadConnectionStringSettingsFromConfigurationFile(connection, true).ConnectionString;
        }

        /// <summary>
        /// Load and returns the <see cref="ConnectionStringSettings"/> from the default config file
        /// based on provided contection string name.
        /// </summary>
        /// <param name="connectionStringName"> The connection string name </param>
        /// <param name="force"> The <see cref="ConnectionStringSettings"/> required other wise thorw Exception </param>
        /// <returns></returns>
        public static ConnectionStringSettings LoadConnectionStringSettingsFromConfigurationFile(string connectionStringName, bool force = false)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];


            if (connectionStringSettings == null && force)
                throw new Exception("ConnectionStringName not found in the configuration file.");

            return connectionStringSettings;
        }

        /// <summary>
        /// Initialize the new object of SimpleAccessSettings with default properties.
        /// </summary>
        public SimpleAccessSettings()
            : this( CommandType.Text, new SimpleLogger())
        {
        }

        /// <summary>
        /// Initialize the new object of SimpleAccessSettings with default properties.
        /// </summary>
        /// <param name="defaultCommandType"> The default <see cref="CommandType"/> of this new Object</param>
        public SimpleAccessSettings(CommandType defaultCommandType)
            : this(defaultCommandType, new SimpleLogger())
        {
        }

        /// <summary>
        /// Initialize the new object of SimpleAccessSettings with default properties.
        /// </summary>
        /// <param name="defaultCommandType"> The default <see cref="CommandType"/> of this new object</param>
        /// <param name="defaultLogger"> The default <see cref="ISimpleLogger"/> implementaion for parent SimpleAccess object</param>
        public SimpleAccessSettings(CommandType defaultCommandType, ISimpleLogger defaultLogger)
        {
            DefaultCommandType = defaultCommandType;
            DefaultLogger = defaultLogger;
        }

        /// <summary>
        /// Default property of <see cref="CommandType"/>.
        /// </summary>
        public CommandType DefaultCommandType { get; set; }
        /// <summary>
        /// Default <see cref="ISimpleLogger"/>.
        /// </summary>
        public ISimpleLogger DefaultLogger { get; set; }

        /// <summary>
        /// Sets the wait time before terminating the attempt to execute a command and generating an error. Default timeout is 30 
        /// </summary>
        public int DbCommandTimeout { get; set; } = 30;

    }
}