# SqlSimpleAccess.ExecuteDynamic Method (SqlTransaction, String, String, SqlParameter[])
 

Sends the CommandText to the Connection and builds a dynamic object from DataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public Object ExecuteDynamic(
	SqlTransaction sqlTransaction,
	string commandText,
	string fieldsToSkip = null,
	params SqlParameter[] sqlParameters
)
```

**VB**<br />
``` VB
Public Function ExecuteDynamic ( 
	sqlTransaction As SqlTransaction,
	commandText As String,
	Optional fieldsToSkip As String = Nothing,
	ParamArray sqlParameters As SqlParameter()
) As Object
```

**C++**<br />
``` C++
public:
virtual Object^ ExecuteDynamic(
	SqlTransaction^ sqlTransaction, 
	String^ commandText, 
	String^ fieldsToSkip = nullptr, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteDynamic : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        ?fieldsToSkip : string * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
*)
-> Object 
override ExecuteDynamic : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        ?fieldsToSkip : string * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
*)
-> Object 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Return Value
Type: Object<br />Result in a dynamic object.

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_7">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteDynamic(TDbTransaction, String, String, TDataParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic">ExecuteDynamic Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />