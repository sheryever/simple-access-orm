using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Defines the extra extensions method for <see cref="SQLiteParameter"/>.
    /// </summary>
    public static class SQLiteParametersExtensions
    {
        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="sqliteParameters"></param>
        /// <param name="otherParameters"></param>
        public static void CreateSQLiteParametersFromObject(this List<SQLiteParameter> sqliteParameters, object otherParameters)
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
                        sqliteParameters.Add(new SQLiteParameter("@" + Clean(propInfo.Name), value));
                        continue;
                    }
                    else if (value != null)
                    {
                        //value = SafeSqlLiteral(value.ToString());
                        value = value.ToString();
                    }
                    sqliteParameters.Add(new SQLiteParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                }
                //var sqlParams = otherParametersObj.GetType().GetProperties().Select(
                //    param =>
                //    {
                //        object value = param.GetValue(otherParameters, new object[] { });
                //        if (param.PropertyType.Name.ToLower() == "string" && value != null)
                //        {
                //            value = SafeSqlLiteral(value.ToString());
                //        }
                //        return new SQLiteParameter("@" + Clean(param.Name), value ?? DBNull.Value);
                //    });//.ToList();
                //SQLiteParameters.AddRange(sqlParams);
            }
        }

        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="SQLiteParameters"></param>
        /// <param name="otherParameters"></param>
        public static SQLiteParameter[] CreateSQLiteParametersFromDynamic(this SQLiteParameter[] sqliteParameters, object otherParameters)
        {
            var SQLiteParameterList = new List<SQLiteParameter>(sqliteParameters);

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
                       return new SQLiteParameter("@" + Clean(param.Name), value);
                   }).ToList();
                SQLiteParameterList.AddRange(sqlParams);
            }
            return SQLiteParameterList.ToArray();
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> DbParameter Name </param>
        /// <returns></returns>
        public static SQLiteParameter ToDataParam<T>(this T value, string paramName)
            where T : struct
        {

            return new SQLiteParameter("@" + paramName, value);
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SQLiteParameter Name </param>
        /// <param name="sqlDbType"> The <see cref="SqlDbType"/> of the SQLiteParameter </param>
        /// <returns></returns>
        public static SQLiteParameter ToDataParam<T>(this T value, string paramName, DbType dbType)
            where T : struct
        {
            var isEnum = typeof(T).IsEnum;

            var sqlParam = new SQLiteParameter("@" + paramName, dbType)
            {
                Value = !isEnum ? value : GetEnumValue(value, dbType)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SQLiteParameter Name </param>
        /// <returns></returns>
        public static SQLiteParameter ToDataParam<T>(this T? value, string paramName)
            where T : struct
        {
            return new SQLiteParameter("@" + paramName, value.HasValue? (object)value : DBNull.Value);
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SQLiteParameter Name </param>
        /// <param name="sqlDbType"> The <see cref="SqlDbType"/> of the SQLiteParameter </param>
        /// <returns></returns>
        public static SQLiteParameter ToDataParam<T>(this T? value, string paramName, DbType dbType)
            where T : struct
        {

            var isEnum = typeof(T).IsEnum;

            var sqlParam = new SQLiteParameter("@" + paramName, dbType)
            {
                Value = value.HasValue ? (!isEnum ? (object)value : GetEnumValue(value, dbType)) : DBNull.Value
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached string.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SQLiteParameter Name </param>
        /// <returns></returns>
        public static SQLiteParameter ToDataParam(this string value, string paramName)
        {
            var sqlParam = new SQLiteParameter("@" + paramName, DbType.String, 4000)
            {
                Value = string.IsNullOrEmpty(value) ? DBNull.Value : (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached struct type.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SQLiteParameter Name </param>
        /// <param name="size"> The length of the string value in the SQLiteParameters</param>
        /// <returns></returns>

        public static SQLiteParameter ToDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new SQLiteParameter("@" + paramName, DbType.String, size)
            {
                Value = string.IsNullOrEmpty(value)? DBNull.Value :  (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a SQLiteParameter of attached struct type.
        /// The method allow to pass "'" character to database
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> SQLiteParameter Name </param>
        /// <param name="size"> The length of the string value in the SQLiteParameters</param>
        /// <returns></returns>
        public static SQLiteParameter ToSafeDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new SQLiteParameter("@" + paramName, DbType.String, size)
            {
                Value = (object)value ?? DBNull.Value
            };
            return sqlParam;
        }

        private static object GetEnumValue(object value, DbType dbType)
        {
            if (value == null)
                return DBNull.Value;
            object v = null;
            switch (dbType)
            {
                case DbType.Boolean:
                    {
                        v = Convert.ToBoolean(value);
                        break;
                    }
                case DbType.Byte:
                    {
                        v = Convert.ToByte(value);
                        break;
                    }
                case DbType.Int32:
                    {
                        v = Convert.ToInt32(value);
                        break;
                    }
                case DbType.Int64:
                    {
                        v = Convert.ToInt64(value);
                        break;
                    }
                case DbType.Int16:
                    {
                        v = Convert.ToInt16(value);
                        break;
                    }
                default:
                {
                    throw new InvalidCastException(string.Format("Value of {0} cannot be converted to {1}", value.GetType().Name, dbType.ToString()));
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