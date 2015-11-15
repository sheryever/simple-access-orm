# IRepository.ExecuteScalar(*T*) Method (String, CommandType, Object)
 

Executes the scalar operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
T ExecuteScalar<T>(
	string sql,
	CommandType commandType,
	Object paramObject = null
)

```

**VB**<br />
``` VB
Function ExecuteScalar(Of T) ( 
	sql As String,
	commandType As CommandType,
	Optional paramObject As Object = Nothing
) As T
```

**C++**<br />
``` C++
generic<typename T>
T ExecuteScalar(
	String^ sql, 
	CommandType commandType, 
	Object^ paramObject = nullptr
)
```

**F#**<br />
``` F#
abstract ExecuteScalar : 
        sql : string * 
        commandType : CommandType * 
        ?paramObject : Object 
(* Defaults:
        let _paramObject = defaultArg paramObject null
*)
-> 'T 

```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: *T*<br />The T value

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_ExecuteScalar">ExecuteScalar Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />