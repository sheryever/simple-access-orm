using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using SimpleAccess.SqlServer;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Represent the interface of SimpleAccess Repository methods
    /// </summary>
    public interface ISqlRepository
    {

        /// <summary>
        /// Internal ISqlSimpleAccess instance
        /// </summary>
        ISqlSimpleAccess SimpleAccess { get; set; }

        /// <summary> Get all TEntity object in a <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all TEntity in this
        /// collection. </returns>
        IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null)
                    where TEntity : new();

        /// <summary> Get TEntity by Id or any other parameter. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(SqlTransaction transaction, SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(object paramObject, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(SqlTransaction transaction, object paramObject, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(long id, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(SqlTransaction transaction, long id, string fieldToSkip = null)
            where TEntity : class,new();

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Find<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        TEntity Find<TEntity>(SqlTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        IEnumerable<TEntity> FindAll<TEntity>(SqlTransaction transaction, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(params SqlParameter[] sqlParameters);

        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(object paramObject);

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(SqlTransaction sqlTransaction, TEntity entity)
            where TEntity : class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        /// 
        /// <returns> The number of affected records</returns>
        int InsertAll<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        int InsertAll<TEntity>(SqlTransaction sqlTransaction, IEnumerable<TEntity> entities)
            where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        /// <summary> Updates the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(object paramObject)
            where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(TEntity entity)
         where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(SqlTransaction sqlTransaction, TEntity entity)
            where TEntity : class;

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int UpdateAll<TEntity>(IEnumerable<TEntity> entities)
         where TEntity : class;

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int UpdateAll<TEntity>(SqlTransaction sqlTransaction, IEnumerable<TEntity> entities)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(object paramObject)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(long id)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/> by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        ///  <param name="sqlTransaction">			 The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(SqlTransaction sqlTransaction, long id)
            where TEntity : class;


        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given <see cref="SqlParameter"/> array. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(SqlTransaction sqlTransaction, params SqlParameter[] sqlParameters)
            where TEntity : class;


        /// <summary> Delete All the <typeparamref name="TEntity"/> records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int DeleteAll<TEntity>()
            where TEntity : class;

        /// <summary> Delete All the <typeparamref name="TEntity"/> records with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int DeleteAll<TEntity>(SqlTransaction sqlTransaction)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObjects"> The <![CDATA[IEnumerable<object>]]> objects as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int DeleteAll<TEntity>(IEnumerable<object> paramObjects)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int DeleteAll<TEntity>(IEnumerable<long> ids)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int DeleteAll<TEntity>(SqlTransaction sqlTransaction, IEnumerable<long> ids)
            where TEntity : class;

        /// <summary> Soft delete the <typeparamref name="TEntity"/> record. </summary>
		/// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
		/// 
        /// <returns> Number of rows affected (integer) </returns>
        int SoftDelete<TEntity>(long id)
			where TEntity : class;

        /// <summary> Soft delete the <typeparamref name="TEntity"/> record. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int SoftDelete<TEntity>(SqlTransaction sqlTransaction, long id)
            where TEntity : class;
    }
}
