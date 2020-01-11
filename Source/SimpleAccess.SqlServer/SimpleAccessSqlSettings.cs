using System;
using System.Configuration;
using System.Data;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;
using System.Collections.Generic;
using System.Data.Common;

namespace SimpleAccess.SqlServer
{
    ///// <summary>
    ///// Used for setting up the default setting of SimpleAccess.
    ///// </summary>
    //public class SimpleAccessSqlSettings 
    //{
    //    public static Dictionary<int, Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>> EntityInfos { get; set; }

    //    /// <summary>
    //    /// Initialize the new object of SimpleAccessSettings with default properties.
    //    /// </summary>
    //    public SimpleAccessSqlSettings()
    //    {
    //        EntityInfos = new Dictionary<int, Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>>();
    //    }


    //    /// <summary>
    //    /// Get the <see cref="EntityInfo"/> object from the cache.
    //    /// </summary>
    //    /// If the <paramref name="type"/> has no <see cref="EntityInfo"/> then it will add the and return the <see cref="EntityInfo"/>.
    //    /// <param name="type"></param>
    //    /// <returns></returns>
    //    public static SimpleAccess.Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter> GetEntityInfo(Type type)
    //    {
    //        Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter> entityInfo = null;
    //        if (EntityInfos.TryGetValue(type.GetHashCode(), out entityInfo))
    //            return entityInfo;

    //        entityInfo = new Core.Entity.EntityInfo<SqlServerSqlBuilder, SqlParameter>(type);
    //        EntityInfos.Add(type.GetHashCode(), entityInfo);

    //        return entityInfo;
    //    }
    //}
}