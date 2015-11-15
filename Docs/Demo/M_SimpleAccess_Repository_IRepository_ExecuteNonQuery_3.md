# IRepository.ExecuteNonQuery Method (String, CommandType, Object)
 

Executes the non query operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
int ExecuteNonQuery(
	string sql,
	CommandType commandType = CommandType.StoredProcedure,
	Object paramObject = null
)
```

**VB**<br />
``` VB
Function ExecuteNonQuery ( 
	sql As String,
	Optional commandType As CommandType = CommandType.StoredProcedure,
	Optional paramObject As Object = Nothing
) As Integer
```

**C++**<br />
``` C++
int ExecuteNonQuery(
	String^ sql, 
	CommandType commandType = CommandType::StoredProcedure, 
	Object^ paramObject = nullptr
)
```

**F#**<br />
``` F#
abstract ExecuteNonQuery : 
        sql : string * 
        ?commandType : CommandType * 
        ?paramObject : Object 
(* Defaults:
        let _commandType = defaultArg commandType CommandType.StoredProcedure
        let _paramObject = defaultArg paramObject null
*)
-> int 

```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType (Optional)</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />