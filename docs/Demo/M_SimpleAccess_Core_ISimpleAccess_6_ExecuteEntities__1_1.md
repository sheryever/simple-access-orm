# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).ExecuteEntities(*TEntity*) Method (String, CommandType, String, Dictionary(String, PropertyInfo), *TDataParameter*[])
 

Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
IEnumerable<TEntity> ExecuteEntities<TEntity>(
	string commandText,
	CommandType commandType,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
	params TDataParameter[] parameters
)
where TEntity : new()

```

**VB**<br />
``` VB
Function ExecuteEntities(Of TEntity As New) ( 
	commandText As String,
	commandType As CommandType,
	Optional fieldsToSkip As String = Nothing,
	Optional propertyInfoDictionary As Dictionary(Of String, PropertyInfo) = Nothing,
	ParamArray parameters As TDataParameter()
) As IEnumerable(Of TEntity)
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : gcnew()
IEnumerable<TEntity>^ ExecuteEntities(
	String^ commandText, 
	CommandType commandType, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ propertyInfoDictionary = nullptr, 
	... array<TDataParameter>^ parameters
)
```

**F#**<br />
``` F#
abstract ExecuteEntities : 
        commandText : string * 
        commandType : CommandType * 
        ?fieldsToSkip : string * 
        ?propertyInfoDictionary : Dictionary<string, PropertyInfo> * 
        parameters : 'TDataParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _propertyInfoDictionary = defaultArg propertyInfoDictionary null
*)
-> IEnumerable<'TEntity>  when 'TEntity : new()

```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>propertyInfoDictionary (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd><dt>parameters</dt><dd>Type: <a href="T_SimpleAccess_Core_ISimpleAccess_6">*TDataParameter*</a>[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: IEnumerable(*TEntity*)<br />The {TEntity} value

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities">ExecuteEntities Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />