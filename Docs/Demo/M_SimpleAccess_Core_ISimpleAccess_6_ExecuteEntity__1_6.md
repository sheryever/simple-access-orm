# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).ExecuteEntity(*TEntity*) Method (*TDbTransaction*, String, String, Dictionary(String, PropertyInfo), Object)
 

Sends the CommandText to the Connection and builds a TEntity from DataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
TEntity ExecuteEntity<TEntity>(
	TDbTransaction transaction,
	string commandText,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
	Object paramObject = null
)
where TEntity : class, new()

```

**VB**<br />
``` VB
Function ExecuteEntity(Of TEntity As {Class, New}) ( 
	transaction As TDbTransaction,
	commandText As String,
	Optional fieldsToSkip As String = Nothing,
	Optional propertyInfoDictionary As Dictionary(Of String, PropertyInfo) = Nothing,
	Optional paramObject As Object = Nothing
) As TEntity
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class, gcnew()
TEntity ExecuteEntity(
	TDbTransaction transaction, 
	String^ commandText, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ propertyInfoDictionary = nullptr, 
	Object^ paramObject = nullptr
)
```

**F#**<br />
``` F#
abstract ExecuteEntity : 
        transaction : 'TDbTransaction * 
        commandText : string * 
        ?fieldsToSkip : string * 
        ?propertyInfoDictionary : Dictionary<string, PropertyInfo> * 
        ?paramObject : Object 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _propertyInfoDictionary = defaultArg propertyInfoDictionary null
        let _paramObject = defaultArg paramObject null
*)
-> 'TEntity  when 'TEntity : not struct, new()

```


#### Parameters
&nbsp;<dl><dt>transaction</dt><dd>Type: <a href="T_SimpleAccess_Core_ISimpleAccess_6">*TDbTransaction*</a><br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>propertyInfoDictionary (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: *TEntity*<br />The value of the entity.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity">ExecuteEntity Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />