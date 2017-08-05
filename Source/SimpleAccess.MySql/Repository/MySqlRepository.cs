using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleAccess.Core;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.MySql
{
    /// <summary> Implements MySqlRepository base SqlSimpleAccess with command type stored procedures. </summary>
    public class MySqlRepository : IMySqlRepository, IDisposable
    {

        /// <summary> The SQL connection. </summary>
        public IMySqlSimpleAccess SimpleAccess { get; set; }
        
        #region Constructor

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlSimpleAccess"> The SQL connection. </param>
        public MySqlRepository(IMySqlSimpleAccess sqlSimpleAccess)
        {
            SimpleAccess = sqlSimpleAccess;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The connection string. </param>
        public MySqlRepository(string connection)
            : this(new MySqlSimpleAccess(connection, CommandType.StoredProcedure))
        {
        }

        /// <summary> Default constructor. </summary>
        public MySqlRepository()
            : this(new MySqlSimpleAccess(CommandType.StoredProcedure))
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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof (TEntity));
            string commandText = string.Format("{0}_GetAll", entityInfo.DbObjectName);
            return SimpleAccess.ExecuteEntities<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip);
        }

        //public TEntity Get<TEntity>(long id, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
        //    where TEntity : new()
        //{
        //    return Get<TEntity>(new MySqlParameter("@id", id), fieldToSkip, piList);
        //}

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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip, null,
                new MySqlParameter("@id", id));

            // return Get<TEntity>(new MySqlParameter("@id", id), transaction, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(MySqlTransaction transaction, long id,  string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure, fieldToSkip, null, 
                new MySqlParameter("@id", id));

           // return Get<TEntity>(new MySqlParameter("@id", id), transaction, fieldToSkip);
        }


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(MySqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip,  null, new [] {sqlParameter});
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(MySqlTransaction transaction, MySqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure, fieldToSkip, null, new[] { sqlParameter });
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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure, paramObject, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(MySqlTransaction transaction, object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure, paramObject, fieldToSkip);
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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure
                    , fieldToSkip, parameters: new MySqlParameter("@whereClause", DynamicQuery.GetStoredProcedureWhere(expression, entityInfo)));
        }

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Find<TEntity>(MySqlTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure
                , fieldToSkip, parameters: new MySqlParameter("@whereClause", DynamicQuery.GetStoredProcedureWhere(expression, entityInfo)));
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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntities<TEntity>(commandText, CommandType.StoredProcedure
                , fieldToSkip, parameters: new MySqlParameter("@whereClause",DynamicQuery.GetStoredProcedureWhere(expression, entityInfo) ));

        }

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public IEnumerable<TEntity> FindAll<TEntity>(MySqlTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntities<TEntity>(transaction, commandText, CommandType.StoredProcedure
                    , fieldToSkip, parameters: new MySqlParameter("@whereClause", DynamicQuery.GetStoredProcedureWhere(expression, entityInfo)));
        }



        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(params MySqlParameter[] sqlParameters)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary> Inserts the given dynamic object as MySqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(object paramObject)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, SimpleAccess.BuildSqlParameters(paramObject));
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
            
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);

            string commandText = string.Format("[dbo].{0}_Insert", entityInfo.DbObjectName);

            var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(MySqlTransaction transaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);


            string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);

            var result = SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure
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

            MySqlTransaction transaction = null;
            int result = 0;
            try
            {
                using (transaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
                    string commandText = string.Format("[dbo].{0}_Insert", entityInfo.DbObjectName);

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetInsertParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(transaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(transaction);
            }
            
            return result;
        }

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        public int InsertAll<TEntity>(MySqlTransaction transaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            int result = 0;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            string commandText = string.Format("[dbo].{0}_Insert", entityInfo.DbObjectName);

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetInsertParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure
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
        public int Update<TEntity>(params MySqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary> Updates the given dynamic object as MySqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> . </returns>
        public int Update<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);
            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, SimpleAccess.BuildSqlParameters(paramObject));
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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

            var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure
                , entityParameters.DataParametersDictionary.Values.ToArray());

            entityParameters.LoadOutParametersProperties(entity);

            return result;
        }

        /// <summary> Updates the given TEntity. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(MySqlTransaction transaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

            var result = SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure
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
            MySqlTransaction transaction = null;
            int result = 0;
            try
            {
                using (transaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
                    string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetUpdateParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(transaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(transaction);
            }

            return result;
        }

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int UpdateAll<TEntity>(MySqlTransaction transaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {

            int result = 0;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetUpdateParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure
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
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
			var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, new [] { id.ToDataParam("id")});
			return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">			 The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public int Delete<TEntity>(MySqlTransaction transaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var result = SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure, new [] { id.ToDataParam("Id")} );
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(params MySqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, sqlParameters);
        }


        /// <summary> Deletes the given dynamic object as MySqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, SimpleAccess.BuildSqlParameters(paramObject));
        }

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(MySqlTransaction transaction, params MySqlParameter[] sqlParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary> Delete All records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>() where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure);
        }

        /// <summary> Delete All records from the table with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int DeleteAll<TEntity>(MySqlTransaction transaction) where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure);
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as MySqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObjects"> The <![CDATA[IEnumerable<object>]]> objects as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(IEnumerable<object> paramObjects)
            where TEntity : class
        {
            MySqlTransaction transaction = null;
            int result = 0;
            try
            {
                using (transaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
                    var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

                    foreach (var paramObject in paramObjects)
                    {

                        result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure, SimpleAccess.BuildSqlParameters(paramObject));

                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(transaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(transaction);
            }

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
            MySqlTransaction transaction = null;
            int result = 0;
            try
            {
                using (transaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
                    var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

                    foreach (var id in ids)
                    {

                        result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("Id") });

                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(transaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(transaction);
            }

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="transaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(MySqlTransaction transaction, IEnumerable<long> ids)
            where TEntity : class
        {
            int result = 0;
  
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            foreach (var id in ids)
            {

                result += SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("Id") });

            }

            return result;
        }

        /// <summary> Soft delete. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public int SoftDelete<TEntity>(long id)
			where TEntity : class
		{
			//var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
			var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);

			return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, new []{ id.ToDataParam("id")});
		}


        /// <summary> Soft delete the <typeparamref name="TEntity"/> record. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int SoftDelete<TEntity>(MySqlTransaction transaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));
            var commandText = string.Format("{0}_SoftDelete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(transaction, commandText, CommandType.StoredProcedure, new[] { id.ToDataParam("id") });
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
