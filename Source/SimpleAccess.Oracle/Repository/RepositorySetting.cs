using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Core;
using SimpleAccess.Entity;

namespace SimpleAccess.Oracle
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class RepositorySetting
    {
        static RepositorySetting()
        {
            EntityInfos = new Dictionary<int, EntityInfo>();
            Entity2Infos = new Dictionary<int, Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter>>();

        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfos"/> for cache.
        /// </summary>
        public static Dictionary<int,EntityInfo> EntityInfos { get; set; }

        public static Dictionary<int, Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter>> Entity2Infos { get; set; }


        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <see cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityInfo GetEntityInfo(Type type)
        {
            EntityInfo entityInfo = null;
            if (EntityInfos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new EntityInfo(type);
            EntityInfos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }


        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <see cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static SimpleAccess.Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter> GetEntity2Info(Type type)
        {
            Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter> entityInfo = null;
            if (Entity2Infos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter>(type);
            Entity2Infos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }
    }
}
