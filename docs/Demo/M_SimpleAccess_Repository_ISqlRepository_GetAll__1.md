# ISqlRepository.GetAll(*TEntity*) Method 
 

Get all TEntity object in a IEnumerable(T).

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
IEnumerable<TEntity> GetAll<TEntity>(
	string fieldToSkip = null
)
where TEntity : new()

```

**VB**<br />
``` VB
Function GetAll(Of TEntity As New) ( 
	Optional fieldToSkip As String = Nothing
) As IEnumerable(Of TEntity)
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : gcnew()
IEnumerable<TEntity>^ GetAll(
	String^ fieldToSkip = nullptr
)
```

**F#**<br />
``` F#
abstract GetAll : 
        ?fieldToSkip : string 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
*)
-> IEnumerable<'TEntity>  when 'TEntity : new()

```


#### Parameters
&nbsp;<dl><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: IEnumerable(*TEntity*)<br />An enumerator that allows for each to be used to process get all TEntity in this collection.

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_ISqlRepository">ISqlRepository Interface</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />