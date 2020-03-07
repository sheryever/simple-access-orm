# SqlRepository.Get(*TEntity*) Method (Int64, SqlTransaction, String)
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public TEntity Get<TEntity>(
	long id,
	SqlTransaction transaction = null,
	string fieldToSkip = null
)
where TEntity : class, new()

```

**VB**<br />
``` VB
Public Function Get(Of TEntity As {Class, New}) ( 
	id As Long,
	Optional transaction As SqlTransaction = Nothing,
	Optional fieldToSkip As String = Nothing
) As TEntity
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class, gcnew()
virtual TEntity Get(
	long long id, 
	SqlTransaction^ transaction = nullptr, 
	String^ fieldToSkip = nullptr
) sealed
```

**F#**<br />
``` F#
abstract Get : 
        id : int64 * 
        ?transaction : SqlTransaction * 
        ?fieldToSkip : string 
(* Defaults:
        let _transaction = defaultArg transaction null
        let _fieldToSkip = defaultArg fieldToSkip null
*)
-> 'TEntity  when 'TEntity : not struct, new()
override Get : 
        id : int64 * 
        ?transaction : SqlTransaction * 
        ?fieldToSkip : string 
(* Defaults:
        let _transaction = defaultArg transaction null
        let _fieldToSkip = defaultArg fieldToSkip null
*)
-> 'TEntity  when 'TEntity : not struct, new()
```


#### Parameters
&nbsp;<dl><dt>id</dt><dd>Type: System.Int64<br />The identifier.</dd><dt>transaction (Optional)</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />(optional) the transaction.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: *TEntity*<br />.

#### Implements
<a href="M_SimpleAccess_Repository_ISqlRepository_Get__1_2">ISqlRepository.Get(TEntity)(Int64, SqlTransaction, String)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_Repository_SqlRepository">SqlRepository Class</a><br /><a href="Overload_SimpleAccess_SqlServer_Repository_SqlRepository_Get">Get Overload</a><br /><a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository Namespace</a><br />