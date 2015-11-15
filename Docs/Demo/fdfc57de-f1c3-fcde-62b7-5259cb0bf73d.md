# Repository.Update(*TEntity*) Method (Object)
 

Updates the given dynamic object as SqlParameter names and values.

**Namespace:**&nbsp;<a href="41571b4f-ca9a-e902-c5ef-a7c14c631bb2">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

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
<a href="7d536e8a-8267-09b8-f425-b5f856a27d2f">IRepository.Update(TEntity)(Object)</a><br />

## See Also


#### Reference
<a href="edb9c152-cd28-6594-590a-18a81e266968">Repository Class</a><br /><a href="b8e1dd79-6b9c-be06-4b1d-b010cd91674f">Update Overload</a><br /><a href="41571b4f-ca9a-e902-c5ef-a7c14c631bb2">SimpleAccess.Repository Namespace</a><br />