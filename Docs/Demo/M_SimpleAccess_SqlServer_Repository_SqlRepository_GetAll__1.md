# SqlRepository.GetAll(*TEntity*) Method 
 

Enumerates get all in this collection.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public virtual IEnumerable<TEntity> GetAll<TEntity>(
	string fieldToSkip = null
)
where TEntity : new()

```

**VB**<br />
``` VB
Public Overridable Function GetAll(Of TEntity As New) ( 
	Optional fieldToSkip As String = Nothing
) As IEnumerable(Of TEntity)
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : gcnew()
virtual IEnumerable<TEntity>^ GetAll(
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
override GetAll : 
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
Type: IEnumerable(*TEntity*)<br />An enumerator that allows for each to be used to process get all {TEntity} in this collection.

#### Implements
<a href="M_SimpleAccess_Repository_ISqlRepository_GetAll__1">ISqlRepository.GetAll(TEntity)(String)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_Repository_SqlRepository">SqlRepository Class</a><br /><a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository Namespace</a><br />