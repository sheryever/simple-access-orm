using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Linq.Expressions;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.Oracle
{
    /// <summary> Implements OracleRepository based on OracleSimpleAccess with command type stored procedures. </summary>
    public class OracleRepository : IOracleRepository, IDisposable
    {

        /// <summary> The SQL connection. </summary>
        public IOracleSimpleAccess SimpleAccess { get; set; }
        
        #region Constructor

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlSimpleAccess"> The SQL connection. </param>
        public OracleRepository(IOracleSimpleAccess sqlSimpleAccess)
        {
            SimpleAccess = sqlSimpleAccess;
        }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The connection string. </param>
        public OracleRepository(string connection)
            : this(new OracleSimpleAccess(connection, CommandType.StoredProcedure))
        {
        }

        /// <summary> Default constructor. </summary>
        public OracleRepository()
            : this(new OracleSimpleAccess(CommandType.StoredProcedure))
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
            string commandText = string.Format("{0}_GetAll", entityInfo.Name);
            return SimpleAccess.ExecuteEntities<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip);
        }

        //public TEntity Get<TEntity>(long id, string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
        //    where TEntity : new()
        //{
        //    return Get<TEntity>(new OracleParameter("@id", id), fieldToSkip, piList);
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

            var commandText = string.Format("{0}_GetById", entityInfo.Name);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip, null,
                new OracleParameter("@id", id));

            // return Get<TEntity>(new OracleParameter("@id", id), transaction, fieldToSkip);
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(OracleTransaction transaction, long id,  string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.Name);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure, fieldToSkip, null, 
                new OracleParameter("@id", id));

           // return Get<TEntity>(new OracleParameter("@id", id), transaction, fieldToSkip);
        }


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(OracleParameter oracleParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.Name);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure, fieldToSkip,  null, new [] {oracleParameter});
        }

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="oracleParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity Get<TEntity>(OracleTransaction transaction, OracleParameter oracleParameter, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.Name);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure, fieldToSkip, null, new[] { oracleParameter });
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

            var commandText = string.Format("{0}_GetById", entityInfo.Name);

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
        public TEntity Get<TEntity>(OracleTransaction transaction, object paramObject, string fieldToSkip = null)
            where TEntity : class, new()
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_GetById", entityInfo.Name);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure, paramObject, fieldToSkip);
        }

        /// <summary> Find a single <typeparamref name="TEntity"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity FindSingle<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(commandText, CommandType.StoredProcedure
                    , fieldToSkip, parameters: new OracleParameter("@whereClause", DynamicQuery.GetStoredProcedureWhere(expression, entityInfo)));
        }

        /// <summary> Find a single <typeparamref name="TEntity"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public TEntity FindSingle<TEntity>(OracleTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntity<TEntity>(transaction, commandText, CommandType.StoredProcedure
                , fieldToSkip, parameters: new OracleParameter("@whereClause", DynamicQuery.GetStoredProcedureWhere(expression, entityInfo)));
        }

        /// <summary> Find a single <typeparamref name="TEntity"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntities<TEntity>(commandText, CommandType.StoredProcedure
                , fieldToSkip, parameters: new OracleParameter("@whereClause",DynamicQuery.GetStoredProcedureWhere(expression, entityInfo) ));

        }

        /// <summary> Find a single <typeparamref name="TEntity"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        public IEnumerable<TEntity> Find<TEntity>(OracleTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new()
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            var commandText = string.Format("{0}_Find", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteEntities<TEntity>(transaction, commandText, CommandType.StoredProcedure
                    , fieldToSkip, parameters: new OracleParameter("@whereClause", DynamicQuery.GetStoredProcedureWhere(expression, entityInfo)));
        }



        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(params OracleParameter[] oracleParameters)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Insert", entityInfo.Name);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, oracleParameters);
        }

        /// <summary> Inserts the given dynamic object as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(object paramObject)
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Insert", entityInfo.Name);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, SimpleAccess.BuildOracleParameters(paramObject));
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
            
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
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
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Insert<TEntity>(OracleTransaction sqlTransaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var entityParameters = entityInfo.GetInsertParameters(entity);


            string commandText = string.Format("{0}_Insert", entityInfo.DbObjectName);

            var result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure
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

            OracleTransaction sqlTransaction = null;
            int result = 0;
            try
            {
                using (sqlTransaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
                    string commandText = string.Format("[dbo].{0}_Insert", entityInfo.DbObjectName);

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetInsertParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(sqlTransaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(sqlTransaction);
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
        public int InsertAll<TEntity>(OracleTransaction sqlTransaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            int result = 0;
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            string commandText = string.Format("[dbo].{0}_Insert", entityInfo.DbObjectName);

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetInsertParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure
                    , entityParameters.DataParametersDictionary.Values.ToArray());

                entityParameters.LoadOutParametersProperties(entity);
            }
            return result;
        }

        /// <summary> Updates the given oracleParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(params OracleParameter[] oracleParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            var commandText = string.Format("{0}_Update", entityInfo.Name);
            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, oracleParameters);
        }

        /// <summary> Updates the given dynamic object as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> . </returns>
        public int Update<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntityInfo(typeof(TEntity));

            string commandText = string.Format("{0}_Update", entityInfo.Name);
            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, SimpleAccess.BuildOracleParameters(paramObject));
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
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
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
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        public int Update<TEntity>(OracleTransaction sqlTransaction, TEntity entity)
            where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var entityParameters = entityInfo.GetUpdateParameters(entity);

            string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

            var result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure
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
            OracleTransaction sqlTransaction = null;
            int result = 0;
            try
            {
                using (sqlTransaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
                    string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

                    foreach (var entity in entities)
                    {
                        var entityParameters = entityInfo.GetUpdateParameters(entity);

                        result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure
                            , entityParameters.DataParametersDictionary.Values.ToArray());

                        entityParameters.LoadOutParametersProperties(entity);
                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(sqlTransaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(sqlTransaction);
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
        public int UpdateAll<TEntity>(OracleTransaction sqlTransaction, IEnumerable<TEntity> entities)
            where TEntity : class
        {

            int result = 0;
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            string commandText = string.Format("{0}_Update", entityInfo.DbObjectName);

            foreach (var entity in entities)
            {
                var entityParameters = entityInfo.GetUpdateParameters(entity);

                result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure
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
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
			var result = SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, new [] { OracleParametersExtensions.ToDataParam(id, (string) "id")});
			return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> . </returns>
        public int Delete<TEntity>(OracleTransaction sqlTransaction, long id)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);
            var result = SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure, new [] { OracleParametersExtensions.ToDataParam(id, (string) "Id")} );
            return result;
        }


        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(params OracleParameter[] oracleParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));

            string commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, oracleParameters);
        }


        /// <summary> Deletes the given dynamic object as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(object paramObject)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, SimpleAccess.BuildOracleParameters(paramObject));
        }

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="oracleParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        public virtual int Delete<TEntity>(OracleTransaction sqlTransaction, params OracleParameter[] oracleParameters)
            where TEntity : class
        {
            //var name = typeof(TEntity).Name;
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure, oracleParameters);
        }

        /// <summary> Delete All records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>() where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure);
        }

        /// <summary> Delete All records from the table with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>

        public int DeleteAll<TEntity>(OracleTransaction sqlTransaction) where TEntity : class
        {
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var commandText = string.Format("{0}_DeleteAll", entityInfo.DbObjectName);

            return SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure);
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as OracleParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObjects"> The <![CDATA[IEnumerable<object>]]> objects as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(IEnumerable<object> paramObjects)
            where TEntity : class
        {
            OracleTransaction sqlTransaction = null;
            int result = 0;
            try
            {
                using (sqlTransaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
                    var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

                    foreach (var paramObject in paramObjects)
                    {

                        result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure, SimpleAccess.BuildOracleParameters(paramObject));

                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(sqlTransaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(sqlTransaction);
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
            OracleTransaction sqlTransaction = null;
            int result = 0;
            try
            {
                using (sqlTransaction = SimpleAccess.BeginTrasaction())
                {
                    var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
                    var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

                    foreach (var id in ids)
                    {

                        result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure, new[] { OracleParametersExtensions.ToDataParam(id, (string) "Id") });

                    }
                }
            }
            catch (Exception)
            {
                SimpleAccess.EndTransaction(sqlTransaction, false);
            }
            finally
            {
                SimpleAccess.EndTransaction(sqlTransaction);
            }

            return result;
        }

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        public int DeleteAll<TEntity>(OracleTransaction sqlTransaction, IEnumerable<long> ids)
            where TEntity : class
        {
            int result = 0;
  
            var entityInfo = RepositorySetting.GetEntity2Info(typeof(TEntity));
            var commandText = string.Format("{0}_Delete", entityInfo.DbObjectName);

            foreach (var id in ids)
            {

                result += SimpleAccess.ExecuteNonQuery(sqlTransaction, commandText, CommandType.StoredProcedure, new[] { OracleParametersExtensions.ToDataParam(id, (string) "Id") });

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
			var commandText = string.Format("{0}_MarkDelete", entityInfo.Name);

			return SimpleAccess.ExecuteNonQuery(commandText, CommandType.StoredProcedure, new []{ OracleParametersExtensions.ToDataParam(id, (string) "id")});
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
