using System;
using System.Configuration;
using System.Data;
using System.IO;
#if NETSTANDARD
#endif
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

            return null;
        }


#if NETSTANDARD

        static SimpleAccessSettings()
        {

        }
#endif

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
        public ISimpleLogger DefaultLogger { get; private set; }

        /// <summary>
        /// Sets the wait time before terminating the attempt to execute a command and generating an error. Default timeout is 30 
        /// </summary>
        public int DbCommandTimeout { get; set; } = 30;

    }


#if NETSTANDARD
    //
    // Summary:
    //     Represents a single, named connection string in the connection strings configuration
    //     file section.
    public class ConnectionStringSettings 
    {
        //
        // Summary:
        //     Initializes a new instance of a System.Configuration.ConnectionStringSettings
        //     class.
        public ConnectionStringSettings() { }
        //
        // Summary:
        //     Initializes a new instance of a System.Configuration.ConnectionStringSettings
        //     class.
        //
        // Parameters:
        //   name:
        //     The name of the connection string.
        //
        //   connectionString:
        //     The connection string.
        public ConnectionStringSettings(string name, string connectionString)
        {
            Name = name;
            ConnectionString = connectionString;
        }
        //
        // Summary:
        //     Initializes a new instance of a System.Configuration.ConnectionStringSettings
        //     object.
        //
        // Parameters:
        //   name:
        //     The name of the connection string.
        //
        //   connectionString:
        //     The connection string.
        //
        //   providerName:
        //     The name of the provider to use with the connection string.
        public ConnectionStringSettings(string name, string connectionString, string providerName)
            : this(name,connectionString)
        {
            ProviderName = providerName;
        }

        //
        // Summary:
        //     Gets or sets the System.Configuration.ConnectionStringSettings name.
        //
        // Returns:
        //     The string value assigned to the System.Configuration.ConnectionStringSettings.Name
        //     property.
        public string Name { get; set; }
        //
        // Summary:
        //     Gets or sets the connection string.
        //
        // Returns:
        //     The string value assigned to the System.Configuration.ConnectionStringSettings.ConnectionString
        //     property.
        public string ConnectionString { get; set; }
        //
        // Summary:
        //     Gets or sets the provider name property.
        //
        // Returns:
        //     Gets or sets the System.Configuration.ConnectionStringSettings.ProviderName property.
        public string ProviderName { get; set; }
    }
#endif
}