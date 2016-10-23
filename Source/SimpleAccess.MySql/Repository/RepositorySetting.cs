using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.MySql
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class RepositorySetting
    {
        static RepositorySetting()
        {
            EntityInfos = new Dictionary<int, Core.Entity.EntityInfo<MySqlSqlBuilder, MySqlParameter>>();

        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfos"/> for cache.
        /// </summary>
        public static Dictionary<int, Core.Entity.EntityInfo<MySqlSqlBuilder, MySqlParameter>> EntityInfos { get; set; }


        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <see cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static SimpleAccess.Core.Entity.EntityInfo<MySqlSqlBuilder, MySqlParameter> GetEntityInfo(Type type)
        {
            Core.Entity.EntityInfo<MySqlSqlBuilder, MySqlParameter> entityInfo = null;
            if (EntityInfos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<MySqlSqlBuilder, MySqlParameter>(type);
            EntityInfos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }
    }
}
