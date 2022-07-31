using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using SimpleAccess.Repository;

namespace SimpleAccess.SQLite
{

    public static partial class SQLiteRepositoryExtensions
    {

#if !NET40

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
    where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
    where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedListAsync<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static async Task<PagedData<dynamic>> GetDynamicPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, bool addRowNumber, params SQLiteParameter[] SQLiteParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            commandText = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();

            if (distinct) commandText = commandText.Replace("SELECT {{", "SELECT DISTINCT {{");

            //if (!string.IsNullOrEmpty(rowNumberOverColumn)) commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {rowNumberOverColumn}) AS RowNumber");
            if (addRowNumber)
            {
                var sortingParam = SQLiteParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

                if (sortingParam == null || sortingParam.Value == null) throw new Exception("@sortexpression parameter is required.");

                commandText = commandText.Replace("{columns}", $"{{columns}}, ROW_NUMBER() OVER (ORDER BY {sortingParam.Value}) AS RowNumber");
            }


            if (select != null)
            {
                var selectProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(select));

                commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Values.Select(v => v.Name)));


                //var selectProperties = LoadEntityProperties<TEntity>(
                //    select.Invoke(new TEntity())
                //   .GetType()
                //   .GetProperties()
                //   .Where(p => !p.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute || a is NotMappedAttribute))
                //   .Select(p => p.Name.ToLower())
                //   .ToArray());   //.ToDictionary(p => p.Name.ToLower());
                //commandText = commandText.Replace("{columns}", string.Join(", ", selectProperties.Keys));

            }
            else
            {
                commandText = commandText.Replace("{columns}", "*");
            }

            var finalWhereClause = CreateWhereClauseFromSQLiteParameter(whereClause, SQLiteParameters, "@sortexpression", "@totalrows", "@startindex", "@pagesize");
            commandText = commandText.Replace("{whereClause}", finalWhereClause);

            var sortExpression = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
            if (sortExpression != null)
            {
                commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                SQLiteParameters = SQLiteParameters.Where(p => p != sortExpression).ToArray();
            }
            else
            {
                throw new Exception("sortExpression parameter is required.");
            }

            commandType = CommandType.Text;
            

            var totalRowsParameter = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SQLiteParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                SQLiteParameters = SQLiteParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            var result = await sqlRepository.SimpleAccess.ExecuteDynamicsAsync(commandText, commandType, parameters: SQLiteParameters);

            var pagedData = new PagedData<dynamic>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedListAsync<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static async Task<PagedData<TEntity>> GetEntitiesPagedListAsync<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, bool addRowNumber, params SQLiteParameter[] SQLiteParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            Dictionary<string, PropertyInfo> selectProperties = null;




            commandText = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            if (distinct) commandText = commandText.Replace("SELECT {", "SELECT DISTINCT {");

            if (addRowNumber)
            {
                var sortingParam = SQLiteParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

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
                    
                if (addRowNumber)
                {
                    commandText = commandText.Replace("rownumber,", "");
                    var rowNumberProperty = typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name.ToLower() == "rownumber");
                    if (rowNumberProperty != null)
                    {
                        selectProperties.Add(rowNumberProperty.Name, rowNumberProperty);
                    }
                }
            }
            else
            {
                commandText = commandText.Replace("{columns}", "*");
            }

            commandText = commandText.Replace("{whereClause}", whereClause);

            var sortExpression = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
            if (sortExpression != null)
            {
                commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                SQLiteParameters = SQLiteParameters.Where(p => p != sortExpression).ToArray();
            }
            else
            {
                throw new Exception("sortExpression parameter is required.");
            }


            commandType = CommandType.Text;
            

            var totalRowsParameter = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SQLiteParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                SQLiteParameters = SQLiteParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            IEnumerable<TEntity> result = null;

            if (select != null)
            {
                result = await sqlRepository.SimpleAccess.ExecuteEntitiesAsync<TEntity>(commandText, commandType, fieldsToSkip: null,
                    propertyInfoDictionary: selectProperties, parameters: SQLiteParameters);
            }
            else
            {
                result = await sqlRepository.SimpleAccess.ExecuteEntitiesAsync<TEntity>(commandText, commandType, fieldsToSkip: null, parameters: SQLiteParameters);
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


        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedList<TEntity>(sqlRepository, false, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
    where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, false, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, false, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, false, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, null, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, false, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, select, null, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<dynamic> GetDynamicPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, bool addRowNumber, params SQLiteParameter[] SQLiteParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            commandText = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();

            if (distinct) commandText = commandText.Replace("SELECT {", "SELECT DISTINCT {");

            if (addRowNumber)
            {
                var sortingParam = SQLiteParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

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

            var finalWhereClause = CreateWhereClauseFromSQLiteParameter(whereClause, SQLiteParameters, "@sortexpression", "@totalrows", "@startindex", "@pagesize");
            commandText = commandText.Replace("{whereClause}", finalWhereClause);

            var sortExpression = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
            if (sortExpression != null)
            {
                commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                SQLiteParameters = SQLiteParameters.Where(p => p != sortExpression).ToArray();
            }
            else
            {
                throw new Exception("sortExpression parameter is required.");
            }

            commandType = CommandType.Text;
            

            var totalRowsParameter = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SQLiteParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                SQLiteParameters = SQLiteParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            var result = sqlRepository.SimpleAccess.ExecuteDynamics(commandText, commandType, parameters: SQLiteParameters);

            var pagedData = new PagedData<dynamic>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedList<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, object whereParameters, bool addRowNumber = false)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, false, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, select, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, false, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }

        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, string whereClause, int startIndex, int pageSize, string sortExpression, bool addRowNumber, params SQLiteParameter[] parameters)
            where TEntity : class, new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetEntitiesPagedList<TEntity>(sqlRepository, distinct, null, whereClause, addRowNumber, pagedListParameters.GetParametersToExecute());
        }
        public static PagedData<TEntity> GetEntitiesPagedList<TEntity>(this ISQLiteRepository sqlRepository, bool distinct, Func<TEntity, object> select, string whereClause, bool addRowNumber, params SQLiteParameter[] SQLiteParameters)
            where TEntity : class, new()
        {
            string commandText = "";
            CommandType commandType = CommandType.StoredProcedure;

            Dictionary<string, PropertyInfo> selectProperties = null;




            commandText = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity)).SqlBuilder.GetGetPagedListStatement();
            if (distinct) commandText = commandText.Replace("SELECT {", "SELECT DISTINCT {");

            if (addRowNumber)
            {
                var sortingParam = SQLiteParameters.FirstOrDefault(p => p.ParameterName.ToLower() == "@sortexpression");

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
                if (addRowNumber)
                {
                    commandText = commandText.Replace("rownumber,", "");
                    var rowNumberProperty = typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name.ToLower() == "rownumber");
                    if (rowNumberProperty != null)
                    {
                        selectProperties.Add(rowNumberProperty.Name.ToLower(), rowNumberProperty);
                    }
                }
            }
            else
            {
                commandText = commandText.Replace("{columns}", "*");
            }

            commandText = commandText.Replace("{whereClause}", whereClause);

            var sortExpression = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@sortExpression");
            if (sortExpression != null)
            {
                commandText = commandText.Replace("{sortExpression}", sortExpression.Value.ToString());

                SQLiteParameters = SQLiteParameters.Where(p => p != sortExpression).ToArray();
            }
            else
            {
                throw new Exception("sortExpression parameter is required.");
            }

            commandType = CommandType.Text;

            var totalRowsParameter = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SQLiteParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                SQLiteParameters = SQLiteParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            IEnumerable<TEntity> result = null;

            if (select != null)
            {
                result = sqlRepository.SimpleAccess.ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip: null,
                    propertyInfoDictionary: selectProperties, parameters: SQLiteParameters);
            }
            else
            {
                result = sqlRepository.SimpleAccess.ExecuteEntities<TEntity>(commandText, commandType, fieldsToSkip: null, parameters: SQLiteParameters);
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
