# SqlSimpleAccess.ExecuteEntities(*TEntity*) Method (SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])
 

Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public IEnumerable<TEntity> ExecuteEntities<TEntity>(
	SqlTransaction sqlTransaction,
	string commandText,
	CommandType commandType,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> propertyInfoDictionary = null,
	params SqlParameter[] sqlParameters
)
where TEntity : new()

```

**VB**<br />
``` VB
Public Function ExecuteEntities(Of TEntity As New) ( 
	sqlTransaction As SqlTransaction,
	commandText As String,
	commandType As CommandType,
	Optional fieldsToSkip As String = Nothing,
	Optional propertyInfoDictionary As Dictionary(Of String, PropertyInfo) = Nothing,
	ParamArray sqlParameters As SqlParameter()
) As IEnumerable(Of TEntity)
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : gcnew()
virtual IEnumerable<TEntity>^ ExecuteEntities(
	SqlTransaction^ sqlTransaction, 
	String^ commandText, 
	CommandType commandType, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ propertyInfoDictionary = nullptr, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteEntities : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        ?fieldsToSkip : string * 
        ?propertyInfoDictionary : Dictionary<string, PropertyInfo> * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _propertyInfoDictionary = defaultArg propertyInfoDictionary null
*)
-> IEnumerable<'TEntity>  when 'TEntity : new()
override ExecuteEntities : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        ?fieldsToSkip : string * 
        ?propertyInfoDictionary : Dictionary<string, PropertyInfo> * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _propertyInfoDictionary = defaultArg propertyInfoDictionary null
*)
-> IEnumerable<'TEntity>  when 'TEntity : new()
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>propertyInfoDictionary (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: IEnumerable(*TEntity*)<br />The {TEntity} value

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_5">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteEntities(TEntity)(TDbTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), TDataParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities">ExecuteEntities Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />