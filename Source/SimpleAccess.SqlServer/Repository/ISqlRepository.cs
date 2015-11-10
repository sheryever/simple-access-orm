using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SimpleAccess.Entity;

namespace SimpleAccess.SqlServer.Repository
{    
    /**--------------------------------------------------------------------------------------------------
    <summary> IRepository. </summary>
    **/
    public interface ISqlRepository
    {

        /// <summary> Enumerates get all in this collection. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> An enumerator that allows for each to be used to process get all <TEntity> in this
        /// collection. </returns>
        IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null, Dictionary<string, PropertyInfo> piList = null)
                    where TEntity : new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(SqlParameter sqlParameter, string fieldToSkip = null
            , Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(SqlTransaction transaction, SqlParameter sqlParameter, string fieldToSkip = null
            , Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();


        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="transaction">  (optional) the transaction. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// <param name="piList">	    (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(dynamic paramObject, SqlTransaction transaction = null, string fieldToSkip = null
            , Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class, new();

        /// <summary> Gets. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id">		   The identifier. </param>
        /// <param name="transaction"> (optional) the transaction. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// <param name="piList">	   (optional) dictionary of property name and PropertyInfo object. </param>
        /// 
        /// <returns> . </returns>
        TEntity Get<TEntity>(long id, SqlTransaction transaction = null, string fieldToSkip = null
            , Dictionary<string, PropertyInfo> piList = null)
            where TEntity : class,new();

        
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		   The SQL. </param>
        /// <param name="id">		   The identifier. </param>
        /// <param name="fieldToSkip"> (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        dynamic Get(string sql, long id, string fieldToSkip = null);

        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		    The SQL. </param>
        /// <param name="sqlParameter"> The SQL parameter. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        dynamic Get(string sql, SqlParameter sqlParameter, string fieldToSkip = null);

        
        /// <summary> Gets. </summary>
        /// 
        /// <param name="sql">		    The SQL. </param>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// <param name="fieldToSkip">  (optional) the field to skip. </param>
        /// 
        /// <returns> . </returns>
        dynamic Get(string sql, dynamic paramObject, string fieldToSkip = null);


        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(params SqlParameter[] sqlParameters);

        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(dynamic paramObject);

        /// <summary> Inserts the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> . </returns>
        /// int Insert<TEntity>(dynamic paramObject);

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters)
            where TEntity: class;

        /// <summary> Inserts the given SQL parameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// 
        /// <returns> . </returns>
        int Insert<TEntity>(SqlTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : class;

        /// <summary> Updates the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>        
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(dynamic paramObject)
            where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(StoredProcedureParameters storedProcedureParameters)
         where TEntity : class;

        /// <summary> Updates the given sqlParameters. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction">			 The SQL transaction. </param>
        /// <param name="storedProcedureParameters"> Options for controlling the stored procedure. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Update<TEntity>(SqlTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)
            where TEntity : class;

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(params SqlParameter[] sqlParameters)
            where TEntity : IEntity;

        /// <summary> Deletes the given dynamic object as SqlParameter names and values. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="paramObject"> The dynamic object as parameters. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(dynamic paramObject)
            where TEntity : IEntity;

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(long id)
            where TEntity : IEntity;

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
        ///  <param name="sqlTransaction">			 The SQL transaction. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(SqlTransaction sqlTransaction, long id)
            where TEntity : IEntity;

        /// <summary> Deletes the given ID. </summary>
        /// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> Number of rows affected (integer) </returns>
        int Delete<TEntity>(SqlTransaction sqlTransaction, params SqlParameter[] sqlParameters)
            where TEntity : IEntity;

        /// <summary> Soft delete. </summary>
		/// 
        /// <typeparam name="TEntity"> Type of the entity. </typeparam>
        /// <param name="id"> The identifier. </param>
		/// 
        /// <returns> Number of rows affected (integer) </returns>
        int SoftDelete<TEntity>(long id)
			where TEntity : IEntity;

    }
}
