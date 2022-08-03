using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1 || NET6_0_OR_GREATER
using Microsoft.Data.SqlClient;
#endif
using System.Linq.Expressions;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.SqlServer
{

    public static partial class SqlRepositoryExtensions
    {

#if !NET40

#endif

        #region Aggregate functions

        #region GetCount
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, byte>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, byte>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, byte?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, byte?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, short>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, short>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, short?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, short?>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            return GetCount<TEntity, int>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, int?>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, long>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where = null)
   where TEntity : class, new()
        {
            return GetCount<TEntity, long?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, string>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, string>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, float>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, float?>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return GetCount<TEntity, decimal>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, decimal?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, double>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, double?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, bool>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, bool?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, bool?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, DateTime>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, DateTime>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, DateTime?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, DateTime?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TimeSpan>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, TimeSpan>(sqlRepository, selector, where);
        }
        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TimeSpan?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return GetCount<TEntity, TimeSpan?>(sqlRepository, selector, where);
        }

        public static int GetCount<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            return GetCount<TEntity, int>(sqlRepository, null, where);
        }

        public static int GetCount<TEntity, TKey>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TKey>> selector = null
        , Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            string commandText = "SELECT {columns} FROM {table} {whereClause}";
            CommandType commandType = CommandType.Text;

            if (selector != null)
            {
                var selectedColumn = GetSingleSelectedProperty(selector);

                commandText = commandText.Replace("{columns}", $"COUNT({selectedColumn}) AS CountOf{selectedColumn}");
            }
            else
            {
                commandText = commandText.Replace("{columns}", "COUNT(*) AS CountOfAll");
            }

            var whereClause = "";

            if (sqlRepository is SqlSpRepository)
            {
                var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }
            else
            {
                var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }

            commandText = commandText.Replace("{whereClause}", whereClause);

            return sqlRepository.SimpleAccess.ExecuteScalar<int>(commandText, commandType);
        }


        public static dynamic GetCount<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector
        , Expression<Func<TEntity, bool>> where = null)
        where TEntity : class, new()
        {
            string commandText = "SELECT {columns} FROM {table} {whereClause}";
            CommandType commandType = CommandType.Text;

            if (selector != null)
            {
                var selectProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(selector));

                commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Values.Select(p => $"COUNT({p.Name}) AS CountOf{p.Name}")));
            }
            else
            {
                commandText = commandText.Replace("{columns}", "COUNT(*) AS CountOfAll");
            }

            var whereClause = "";

            if (sqlRepository is SqlSpRepository)
            {
                var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }
            else
            {
                var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }

            commandText = commandText.Replace("{whereClause}", whereClause);

            return sqlRepository.SimpleAccess.ExecuteDynamic(commandText, commandType);
        }

        #endregion

        #region GetSum
        public static int GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, int>(sqlRepository, selector);
        }

        public static int? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, int?>(sqlRepository, selector);
        }

        public static long GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector)
    where TEntity : class, new()
        {
            return GetSum<TEntity, long>(sqlRepository, selector);
        }

        public static long? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, long?>(sqlRepository, selector);
        }

        public static float GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, float>(sqlRepository, selector);
        }

        public static float? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, float?>(sqlRepository, selector);
        }

        public static decimal GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector)
    where TEntity : class, new()
        {
            return GetSum<TEntity, decimal>(sqlRepository, selector);
        }

        public static decimal? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, decimal?>(sqlRepository, selector);
        }

        public static double GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector)
    where TEntity : class, new()
        {
            return GetSum<TEntity, double>(sqlRepository, selector);
        }

        public static double? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector)
            where TEntity : class, new()
        {
            return GetSum<TEntity, double?>(sqlRepository, selector);
        }

        private static TReturn GetSum<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector)
            where TEntity : class, new()
        {

            var column = GetSingleSelectedProperty(selector);
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Sum", column, null);
        }

        public static int GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, int>(sqlRepository, selector, where);
        }

        public static int? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, int?>(sqlRepository, selector, where);
        }

        public static long GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, long>(sqlRepository, selector, where);
        }

        public static long? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, long?>(sqlRepository, selector, where);
        }

        public static float GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, float>(sqlRepository, selector, where);
        }

        public static float? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, float?>(sqlRepository, selector, where);
        }

        public static decimal GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, decimal>(sqlRepository, selector, where);
        }

        public static decimal? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, decimal?>(sqlRepository, selector, where);
        }

        public static double GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, double>(sqlRepository, selector, where);
        }

        public static double? GetSum<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetSum<TEntity, double?>(sqlRepository, selector, where);
        }

        public static TReturn GetSum<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Sum", GetSingleSelectedProperty(selector), where);
        }

        public static dynamic GetSum<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Sum", selector, null);
        }

        public static dynamic GetSum<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Sum", selector, where);
        }

        #endregion

        #region GetMin
        public static int GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, int>(sqlRepository, selector);
        }

        public static int? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, int?>(sqlRepository, selector);
        }

        public static long GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector)
    where TEntity : class, new()
        {
            return GetMin<TEntity, long>(sqlRepository, selector);
        }

        public static long? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, long?>(sqlRepository, selector);
        }

        public static float GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, float>(sqlRepository, selector);
        }

        public static float? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, float?>(sqlRepository, selector);
        }

        public static decimal GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector)
    where TEntity : class, new()
        {
            return GetMin<TEntity, decimal>(sqlRepository, selector);
        }

        public static decimal? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, decimal?>(sqlRepository, selector);
        }

        public static double GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector)
    where TEntity : class, new()
        {
            return GetMin<TEntity, double>(sqlRepository, selector);
        }

        public static double? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector)
            where TEntity : class, new()
        {
            return GetMin<TEntity, double?>(sqlRepository, selector);
        }

        private static TReturn GetMin<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector)
            where TEntity : class, new()
        {

            var column = GetSingleSelectedProperty(selector);
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Min", column, null);
        }

        public static int GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, int>(sqlRepository, selector, where);
        }

        public static int? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, int?>(sqlRepository, selector, where);
        }

        public static long GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, long>(sqlRepository, selector, where);
        }

        public static long? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, long?>(sqlRepository, selector, where);
        }

        public static float GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, float>(sqlRepository, selector, where);
        }

        public static float? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, float?>(sqlRepository, selector, where);
        }

        public static decimal GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, decimal>(sqlRepository, selector, where);
        }

        public static decimal? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, decimal?>(sqlRepository, selector, where);
        }

        public static double GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, double>(sqlRepository, selector, where);
        }

        public static double? GetMin<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMin<TEntity, double?>(sqlRepository, selector, where);
        }

        public static TReturn GetMin<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Min", GetSingleSelectedProperty(selector), where);
        }

        public static dynamic GetMin<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Min", selector, null);
        }

        public static dynamic GetMin<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Min", selector, where);
        }

        #endregion


        //public static dynamic GetMin<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateResult<TEntity>(sqlRepository, "Min", selector, null);
        //}

        //public static dynamic GetMin<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateResult<TEntity>(sqlRepository, "Min", selector, where);
        //}

        #region GetMax
        public static int GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, int>(sqlRepository, selector);
        }

        public static int? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, int?>(sqlRepository, selector);
        }

        public static long GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector)
    where TEntity : class, new()
        {
            return GetMax<TEntity, long>(sqlRepository, selector);
        }

        public static long? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, long?>(sqlRepository, selector);
        }

        public static float GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, float>(sqlRepository, selector);
        }

        public static float? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, float?>(sqlRepository, selector);
        }

        public static decimal GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector)
    where TEntity : class, new()
        {
            return GetMax<TEntity, decimal>(sqlRepository, selector);
        }

        public static decimal? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, decimal?>(sqlRepository, selector);
        }

        public static double GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector)
    where TEntity : class, new()
        {
            return GetMax<TEntity, double>(sqlRepository, selector);
        }

        public static double? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector)
            where TEntity : class, new()
        {
            return GetMax<TEntity, double?>(sqlRepository, selector);
        }

        private static TReturn GetMax<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector)
            where TEntity : class, new()
        {

            var column = GetSingleSelectedProperty(selector);
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Max", column, null);
        }

        public static int GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, int>(sqlRepository, selector, where);
        }

        public static int? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, int?>(sqlRepository, selector, where);
        }

        public static long GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, long>(sqlRepository, selector, where);
        }

        public static long? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, long?>(sqlRepository, selector, where);
        }

        public static float GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, float>(sqlRepository, selector, where);
        }

        public static float? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, float?>(sqlRepository, selector, where);
        }

        public static decimal GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, decimal>(sqlRepository, selector, where);
        }

        public static decimal? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, decimal?>(sqlRepository, selector, where);
        }

        public static double GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, double>(sqlRepository, selector, where);
        }

        public static double? GetMax<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetMax<TEntity, double?>(sqlRepository, selector, where);
        }

        public static TReturn GetMax<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Max", GetSingleSelectedProperty(selector), where);
        }

        public static dynamic GetMax<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Max", selector, null);
        }

        public static dynamic GetMax<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Max", selector, where);
        }

        #endregion


        //public static dynamic GetMax<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateResult<TEntity>(sqlRepository, "Max", selector, null);
        //}

        //public static dynamic GetMax<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateResult<TEntity>(sqlRepository, "Max", selector, where);
        //}


        #region GetAverage
        public static int GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, int>(sqlRepository, selector);
        }

        public static int? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, int?>(sqlRepository, selector);
        }

        public static long GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector)
    where TEntity : class, new()
        {
            return GetAverage<TEntity, long>(sqlRepository, selector);
        }

        public static long? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, long?>(sqlRepository, selector);
        }

        public static float GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, float>(sqlRepository, selector);
        }

        public static float? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, float?>(sqlRepository, selector);
        }

        public static decimal GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector)
    where TEntity : class, new()
        {
            return GetAverage<TEntity, decimal>(sqlRepository, selector);
        }

        public static decimal? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, decimal?>(sqlRepository, selector);
        }

        public static double GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector)
    where TEntity : class, new()
        {
            return GetAverage<TEntity, double>(sqlRepository, selector);
        }

        public static double? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, double?>(sqlRepository, selector);
        }

        private static TReturn GetAverage<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector)
            where TEntity : class, new()
        {

            var column = GetSingleSelectedProperty(selector);
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Avg", column, null);
        }

        public static int GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, int>(sqlRepository, selector, where);
        }

        public static int? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, int?>(sqlRepository, selector, where);
        }

        public static long GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, long>(sqlRepository, selector, where);
        }

        public static long? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, long?>(sqlRepository, selector, where);
        }

        public static float GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, float>(sqlRepository, selector, where);
        }

        public static float? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, float?>(sqlRepository, selector, where);
        }

        public static decimal GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, decimal>(sqlRepository, selector, where);
        }

        public static decimal? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, decimal?>(sqlRepository, selector, where);
        }

        public static double GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, double>(sqlRepository, selector, where);
        }

        public static double? GetAverage<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAverage<TEntity, double?>(sqlRepository, selector, where);
        }

        public static TReturn GetAverage<TEntity, TReturn>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TReturn>> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity, TReturn>(sqlRepository, "Avg", GetSingleSelectedProperty(selector), where);
        }

        public static dynamic GetAverage<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Avg", selector, null);
        }

        public static dynamic GetAverage<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            return GetAggregateResult<TEntity>(sqlRepository, "Avg", selector, where);
        }

        #endregion


        //public static dynamic GetAverage<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateResult<TEntity>(sqlRepository, "Avg", selector, null);
        //}

        //public static dynamic GetAverage<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateResult<TEntity>(sqlRepository, "Avg", selector, where);
        //}

        private static dynamic GetAggregateResult<TEntity>(this ISqlRepository sqlRepository, string function
            , Func<TEntity, object> selector, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {
            if (selector == null) throw new NullReferenceException($"{nameof(selector)} cannot be null");


            string commandText = "SELECT  {columns} FROM {table} {whereClause}";
            string whereClause = "";
            if (sqlRepository is SqlSpRepository)
            {
                var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }
            else
            {
                var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }

            CommandType commandType = CommandType.Text;
            var selectProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(selector));

            commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Values.Select(p => $"{function}({p.Name}) AS {function}Of{p.Name}")));

            commandText = commandText.Replace("{whereClause}", whereClause);

            return sqlRepository.SimpleAccess.ExecuteDynamic(commandText, commandType);
        }

        private static TReturn GetAggregateResult<TEntity, TReturn>(this ISqlRepository sqlRepository, string function
            , string selectColumn, Expression<Func<TEntity, bool>> where)
            where TEntity : class, new()
        {

            string commandText = "SELECT  {columns} FROM {table} {whereClause}";
            string whereClause = "";
            if (sqlRepository is SqlSpRepository)
            {
                var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }
            else
            {
                var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
            }

            CommandType commandType = CommandType.Text;

            commandText = commandText.Replace("{columns}", $"{function}({selectColumn}) AS Value");

            commandText = commandText.Replace("{whereClause}", whereClause);

            return sqlRepository.SimpleAccess.ExecuteScalar<TReturn>(commandText, commandType);
        }


        #endregion

    }
}
