# IRepository.Get Method (String, Object, String, Dictionary(String, PropertyInfo))
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
Object Get(
	string sql,
	Object paramObject,
	string fieldToSkip = null,
	Dictionary<string, PropertyInfo> piList = null
)
```

**VB**<br />
``` VB
Function Get ( 
	sql As String,
	paramObject As Object,
	Optional fieldToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing
) As Object
```

**C++**<br />
``` C++
Object^ Get(
	String^ sql, 
	Object^ paramObject, 
	String^ fieldToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr
)
```

**F#**<br />
``` F#
abstract Get : 
        sql : string * 
        paramObject : Object * 
        ?fieldToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
        let _piList = defaultArg piList null
*)
-> Object 

```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd></dl>

#### Return Value
Type: Object<br />.

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_Get">Get Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />