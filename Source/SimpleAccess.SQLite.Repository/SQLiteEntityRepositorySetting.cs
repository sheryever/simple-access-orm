using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class SQLiteEntityRepositorySetting
    {
        static SQLiteEntityRepositorySetting()
        {
       
        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfos"/> for cache.
        /// </summary>
        public static Dictionary<string, Core.Entity.EntityInfo<SQLiteEntitiesSqlBuilder, SQLiteParameter>> EntityInfos { get; } =
             new Dictionary<string, Core.Entity.EntityInfo<SQLiteEntitiesSqlBuilder, SQLiteParameter>>();


        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <se
        /// cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityInfo<SQLiteEntitiesSqlBuilder, SQLiteParameter> GetEntityInfo(Type type)
        {
            Core.Entity.EntityInfo<SQLiteEntitiesSqlBuilder, SQLiteParameter> entityInfo = null;
            if (EntityInfos.TryGetValue(type.FullName, out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<SQLiteEntitiesSqlBuilder, SQLiteParameter>(type);

            lock (EntityInfos)
            {
                if (!EntityInfos.ContainsKey(type.FullName))
                    EntityInfos.Add(type.FullName, entityInfo);

            }

            return entityInfo;
        }
    }
}
