# SqlSimpleAccess.ExecuteEntity(*TEntity*) Method (String, String, Dictionary(String, PropertyInfo), SqlParameter[])
 

Sends the CommandText to the Connection and builds a TEntity from DataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public TEntity ExecuteEntity<TEntity>(
	string commandText,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
	params SqlParameter[] sqlParameters
)
where TEntity : class, new()

```

**VB**<br />
``` VB
Public Function ExecuteEntity(Of TEntity As {Class, New}) ( 
	commandText As String,
	Optional fieldsToSkip As String = Nothing,
	Optional propertyInfoDictionary As Dictionary(Of String, PropertyInfo) = Nothing,
	ParamArray sqlParameters As SqlParameter()
) As TEntity
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class, gcnew()
virtual TEntity ExecuteEntity(
	String^ commandText, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ propertyInfoDictionary = nullptr, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteEntity : 
        commandText : string * 
        ?fieldsToSkip : string * 
        ?propertyInfoDictionary : Dictionary<string, PropertyInfo> * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _propertyInfoDictionary = defaultArg propertyInfoDictionary null
*)
-> 'TEntity  when 'TEntity : not struct, new()
override ExecuteEntity : 
        commandText : string * 
        ?fieldsToSkip : string * 
        ?propertyInfoDictionary : Dictionary<string, PropertyInfo> * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _propertyInfoDictionary = defaultArg propertyInfoDictionary null
*)
-> 'TEntity  when 'TEntity : not struct, new()
```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>propertyInfoDictionary (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity.</dd></dl>

#### Return Value
Type: *TEntity*<br />The value of the entity.

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_3">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteEntity(TEntity)(String, String, Dictionary(String, PropertyInfo), TDataParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity">ExecuteEntity Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />