# SqlSimpleAccess.ExecuteNonQuery Method (String, Object)
 

Executes the non query operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int ExecuteNonQuery(
	string commandText,
	Object paramObject = null
)
```

**VB**<br />
``` VB
Public Function ExecuteNonQuery ( 
	commandText As String,
	Optional paramObject As Object = Nothing
) As Integer
```

**C++**<br />
``` C++
public:
virtual int ExecuteNonQuery(
	String^ commandText, 
	Object^ paramObject = nullptr
) sealed
```

**F#**<br />
``` F#
abstract ExecuteNonQuery : 
        commandText : string * 
        ?paramObject : Object 
(* Defaults:
        let _paramObject = defaultArg paramObject null
*)
-> int 
override ExecuteNonQuery : 
        commandText : string * 
        ?paramObject : Object 
(* Defaults:
        let _paramObject = defaultArg paramObject null
*)
-> int 
```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_2">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteNonQuery(String, Object)</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />