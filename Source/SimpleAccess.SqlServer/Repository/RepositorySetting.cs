using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class RepositorySetting
    {
        static RepositorySetting()
        {
        EntityInfos = new Dictionary<int, Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>>();

        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfos"/> for cache.
        /// </summary>
        public static Dictionary<int, Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>> EntityInfos { get; set; }


        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <see cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityInfo<SqlServerSqlBuilder, SqlParameter> GetEntityInfo(Type type)
        {
            Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter> entityInfo = null;
            if (EntityInfos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>(type);
            EntityInfos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }
    }
}
