using System;
using System.Configuration;
using System.Data;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess
{
    public class SimpleAccessSettings
    {

        public static string GetProperConnectionString(string connection)
        {
            if (connection.IndexOf(";") > 0)
            {
                return connection;

            }

            return LoadConnectionStringSettingsFromConfigurationFile(connection, true).ConnectionString;
        }

        public static ConnectionStringSettings LoadConnectionStringSettingsFromConfigurationFile(string connectionStringName, bool force = false)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];


            if (connectionStringSettings == null && force)
                throw new Exception("ConnectionStringName not found in the configuration file.");

            return connectionStringSettings;
        }

        public SimpleAccessSettings()
            : this( CommandType.Text, new SimpleLogger())
        {
        }

        public SimpleAccessSettings(CommandType defaultCommandType)
            : this(defaultCommandType, new SimpleLogger())
        {
        }

        public SimpleAccessSettings(CommandType defaultCommandType, ISimpleLogger defaultLogger)
        {
            DefaultCommandType = defaultCommandType;
            DefaultLogger = defaultLogger;
        }

        public CommandType DefaultCommandType { get; set; }
        public ISimpleLogger DefaultLogger { get; set; }
    }
}