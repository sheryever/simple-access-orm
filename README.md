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
| ExecuteEntity&lt;TEntity&gt; | Sends the CommandText to the Database Connection and builds a TEntity from DataReader. |
| ExecuteEntities&lt;TEntity&gt; | Sends the CommandText to the Database Connection and builds a IEnumerable&lt;TEntity&gt; from DataReader. |
| ExecuteDynamic | Sends the CommandText to the Database Connection and builds a dynamic object from DataReader. |
| ExecuteDynamics | Sends the CommandText to the Database Connection and builds a IEnumerable&lt;dynamic&gt; from DataReader. |
| ExecuteNonQuery  | Execute CommandText and returns the count of rows affected.|
| ExecuteReader    | Executes the commandText and returns a DataReader.|
| ExecuteScalar&lt;T&gt; | Executes the command text, and returns the first column of the first row in the result set returned by the query.Additional columns or rows are ignored. |
| Fill | Execute commant text against connection and add or refresh rows in DataSet or DataTable. |
| GetNewConnection | Gets the new connection with the SimpleAccess Ojbect ConnectionString.|

** *All Execute and Fill methods have different overloads.* **

## SqlRepository

All methods as bsed on stored procedures with a specific sotred procedure naming convention.

| Methods            | Sp Name | Description |
|--------------------|---------|-------------|
| Get&lt;TEntity&gt; | TEntity_GetById </br> ie. Person_GetById | Get TEntity by Id or anyother parameter |
| GetAll&lt;TEntity&gt; | TEntity_GetAll </br> ie. Person_GetAll | Get all TEntity object in an IEnumerable&lt;TEntity&gt;. |
| Insert&lt;TEntity&gt; | TEntity_Insert </br> ie. Person_Insert  | Inserts the given TEntity |
| Update&lt;TEntity&gt; | TEntity_Update </br> ie. Person_Update | Updates the given TEntity |
| Delete&lt;TEntity&gt; | TEntity_Delete </br> ie. Person_Delete | Deletes TEntity by the given Id |
| SoftDelete&lt;TEntity&gt; | TEntity_MarkDelete </br> ie. Person_MarkDelete | Marks  TEntity deleted by the given Id   |


## Support
- Simple Access is written in C# and support .net Managed Code langues (C# and VB.net etc)
- Sql Server 2005 and later
- Oracle 10g and later (in default Simple Access uese Oracle Data Provider for .NET, to use Oracle Data Access Components (ODAC))

## Roadmap
- Separate SimpleCommand and Repositoy (Testing)
- vitual properties must behave like NotASpParameter marked perperty in Entities drived from StoredProcedureParameters (Testing)
[Read more...](docs/roadmap.md)