## Roadmap
- [x] Separate SimpleCommand and Repositoy
- [x] vitual properties must behave like NotASpParameter marked perperty in Entities drived from StoredProcedureParameters
- [x] Remove StoredProcedureParameters inheritance from Enity Class to make entity more lighter
- [x] Add InsertAll\<TEntity\>, UpdateAll\<TEntity\>, DeleteAll\<TEntity\> with support of internal trasaction in Repository
- [x] Add Find\<TEntity\> and  FindAll\<TEntity\> in Repository
- [X] Add ExecuteValues\<T\> for getting result of a single column query in IEnumerable\<T\>
- [X] Write unit test
- [ ] Configure StoredProcedure naming convention mapping to repository method (Insert, Update, Delete, Get, GetAll, Find, FindAll) methods in repository settings
- [ ] Add Sql Generation for Non StoredProcedures command types (Insert, Update, Delete, Get, GetAll, Find, FindAll)
- [ ] Fixing comments, documention and adding examples
- [ ] SimpleAccess Factory, Allow SimpleAccess to create SimpleAccess object the base of configuration(xml/json)
```C#
ISimpleAccess sqlSimpleAccess = SimpleAccessFactory.Create("SqlServer")
ISimpleAccess oralceSimpleAccess = SimpleAccessFactory.Create("OracleServer")
```
- [ ] Allow developer to add database column custom/special value mapper and parameter builder using IDbDataMapper.
```C#
SimpleAccess.DataMappers.Add(new GeomaryDataMapper());
```
- [ ] Allow developer to force SimpleAccess to use specific custom DataMapper to map and build parameter using DbMapperAttribute DbMapper(typeof(GeomaryDataMapper)) 
- [ ] ExecuteJson and ExecuteBson
- [ ] Rewrite code generation application
 - [ ] Allow developer to add more T4 Templates
 - [ ] Allow developer to edit T4 Templates directly inside the application

### SqlRepository Without SP implementation
- [x] Add `DefaultView` property in EntityAttribute
- [ ] `DefaultView` property in EntityAttribute support for default select
- [ ] Add `GetAllFrom(string viewName)` select the data from given view name insead of EntityNmae (Table name) & EntityAttribute (EntityName / DefaultView)
