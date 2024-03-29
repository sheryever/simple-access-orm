# Simple Access ORM

SimpleAccess provides a simple and easy database access as well as a repository for CRUD and other helper methods.

SimpleAccess supports multiple databases. All implement the same interface for each database.

SimpleAccess also provides excpetion logging.

SimpleAccess returns data in Entity and dynamic data type but also allow developers to work on direct DataReader or DataSet

## Using SimpleAccess
Insall your required SimpleAccess implementaion from nuget

### Nuget package
#### Sql Server
```powershell
PM > Install-Package SimpleAccess.SqlServer
```
#### Oralce
```powershell
PM > Install-Package SimpleAccess.Oracle
```
#### MySql
```powershell
PM > Install-Package SimpleAccess.MySql
```
#### SQLite
```powershell
PM > Install-Package SimpleAccess.SQLite
```

**_We will use the SimpleAccess implementation for Sql Server in our example, The Implementation for Oracle, MySql and SQLite have their own IOracleSimpleAccess with OracleSimpleAccess, IMySqlSimpleAccess with MySqlSimpleAccess and ISQLiteSimpleAccess with SQLiteSimpleAccess.
They all implement ISimpleAccess_**


Creating SimpleAccess instance for Sql Server
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess();
```

Reading single record from the database as dynamic object
``` C#
var person = simpleAccess.ExecuteDynamic("SELECT * FROM dbo.People where id = @id;", new { id  = 12});
```

Reading records from the database as IEnumerable&lt;dynamic&gt;
``` C#
var people = simpleAccess.ExecuteDynamics("SELECT * FROM dbo.People;");
```

Reading multiple recoreds of a column from the database as value type
``` C#
var peopleNames = simpleAccess.ExecuteValues<string>("SELECT Name FROM dbo.People;");
```

Reading single record from the database as Person object
``` C#
var person = simpleAccess.ExecuteEntity<Person>("SELECT * FROM dbo.People where id = @id;", new { id  = 12});
```

Reading records from the database as IEnumerable&lt;Person&gt;
``` C#
var people = simpleAccess.ExecuteEntities<Person>("SELECT * FROM dbo.People;");
```

Get DataReader to read the records from the database
``` C#
var dataReader = simpleAccess.ExecuteReader("SELECT * FROM dbo.People;");
```

Executing aggregate query using SimpleAccess
``` C#
var totalPeople = simpleAccess.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.People;");
```

Executes a Insert or Update SQL statement with a class object and returns the number of rows affected
``` C#
public class PersonInsertViewModel
{
    public string Name { get; set; }
    public string Address { get; set; }
}

var person = new PersonInsertViewModel {Name = "Ahmed", Address = "Madina"};
var rowAffected = simpleAccess.ExecuteNonQuery("INSERT INTO dbo.People values (@name, @address);", person);

var rowAffected = simpleAccess.ExecuteNonQuery("UPDATE dbo.People SET Name=@name WHERE Id = @id;", new {id = 1, name = "Muhammad"});

```

Using transactions with SimpleAccess
```C#
using (var transaction = simpleAccess.BeginTrasaction())
{
    try
    {
        var person = new Person() { Name = "Ahmed", Address = "Madina" };

        var newId = simpleAccess.ExecuteScalar<int>(transaction, "INSERT INTO dbo.People VALUES (@name, @Address); SELECT SCOPE_IDENTITY();", person);

        simpleAccess.EndTransaction(transaction);
    }
    catch (Exception)
    {
        simpleAccess.EndTransaction(transaction, false);
        throw;
    }
}
```
### SimpleAccess interface

#### Methods

| Methods            | Description                                                                                                     |
|--------------------|-----------------------------------------------------------------------------------------------------------------|
| BeginTransaction    | Begins and return a database transaction.                                                                      |
| CloseDbConnection  | Close the current open connection.|
| EndTransaction     | Close an open database transaction.|
| ExecuteEntity&lt;TEntity&gt; | Sends the CommandText to the Database Connection and builds a TEntity from DataReader. |
| ExecuteEntities&lt;TEntity&gt; | Sends the CommandText to the Database Connection and builds a IEnumerable&lt;TEntity&gt; from DataReader. |
| ExecuteDynamic | Sends the CommandText to the Database Connection and builds a dynamic object from DataReader. |
| ExecuteDynamics | Sends the CommandText to the Database Connection and builds a IEnumerable&lt;dynamic&gt; from DataReader. |
| ExecuteNonQuery  | Execute CommandText and returns the count of rows affected.|
| ExecuteReader    | Executes the commandText and returns a DataReader.|
| ExecuteScalar&lt;T&gt; | Executes the command text, and returns the first column of the first row in the result set returned by the query.Additional columns or rows are ignored. |
| ExecuteValues&lt;T&gt; | Executes the command text, and returns rows as IEnumerable\<T\> of the first column |
| Fill | Execute commant text against connection and add or refresh rows in DataSet or DataTable. |
| GetNewConnection | Gets the new connection with the SimpleAccess Ojbect ConnectionString.|

***BeginTransaction and all Execute methods support async functionality***

Creating SimpleAccess object for Sql Server
``` C#
// Uses the provided connnection string
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("Data Source=SQLEXPRESS2014;Initial Catalog=SimpleAccessTest;Persist Security Info=True;Integrated Security=True;");

