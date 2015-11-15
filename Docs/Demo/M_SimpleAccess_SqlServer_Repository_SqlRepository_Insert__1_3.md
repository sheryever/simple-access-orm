# SqlRepository.Insert(*TEntity*) Method (Object)
 

Inserts the given dynamic object as SqlParameter names and values.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Insert<TEntity>(
	Object paramObject
)

```

**VB**<br />
``` VB
Public Function Insert(Of TEntity) ( 
	paramObject As Object
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
virtual int Insert(
	Object^ paramObject
) sealed
```

**F#**<br />
``` F#
abstract Insert : 
        paramObject : Object -> int 
override Insert : 
        paramObject : Object -> int 
```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_ISqlRepository_Insert__1_3">ISqlRepository.Insert(TEntity)(Object)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_Repository_SqlRepository">SqlRepository Class</a><br /><a href="Overload_SimpleAccess_SqlServer_Repository_SqlRepository_Insert">Insert Overload</a><br /><a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository Namespace</a><br />