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

namespace SimpleAccess.SqlServer
{

    public static class SqlRepositoryExtensions
    {

#if !NET40
        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, object whereParameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, params SqlParameter[] parameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static async Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            string commandText = "";
            if (sqlRepository is SqlSpRepository)
            {
                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();

            }

            var totalRowsParameter = sqlParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SqlParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                sqlParameters = sqlParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            var result = await sqlRepository.SimpleAccess.ExecuteDynamicsAsync(commandText, sqlParameters);
            var pagedData = new PagedData<dynamic>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }
#endif


        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize);
            return GetDynamicPagedList<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedList<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, object whereParameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, params SqlParameter[] parameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;
            if (sqlRepository is SqlSpRepository)
            {
                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();


                commandText = commandText.Replace("{columns}", "*");
                var whereClause = "";
                commandText = commandText.Replace("{whereClause}", whereClause);

                var sortExpression = sqlParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
                if (sortExpression != null)
                {
                    commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                    sqlParameters = sqlParameters.Where(p => p != sortExpression).ToArray();
                }



                commandType = CommandType.Text;
            }

            var totalRowsParameter = sqlParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SqlParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                sqlParameters = sqlParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            var result = sqlRepository.SimpleAccess.ExecuteDynamics(commandText, commandType, parameters: sqlParameters);
            var pagedData = new PagedData<dynamic>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }



        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize);
            return GetEntitiesPagedList<TEntity>(sqlRepository, select, whereClause, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedList<TEntity>(sqlRepository, select, whereClause, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, select, whereClause, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, select, whereClause, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, params SqlParameter[] sqlParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            var selectProperties = LoadEntityProperties<TEntity>(
                select.Invoke(new TEntity())
                    .GetType()
                    .GetProperties()
                    .Select(p => p.Name.ToLower())
                    .ToArray());   //.ToDictionary(p => p.Name.ToLower());

            if (sqlRepository is SqlSpRepository)
            {
                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();


                commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Keys));

                commandText = commandText.Replace("{whereClause}", whereClause);

                var sortExpression = sqlParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
                if (sortExpression != null)
                {
                    commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                    sqlParameters = sqlParameters.Where(p => p != sortExpression).ToArray();
                }


                commandType = CommandType.Text;
            }

            var totalRowsParameter = sqlParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SqlParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                sqlParameters = sqlParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }


            
            var result = sqlRepository.SimpleAccess.ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip: null, 
                propertyInfoDictionary: selectProperties, parameters: sqlParameters);
            var pagedData = new PagedData<TEntity>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }


        private static Dictionary<string, PropertyInfo> LoadEntityProperties<TEntity>(string[] properitiesNames)
            where TEntity: class
        {
            return typeof(TEntity).GetProperties()
                .Where(p => properitiesNames.Contains(p.Name.ToLower()))
                .ToDictionary(p => p.Name.ToLower());
        }

        private static string CreateWhereClauseFromSqlParameter(SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }


        #region SqlEntityRepository

        #endregion
    }
}
