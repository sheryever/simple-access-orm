# Repository.Get Method (String, SqlParameter, String, Dictionary(String, PropertyInfo))
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public Object Get(
	string sql,
	SqlParameter sqlParameter,
	string fieldToSkip = null,
	Dictionary<string, PropertyInfo> piList = null
)
```

**VB**<br />
``` VB
Public Function Get ( 
	sql As String,
	sqlParameter As SqlParameter,
	Optional fieldToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing
) As Object
```

**C++**<br />
``` C++
public:
virtual Object^ Get(
	String^ sql, 
	SqlParameter^ sqlParameter, 
	String^ fieldToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr
) sealed
```

**F#**<br />
``` F#
abstract Get : 
        sql : string * 
        sqlParameter : SqlParameter * 
        ?fieldToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
        let _piList = defaultArg piList null
*)
-> Object 
override Get : 
        sql : string * 
        sqlParameter : SqlParameter * 
        ?fieldToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
        let _piList = defaultArg piList null
*)
-> Object 
```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>sqlParameter</dt><dd>Type: System.Data.SqlClient.SqlParameter<br />The SQL parameter.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd></dl>

#### Return Value
Type: Object<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_Get">IRepository.Get(String, SqlParameter, String, Dictionary(String, PropertyInfo))</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_Get">Get Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />