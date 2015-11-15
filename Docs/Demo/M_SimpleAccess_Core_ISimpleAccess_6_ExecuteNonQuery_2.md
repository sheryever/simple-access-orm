# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).ExecuteNonQuery Method (String, Object)
 

Executes a command text against the connection and returns the number of rows affected.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
int ExecuteNonQuery(
	string commandText,
	Object paramObject = null
)
```

**VB**<br />
``` VB
Function ExecuteNonQuery ( 
	commandText As String,
	Optional paramObject As Object = Nothing
) As Integer
```

**C++**<br />
``` C++
int ExecuteNonQuery(
	String^ commandText, 
	Object^ paramObject = nullptr
)
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

```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />