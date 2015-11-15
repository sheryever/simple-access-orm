# Repository.Insert(*TEntity*) Method (StoredProcedureParameters, SqlTransaction)
 

Inserts the given SQL parameters.

**Namespace:**&nbsp;<a href="41571b4f-ca9a-e902-c5ef-a7c14c631bb2">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

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
&nbsp;<dl><dt>storedProcedureParameters</dt><dd>Type: <a href="1e3afd83-1b60-7d93-412a-daa2862067e2">SimpleAccess.StoredProcedureParameters</a><br />Options for controlling the stored procedure.</dd><dt>sqlTransaction (Optional)</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="46238d97-cc3c-3d43-8b0a-7b40726217de">IRepository.Insert(TEntity)(StoredProcedureParameters, SqlTransaction)</a><br />

## See Also


#### Reference
<a href="edb9c152-cd28-6594-590a-18a81e266968">Repository Class</a><br /><a href="bb724172-ee9d-a4ff-106f-770b12cb5279">Insert Overload</a><br /><a href="41571b4f-ca9a-e902-c5ef-a7c14c631bb2">SimpleAccess.Repository Namespace</a><br />