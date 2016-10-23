using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Oracle.ManagedDataAccess.Client;

namespace SimpleAccess.Oracle
{
    public static class OracleParametersExtensions
    {
        /// <summary>
        /// Takes the dynamic object and creates the Sql Parameters from its properties
        /// </summary>
        /// <param name="oracleParameters"></param>
        /// <param name="otherParameters"></param>
        public static void CreateOracleParametersFromDynamic(this List<OracleParameter> oracleParameters, dynamic otherParameters)
        {


            var otherParametersObj = otherParameters as Object;
            if (otherParametersObj != null)
            {
                 var oracleParams = otherParametersObj.GetType().GetProperties().Select(
                    param => {
                        object value = param.GetValue(otherParameters);
                        if (param.Name.GetType().Name.ToLower() == "string" && value != null)
                        {
                            value = SafeSqlLiteral(value.ToString());
                        }
                        return new OracleParameter("@" + Clean(param.Name), value);
                    }).ToList();
                 oracleParameters.AddRange(oracleParams);
            }
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