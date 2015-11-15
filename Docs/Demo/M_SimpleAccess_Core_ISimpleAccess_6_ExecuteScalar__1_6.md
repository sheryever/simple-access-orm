# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).ExecuteScalar(*T*) Method (*TDbTransaction*, String, Object)
 

Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
T ExecuteScalar<T>(
	TDbTransaction transaction,
	string commandText,
	Object paramObject = null
)

```

**VB**<br />
``` VB
Function ExecuteScalar(Of T) ( 
	transaction As TDbTransaction,
	commandText As String,
	Optional paramObject As Object = Nothing
) As T
```

**C++**<br />
``` C++
generic<typename T>
T ExecuteScalar(
	TDbTransaction transaction, 
	String^ commandText, 
	Object^ paramObject = nullptr
)
```

**F#**<br />
``` F#
abstract ExecuteScalar : 
        transaction : 'TDbTransaction * 
        commandText : string * 
        ?paramObject : Object 
(* Defaults:
        let _paramObject = defaultArg paramObject null
*)
-> 'T 

```


#### Parameters
&nbsp;<dl><dt>transaction</dt><dd>Type: <a href="T_SimpleAccess_Core_ISimpleAccess_6">*TDbTransaction*</a><br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: *T*<br />The {T} value

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar">ExecuteScalar Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />