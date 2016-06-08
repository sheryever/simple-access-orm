## Roadmap
- Separate SimpleCommand and Repositoy (Testing)
- vitual properties must behave like NotASpParameter marked perperty in Entities drived from StoredProcedureParameters (Testing)
- Remove StoredProcedureParameters inheritance from Enity Class to make entity more lighter (Testing)
- Add StoredProcedure names with repository method mappings in repository settings
- Add Sql Generation for Non StoredProcedures command types (Insert,Update,Delete,GetAll and Get)
- Documentation in reStructuredText on [readthedocs](https://readthedocs.org/)
- SimpleAccess Factory, Allow ISimpleAccess to be created on the base of configuration(xml/json)
```C#
ISimpleAccess sqlSimpleAccess = SimpleAccessFactory.Create("SqlServer")
ISimpleAccess oralceSimpleAccess = SimpleAccessFactory.Create("OracleServer")
```
- Allow developer to add database column custom/special value mapper and parameter builder using IDbDataMapper.
```C#
SimpleAccess.DataMappers.Add(new GeomaryDataMapper());
```
- Allow developer to force SimpleAccess to use specific custom DataMapper to map and build parameter using DbMapperAttribute DbMapper(typeof(GeomaryDataMapper)) 
- Add InsertAll\<TEntity\>, UpdateAll\<TEntity\>, DeleteAll\<TEntity\> with support of internal trasaction in Repository
- Add Find\<TEntity\> and  FindAll\<TEntity\> in Repository
- ExecuteJson and ExecuteBson
- Rewrite code generation application
 - Allow developer to add more T4 Templates
 - Allow developer to edit T4 Templates directly inside the application
