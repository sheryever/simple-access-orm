using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

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
        /// <param name="SQLiteParameters"></param>
        /// <param name="otherParameters"></param>
        public static void CreateSQLiteParametersFromObject(this List<SQLiteParameter> SQLiteParameters, object otherParameters)
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
                        SQLiteParameters.Add(value as SQLiteParameter);
                        continue;
                    }

                    //if (propInfo.PropertyType.Name.ToLower() != "string")
                    //{
                    //    SQLiteParameters.Add(new SQLiteParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                    //    continue;
                    //}
                    //else if (value != null)
                    //{
                    //    value = SafeSqlLiteral(value.ToString());
                    //}

                    if (propInfo.PropertyType.Name.ToLower() == "string" && value != null)
                    {
                        SQLiteParameters.Add(new SQLiteParameter("@" + Clean(propInfo.Name), SafeSqlLiteral(value.ToString())));
                        continue;
                    }

                    SQLiteParameters.Add(new SQLiteParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                }

            }
        }

        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="SQLiteParameters"></param>
        /// <param name="otherParameters"></param>
        public static SQLiteParameter[] CreateSQLiteParametersFromDynamic(this SQLiteParameter[] SQLiteParameters, object otherParameters)
        {
            var SQLiteParameterList = new List<SQLiteParameter>(SQLiteParameters);

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
        public static SQLiteParameter ToDataParam<T>(this T value, string paramName, SqlDbType sqlDbType)
            where T : struct
        {
            var isEnum = typeof(T).IsEnum;

            var sqlParam = new SQLiteParameter("@" + paramName, sqlDbType)
            {
                Value = !isEnum ? value : GetEnumValue(value, sqlDbType)
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
        public static SQLiteParameter ToDataParam<T>(this T? value, string paramName, SqlDbType sqlDbType)
            where T : struct
        {

            var isEnum = typeof(T).IsEnum;

            var sqlParam = new SQLiteParameter("@" + paramName, sqlDbType)
            {
                Value = value.HasValue ? (!isEnum ? (object)value : GetEnumValue(value, sqlDbType)) : DBNull.Value
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
            /// Check SQLite string parameter lenght
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
            var SQLiteConnection = simpleAccess.GetNewConnection();
            SQLiteCommand cmd = new SQLiteCommand(spName, SQLiteConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SQLiteConnection.Open();
            SQLiteCommandBuilder.DeriveParameters(cmd);
            foreach (SQLiteParameter p in cmd.Parameters)
            {
                paramNames.Add(p.ParameterName);
            }
            return paramNames.ToArray();
        }

        private static string[] GetExtraParameterPropertiesNames(this ISqlSimpleAccess simpleAccess, string spName, SQLiteParameter[] propertiesParameters)
        {
            List<string> paramNames = new List<string>();

            var spParameters = GetSpParametersNames(simpleAccess, spName);

            var missingParameters = propertiesParameters.Select(pp => pp.ParameterName).Except(spParameters);

            return missingParameters.ToArray();
        }
#endif
    }

}