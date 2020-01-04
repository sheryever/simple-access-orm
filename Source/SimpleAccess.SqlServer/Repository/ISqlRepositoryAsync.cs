using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SimpleAccess.SqlServer;

namespace SimpleAccess.SqlServer
{
#if !NET40

    /// <summary>
    /// Represent the interface of SimpleAccess Repository methods
    /// </summary>
    public interface ISqlRepositoryAsync
    {
        /// <summary> Get all TEntity object in a <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all TEntity in this
        /// collection. </returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(string fieldToSkip = null)
                    where TEntity : new();

        /// <summary> Get TEntity by Id or any other parameter. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext">  (optional) the transaction. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(SqlTransactionAsyncContext transactionContext, SqlParameter sqlParameter, string fieldToSkip = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(object paramObject, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(SqlTransactionAsyncContext transactionContext, object paramObject, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(long id, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(SqlTransactionAsyncContext transactionContext, long id, string fieldToSkip = null)
            where TEntity : class,new();

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the first record of the result. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> FindAsync<TEntity>(SqlTransactionAsyncContext transactionContext, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for all <typeparamref name="TEntity"/> and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for all <typeparamref name="TEntity"/> and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, string fieldToSkip = null)
            where TEntity : class, new();

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();
        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        Task<int> InsertAsync<TEntity>(params SqlParameter[] sqlParameters);

        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        Task<int> InsertAsync<TEntity>(object paramObject);

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        Task<int> InsertAsync<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        Task<int> InsertAsync<TEntity>(SqlTransactionAsyncContext transactionContext, TEntity entity)
            where TEntity : class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        /// 
        /// <returns> The number of affected records</returns>
        Task<int> InsertAllAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        Task<int> InsertAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, IEnumerable<TEntity> entities)
            where TEntity : class;
        
        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAsync<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        /// <summary> Updates the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAsync<TEntity>(object paramObject)
            where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity)
         where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext">			 The SqlTransactionAsyncContext. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAsync<TEntity>(SqlTransactionAsyncContext transactionContext, TEntity entity)
            where TEntity : class;

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAllAsync<TEntity>(IEnumerable<TEntity> entities)
         where TEntity : class;

        /// <summary> Updates all the given entities. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, IEnumerable<TEntity> entities)
            where TEntity : class;
        
        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(object paramObject)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(long id)
            where TEntity : class;

        /// <summary> Deletes the <typeparamref name="TEntity"/> by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        ///  <param name="transactionContext">			 The SqlTransactionAsyncContext. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(SqlTransactionAsyncContext transactionContext, long id)
            where TEntity : class;


        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given <see cref="SqlParameter"/> array. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(SqlTransactionAsyncContext transactionContext, params SqlParameter[] sqlParameters)
            where TEntity : class;


        /// <summary> Delete All the <typeparamref name="TEntity"/> records from the table. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>()
            where TEntity : class;

        /// <summary> Delete All the <typeparamref name="TEntity"/> records with a transaction. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObjects"> The <![CDATA[IEnumerable<object>]]> objects as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(IEnumerable<object> paramObjects)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(IEnumerable<long> ids)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by given IDs. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="ids"> The identifiers of records. </param>
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(SqlTransactionAsyncContext transactionContext, IEnumerable<long> ids)
            where TEntity : class;
        /*
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
        /// <param name="transactionContext"> The SqlTransactionAsyncContext. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int SoftDelete<TEntity>(SqlTransactionAsyncContext transactionContext, long id)
            where TEntity : class;
            */
    }
#endif
}
