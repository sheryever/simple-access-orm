# Repository.Delete(*TEntity*) Method (Object)
 

Deletes the given dynamic object as SqlParameter names and values.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public virtual int Delete<TEntity>(
	Object paramObject
)
where TEntity : class

```

**VB**<br />
``` VB
Public Overridable Function Delete(Of TEntity As Class) ( 
	paramObject As Object
) As Integer
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual int Delete(
	Object^ paramObject
)
```

**F#**<br />
``` F#
abstract Delete : 
        paramObject : Object -> int  when 'TEntity : not struct
override Delete : 
        paramObject : Object -> int  when 'TEntity : not struct
```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Delete__1_3">IRepository.Delete(TEntity)(Object)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Delete">Delete Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />