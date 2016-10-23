using System;
using System.Collections.Generic;
using System.Configuration;
using SimpleAccess.Oracle.Entity;

namespace SimpleAccess.Oracle
{
    public static class RepositorySetting
    {
        static RepositorySetting()
        {
            EntityInfos = new Dictionary<int, EntityInfo>();
        }

        public static Dictionary<int,EntityInfo> EntityInfos { get; set; }

        public static EntityInfo GetEntityInfo(Type type)
        {
            EntityInfo entityInfo = null;
            if (EntityInfos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new EntityInfo(type);
            EntityInfos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }

        public static ConnectionStringSettings LoadConnectionStringSettingsFromConfigurationFile(string connectionStringName, bool force = false)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];


            if (connectionStringSettings == null && force)
                throw new Exception("ConnectionString not found in the configuration file.");

            return connectionStringSettings;
        }
    }
}