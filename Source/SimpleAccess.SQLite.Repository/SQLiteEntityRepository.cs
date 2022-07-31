using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleAccess.Core.Entity.RepoWrapper;
using SimpleAccess.Core;
using SimpleAccess.Repository;

namespace SimpleAccess.SQLite
{
    /// <summary> Implements SqlRepository base SqlSimpleAccess with command type stored procedures. </summary>
    public partial class SQLiteEntityRepository : ISQLiteRepository, IDisposable
    {

        /// <summary> The SQL connection. </summary>
        public ISimpleAccess<SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter, SQLiteDataReader, SQLiteTransactionAsyncContext> SimpleAccess { get; set; }


        #region Constructor

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlSimpleAccess"> The SQL connection. </param>
        public SQLiteEntityRepository(ISQLiteSimpleAccess sqliteSimpleAccess)
        {
            SimpleAccess = sqliteSimpleAccess;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The connection string. </param>
        public SQLiteEntityRepository(string connection)
            : this(new SQLiteSimpleAccess(connection, CommandType.Text))
        {
        }

        /// <summary> Default constructor. </summary>
        public SQLiteEntityRepository()
            : this(new SQLiteSimpleAccess(CommandType.Text))
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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
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

        public virtual IEnumerable<TEntity> GetAll<TEntity>(SQLiteTransaction transaction, string fieldToSkip = null)
            where TEntity : new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            string commandText = entityInfo.SqlBuilder.GetGetAllStatement();
            return SimpleAccess.ExecuteEntities<TEntity>(transaction, commandText, CommandType.StoredProcedure, fieldToSkip);
        }


        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize);
            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize, string sortExpression)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize, string sortExpression, object whereParameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression);
            pagedListParameters.AddOtherParams(whereParameters);
            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(int startIndex, int pageSize, string sortExpression, params SQLiteParameter[] parameters)
            where TEntity : new()
        {
            var pagedListParameters = new PagedListParameters<SQLiteParameter>(startIndex, pageSize, sortExpression, parameters);

            return GetDynamicPagedList<TEntity>(pagedListParameters.GetParametersToExecute());
        }

        public PagedData<dynamic> GetDynamicPagedList<TEntity>(params SQLiteParameter[] SQLiteParameters)
            where TEntity : new()
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            var totalRowsParameter = SQLiteParameters.FirstOrDefault(p => p.ParameterName == "@totalRows");
            if (totalRowsParameter == null)
            {
                totalRowsParameter = new SQLiteParameter("@totalRows", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                SQLiteParameters = SQLiteParameters.Concat(new[] { totalRowsParameter }).ToArray();
            }

            var commandText = entityInfo.SqlBuilder.GetGetPagedListStatement();

            var result = SimpleAccess.ExecuteDynamics(commandText, SQLiteParameters);
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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);
            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE Id = @id";

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.Text, fieldToSkip, null,
                new SQLiteParameter("@id", id));

            // return Get<TEntity>(new SQLiteParameter("@id", id), transaction, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(SQLiteTransaction transaction, long id, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE Id = @id";

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.Text, fieldToSkip, null,
                new SQLiteParameter("@id", id));

            // return Get<TEntity>(new SQLiteParameter("@id", id), transaction, fieldToSkip);
        }


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(SQLiteParameter SQLiteParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE {SQLiteParameter.ParameterName.Replace("@", "")} = @{SQLiteParameter.ParameterName}";

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.Text, fieldToSkip, null, new[] { SQLiteParameter });
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="SQLiteParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(SQLiteTransaction transaction, SQLiteParameter SQLiteParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} WHERE {SQLiteParameter.ParameterName.Replace("@", "")} = @{SQLiteParameter.ParameterName}";

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.Text, fieldToSkip, null, new[] { SQLiteParameter });
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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

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
        public TEntity Get<TEntity>(SQLiteTransaction transaction, object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

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
        public TEntity Find<TEntity>(SQLiteTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

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
        public IEnumerable<TEntity> FindAll<TEntity>(SQLiteTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);
            string commandText = $"{entityInfo.SqlBuilder.GetGetAllStatement()} {DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo)}";


            return SimpleAccess.ExecuteEntities<TEntity>(transaction, commandText, CommandType.Text
                    , fieldToSkip);
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(params SQLiteParameter[] SQLiteParameters)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SQLiteParameters);
        }

        /// <summary> Inserts the given dynamic object as SQLiteParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(object paramObject)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SimpleAccess.BuildDbParameters(paramObject));
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

            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);
            int result = 0;

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();
            if (commandText.IndexOf("||") < 1)
            {
                result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text
                    , entityParameters.DataParametersDictionary.Values.ToArray());

            }
            else
            {
                using (var transaction = SimpleAccess.BeginTransaction())
                {
                    try
                    {
                        var quries = commandText.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                        result = SimpleAccess.ExecuteNonQuery(transaction, quries[0], CommandType.Text
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        var id = SimpleAccess.ExecuteScalar<object>(transaction, quries[1], CommandType.Text);

                        var pkPropertyInfo = entityParameters.OutParametersDictionary.Keys.First();
                        pkPropertyInfo.SetValue(entity, Convert.ChangeType(id, pkPropertyInfo.PropertyType));

                        SimpleAccess.EndTransaction(transaction);
                    }
                    catch (Exception)
                    {
                        SimpleAccess.EndTransaction(transaction, false);
                        throw;
                    }

                }
            }

            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(SQLiteTransaction sqliteTransaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);
            var result = 0;

            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            string commandText = entityInfo.SqlBuilder.GetInsertStatement();

            if (commandText.IndexOf("||") < 1)
            {
                result = SimpleAccess.ExecuteNonQuery(sqliteTransaction, commandText, CommandType.Text
                    , entityParameters.DataParametersDictionary.Values.ToArray());

            }
            else
            {

                try
                {
                    var quries = commandText.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    result = SimpleAccess.ExecuteNonQuery(sqliteTransaction, quries[0], CommandType.Text
                        , entityParameters.DataParametersDictionary.Values.ToArray());

                    var id = SimpleAccess.ExecuteScalar<object>(sqliteTransaction, quries[1], CommandType.Text);

                    var pkPropertyInfo = entityParameters.OutParametersDictionary.Keys.First();
                    pkPropertyInfo.SetValue(entity, Convert.ChangeType(id, pkPropertyInfo.PropertyType));

                }
                catch (Exception)
                {
                    SimpleAccess.EndTransaction(sqliteTransaction, false);
                    throw;
                }

            }
            //entityParameters.LoadOutParametersProperties(entity);

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

            SQLiteTransaction sqliteTransaction = null;
            int result = 0;
            using (sqliteTransaction = SimpleAccess.BeginTransaction())
            {
                try
                {
                    var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
                    //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
                    string commandText = entityInfo.SqlBuilder.GetInsertStatement();


                    if (commandText.IndexOf("||") < 1)
                    {
                        foreach (var entity in entities)
                        {
                            var entityParameters = entityInfo.GetInsertParameters(entity);

                            result += SimpleAccess.ExecuteNonQuery(sqliteTransaction, commandText, CommandType.Text
                                , entityParameters.DataParametersDictionary.Values.ToArray());

                        }
                    }
                    else
                    {

                        var quries = commandText.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var entity in entities)
                        {
                            var entityParameters = entityInfo.GetInsertParameters(entity);

                            result += SimpleAccess.ExecuteNonQuery(sqliteTransaction, quries[0], CommandType.Text
                             , entityParameters.DataParametersDictionary.Values.ToArray());

                            var id = SimpleAccess.ExecuteScalar<object>(sqliteTransaction, quries[1], CommandType.Text);

                            var pkPropertyInfo = entityParameters.OutParametersDictionary.Keys.First();
                            pkPropertyInfo.SetValue(entity, Convert.ChangeType(id, pkPropertyInfo.PropertyType));

                        }
                    }
                    SimpleAccess.EndTransaction(sqliteTransaction);
                }
                catch (Exception)
                {
                    SimpleAccess.EndTransaction(sqliteTransaction, false);
                    throw;
                }
            }
            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction">			 The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        public int InsertAll<TEntity>(SQLiteTransaction sqliteTransaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            int result = 0;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetInsertStatement();
            if (commandText.IndexOf("||") < 1)
            {
                foreach (var entity in entities)
                {
                    var entityParameters = entityInfo.GetInsertParameters(entity);

                    result += SimpleAccess.ExecuteNonQuery(sqliteTransaction, commandText, CommandType.Text
                        , entityParameters.DataParametersDictionary.Values.ToArray());

                }
            }
            else
            {

                var quries = commandText.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var entity in entities)
                {
                    var entityParameters = entityInfo.GetInsertParameters(entity);

                    result += SimpleAccess.ExecuteNonQuery(sqliteTransaction, quries[0], CommandType.Text
                     , entityParameters.DataParametersDictionary.Values.ToArray());

                    var id = SimpleAccess.ExecuteScalar<object>(sqliteTransaction, quries[1], CommandType.Text);

                    var pkPropertyInfo = entityParameters.OutParametersDictionary.Keys.First();
                    pkPropertyInfo.SetValue(entity, Convert.ChangeType(id, pkPropertyInfo.PropertyType));

                }
            }
            return result;
        }

        /// <summary> Updates the given SQLiteParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(params SQLiteParameter[] SQLiteParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SQLiteParameters);
        }

        /// <summary> Updates the given dynamic object as SQLiteParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> . </returns>
        public int Update<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SimpleAccess.BuildDbParameters(paramObject));
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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
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
        /// <param name="SQLiteTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(SQLiteTransaction SQLiteTransaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            var result = SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text
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
            SQLiteTransaction SQLiteTransaction = null;
            int result = 0;
            using (SQLiteTransaction = SimpleAccess.BeginTransaction())
            {

                try
                {
                    var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

                    var commandText = entityInfo.SqlBuilder.GetUpdateStatement();

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetUpdateParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                    SimpleAccess.EndTransaction(SQLiteTransaction);

                }

                catch (Exception)
                {
                    SimpleAccess.EndTransaction(SQLiteTransaction, false);
                    throw;
                }
            }
            return result;
        }

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int UpdateAll<TEntity>(SQLiteTransaction SQLiteTransaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {

            int result = 0;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetUpdateStatement();


            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetUpdateParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text
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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, new[] { id.ToDataParam("id") });
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction">			 The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public int Delete<TEntity>(SQLiteTransaction SQLiteTransaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            var result = SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text, new[] { id.ToDataParam("Id") });
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(params SQLiteParameter[] SQLiteParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();


            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SQLiteParameters);
        }


        /// <summary> Deletes the given dynamic object as SQLiteParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, SimpleAccess.BuildDbParameters(paramObject));
        }

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(SQLiteTransaction SQLiteTransaction, params SQLiteParameter[] SQLiteParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement();

            return SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text, SQLiteParameters);
        }

        /// <summary> Delete All records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>() where TEntity : class
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


            //var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text);
        }

        /// <summary> Delete All records from the table with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int DeleteAll<TEntity>(SQLiteTransaction SQLiteTransaction) where TEntity : class
        {
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            return SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text);
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


            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement();

            result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo));

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by expression. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQLiteTransaction. </param>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(SQLiteTransaction SQLiteTransaction, Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteAllStatement() + DynamicQuery.CreateDbParametersFormWhereExpression(expression, entityInfo);

            result = SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text);

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

            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement().Replace("= @id;", $" IN ({String.Join(", ", ids)});");

            result += SimpleAccess.ExecuteNonQuery(commandText);

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(SQLiteTransaction SQLiteTransaction, IEnumerable<long> ids)
            where TEntity : class
        {
            int result = 0;

            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
            //var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetDeleteStatement().Replace("= @id;", $" IN ({String.Join(", ", ids)});");


            result += SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText);

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
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetSoftDeleteStatement();


            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.Text, parameters: id.ToDataParam("id"));
        }

        /// <summary> Soft delete the <typeparamref name="TEntity"/> record. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int SoftDelete<TEntity>(SQLiteTransaction SQLiteTransaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = SQLiteEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

            //var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);
            var commandText = entityInfo.SqlBuilder.GetSoftDeleteStatement();

            return SimpleAccess.ExecuteNonQuery(SQLiteTransaction, commandText, CommandType.Text, parameters: id.ToDataParam("id"));
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
