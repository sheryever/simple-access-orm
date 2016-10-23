using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SimpleAccess.Core
{
    
    /// <summary>
    /// Class only exsist for idea
    /// </summary>
    public static class DatabaseExtensions
    {
        /*
        //public static IEnumerable<TResult> ExecuteStoredProcedure<TResult>(this Database database, IStoredProcedure<TResult> procedure)
        //{
        //    var parameters = CreateSqlParametersFromProperties(procedure);

        //    var format = CreateSPCommand<TResult>(parameters);

        //    return database.SqlQuery<TResult>(format, parameters.Cast<object>().ToArray());
        //}

        private static List<IDataParameter> CreateSqlParametersFromProperties<TResult>(IStoredProcedure<TResult> procedure)
        {
            var procedureType = procedure.GetType();
            var propertiesOfProcedure = procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var parameters =
                propertiesOfProcedure.Select(propertyInfo => new SqlParameter(string.Format("@{0}", (object) propertyInfo.Name),
                                                                              propertyInfo.GetValue(procedure, new object[] {})))
                    .ToList();
            return parameters;
        }

        private static string CreateSpCommand<TResult>(List<SqlParameter> parameters)
        {
            var name = typeof(TResult).Name;
            // i am not using the sp_ prefix in the storedprocedres that is why i am commenting this line
            //string queryString = string.Format("sp_{0}", name);
            string queryString = name;
            parameters.ForEach(x => queryString = string.Format("{0} {1},", queryString, x.ParameterName));

            return queryString.TrimEnd(',');
        }

        /// <summary>
        /// Only Exist for idea
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        public interface IStoredProcedure<TResult>
        {
        }

        */

        public static void ClearDbCommand(this IDbCommand dbCommand)
        {
            if (dbCommand != null)
                dbCommand.Parameters.Clear();
        }
    }
}