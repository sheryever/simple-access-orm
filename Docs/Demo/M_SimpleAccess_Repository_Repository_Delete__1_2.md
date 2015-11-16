# Repository.Delete(*TEntity*) Method (Int64, SqlTransaction)
 

Deletes the given ID.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Delete<TEntity>(
	long id,
	SqlTransaction sqlTransaction = null
)
where TEntity : class

```

**VB**<br />
``` VB
Public Function Delete(Of TEntity As Class) ( 
	id As Long,
	Optional sqlTransaction As SqlTransaction = Nothing
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual int Delete(
	long long id, 
	SqlTransaction^ sqlTransaction = nullptr
) sealed
```

**F#**<br />
``` F#
abstract Delete : 
        id : int64 * 
        ?sqlTransaction : SqlTransaction 
(* Defaults:
        let _sqlTransaction = defaultArg sqlTransaction null
*)
-> int  when 'TEntity : not struct
override Delete : 
        id : int64 * 
        ?sqlTransaction : SqlTransaction 
(* Defaults:
        let _sqlTransaction = defaultArg sqlTransaction null
*)
-> int  when 'TEntity : not struct
```


#### Parameters
&nbsp;<dl><dt>id</dt><dd>Type: System.Int64<br />The identifier.</dd><dt>sqlTransaction (Optional)</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Delete__1_2">IRepository.Delete(TEntity)(Int64, SqlTransaction)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Delete">Delete Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />