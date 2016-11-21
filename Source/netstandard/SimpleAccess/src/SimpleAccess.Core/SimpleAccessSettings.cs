using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
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
            
            return LoadConnectionStringSettingsFromConfigurationFile(connection, true);
        }

        /// <summary>
        /// Load and returns the connection string from the default config file
        /// based on provided contection string name.
        /// </summary>
        /// <param name="connectionStringName"> The connection string name </param>
        /// <returns>The complete connection string</returns>
        public static string LoadConnectionStringSettingsFromConfigurationFile(string connectionStringName, bool force = false)
        {

            var connectionString = Configuration.GetConnectionString(connectionStringName);
            //var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];


            if (connectionString == null && force)
                throw new Exception("ConnectionStringName not found in the appSetting.json file.");

            return connectionString;
        }

        static SimpleAccessSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }

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

    }
}