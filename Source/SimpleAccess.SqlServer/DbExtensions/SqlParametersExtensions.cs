using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Defines the extra extensions method for <see cref="SqlParameter"/>.
    /// </summary>
    public static class SqlParametersExtensions
    {
        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="sqlParameters"></param>
        /// <param name="otherParameters"></param>
        public static void CreateSqlParametersFromObject(this List<SqlParameter> sqlParameters, object otherParameters)
        {

            var otherParametersObj = otherParameters as Object;
            if (otherParametersObj != null)
            {
                var propInfos = otherParametersObj.GetType().GetProperties();
                var propCount = propInfos.Length;
                for (int i = 0; i < propCount; i++)
                {
                    var propInfo = propInfos[i];

                    var getMethodInfo = propInfo.GetGetMethod();

                    if (getMethodInfo.IsVirtual && !getMethodInfo.IsFinal)
                        continue;

                    object value = propInfo.GetValue(otherParameters, new object[] { });
                    if (value is IDataParameter)
                    {
                        sqlParameters.Add(value as SqlParameter);
                        continue;
                    }

                    //if (propInfo.PropertyType.Name.ToLower() != "string")
                    //{
                    //    sqlParameters.Add(new SqlParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                    //    continue;
                    //}
                    //else if (value != null)
                    //{
                    //    value = SafeSqlLiteral(value.ToString());
                    //}

                    if (propInfo.PropertyType.Name.ToLower() == "string" && value != null)
                    {
                        sqlParameters.Add(new SqlParameter("@" + Clean(propInfo.Name), SafeSqlLiteral(value.ToString())));
                        continue;
                    }

                    sqlParameters.Add(new SqlParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                }

            }
        }

        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="sqlParameters"></param>
        /// <param name="otherParameters"></param>
        public static SqlParameter[] CreateSqlParametersFromDynamic(this SqlParameter[] sqlParameters, object otherParameters)
        {
            var sqlParameterList = new List<SqlParameter>(sqlParameters);

            var otherParametersObj = otherParameters as Object;
            if (otherParametersObj != null)
            {
                var sqlParams = otherParametersObj.GetType().GetProperties().Select(
                   param =>
                   {
                       object value = param.GetValue(otherParameters, new object[] { });
                       if (param.Name.GetType().Name.ToLower() == "string" && value != null)
                       {
                           value = SafeSqlLiteral(value.ToString());
                       }
                       return new SqlParameter("@" + Clean(param.Name), value);
                   }).ToList();
                sqlParameterList.AddRange(sqlParams);
            }
            return sqlParameterList.ToArray();
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> DbParameter Name </param>
        /// <returns></returns>
        public static SqlParameter ToDataParam<T>(this T value, string paramName)
            where T : struct
        {

            return new SqlParameter("@" + paramName, value);
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SqlParameter Name </param>
        /// <param name="sqlDbType"> The <see cref="SqlDbType"/> of the SqlParameter </param>
        /// <returns></returns>
        public static SqlParameter ToDataParam<T>(this T value, string paramName, SqlDbType sqlDbType)
            where T : struct
        {
            var isEnum = typeof(T).IsEnum;

            var sqlParam = new SqlParameter("@" + paramName, sqlDbType)
            {
                Value = !isEnum ? value : GetEnumValue(value, sqlDbType)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SqlParameter Name </param>
        /// <returns></returns>
        public static SqlParameter ToDataParam<T>(this T? value, string paramName)
            where T : struct
        {
            return new SqlParameter("@" + paramName, value.HasValue? (object)value : DBNull.Value);
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SqlParameter Name </param>
        /// <param name="sqlDbType"> The <see cref="SqlDbType"/> of the SqlParameter </param>
        /// <returns></returns>
        public static SqlParameter ToDataParam<T>(this T? value, string paramName, SqlDbType sqlDbType)
            where T : struct
        {

            var isEnum = typeof(T).IsEnum;

            var sqlParam = new SqlParameter("@" + paramName, sqlDbType)
            {
                Value = value.HasValue ? (!isEnum ? (object)value : GetEnumValue(value, sqlDbType)) : DBNull.Value
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached string.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SqlParameter Name </param>
        /// <returns></returns>
        public static SqlParameter ToDataParam(this string value, string paramName)
        {
            var sqlParam = new SqlParameter("@" + paramName, SqlDbType.NVarChar, 4000)
            {
                Value = string.IsNullOrEmpty(value) ? DBNull.Value : (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached struct type.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SqlParameter Name </param>
        /// <param name="size"> The length of the string value in the SqlParameters</param>
        /// <returns></returns>

        public static SqlParameter ToDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new SqlParameter("@" + paramName, SqlDbType.NVarChar, size)
            {
                Value = string.IsNullOrEmpty(value)? DBNull.Value :  (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SqlParameter of attached struct type.
        /// The method allow to pass "'" character to database
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SqlParameter Name </param>
        /// <param name="size"> The length of the string value in the SqlParameters</param>
        /// <returns></returns>
        public static SqlParameter ToSafeDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new SqlParameter("@" + paramName, SqlDbType.NVarChar, size)
            {
                Value = (object)value ?? DBNull.Value
            };
            return sqlParam;
        }

        private static object GetEnumValue(object value, SqlDbType sqlDbType)
        {
            if (value == null)
                return DBNull.Value;
            object v = null;
            switch (sqlDbType)
            {
                case SqlDbType.Bit:
                    {
                        v = Convert.ToBoolean(value);
                        break;
                    }
                case SqlDbType.TinyInt:
                    {
                        v = Convert.ToByte(value);
                        break;
                    }
                case SqlDbType.Int:
                    {
                        v = Convert.ToInt32(value);
                        break;
                    }
                case SqlDbType.BigInt:
                    {
                        v = Convert.ToInt64(value);
                        break;
                    }
                case SqlDbType.SmallInt:
                    {
                        v = Convert.ToInt16(value);
                        break;
                    }
                default:
                {
                    throw new InvalidCastException(string.Format("Value of {0} cannot be converted to {1}", value.GetType().Name, sqlDbType.ToString()));
                }
            }

            return v;
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


#if NET40
        private static string[] GetSpParametersNames(this ISqlSimpleAccess simpleAccess, string spName)
        {
            List<string> paramNames = new List<string>();
            var sqlConnection = simpleAccess.GetNewConnection();
            SqlCommand cmd = new SqlCommand(spName, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            foreach (SqlParameter p in cmd.Parameters)
            {
                paramNames.Add(p.ParameterName);
            }
            return paramNames.ToArray();
        }

        private static string[] GetExtraParameterPropertiesNames(this ISqlSimpleAccess simpleAccess, string spName, SqlParameter[] propertiesParameters)
        {
            List<string> paramNames = new List<string>();

            var spParameters = GetSpParametersNames(simpleAccess, spName);

            var missingParameters = propertiesParameters.Select(pp => pp.ParameterName).Except(spParameters);

            return missingParameters.ToArray();
        }
#endif
    }

}