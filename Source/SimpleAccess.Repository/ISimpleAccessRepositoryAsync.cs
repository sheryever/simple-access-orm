using SimpleAccess.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

using System.Threading.Tasks;
 
namespace SimpleAccess.Repository
{


    /// <summary>
    /// Represent the interface of SimpleAccess Repository methods
    /// </summary>
    public interface ISimpleAccessRepositoryAsync
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

        /// <summary> Get all TEntity object in a <see cref="IEnumerable{TEntity}"/>. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all TEntity in this
        /// collection. </returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, string fieldToSkip = null)
            where TEntity : new();

        /// <summary> Get TEntity by Id or any other parameter. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(IDataParameter parameter, string fieldToSkip = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The transaction. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, IDataParameter parameter, string fieldToSkip = null)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, object paramObject, string fieldToSkip = null)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> GetAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, long id, string fieldToSkip = null)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<TEntity> FindAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
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

        /// <summary> Searches for all <typeparamref name="TEntity"/> that matches the conditions defined by the specified predicate, and returns the result as <see cref="IEnumerable{TEntity}"/>. </summary>
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="expression">The expression.</param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, Expression<Func<TEntity, bool>> expression, string fieldToSkip = null)
            where TEntity : class, new();
        
        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        Task<int> InsertAsync<TEntity>(params IDataParameter[] sqlParameters);

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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> . </returns>
        Task<int> InsertAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, TEntity entity)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to insert </param>
        ///
        /// <returns> The number of affected records</returns>
        Task<int> InsertAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, IEnumerable<TEntity> entities)
            where TEntity : class;
        
        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAsync<TEntity>(params IDataParameter[] sqlParameters)
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
        /// <param name="transactionContext">			 The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="entity"> Entity to insert </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, TEntity entity)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="entities"> The <![CDATA[IEnumerable<TEntity>]]> to update </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> UpdateAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, IEnumerable<TEntity> entities)
            where TEntity : class;
        
        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(params IDataParameter[] sqlParameters)
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
        ///  <param name="transactionContext">			 The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, long id)
            where TEntity : class;


        /// <summary> Deletes the <typeparamref name="TEntity"/>  by given <see cref="SqlParameter"/> array. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, params IDataParameter[] sqlParameters)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class;

        /// <summary> Deletes all the <typeparamref name="TEntity"/> records by  objects as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="expression">The expression.</param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, Expression<Func<TEntity, bool>> expression)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        Task<int> DeleteAllAsync<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, IEnumerable<long> ids)
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
        /// <param name="transactionContext"> The IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext,. </param>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int SoftDelete<TEntity>(IDbTransactionAsyncContext<IDbConnection, IDbTransaction> transactionContext, long id)
            where TEntity : class;
            */
    }
}
