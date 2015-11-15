# IRepository.Update(*TEntity*) Method (SqlTransaction, StoredProcedureParameters)
 

Updates the given sqlParameters.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
int Update<TEntity>(
	SqlTransaction sqlTransaction,
	StoredProcedureParameters storedProcedureParameters
)
where TEntity : class

```

**VB**<br />
``` VB
Function Update(Of TEntity As Class) ( 
	sqlTransaction As SqlTransaction,
	storedProcedureParameters As StoredProcedureParameters
) As Integer
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class
int Update(
	SqlTransaction^ sqlTransaction, 
	StoredProcedureParameters^ storedProcedureParameters
)
```

**F#**<br />
``` F#
abstract Update : 
        sqlTransaction : SqlTransaction * 
        storedProcedureParameters : StoredProcedureParameters -> int  when 'TEntity : not struct

```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>storedProcedureParameters</dt><dd>Type: <a href="T_SimpleAccess_StoredProcedureParameters">SimpleAccess.StoredProcedureParameters</a><br />Options for controlling the stored procedure.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_Update">Update Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />