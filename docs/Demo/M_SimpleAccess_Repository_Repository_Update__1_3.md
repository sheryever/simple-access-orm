# Repository.Update(*TEntity*) Method (Object)
 

Updates the given dynamic object as SqlParameter names and values.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Update<TEntity>(
	Object paramObject
)
where TEntity : class

```

**VB**<br />
``` VB
Public Function Update(Of TEntity As Class) ( 
	paramObject As Object
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual int Update(
	Object^ paramObject
) sealed
```

**F#**<br />
``` F#
abstract Update : 
        paramObject : Object -> int  when 'TEntity : not struct
override Update : 
        paramObject : Object -> int  when 'TEntity : not struct
```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Update__1_3">IRepository.Update(TEntity)(Object)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Update">Update Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />