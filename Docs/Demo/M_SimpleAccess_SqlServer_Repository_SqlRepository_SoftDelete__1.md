# SqlRepository.SoftDelete(*TEntity*) Method 
 

Soft delete.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int SoftDelete<TEntity>(
	long id
)
where TEntity : class

```

**VB**<br />
``` VB
Public Function SoftDelete(Of TEntity As Class) ( 
	id As Long
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual int SoftDelete(
	long long id
) sealed
```

**F#**<br />
``` F#
abstract SoftDelete : 
        id : int64 -> int  when 'TEntity : not struct
override SoftDelete : 
        id : int64 -> int  when 'TEntity : not struct
```


#### Parameters
&nbsp;<dl><dt>id</dt><dd>Type: System.Int64<br />The identifier.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_ISqlRepository_SoftDelete__1">ISqlRepository.SoftDelete(TEntity)(Int64)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_Repository_SqlRepository">SqlRepository Class</a><br /><a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository Namespace</a><br />