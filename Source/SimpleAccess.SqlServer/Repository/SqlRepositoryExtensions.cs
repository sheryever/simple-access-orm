using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using SimpleAccess.SqlServer;
using System.Text;
using SimpleAccess.Core.Extensions;
using System.Data.Common;
using SimpleAccess.Core.Entity.RepoWrapper;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SqlServer
{

    public static partial class SqlRepositoryExtensions
    {

#if !NET40

#endif
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, byte>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, byte>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, byte?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, byte?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, short>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, short>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, short?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, short?>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            return IsExist<TEntity, int>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, int?>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, long>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where = null)
   where TEntity : class, new()
        {
            return IsExist<TEntity, long?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, string>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, string>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, float>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, float?>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, decimal>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, decimal?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, double>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, double?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, bool>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, bool?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, bool?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, DateTime>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, DateTime>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, DateTime?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, DateTime?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TimeSpan>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, TimeSpan>(sqlRepository, null, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, TimeSpan?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, TimeSpan?>(sqlRepository, null, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            return IsExist<TEntity, int>(sqlRepository, null, null, where);
        }

        /* */

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, byte>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, byte>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, byte?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, byte?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, short>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, short>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, short?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, short?>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            return IsExist<TEntity, int>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, int?>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, long>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> where = null)
   where TEntity : class, new()
        {
            return IsExist<TEntity, long?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, string>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, string>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, float>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, float?>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> where = null)
    where TEntity : class, new()
        {
            return IsExist<TEntity, decimal>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, decimal?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, double>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, double?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, bool>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, bool?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, bool?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, DateTime>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, DateTime>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, DateTime?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, DateTime?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, TimeSpan>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, TimeSpan>(sqlRepository, transaction, selector, where);
        }
        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, TimeSpan?>> selector, Expression<Func<TEntity, bool>> where = null)
where TEntity : class, new()
        {
            return IsExist<TEntity, TimeSpan?>(sqlRepository, transaction, selector, where);
        }

        public static bool IsExist<TEntity>(this ISqlRepository sqlRepository, SqlTransaction transaction, Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            return IsExist<TEntity, int>(sqlRepository, transaction, null, where);
        }

        private static bool IsExist<TEntity, TKey>(this ISqlRepository sqlRepository, SqlTransaction transaction = null, Expression<Func<TEntity, TKey>> selector = null
        , Expression<Func<TEntity, bool>> where = null)
            where TEntity : class, new()
        {
            string commandText = "IF EXISTS (SELECT {column} FROM {table} {whereClause}) SELECT 1 ELSE SELECT 0;";
            CommandType commandType = CommandType.Text;

            if (selector != null)
            {
                var selectedColumn = GetSingleSelectedProperty(selector);

                commandText = commandText.Replace("{column}", $"{selectedColumn}");
            }
            else
            {
                commandText = commandText.Replace("{column}", "*");
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
            if (transaction != null)
            {
                return sqlRepository.SimpleAccess.ExecuteScalar<int>(transaction, commandText, commandType) > 0;
            }

            return sqlRepository.SimpleAccess.ExecuteScalar<int>(commandText, commandType) > 0;

        }


        private static string[] GetSelectedProperties<TEntity>(Func<TEntity, object> select)
            where TEntity : class, new()
        {
            return select.Invoke(new TEntity())
                   .GetType()
                   .GetProperties()
                   .Where(p => !p.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute || a is NotMappedAttribute))
                   .Select(p => p.Name.ToLower())
                   .ToArray();
        }



        private static string GetSingleSelectedProperty<TEntity, TReturn>(Expression<Func<TEntity, TReturn>> select)
            where TEntity : class, new()
        {
            var expression = (MemberExpression)select.Body;
            string name = expression.Member.Name;

            return name;
        }

        private static Dictionary<string, PropertyInfo> LoadEntityProperties<TEntity>(string[] properitiesNames)
            where TEntity : class
        {
            return typeof(TEntity).GetProperties()
                .Where(p => properitiesNames.Contains(p.Name.ToLower()))
                .ToDictionary(p => p.Name.ToLower());
        }

        private static string CreateWhereClauseFromSqlParameter(SqlParameter[] parameters, params string[] skipParameters)
        {
            if (parameters == null || parameters.Length < 1) return "";

            var whereSb = new StringBuilder();

            whereSb.Append($"WHERE 1 = 1 ");

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                if (parameter.ParameterName.ToLower().In(skipParameters)) continue;

                whereSb.Append($" AND {parameter.ParameterName.Replace("@", "")} = {parameter.ParameterName}");
            }

            var whereClause = whereSb.ToString();

            return whereClause == "WHERE 1 = 1 " ? "" : whereClause;
        }


        #region SqlEntityRepository

        #endregion



        public static T[] CreateSqlParameters<T>(object otherParameters)
            where T : DbParameter, new()
        {
            var otherParametersObj = otherParameters as Object;
            if (otherParametersObj != null)
            {
                var sqlParams = otherParametersObj.GetType().GetProperties().Select(
                   param =>
                   {
#if NET40
                       var emptyArray = new object[0];
                       object value = param.GetValue(otherParameters, emptyArray);
#else
                       object value = param.GetValue(otherParameters);

#endif
                       if (value is IDataParameter)
                       { return value as T; }
                       if (param.Name.GetType().Name.ToLower() == "string" && value != null)
                       {
                           value = SafeSqlLiteral(value.ToString());
                       }
                       return new T { ParameterName = "@" + Clean(param.Name), Value = value ?? DBNull.Value };
                   }).ToList();

                return sqlParams.ToArray();
            }
            return new T[0];
        }
        private static string SafeSqlLiteral(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }

        private static string Clean(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name[0])
                {
                    case '@':
                    case ':':
                    case '?':
                        return name.Substring(1);
                }
            }
            return name;
        }
    }
}
