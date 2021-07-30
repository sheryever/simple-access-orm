# Simple Access ORM

SimpleAccess provides a simple and easy database access as well as a repository for CRUD and other helper methods.

SimpleAccess supports multiple databases. All implement the same interface for each database.

SimpleAccess also provides excpetion logging.

SimpleAccess returns data in Entity and dynamic data type but also allow developers to work on direct DataReader or DataSet

## Documentation
[simpleaccessorm.com](https://simpleaccessorm.com)

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

## Support
- SimpleAccess is written in C# and support .net Managed Code languages
- Sql Server 2005 and later
- Oracle 10g and later (in default SimpleAccess uses Oracle Managed Data Provider for .NET)
- SQLite
- MySql
- PostgreSQL (coming)

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
