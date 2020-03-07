# SqlRepository.Get Method (String, Int64, String)
 

Gets.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public Object Get(
	string sql,
	long id,
	string fieldToSkip = null
)
```

**VB**<br />
``` VB
Public Function Get ( 
	sql As String,
	id As Long,
	Optional fieldToSkip As String = Nothing
) As Object
```

**C++**<br />
``` C++
public:
Object^ Get(
	String^ sql, 
	long long id, 
	String^ fieldToSkip = nullptr
)
```

**F#**<br />
``` F#
member Get : 
        sql : string * 
        id : int64 * 
        ?fieldToSkip : string 
(* Defaults:
        let _fieldToSkip = defaultArg fieldToSkip null
*)
-> Object 

```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>id</dt><dd>Type: System.Int64<br />The identifier.</dd><dt>fieldToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the field to skip.</dd></dl>

#### Return Value
Type: Object<br />.

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_Repository_SqlRepository">SqlRepository Class</a><br /><a href="Overload_SimpleAccess_SqlServer_Repository_SqlRepository_Get">Get Overload</a><br /><a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository Namespace</a><br />