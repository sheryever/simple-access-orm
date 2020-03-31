using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SimpleAccess.MySql
{
    /// <summary>
    /// Defines the extra extensions method for <see cref="MySqlParameter"/>.
    /// </summary>
    public static class SqlParametersExtensions
    {
        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="sqlParameters"></param>
        /// <param name="otherParameters"></param>
        public static void CreateSqlParametersFromObject(this List<MySqlParameter> sqlParameters, object otherParameters)
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
                    if (propInfo.PropertyType.Name.ToLower() != "string")
                    {
                        sqlParameters.Add(new MySqlParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                        continue;
                    }
                    else if (value != null)
                    {
                        value = SafeSqlLiteral(value.ToString());
                    }
                    sqlParameters.Add(new MySqlParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                }
                //var sqlParams = otherParametersObj.GetType().GetProperties().Select(
                //    param =>
                //    {
                //        object value = param.GetValue(otherParameters, new object[] { });
                //        if (param.PropertyType.Name.ToLower() == "string" && value != null)
                //        {
                //            value = SafeSqlLiteral(value.ToString());
                //        }
                //        return new MySqlParameter("@" + Clean(param.Name), value ?? DBNull.Value);
                //    });//.ToList();
                //sqlParameters.AddRange(sqlParams);
            }
        }

        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="sqlParameters"></param>
        /// <param name="otherParameters"></param>
        public static MySqlParameter[] CreateSqlParametersFromDynamic(this MySqlParameter[] sqlParameters, object otherParameters)
        {
            var sqlParameterList = new List<MySqlParameter>(sqlParameters);

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
                       return new MySqlParameter("@" + Clean(param.Name), value);
                   }).ToList();
                sqlParameterList.AddRange(sqlParams);
            }
            return sqlParameterList.ToArray();
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> DbParameter Name </param>
        /// <returns></returns>
        public static MySqlParameter ToDataParam<T>(this T value, string paramName)
            where T : struct
        {

            return new MySqlParameter("@" + paramName, value);
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> MySqlParameter Name </param>
        /// <param name="mySqlDbType"> The <see cref="SqlDbType"/> of the MySqlParameter </param>
        /// <returns></returns>
        public static MySqlParameter ToDataParam<T>(this T value, string paramName, MySqlDbType mySqlDbType)
            where T : struct
        {
            var isEnum = typeof(T).IsEnum;

            var sqlParam = new MySqlParameter("@" + paramName, mySqlDbType)
            {
                Value = !isEnum ? value : GetEnumValue(value, mySqlDbType)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> MySqlParameter Name </param>
        /// <returns></returns>
        public static MySqlParameter ToDataParam<T>(this T? value, string paramName)
            where T : struct
        {
            return new MySqlParameter("@" + paramName, value.HasValue? (object)value : DBNull.Value);
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> MySqlParameter Name </param>
        /// <param name="mySqlDbType"> The <see cref="SqlDbType"/> of the MySqlParameter </param>
        /// <returns></returns>
        public static MySqlParameter ToDataParam<T>(this T? value, string paramName, MySqlDbType mySqlDbType)
            where T : struct
        {

            var isEnum = typeof(T).IsEnum;

            var sqlParam = new MySqlParameter("@" + paramName, mySqlDbType)
            {
                Value = value.HasValue ? (!isEnum ? (object)value : GetEnumValue(value, mySqlDbType)) : DBNull.Value
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached string.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> MySqlParameter Name </param>
        /// <returns></returns>
        public static MySqlParameter ToDataParam(this string value, string paramName)
        {
            var sqlParam = new MySqlParameter("@" + paramName, MySqlDbType.VarChar, 4000)
            {
                Value = string.IsNullOrEmpty(value) ? DBNull.Value : (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached struct type.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> MySqlParameter Name </param>
        /// <param name="size"> The length of the string value in the SqlParameters</param>
        /// <returns></returns>

        public static MySqlParameter ToDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new MySqlParameter("@" + paramName, MySqlDbType.VarChar, size)
            {
                Value = string.IsNullOrEmpty(value)? DBNull.Value :  (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a MySqlParameter of attached struct type.
        /// The method allow to pass "'" character to database
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> MySqlParameter Name </param>
        /// <param name="size"> The length of the string value in the SqlParameters</param>
        /// <returns></returns>
        public static MySqlParameter ToSafeDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new MySqlParameter("@" + paramName, MySqlDbType.VarChar, size)
            {
                Value = (object)value ?? DBNull.Value
            };
            return sqlParam;
        }

        private static object GetEnumValue(object value, MySqlDbType mySqlDbType)
        {
            if (value == null)
                return DBNull.Value;
            object v = null;
            switch (mySqlDbType)
            {
                case MySqlDbType.Bit:
                    {
                        v = Convert.ToBoolean(value);
                        break;
                    }
                case MySqlDbType.Byte:
                    {
                        v = Convert.ToByte(value);
                        break;
                    }
                case MySqlDbType.Int32:
                    {
                        v = Convert.ToInt32(value);
                        break;
                    }
                case MySqlDbType.Int64:
                    {
                        v = Convert.ToInt64(value);
                        break;
                    }
                case MySqlDbType.Int16:
                    {
                        v = Convert.ToInt16(value);
                        break;
                    }
                default:
                {
                    throw new InvalidCastException(string.Format("Value of {0} cannot be converted to {1}", value.GetType().Name, mySqlDbType.ToString()));
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

   }
}