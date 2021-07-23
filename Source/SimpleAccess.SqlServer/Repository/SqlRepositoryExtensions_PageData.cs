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

namespace SimpleAccess.SqlServer
{

    public static partial class SqlRepositoryExtensions
    {

#if !NET40

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
    where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static async Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, bool addRowNumber, params SqlParameter[] sqlParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;
            if (sqlRepository is SqlSpRepository)
            {
                if (distinct) throw new NotSupportedException($"Parameter {nameof(distinct)} is not supported with SqlSpRepository");

                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();

                if (distinct) commandText = commandText.Replace("SELECT {{", "SELECT DISTINCT {{");

                //if (!string.IsNullOrEmpty(rowNumberOverColumn)) commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {rowNumberOverColumn}) AS RowNumber");
                if (addRowNumber)
                {
                    var sortingParam = sqlParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

                    if (sortingParam == null || sortingParam.Value == null) throw new Exception("@sortexpression parameter is required.");

                    commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {sortingParam.Value}) AS RowNumber");
                }


                if (select != null)
                {
                    var selectProperties = LoadEntityProperties<TEntity>(
                        select.Invoke(new TEntity())
                       .GetType()
                       .GetProperties()
                       .Where(p => !p.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute || a is NotMappedAttribute))
                       .Select(p => p.Name.ToLower())
                       .ToArray());   //.ToDictionary(p => p.Name.ToLower());
                    commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Keys));

                }
                else
                {
                    commandText = commandText.Replace("{columns}", "*");
                }

                var whereClause = CreateWhereClauseFromSqlParameter(sqlParameters, "@sortexpression", "@totalrows", "@startindex", "@pagesize");
                commandText = commandText.Replace("{whereClause}", whereClause);

                var sortExpression = sqlParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
                if (sortExpression != null)
                {
                    commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                    sqlParameters = sqlParameters.Where(p => p != sortExpression).ToArray();
                }
                else
                {
                    throw new Exception("sortExpression parameter is required.");
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

            var result = await sqlRepository.SimpleAccess.ExecuteDynamicsAsync(commandText, commandType, parameters: sqlParameters);

            var pagedData = new PagedData<dynamic>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static async Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, bool addRowNumber, params SqlParameter[] sqlParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            Dictionary<string, PropertyInfo> selectProperties = null;



            if (sqlRepository is SqlSpRepository)
            {
                if (distinct) throw new NotSupportedException($"Parameter {nameof(distinct)} is not supported with SqlSpRepository");

                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
                if (distinct) commandText = commandText.Replace("SELECT {", "SELECT DISTINCT {");

                if (addRowNumber)
                {
                    var sortingParam = sqlParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

                    if (sortingParam == null || sortingParam.Value == null) throw new Exception("@sortexpression parameter is required.");

                    commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {sortingParam.Value}) AS RowNumber");
                }


                if (select != null)
                {
                    selectProperties = LoadEntityProperties<TEntity>(
                        select.Invoke(new TEntity())
                       .GetType()
                       .GetProperties()
                       .Where(p => !p.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute || a is NotMappedAttribute))
                       .Select(p => p.Name.ToLower())
                       .ToArray());   //.ToDictionary(p => p.Name.ToLower());
                    commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Keys));
                    
                    if (addRowNumber) commandText = commandText.Replace("rownumber,", "");

                }
                else
                {
                    commandText = commandText.Replace("{columns}", "*");
                }

                commandText = commandText.Replace("{whereClause}", whereClause);

                var sortExpression = sqlParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
                if (sortExpression != null)
                {
                    commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                    sqlParameters = sqlParameters.Where(p => p != sortExpression).ToArray();
                }
                else
                {
                    throw new Exception("sortExpression parameter is required.");
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

            IEnumerable<TEntity> result = null;

            if (select != null)
            {
                result = await sqlRepository.SimpleAccess.ExecuteEntitiesAsync<TEntity>(commandText, commandType, fieldsToSkip: null,
                    propertyInfoDictionary: selectProperties, parameters: sqlParameters);
            }
            else
            {
                result = await sqlRepository.SimpleAccess.ExecuteEntitiesAsync<TEntity>(commandText, commandType, fieldsToSkip: null, parameters: sqlParameters);
            }

            var pagedData = new PagedData<TEntity>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }
#endif


        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedList<TEntity>(sqlRepository, false, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
    where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, false, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, false, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, false, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, false, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, select, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, bool addRowNumber, params SqlParameter[] sqlParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;
            if (sqlRepository is SqlSpRepository)
            {
                if (distinct) throw new NotSupportedException($"Parameter {nameof(distinct)} is not supported with SqlSpRepository");

                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();

                if (distinct) commandText = commandText.Replace("SELECT {", "SELECT DISTINCT {");

                if (addRowNumber)
                {
                    var sortingParam = sqlParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

                    if (sortingParam == null || sortingParam.Value == null) throw new Exception("@sortexpression parameter is required.");

                    commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {sortingParam.Value}) AS RowNumber");
                }

                if (select != null)
                {
                    var selectProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(select));

                    commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Values.Select(v => v.Name)));
                }
                else
                {
                    commandText = commandText.Replace("{columns}", "*");
                }

                var whereClause = CreateWhereClauseFromSqlParameter(sqlParameters, "@sortexpression", "@totalrows", "@startindex", "@pagesize");
                commandText = commandText.Replace("{whereClause}", whereClause);

                var sortExpression = sqlParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
                if (sortExpression != null)
                {
                    commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                    sqlParameters = sqlParameters.Where(p => p != sortExpression).ToArray();
                }
                else
                {
                    throw new Exception("sortExpression parameter is required.");
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

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedList<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SqlParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISqlRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, bool addRowNumber, params SqlParameter[] sqlParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            Dictionary<string, PropertyInfo> selectProperties = null;



            if (sqlRepository is SqlSpRepository)
            {
                if (distinct) throw new NotSupportedException($"Parameter {nameof(distinct)} is not supported with SqlSpRepository");

                commandText = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            }
            else
            {
                commandText = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
                if (distinct) commandText = commandText.Replace("SELECT {", "SELECT DISTINCT {");

                if (addRowNumber)
                {
                    var sortingParam = sqlParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

                    if (sortingParam == null || sortingParam.Value == null) throw new Exception("@sortexpression parameter is required.");

                    commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {sortingParam.Value}) AS RowNumber");
                }

                if (select != null)
                {
                    selectProperties = LoadEntityProperties<TEntity>(
                        select.Invoke(new TEntity())
                       .GetType()
                       .GetProperties()
                       .Where(p => !p.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute || a is NotMappedAttribute))
                       .Select(p => p.Name.ToLower())
                       .ToArray());   //.ToDictionary(p => p.Name.ToLower());
                    commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Values.Select(v => v.Name)));
                    if (addRowNumber) commandText = commandText.Replace("rownumber,", "");
                }
                else
                {
                    commandText = commandText.Replace("{columns}", "*");
                }

                commandText = commandText.Replace("{whereClause}", whereClause);

                var sortExpression = sqlParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
                if (sortExpression != null)
                {
                    commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                    sqlParameters = sqlParameters.Where(p => p != sortExpression).ToArray();
                }
                else
                {
                    throw new Exception("sortExpression parameter is required.");
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

            IEnumerable<TEntity> result = null;

            if (select != null)
            {
                result = sqlRepository.SimpleAccess.ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip: null,
                    propertyInfoDictionary: selectProperties, parameters: sqlParameters);
            }
            else
            {
                result = sqlRepository.SimpleAccess.ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip: null, parameters: sqlParameters);
            }

            var pagedData = new PagedData<TEntity>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }
    }
}
