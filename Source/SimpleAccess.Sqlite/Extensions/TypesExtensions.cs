using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SimpleAccess.SqlServer
{
    public static class TypesExtensions
    {

        public static string CreateWhereWithAnd(this object source)
        {
            var whereClause = "WHERE ";
            var sets = new List<string>();


            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                sets.Add($"{propertyInfo.Name.Replace("@", "")} = @{propertyInfo.Name}");
            }

            return whereClause + string.Join(" AND ", sets.ToArray());
        }

        public static IList<T> ToDataParameters<T>(this object otherParameters)
            where T: DbParameter, new()
        {
            var parameters = new List<T>();
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
                           value = value.ToString().SafeSqlLiteral();
                       }
                       return new T { ParameterName = "@" + param.Name.CleanParameterName(), Value = value ?? DBNull.Value };
                   }).ToList();

                return sqlParams;
            }
            return parameters;
        }

        private static string CleanParameterName(this string name)
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

        private static string SafeSqlLiteral(this string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }
    }
}
