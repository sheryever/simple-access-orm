# ISqlRepository.Get(*TEntity*) Method (SqlTransaction, SqlParameter, String)
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
TEntity Get<TEntity>(
	SqlTransaction transaction,
	SqlParameter sqlParameter,
	string fieldToSkip = null
)
where TEntity : class, new()

```

**VB**<br />
``` VB
Function Get(Of TEntity As {Class, New}) ( 
	transaction As SqlTransaction,
	sqlParameter As SqlParameter,
	Optional fieldToSkip As String = Nothing
) As TEntity
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class, gcnew()
TEntity Get(
	SqlTransaction^ transaction, 
	SqlParameter^ sqlParameter, 
	String^ fieldToSkip = nullptr
)
```

**F#**<br />
``` F#
abstract Get : 
        transaction : SqlTransaction * 
        sqlParameter : SqlParameter * 
        ?fieldToSkip : string 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
*)
-> 'TEntity  when 'TEntity : not struct, new()

```


#### Parameters
&nbsp;<dl><dt>transaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />(optional) the transaction.</dd><dt>sqlParameter</dt><dd>Type: System.Data.SqlClient.SqlParameter<br />The SQL parameter.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: *TEntity*<br />.

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_ISqlRepository">ISqlRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_ISqlRepository_Get">Get Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />