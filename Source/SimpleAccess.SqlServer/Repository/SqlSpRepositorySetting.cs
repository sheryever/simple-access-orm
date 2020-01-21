using System;
using System.Collections.Generic;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Hold the default settings of SimpleAccess Repositories
    /// </summary>
    public static class SqlSpRepositorySetting
    {
        static SqlSpRepositorySetting()
        {
       
        }

        /// <summary>
        /// The Dictionary of <see cref="EntityInfos"/> for cache.
        /// </summary>
        public static Dictionary<string, Core.Entity.EntityInfo<SqlSpSqlBuilder, SqlParameter>> EntityInfos { get; } =
             new Dictionary<string, Core.Entity.EntityInfo<SqlSpSqlBuilder, SqlParameter>>();


        /// <summary>
        /// Get the <see cref="EntityInfo"/> object from the cache.
        /// </summary>
        /// If the <paramref name="type"/> has no <se
        /// cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityInfo<SqlSpSqlBuilder, SqlParameter> GetEntityInfo(Type type)
        {
            Core.Entity.EntityInfo<SqlSpSqlBuilder, SqlParameter> entityInfo = null;
            if (EntityInfos.TryGetValue(type.FullName, out entityInfo))
                return entityInfo;

            entityInfo = new Core.Entity.EntityInfo<SqlSpSqlBuilder, SqlParameter>(type);

            lock (EntityInfos)
            {
                if (!EntityInfos.ContainsKey(type.FullName))
                    EntityInfos.Add(type.FullName, entityInfo);
            }

            return entityInfo;
        }

        public static string SpGetAllPattern { get; set; } = "{0}_GetAll";
        public static string SpGetByIdPattern { get; set; } = "{0}_GetById";
        public static string SpFindPattern { get; set; } = "{0}_Find";
        public static string SpInsertPattern { get; set; } = "{0}_Insert";
        public static string SpUpdatePattern { get; set; } = "{0}_Update";
        public static string SpDeletePattern { get; set; } = "{0}_Delete";
        public static string SpDeleteAllPattern { get; set; } = "{0}_DeleteAll";
        public static string SpSoftDeletePattern { get; set; } = "{0}_SoftDelete";
    }
}
