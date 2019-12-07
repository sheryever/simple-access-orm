using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Core;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.Oracle
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class RepositorySetting
    {
        static RepositorySetting()
        {

        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfos"/> for cache.
        /// </summary>
        public static Dictionary<string, Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter>> EntityInfos { get; }
            = new Dictionary<string, Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter>>();



        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <see cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static SimpleAccess.Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter> GetEntityInfo(Type type)
        {
            Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter> entityInfo = null;
            if (EntityInfos.TryGetValue(type.FullName, out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<OracleSqlBuilder, OracleParameter>(type);
            lock (EntityInfos)
            {
                if (!EntityInfos.ContainsKey(type.FullName))
                    EntityInfos.Add(type.FullName, entityInfo);

            }
            return entityInfo;
        }
    }
}
