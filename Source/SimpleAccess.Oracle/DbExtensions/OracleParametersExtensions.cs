using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Oracle.ManagedDataAccess.Client;

namespace SimpleAccess.Oracle
{
    /// <summary>
    /// Defines the extra extensions method for <see cref="OracleParameter"/>.
    /// </summary>
    public static class OracleParametersExtensions
    {
        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="oracleParameters"></param>
        /// <param name="otherParameters"></param>
        public static void CreateOracleParametersFromObject(this List<OracleParameter> oracleParameters, object otherParameters)
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
                        oracleParameters.Add(new OracleParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                        continue;
                    }
                    else if (value != null)
                    {
                        value = SafeSqlLiteral(value.ToString());
                    }
                    oracleParameters.Add(new OracleParameter("@" + Clean(propInfo.Name), value ?? DBNull.Value));
                }
                //var sqlParams = otherParametersObj.GetType().GetProperties().Select(
                //    param =>
                //    {
                //        object value = param.GetValue(otherParameters, new object[] { });
                //        if (param.PropertyType.Name.ToLower() == "string" && value != null)
                //        {
                //            value = SafeSqlLiteral(value.ToString());
                //        }
                //        return new OracleParameter("@" + Clean(param.Name), value ?? DBNull.Value);
                //    });//.ToList();
                //oracleParameters.AddRange(sqlParams);
            }
        }

        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="oracleParameters"></param>
        /// <param name="otherParameters"></param>
        public static OracleParameter[] CreateOracleParametersFromDynamic(this OracleParameter[] oracleParameters, object otherParameters)
        {
            var oracleParameterList = new List<OracleParameter>(oracleParameters);

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
                       return new OracleParameter("@" + Clean(param.Name), value);
                   }).ToList();
                oracleParameterList.AddRange(sqlParams);
            }
            return oracleParameterList.ToArray();
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> DbParameter Name </param>
        /// <returns></returns>
        public static OracleParameter ToDataParam<T>(this T value, string paramName)
            where T : struct
        {

            return new OracleParameter("@" + paramName, value);
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> OracleParameter Name </param>
        /// <param name="oracleDbType"> The <see cref="OracleDbType"/> of the OracleParameter </param>
        /// <returns></returns>
        public static OracleParameter ToDataParam<T>(this T value, string paramName, OracleDbType oracleDbType)
            where T : struct
        {
            var isEnum = typeof(T).IsEnum;

            var sqlParam = new OracleParameter("@" + paramName, oracleDbType)
            {
                Value = !isEnum ? value : GetEnumValue(value, oracleDbType)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> OracleParameter Name </param>
        /// <returns></returns>
        public static OracleParameter ToDataParam<T>(this T? value, string paramName)
            where T : struct
        {
            return new OracleParameter("@" + paramName, value.HasValue? (object)value : DBNull.Value);
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached nullable struct type.
        /// </summary>
        /// <typeparam name="T"> Attached by variable type.</typeparam>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> OracleParameter Name </param>
        /// <param name="oracleDbType"> The <see cref="OracleDbType"/> of the OracleParameter </param>
        /// <returns></returns>
        public static OracleParameter ToDataParam<T>(this T? value, string paramName, OracleDbType oracleDbType)
            where T : struct
        {

            var isEnum = typeof(T).IsEnum;

            var sqlParam = new OracleParameter("@" + paramName, oracleDbType)
            {
                Value = value.HasValue ? (!isEnum ? (object)value : GetEnumValue(value, oracleDbType)) : DBNull.Value
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached string.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> OracleParameter Name </param>
        /// <returns></returns>
        public static OracleParameter ToDataParam(this string value, string paramName)
        {
            var sqlParam = new OracleParameter("@" + paramName, OracleDbType.NVarchar2, 4000)
            {
                Value = string.IsNullOrEmpty(value) ? DBNull.Value : (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached struct type.
        /// The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> OracleParameter Name </param>
        /// <param name="size"> The length of the string value in the OracleParameters</param>
        /// <returns></returns>

        public static OracleParameter ToDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new OracleParameter("@" + paramName, OracleDbType.NVarchar2, size)
            {
                Value = string.IsNullOrEmpty(value)? DBNull.Value :  (object)SafeSqlLiteral(value)
            };
            return sqlParam;
        }

        /// <summary>
        /// Create and returns a OracleParameter of attached struct type.
        /// The method allow to pass "'" character to database
        /// </summary>
        /// <param name="value"> The value of attached variable.</param>
        /// <param name="paramName"> OracleParameter Name </param>
        /// <param name="size"> The length of the string value in the OracleParameters</param>
        /// <returns></returns>
        public static OracleParameter ToSafeDataParam(this string value, string paramName, int size)
        {
            var sqlParam = new OracleParameter("@" + paramName, OracleDbType.NVarchar2, size)
            {
                Value = (object)value ?? DBNull.Value
            };
            return sqlParam;
        }

        private static object GetEnumValue(object value, OracleDbType oracleDbType)
        {
            if (value == null)
                return DBNull.Value;
            object v = null;
            switch (oracleDbType)
            {
                //case OracleDbType.:
                //    {
                //        v = Convert.ToBoolean(value);
                //        break;
                //    }
                case OracleDbType.Byte:
                    {
                        v = Convert.ToByte(value);
                        break;
                    }
                case OracleDbType.Int32:
                    {
                        v = Convert.ToInt32(value);
                        break;
                    }
                case OracleDbType.Int64:
                    {
                        v = Convert.ToInt64(value);
                        break;
                    }
                case OracleDbType.Int16:
                    {
                        v = Convert.ToInt16(value);
                        break;
                    }
                default:
                {
                    throw new InvalidCastException(string.Format("Value of {0} cannot be converted to {1}", value.GetType().Name, oracleDbType.ToString()));
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