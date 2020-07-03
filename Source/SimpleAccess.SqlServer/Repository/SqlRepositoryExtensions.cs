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

namespace SimpleAccess.SqlServer
{

    public static partial class SqlRepositoryExtensions
    {

#if !NET40

#endif
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
            where T: DbParameter, new()
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