// Loads the connectionString from web.config or app.config connection strings
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnectionString");

// Loads the connection string name from the value of appSetting/simpleAccess:sqlConnectionStringName key in web.confg or app.config.
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess();

// Uses the provided SqlConnection object.
var sqlConnection = new SqlConnection("Data Source=SQLEXPRESS2014;Initial Catalog=SimpleAccessTest;Persist Security Info=True;Integrated Security=True;");
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess(sqlConnection);
```
***There are more constructors to configurtion the SimpleAccess***

## SimpleAccess with Repository pattern

SimpleAccess provides ready repository with Stored Procedure

***SimpleAccess now also provide SqlEntityRepository and SqlRepository has renamed to SqlSpRepository.
Both SqlSpRepository and SqlEntityRepository implements the ISqlRepository interface (version 3.1)***

#### Properties

| Property      | Description |
|---------------|-------------|
| SimpleAccess  | Base SimpleAccess object of repository. |


All methods are based on stored procedures with its related sotred procedure naming convention.
#### Methods

| Methods            | Sp Name | Description |
|--------------------|---------|-------------|
| Get&lt;TEntity&gt; | TEntity_GetById <br /> ie. People_GetById | Get TEntity by Id or anyother parameter |
| GetAll&lt;TEntity&gt; | TEntity_GetAll <br /> ie. People_GetAll | Get all TEntity object in an IEnumerable&lt;TEntity&gt;. |
| Find&lt;TEntity&gt; | TEntity_Find <br /> ie. People_Find | Searches for TEntity that matches the conditions defined by the specified predicate, and returns the first record of the result. |
| FindAll&lt;TEntity&gt; | TEntity_Find <br /> ie. People_Find | Searches for all TEntity that matches the conditions defined by the specified predicate, and returns the result as IEnumerable&lt;TEntity&gt;. |
| Insert&lt;TEntity&gt; | TEntity_Insert <br /> ie. People_Insert  | Inserts the given TEntity |
| InsertAll&lt;TEntity&gt; | TEntity_Insert <br /> ie. People_Insert  | Inserts all the given entities |
| Update&lt;TEntity&gt; | TEntity_Update <br /> ie. People_Update | Updates the given TEntity |
| UpdateAll&lt;TEntity&gt; | TEntity_Update <br /> ie. People_Update  | Updates all the given entities |
| Delete&lt;TEntity&gt; | TEntity_Delete <br /> ie. People_Delete | Deletes TEntity by the given Id |
| DeleteAll&lt;TEntity&gt; | TEntity_Delete <br /> ie. People_Delete | Deletes all the TEntity records by the given Ids |
| SoftDelete&lt;TEntity&gt; | TEntity_SoftDelete <br /> ie. People_SoftDelete | Marks TEntity deleted by the given Id   |

***All these operations also support async functionality***

**SqlEntityRepository** is designed for the developers who don't use the Stored Producers

#### Paged Extension methods

| Methods            | Description |
|--------------------|-------------|
| GetDynamicPagedList&lt;TEntity&gt; | Get the paged records of the entity as IEnumberable&lt;dynamic&gt; |
| GetEntityPagedList&lt;TEntity&gt; | Get the paged records of the entity as IEnumberable&lt;TEntity&gt; |

***All these operations also support async functionality***

#### Others Extension methods

| Methods            | Description |
|--------------------|-------------|
| IsExist&lt;TEntity&gt; | Executes the Exist query to check the record |
| GetCount&lt;TEntity&gt; | Get the count of the records |
| GetSum&lt;TEntity&gt; | Get the Sum of a column |
| GetMin&lt;TEntity&gt; | Get the minimum value of the column |
| GetMax&lt;TEntity&gt; | Get the maximum value of the column |
| GetAverage&lt;TEntity&gt; | Get the average value of the column |
| GetAverage&lt;TEntity&gt; | Get the average value of the column |
| GetAggrate&lt;TEntity&gt; | Get the aggreage functions values as a dyanmic object |
| GetAggrateWithGroupBy&lt;TEntity&gt; | Get the aggreage function values as a IEnumberable&lt;dynamic&gt; object |

#### Using SimpleAccess Repository
[Using SimpleAccess v3.1 SqlEntityRepository](UsingSimpleAccess.v31.SqlEntityRepository.md)

[Using SimpleAccess v3.1 SqlSpRepository (Repository based on Stored Procedures)](UsingSimpleAccess.v31.SqlSpRepository.md)

[Using SimpleAccess v2 and later SqlRepository with StoredProcedure](UsingSimpleAccess.v2.Repository.md)

[Using SimpleAccess v1](UsingSimpleAccess.v1.md)

## Support
- Sql Server 2005 and later
- Oracle 10g and later (in default SimpleAccess uses Oracle Managed Data Provider for .NET)
- SQLite
- MySql
- PostgreSQL (coming)

Simple Access ORM 3.1 supports dotnet 4.0, dotnet 4.5, .Net Standard 2.0 (for .netcore 2.1) and .Net Standard 2.1, while Simple Access ORM 2.x is built on dotnet full framework 3.5 to support our clients Windows XP applications which are deployed on 100s of PCs

## Roadmap
- [x] Separate SimpleCommand and Repositoy
- [x] vitual properties must behave like NotASpParameter marked perperty in Entities drived from StoredProcedureParameters
- [x] Remove StoredProcedureParameters inheritance from Enity Class to make entity more lighter
- [x] Add InsertAll\<TEntity\>, UpdateAll\<TEntity\>, DeleteAll\<TEntity\> with support of internal trasaction in Repository
- [x] Add Find\<TEntity\> and  FindAll\<TEntity\> in Repository
- [X] Add ExecuteValues\<T\> for getting result of a single column query in IEnumerable\<T\>
- [X] NetStandard 2.0 support
- [X] Write unit test
- [X] NetStandard 2.1 support with Microsoft.Data.SqlClient
- [X] Configure StoredProcedure naming convention mapping to repository method (Insert, Update, Delete, Get, GetAll, Find, FindAll) methods in repository settings
- [X] Add Sql Generation for Non StoredProcedures command types (Insert, Update, Delete, Get, GetAll, Find, FindAll) (90%)
### SqlEntityRepository Without SP implementation (version 3.1)
- [X] SqlEntityRepository implementation (90%)
- [x] Add `DefaultView` property in EntityAttribute
- [x] Add `PrimaryKeyAttribute` for decorating primary key of an entity
- [x] Add database sequence support with `PrimaryKeyAttribute`
- [X] `DefaultView` property in EntityAttribute support for default select
- [X] Add GetEntityPage and GetDynamicPage support
- [X] Add DISTINCT keyword support in GetEntityPage and GetDynamicPage query
- [X] Add RowNumber column support in GetEntityPage and GetDynamicPage query
- [X] Add IsExist function support
- [X] Add Aggregate(Count, Sum, Min, Max, Avg) functions support
- [X] Add multiple Aggregates (Count, Sum, Min, Max, Avg) data as dynamic object support
- [ ] Replace ISimpleLogger with ILogger
- [ ] Column selection with `Find`, `FindAll`, `Get`, `GetAll`
- [ ] Fixing comments, documention and adding examples
- [ ] SimpleAccess Factory, Allow SimpleAccess to create SimpleAccess object the base of configuration(xml/json)
```C#
ISimpleAccess sqlSimpleAccess = SimpleAccessFactory.Create("SqlServer")
ISimpleAccess oralceSimpleAccess = SimpleAccessFactory.Create("OracleServer")
```
- [ ] Allow developer to add database column custom/special value mapper and parameter builder using IDbDataMapper.
```
SimpleAccess.DataMappers.Add(new GeomaryDataMapper());
```
- [ ] Allow developer to force SimpleAccess to use specific custom DataMapper to map and build parameter using DbMapperAttribute DbMapper(typeof(GeomaryDataMapper)) 
- [ ] ExecuteJson and ExecuteBson
- [ ] Rewrite code generation application
 - [ ] Allow developer to add more T4 Templates
 - [ ] Allow developer to edit T4 Templates directly inside the application
