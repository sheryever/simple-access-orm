# SqlSimpleAccess.ExecuteDynamics Method (SqlTransaction, String, CommandType, String, SqlParameter[])
 

Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public IEnumerable<Object> ExecuteDynamics(
	SqlTransaction sqlTransaction,
	string commandText,
	CommandType commandType,
	string fieldsToSkip = null,
	params SqlParameter[] sqlParameters
)
```

**VB**<br />
``` VB
Public Function ExecuteDynamics ( 
	sqlTransaction As SqlTransaction,
	commandText As String,
	commandType As CommandType,
	Optional fieldsToSkip As String = Nothing,
	ParamArray sqlParameters As SqlParameter()
) As IEnumerable(Of Object)
```

**C++**<br />
``` C++
public:
virtual IEnumerable<Object^>^ ExecuteDynamics(
	SqlTransaction^ sqlTransaction, 
	String^ commandText, 
	CommandType commandType, 
	String^ fieldsToSkip = nullptr, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteDynamics : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        ?fieldsToSkip : string * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
*)
-> IEnumerable<Object> 
override ExecuteDynamics : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        ?fieldsToSkip : string * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
*)
-> IEnumerable<Object> 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Return Value
Type: IEnumerable(Object)<br />A list of dynamic.

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_5">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteDynamics(TDbTransaction, String, CommandType, String, TDataParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics">ExecuteDynamics Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />