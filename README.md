# Simple Access
SimpleAccess provides a simplle database access as well as simple repository for CURD and other helper methods.

SimpleAccess provides excpetion logging.

SimpleAccess returns data in Entity and dynamic data type but also allow developers to work on direct DataReader or DataSet

## SqlSimpleAccess
Sql Server implementaion for SimpleAccess.

| Methods | Description |
|--------------------|--------|
| BeginTrasaction  | Begins a database transaction.|
| CloseCurrentDbConnection | Close the current open connection.|
| EndTransaction   | Close an open database transaction.|
| ExecuteEntity<TEntity> | Sends the CommandText to the Database Connection and builds a TEntity from DataReader. |
| ExecuteEntities<TEntity> | Sends the CommandText to the Database Connection and builds a IEnumerable&lt; TEntity&gt; from DataReader. |
| ExecuteDynamic | Sends the CommandText to the Database Connection and builds a dynamic object from DataReader. |
| ExecuteDynamics | Sends the CommandText to the Database Connection and builds a IEnumerable&lt; dynamic&gt; from DataReader. |
| ExecuteNonQuery  | Execute CommandText and returns the count of rows affected.|
| ExecuteReader    | Executes the commandText and returns a DataReader.|
| ExecuteScalar<T> | Executes the command text, and returns the first column of the first row in the result set returned by the query.Additional columns or rows are ignored. |
| Fill | Execute commant text against connection and add or refresh rows in DataSet or DataTable. |
| GetNewConnection | Gets the new connection with the SimpleAccess Ojbect ConnectionString.|



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
- Separate SimpleCommand and Repositoy (Testing)
- vitual properties must behave like NotASpParameter marked perperty in Entities drived from StoredProcedureParameters (Testing)
- Remove StoredProcedureParameters inheritance from Enity Class to make entity more lighter
- Add StoredProcedure names with repository method mappings in repository settings
- Add Sql Generation for Non StoredProcedures command types
- Rewrite code generation application
 - Allow developer to add more T4 Templates
 - Allow developer to edit T4 Templates directly inside the application
