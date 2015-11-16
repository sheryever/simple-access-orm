# Repository.Insert(*TEntity*) Method (StoredProcedureParameters, SqlTransaction)
 

Inserts the given SQL parameters.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Insert<TEntity>(
	StoredProcedureParameters storedProcedureParameters,
	SqlTransaction sqlTransaction = null
)
where TEntity : class

```

**VB**<br />
``` VB
Public Function Insert(Of TEntity As Class) ( 
	storedProcedureParameters As StoredProcedureParameters,
	Optional sqlTransaction As SqlTransaction = Nothing
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual int Insert(
	StoredProcedureParameters^ storedProcedureParameters, 
	SqlTransaction^ sqlTransaction = nullptr
) sealed
```

**F#**<br />
``` F#
abstract Insert : 
        storedProcedureParameters : StoredProcedureParameters * 
        ?sqlTransaction : SqlTransaction 
(* Defaults:
        let _sqlTransaction = defaultArg sqlTransaction null
*)
-> int  when 'TEntity : not struct
override Insert : 
        storedProcedureParameters : StoredProcedureParameters * 
        ?sqlTransaction : SqlTransaction 
(* Defaults:
        let _sqlTransaction = defaultArg sqlTransaction null
*)
-> int  when 'TEntity : not struct
```


#### Parameters
&nbsp;<dl><dt>storedProcedureParameters</dt><dd>Type: <a href="T_SimpleAccess_StoredProcedureParameters">SimpleAccess.StoredProcedureParameters</a><br />Options for controlling the stored procedure.</dd><dt>sqlTransaction (Optional)</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Insert__1_1">IRepository.Insert(TEntity)(StoredProcedureParameters, SqlTransaction)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Insert">Insert Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />