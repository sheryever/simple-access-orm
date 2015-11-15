# Repository.Insert(*TEntity*) Method (SqlParameter[])
 

Inserts the given SQL parameters.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Insert<TEntity>(
	params SqlParameter[] sqlParameters
)

```

**VB**<br />
``` VB
Public Function Insert(Of TEntity) ( 
	ParamArray sqlParameters As SqlParameter()
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
virtual int Insert(
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract Insert : 
        sqlParameters : SqlParameter[] -> int 
override Insert : 
        sqlParameters : SqlParameter[] -> int 
```


#### Parameters
&nbsp;<dl><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Options for controlling the SQL.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Insert__1_2">IRepository.Insert(TEntity)(SqlParameter[])</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Insert">Insert Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />