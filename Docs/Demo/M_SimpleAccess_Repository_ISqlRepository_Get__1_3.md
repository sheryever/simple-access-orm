# ISqlRepository.Get(*TEntity*) Method (Object, SqlTransaction, String)
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
TEntity Get<TEntity>(
	Object paramObject,
	SqlTransaction transaction = null,
	string fieldToSkip = null
)
where TEntity : class, new()

```

**VB**<br />
``` VB
Function Get(Of TEntity As {Class, New}) ( 
	paramObject As Object,
	Optional transaction As SqlTransaction = Nothing,
	Optional fieldToSkip As String = Nothing
) As TEntity
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class, gcnew()
TEntity Get(
	Object^ paramObject, 
	SqlTransaction^ transaction = nullptr, 
	String^ fieldToSkip = nullptr
)
```

**F#**<br />
``` F#
abstract Get : 
        paramObject : Object * 
        ?transaction : SqlTransaction * 
        ?fieldToSkip : string 
(* Defaults:
        let _transaction = defaultArg transaction null
        let _fieldToSkip = defaultArg fieldToSkip null
*)
-> 'TEntity  when 'TEntity : not struct, new()

```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd><dt>transaction (Optional)</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />(optional) the transaction.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: *TEntity*<br />.

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_ISqlRepository">ISqlRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_ISqlRepository_Get">Get Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />