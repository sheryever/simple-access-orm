# ISqlRepository.Update(*TEntity*) Method (StoredProcedureParameters)
 

Updates the given sqlParameters.

**Namespace:**&nbsp;<a href="41571b4f-ca9a-e902-c5ef-a7c14c631bb2">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
int Update<TEntity>(
	StoredProcedureParameters storedProcedureParameters
)
where TEntity : class

```

**VB**<br />
``` VB
Function Update(Of TEntity As Class) ( 
	storedProcedureParameters As StoredProcedureParameters
) As Integer
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class
int Update(
	StoredProcedureParameters^ storedProcedureParameters
)
```

**F#**<br />
``` F#
abstract Update : 
        storedProcedureParameters : StoredProcedureParameters -> int  when 'TEntity : not struct

```


#### Parameters
&nbsp;<dl><dt>storedProcedureParameters</dt><dd>Type: <a href="1e3afd83-1b60-7d93-412a-daa2862067e2">SimpleAccess.StoredProcedureParameters</a><br />Options for controlling the stored procedure.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

## See Also


#### Reference
<a href="f40c60f9-7bd9-9bed-0857-200cfb858bcb">ISqlRepository Interface</a><br /><a href="9de346ec-866a-d5d9-fddc-d74c53ef3710">Update Overload</a><br /><a href="41571b4f-ca9a-e902-c5ef-a7c14c631bb2">SimpleAccess.Repository Namespace</a><br />