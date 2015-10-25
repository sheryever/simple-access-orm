# Simple Access
Simple Access provides database access helpers as well as repository (CURD).

Simple Access focus on database stored procedures also support other DBCommand types.

Simple Access returns data in Entity and dynamic data type.

The current version os Simple Access is version 1 and now version 2 is under development. Soon the new version code will be published on github.

## SimpleAccess helpers
- `int ExecuteNonQuery(string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] sqlParameters)`
- `int ExecuteNonQuery(string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null)`
- `int ExecuteNonQuery(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] sqlParameters)`
- `int ExecuteNonQuery(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null)`
- `T ExecuteScalar<T>(string sql, CommandType commandType, params SqlParameter[] sqlParameters)`
- `T ExecuteScalar<T>(string sql, CommandType commandType, dynamic paramObject = null)`
- `T ExecuteScalar<T>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] sqlParameters)`
- `T ExecuteScalar<T>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, dynamic paramObject = null)`
- `List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType = CommandType.StoredProcedure, string, fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `List<TEntity> ExecuteReader<TEntity>(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `List<TEntity> ExecuteReader<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `List<TEntity> ExecuteReader<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `T ExecuteReaderSingle<T>(string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `T ExecuteReaderSingle<T>(string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `TEntity ExecuteReaderSingle<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `TEntity ExecuteReaderSingle<TEntity>(SqlTransaction sqlTransaction, string sql, CommandType commandType, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)` 
- `IList<dynamic> ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `IList<dynamic> ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `IList<dynamic> ExecuteReader(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `IList<dynamic> ExecuteReader(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `dynamic ExecuteReaderSingle(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `dynamic ExecuteReaderSingle(string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `dynamic ExecuteReaderSingle(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, params SqlParameter[] sqlParameters)`
- `dynamic ExecuteReaderSingle(SqlTransaction sqlTransaction, string sql, CommandType commandType = CommandType.StoredProcedure, string fieldsToSkip = null, Dictionary<string, PropertyInfo> piList = null, dynamic paramObject = null)`
- `SqlTransaction BeginTrasaction()`
- `SqlConnection GetNewConnection()`
-  `void EndTransaction(SqlTransaction sqlTransaction, bool transactionSucced = true, bool closeConnection = true)`

*Simple Access uses Oracle objects (ie. OracleTransaction, OralceParameters etc) with Simple Access Oracle*


## Reporsitory
All methods as bsed on stored procedures. All procedures are based on stored procedures and will call <IEntity>_<MethodName>.
- `IEnumerable<TEntity> GetAll<TEntity>(string fieldToSkip = null)`  
- `TEntity Get<TEntity>(long id, SqlTransaction transaction = null, string fieldToSkip = null)`
- `TEntity Get<TEntity>(SqlParameter parameter, SqlTransaction transaction = null, string fieldToSkip = null)`
- `TEntity Get<TEntity>(dynamic paramObject, SqlTransaction transaction = null, string fieldToSkip = null)`
- `dynamic Get(string sql, long id, string fieldToSkip = null)`
- `dynamic Get(string sql, SqlParameter sqlParameter, string fieldToSkip = null)`
- `dynamic Get(string sql, dynamic paramObject, string fieldToSkip = null)`
- `int Insert<TEntity>(params SqlParameter[] sqlParameters)`
- `int Insert<TEntity>(StoredProcedureParameters storedProcedureParameters)` /* Enitis are drived by StoredProcedureParameters */
- `int Insert<TEntity>(SqlTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)`
- `int Update<TEntity>(params SqlParameter[] sqlParameters)`
- `int Update<TEntity>(dynamic paramObject)`
- `int Update<TEntity>(StoredProcedureParameters storedProcedureParameters)`
- `int Update<TEntity>(SqlTransaction sqlTransaction, StoredProcedureParameters storedProcedureParameters)`
- `int Delete<TEntity>(params SqlParameter[] sqlParameters)`
- `int Delete<TEntity>(dynamic paramObject)`
- `int Delete<TEntity>(long id, SqlTransaction sqlTransaction = null)`
- `int Delete<TEntity>(SqlTransaction sqlTransaction, params SqlParameter[] sqlParameters)`
- `Delete<T>(long id)`

GetAll<Person>() will call Person_GetAll stored procedure.

Get<Person>(1) will call Person_Get stored procedure.

Insert<Person>(person) will call Person_Insert stored procedure.

Update<Person>(person) will call Person_Update stored procedure.

Delete(1) will call Person_Delete stored procdue.

## Support
- Simple Access is written in C# and support .net Managed Code langues (C# and VB.net etc)
- Sql Server 2005 and later
- Oracle 10g and later (in default Simple Access uese Oracle Data Provider for .NET, to use Oracle Data Access Components (ODAC))

## Roadmap
- Separate SimpleCommand and Repositoy
- Remove StoredProcedureParameters inheritance from Enity Class to make entity more lighter
- Add StoredProcedure names with repository method mappings in repository settings
- Add Sql Generation for Non StoredProcedures command types
- Rewrite code generation application
