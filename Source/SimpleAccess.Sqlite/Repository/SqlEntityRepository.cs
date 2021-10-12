using System;
using System.Collections.Generic;
using System.Data;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.SqlServer
{
    /// <summary> Implements SqlRepository base SqlSimpleAccess with command type stored procedures. </summary>
    public partial class SqlEntityRepository : ISqlRepository, IDisposable
    {

        /// <summary> The SQL connection. </summary>
        public ISqlSimpleAccess SimpleAccess { get; set; }

        #region Constructor

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlSimpleAccess"> The SQL connection. </param>
        public SqlEntityRepository(ISqlSimpleAccess sqlSimpleAccess)
        {
            SimpleAccess = sqlSimpleAccess;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The connection string. </param>
        public SqlEntityRepository(string connection)
            : this(new SqlSimpleAccess(connection, CommandType.Text))
        {
        }

        /// <summary> Default constructor. </summary>
        public SqlEntityRepository()
            : this(new SqlSimpleAccess(CommandType.Text))
        {
        }

        #endregion

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all {TEntity} in this
        /// collection. </returns>

        public virtual IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null)
            where TEntity : new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            string commandText = entityInfo.SqlBuilder.GetGetAllStatement();
            return SimpleAccess.ExecuteEntities<TEntity>(commandText, CommandType.Text, fieldToSkip);
        }

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all {TEntity} in this
        /// collection. </returns>

        public virtual IEnumerable<TEntity> GetAll<TEntity>(SqlTransaction transaction, string fieldToSkip = null)
            where TEntity : new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            string commandText = entityInfo.SqlBuilder.GetGetAllStatement();
            return SimpleAccess.ExecuteEntities<TEntity>(transaction, commandText, CommandType.StoredProcedure, fieldToSkip);
        }


        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize);
            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize, string sortExpression)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize, string sortExpression, object whereParameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize, string sortExpression, params SqlParameter[] parameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SqlParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            var totalRowsParameter = sqlParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SqlParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                sqlParameters = sqlParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            var commandText = entityInfo.SqlBuilder.GetGetPagedListStatement();

            var result = SimpleAccess.ExecuteDynamics(commandText, sqlParameters);
            var pagedData = new PagedData<dynamic>
            {
                Data = result,
                TotalRows = (long)(totalRowsParameter.Value == DBNull.Value || totalRowsParameter.Value == null
                    ? 0
                    : totalRowsParameter.Value)
            };

            return pagedData;
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(long id, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE Id = @id";

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.Text, fieldToSkip, null,
                new SqlParameter("@id", id));

            // return Get<TEntity>(new SqlParameter("@id", id), transaction, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(SqlTransaction transaction, long id, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE Id = @id";

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.Text, fieldToSkip, null,
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
        public TEntity Get<TEntity>(SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE {sqlParameter.ParameterName.Replace("@", "")} = @{sqlParameter.ParameterName}";
            
            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.Text, fieldToSkip, null, new[] { sqlParameter });
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(SqlTransaction transaction, SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE {sqlParameter.ParameterName.Replace("@", "")} = @{sqlParameter.ParameterName}";

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.Text, fieldToSkip, null, new[] { sqlParameter });
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {paramObject.CreateWhereWithAnd()}";

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.Text, paramObject, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(SqlTransaction transaction, object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {paramObject.CreateWhereWithAnd()}";

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.Text, paramObject, fieldToSkip);
        }

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Find<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)}";


            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.Text
                    , fieldToSkip);
        }

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Find<TEntity>(SqlTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)}";


            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.Text
                , fieldToSkip);
        }

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)}";


            return SimpleAccess.ExecuteEntities<TEntity>(commandText, CommandType.Text
                , fieldToSkip);

        }

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public IEnumerable<TEntity> FindAll<TEntity>(SqlTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)}";


            return SimpleAccess.ExecuteEntities<TEntity>(transaction, commandText, CommandType.Text
                    , fieldToSkip);
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(params SqlParameter[] sqlParameters)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, sqlParameters);
        }

        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(object paramObject)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SimpleAccess.BuildSqlParameters(paramObject));
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(TEntity entity)
            where TEntity : class
        {

            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(SqlTransaction sqlTransaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);


            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            var result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text
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
        public int InsertAll<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {

            SqlTransaction sqlTransaction = null;
            int result = 0;
            using (sqlTransaction = SimpleAccess.BeginTransaction())
            {
                try
                {
                    var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
                    //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
                    string commandText = entityInfo.SqlBuilder.GetInsertStatement();


                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetInsertParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                    SimpleAccess.EndTransaction(sqlTransaction);

                }

                catch (Exception)
                {
                    SimpleAccess.EndTransaction(sqlTransaction, false);
                    throw;
                }
            }
            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        public int InsertAll<TEntity>(SqlTransaction sqlTransaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            int result = 0;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetInsertParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text
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
        public int Update<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, sqlParameters);
        }

        /// <summary> Updates the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> . </returns>
        public int Update<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SimpleAccess.BuildSqlParameters(paramObject));
        }

        /// <summary> Updates the given TEntity. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Updates the given TEntity. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(SqlTransaction sqlTransaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            var result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text
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
        public int UpdateAll<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            SqlTransaction sqlTransaction = null;
            int result = 0;
            using (sqlTransaction = SimpleAccess.BeginTransaction())
            {

                try
                {
                    var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

                    var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetUpdateParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                    SimpleAccess.EndTransaction(sqlTransaction);

                }

                catch (Exception)
                {
                    SimpleAccess.EndTransaction(sqlTransaction, false);
                    throw;
                }
            }
            return result;
        }

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int UpdateAll<TEntity>(SqlTransaction sqlTransaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {

            int result = 0;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetUpdateParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text
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
        public int Delete<TEntity>(long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, new[] { id.ToDataParam("id") });
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public int Delete<TEntity>(SqlTransaction sqlTransaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            var result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text, new[] { id.ToDataParam("Id") });
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();


            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, sqlParameters);
        }


        /// <summary> Deletes the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SimpleAccess.BuildSqlParameters(paramObject));
        }

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(SqlTransaction sqlTransaction, params SqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text, sqlParameters);
        }

        /// <summary> Delete All records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>() where TEntity : class
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


            //var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text);
        }

        /// <summary> Delete All records from the table with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int DeleteAll<TEntity>(SqlTransaction sqlTransaction) where TEntity : class
        {
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            return SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text);
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by expression. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            int result = 0;


            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo));
            
            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by expression. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SqlTransaction. </param>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(SqlTransaction sqlTransaction, Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement() + DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo);

            result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text);

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(IEnumerable<long> ids)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement().Replace("= @id;", $" IN ({String.Join(", ", ids)});");

            result += SimpleAccess.ExecuteNonQuery(commandText);

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(SqlTransaction sqlTransaction, IEnumerable<long> ids)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement().Replace("= @id;", $" IN ({String.Join(", ", ids)});");


            result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText);

            return result;
        }

        /// <summary> Soft delete. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int SoftDelete<TEntity>(long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetSoftDeleteStatement();


            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, parameters: id.ToDataParam("id"));
        }

        /// <summary> Soft delete the <typeparamref name="TEntity"/> record. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int SoftDelete<TEntity>(SqlTransaction sqlTransaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetSoftDeleteStatement();

            return SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.Text, parameters: id.ToDataParam("id"));
        }

        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. </summary>
        public void Dispose()
        {
            if (SimpleAccess != null)
                SimpleAccess.Dispose();
        }

    }

}
