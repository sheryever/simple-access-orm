# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).ExecuteReader Method (String, CommandType, *TDataParameter*[])
 

Executes the commandText and return TDbDataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
TDbDataReader ExecuteReader(
	string commandText,
	CommandType commandType,
	params TDataParameter[] parameters
)
```

**VB**<br />
``` VB
Function ExecuteReader ( 
	commandText As String,
	commandType As CommandType,
	ParamArray parameters As TDataParameter()
) As TDbDataReader
```

**C++**<br />
``` C++
TDbDataReader ExecuteReader(
	String^ commandText, 
	CommandType commandType, 
	... array<TDataParameter>^ parameters
)
```

**F#**<br />
``` F#
abstract ExecuteReader : 
        commandText : string * 
        commandType : CommandType * 
        parameters : 'TDataParameter[] -> 'TDbDataReader 

```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>parameters</dt><dd>Type: <a href="T_SimpleAccess_Core_ISimpleAccess_6">*TDataParameter*</a>[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Return Value
Type: <a href="T_SimpleAccess_Core_ISimpleAccess_6">*TDbDataReader*</a><br />The TDbDataReader

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_ExecuteReader">ExecuteReader Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />