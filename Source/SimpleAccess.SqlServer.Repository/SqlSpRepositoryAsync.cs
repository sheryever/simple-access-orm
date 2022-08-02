using System;
using System.Collections.Generic;
using System.Data;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1 || NET6_0_OR_GREATER
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.SqlServer
{

    /// <summary> Implements SqlRepository base SqlSimpleAccess with command type stored procedures. </summary>
    public partial class SqlSpRepository : ISqlRepository
    {

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all {TEntity} in this
        /// collection. </returns>

        public virtual Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(string fieldToSkip = null)
            where TEntity : new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
//            string commandText = string.Format("{0}_GetAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetAllStatement();
            
            return SimpleAccess.ExecuteEntitiesAsync<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip);
        }

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all {TEntity} in this
        /// collection. </returns>

        public virtual Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string fieldToSkip = null)
            where TEntity : new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //            string commandText = string.Format("{0}_GetAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetAllStatement();

            return SimpleAccess.ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, CommandType.StoredProcedure, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> GetAsync<TEntity>(long id, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetByIdStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip, null,
                new SqlParameter("@id", id));

            // return Get<TEntity>(new SqlParameter("@id", id), transaction, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transactionContext"> (optional) the SqlTransactionAsyncContext. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> GetAsync<TEntity>(SqlTransactionAsyncContext transactionContext, long id, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            
            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetByIdStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(transactionContext, commandText, CommandType.StoredProcedure, fieldToSkip, null,
                new SqlParameter("@id", id));

            // return Get<TEntity>(new SqlParameter("@id", id), transaction, fieldToSkip);
        }


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> GetAsync<TEntity>(SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetByIdStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip, null, new[] { sqlParameter });
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> GetAsync<TEntity>(SqlTransactionAsyncContext transactionContext, SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetByIdStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(transactionContext, commandText, CommandType.StoredProcedure, fieldToSkip, null, new[] { sqlParameter });
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> GetAsync<TEntity>(object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            
            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetByIdStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(commandText, CommandType.StoredProcedure, paramObject, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> GetAsync<TEntity>(SqlTransactionAsyncContext transactionContext, object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetGetByIdStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(transactionContext, commandText, CommandType.StoredProcedure, paramObject, fieldToSkip);
        }

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetFindStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(commandText, CommandType.StoredProcedure
                    , fieldToSkip, parameters: new SqlParameter("@whereClause", DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)));
        }

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<TEntity> FindAsync<TEntity>(SqlTransactionAsyncContext transactionContext, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetFindStatement();

            return SimpleAccess.ExecuteEntityAsync<TEntity>(transactionContext, commandText, CommandType.StoredProcedure
                , fieldToSkip, parameters: new SqlParameter("@whereClause", DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)));
        }

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetFindStatement();

            return SimpleAccess.ExecuteEntitiesAsync<TEntity>(commandText, CommandType.StoredProcedure
                , fieldToSkip, parameters: new SqlParameter("@whereClause", DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)));

        }

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetFindStatement();

            return SimpleAccess.ExecuteEntitiesAsync<TEntity>(transactionContext, commandText, CommandType.StoredProcedure
                    , fieldToSkip, parameters: new SqlParameter("@whereClause", DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)));
        }



        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public Task<int> InsertAsync<TEntity>(params SqlParameter[] sqlParameters)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();


            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public Task<int> InsertAsync<TEntity>(object paramObject)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();

            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, SimpleAccess.BuildDbParameters(paramObject));
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public async Task<int> InsertAsync<TEntity>(TEntity entity)
            where TEntity : class
        {

            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);

            //var commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();

            var result = await SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public async Task<int> InsertAsync<TEntity>(SqlTransactionAsyncContext transactionContext, TEntity entity)
            where TEntity : class
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);

            //var commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();

            var result = await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;

        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        /// 
        /// <returns> The number of affected records</returns>
        public async Task<int> InsertAllAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {

            SqlTransactionAsyncContext transactionContext = null;
            int result = 0;
            using (transactionContext = await SimpleAccess.BeginTransactionAsync())
            {
                try
                {
                    var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                    //var commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
                    var commandText = entityInfo.SqlBuilder.GetInsertStatement();

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetInsertParameters(entity);

                        result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                    SimpleAccess.EndTransaction(transactionContext);

                }

                catch (Exception)
                {
                    SimpleAccess.EndTransaction(transactionContext, false);
                    throw;
                }
            }
            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext">			 The SqlTransactionAsyncContext. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        public async Task<int> InsertAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            int result = 0;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetInsertParameters(entity);

                result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure
                    , entityParameters.DataParametersDictionary.Values.ToArray());

                entityParameters.LoadOutParametersProperties(entity);
            }
            return result;
        }

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public Task<int> UpdateAsync<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary> Updates the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> . </returns>
        public Task<int> UpdateAsync<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, SimpleAccess.BuildDbParameters(paramObject));
        }

        /// <summary> Updates the given TEntity. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public async Task<int> UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            var result = await SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Updates the given TEntity. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public async Task<int> UpdateAsync<TEntity>(SqlTransactionAsyncContext transactionContext, TEntity entity)
            where TEntity : class
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            var result = await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> UpdateAllAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            SqlTransactionAsyncContext transactionContext = null;
            int result = 0;
            using (transactionContext = await SimpleAccess.BeginTransactionAsync())
            {

                try
                {
                    var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                    //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
                    var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetUpdateParameters(entity);

                        result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                    SimpleAccess.EndTransaction(transactionContext);

                }

                catch (Exception)
                {
                    SimpleAccess.EndTransaction(transactionContext, false);
                    throw;
                }
            }
            return result;
        }

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> UpdateAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, IEnumerable<TEntity> entities)
            where TEntity : class
        {

            int result = 0;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetUpdateParameters(entity);

                result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure
                    , entityParameters.DataParametersDictionary.Values.ToArray());


                entityParameters.LoadOutParametersProperties(entity);
            }
            return result;
        }

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public Task<int> DeleteAsync<TEntity>(long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            var result = SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("id") });
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext">			 The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public Task<int> DeleteAsync<TEntity>(SqlTransactionAsyncContext transactionContext, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            var result = SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("Id") });
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual Task<int> DeleteAsync<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, sqlParameters);
        }


        /// <summary> Deletes the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public virtual Task<int> DeleteAsync<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, SimpleAccess.BuildDbParameters(paramObject));
        }

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQL transaction. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual Task<int> DeleteAsync<TEntity>(SqlTransactionAsyncContext transactionContext, params SqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary> Delete All records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> DeleteAllAsync<TEntity>() where TEntity : class
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure);
        }

        /// <summary> Delete All records from the table with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public Task<int> DeleteAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext) where TEntity : class
        {
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            return SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure);
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObjects"> The <![CDATA[IEnumerable<object>]]> objects as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> DeleteAllAsync<TEntity>(IEnumerable<object> paramObjects)
            where TEntity : class
        {
            SqlTransactionAsyncContext transactionContext = null;
            int result = 0;

            using (transactionContext = await SimpleAccess.BeginTransactionAsync())
            {
                try
                {
                    var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                    //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
                    var commandText = entityInfo.SqlBuilder.GetDeleteStatement();


                    foreach (var paramObject in paramObjects)
                    {

                        result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, SimpleAccess.BuildDbParameters(paramObject));

                    }
                    SimpleAccess.EndTransaction(transactionContext);
                }
                catch (Exception)
                {
                    SimpleAccess.EndTransaction(transactionContext, false);
                }
            }


            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  expression. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> DeleteAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            result = await SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo));

            return result;
        }


        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> DeleteAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            result = await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo));

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> DeleteAllAsync<TEntity>(IEnumerable<long> ids)
            where TEntity : class
        {
            SqlTransactionAsyncContext transactionContext = null;
            int result = 0;
            using (transactionContext = await SimpleAccess.BeginTransactionAsync())
            {
                try
                {

                    var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
                    //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
                    var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

                    foreach (var id in ids)
                    {

                        result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("Id") });

                    }
                    SimpleAccess.EndTransaction(transactionContext);

                }
                catch (Exception)
                {
                    SimpleAccess.EndTransaction(transactionContext, false);
                }
            }


            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public async Task<int> DeleteAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, IEnumerable<long> ids)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();


            foreach (var id in ids)
            {

                result += await SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("Id") });

            }

            return result;
        }

        /// <summary> Soft delete. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> SoftDeleteAsync<TEntity>(long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
 
            //var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetSoftDeleteStatement();
            
            return SimpleAccess.ExecuteNonQueryAsync(commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("id") });
        }

        /// <summary> Soft delete the <typeparamref name="TEntity"/> record. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public Task<int> SoftDeleteAsync<TEntity>(SqlTransactionAsyncContext transactionContext, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetSoftDeleteStatement();
            
            return SimpleAccess.ExecuteNonQueryAsync(transactionContext, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("id") });
        }

    }

}
