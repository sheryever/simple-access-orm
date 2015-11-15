# IRepository.GetAll(*TEntity*) Method 
 

Enumerates get all in this collection.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
IEnumerable<TEntity> GetAll<TEntity>(
	string fieldToSkip = null,
	Dictionary<string, PropertyInfo> piList = null
)
where TEntity : new()

```

**VB**<br />
``` VB
Function GetAll(Of TEntity As New) ( 
	Optional fieldToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing
) As IEnumerable(Of TEntity)
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : gcnew()
IEnumerable<TEntity>^ GetAll(
	String^ fieldToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr
)
```

**F#**<br />
``` F#
abstract GetAll : 
        ?fieldToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
        let _piList = defaultArg piList null
*)
-> IEnumerable<'TEntity>  when 'TEntity : new()

```


#### Parameters
&nbsp;<dl><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: IEnumerable(*TEntity*)<br />An enumerator that allows for each to be used to process get all TEntity in this collection.

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />