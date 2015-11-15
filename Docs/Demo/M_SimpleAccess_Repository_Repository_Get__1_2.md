# Repository.Get(*TEntity*) Method (Object, SqlTransaction, String, Dictionary(String, PropertyInfo))
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public TEntity Get<TEntity>(
	Object paramObject,
	SqlTransaction transaction = null,
	string fieldToSkip = null,
	Dictionary<string, PropertyInfo> piList = null
)
where TEntity : class, new()

```

**VB**<br />
``` VB
Public Function Get(Of TEntity As {Class, New}) ( 
	paramObject As Object,
	Optional transaction As SqlTransaction = Nothing,
	Optional fieldToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing
) As TEntity
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class, gcnew()
virtual TEntity Get(
	Object^ paramObject, 
	SqlTransaction^ transaction = nullptr, 
	String^ fieldToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr
) sealed
```

**F#**<br />
``` F#
abstract Get : 
        paramObject : Object * 
        ?transaction : SqlTransaction * 
        ?fieldToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _transaction = defaultArg transaction null
        let _fieldToSkip = defaultArg fieldToSkip null
        let _piList = defaultArg piList null
*)
-> 'TEntity  when 'TEntity : not struct, new()
override Get : 
        paramObject : Object * 
        ?transaction : SqlTransaction * 
        ?fieldToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _transaction = defaultArg transaction null
        let _fieldToSkip = defaultArg fieldToSkip null
        let _piList = defaultArg piList null
*)
-> 'TEntity  when 'TEntity : not struct, new()
```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd><dt>transaction (Optional)</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />(optional) the transaction.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: *TEntity*<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Get__1_2">IRepository.Get(TEntity)(Object, SqlTransaction, String, Dictionary(String, PropertyInfo))</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Get">Get Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />