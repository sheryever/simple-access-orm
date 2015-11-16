# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).ExecuteDynamic Method (String, CommandType, String, Object)
 

Sends the CommandText to the Connection and builds a dynamic object from DataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
Object ExecuteDynamic(
	string commandText,
	CommandType commandType,
	string fieldsToSkip = null,
	Object paramObject = null
)
```

**VB**<br />
``` VB
Function ExecuteDynamic ( 
	commandText As String,
	commandType As CommandType,
	Optional fieldsToSkip As String = Nothing,
	Optional paramObject As Object = Nothing
) As Object
```

**C++**<br />
``` C++
Object^ ExecuteDynamic(
	String^ commandText, 
	CommandType commandType, 
	String^ fieldsToSkip = nullptr, 
	Object^ paramObject = nullptr
)
```

**F#**<br />
``` F#
abstract ExecuteDynamic : 
        commandText : string * 
        commandType : CommandType * 
        ?fieldsToSkip : string * 
        ?paramObject : Object 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _paramObject = defaultArg paramObject null
*)
-> Object 

```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Object<br />Result in a dynamic object.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic">ExecuteDynamic Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />