# ISqlRepository.Insert(*TEntity*) Method (Object)
 

Inserts the given dynamic object as SqlParameter names and values.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
int Insert<TEntity>(
	Object paramObject
)

```

**VB**<br />
``` VB
Function Insert(Of TEntity) ( 
	paramObject As Object
) As Integer
```

**C++**<br />
``` C++
generic<typename TEntity>
int Insert(
	Object^ paramObject
)
```

**F#**<br />
``` F#
abstract Insert : 
        paramObject : Object -> int 

```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_ISqlRepository">ISqlRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_ISqlRepository_Insert">Insert Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />