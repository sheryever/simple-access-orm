using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.Repository
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class RepositorySetting
    {
        static RepositorySetting()
        {
            Entity2Infos = new Dictionary<int, Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>>();

        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfo{TISqlBuilder,TDbParameter}"/> for cache.
        /// </summary>
        public static Dictionary<int, Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>> Entity2Infos { get; set; }


        /// <summary>
        /// Get the <see cref="EntityInfo{TISqlBuilder,TDbParameter}"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <see cref="EntityInfo{TISqlBuilder,TDbParameter}"/> then it will add the and return the <see cref="EntityInfo{TISqlBuilder,TDbParameter}"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static SimpleAccess.Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter> GetEntityInfo(Type type)
        {
            Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter> entityInfo = null;
            if (Entity2Infos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>(type);
            Entity2Infos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }
    }
}
