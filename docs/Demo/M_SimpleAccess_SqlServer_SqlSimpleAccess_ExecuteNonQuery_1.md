# SqlSimpleAccess.ExecuteNonQuery Method (SqlTransaction, String, CommandType, Object)
 

Executes a command text against the connection and returns the number of rows affected.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int ExecuteNonQuery(
	SqlTransaction sqlTransaction,
	string commandText,
	CommandType commandType,
	Object paramObject = null
)
```

**VB**<br />
``` VB
Public Function ExecuteNonQuery ( 
	sqlTransaction As SqlTransaction,
	commandText As String,
	commandType As CommandType,
	Optional paramObject As Object = Nothing
) As Integer
```

**C++**<br />
``` C++
public:
virtual int ExecuteNonQuery(
	SqlTransaction^ sqlTransaction, 
	String^ commandText, 
	CommandType commandType, 
	Object^ paramObject = nullptr
) sealed
```

**F#**<br />
``` F#
abstract ExecuteNonQuery : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        ?paramObject : Object 
(* Defaults:
        let _paramObject = defaultArg paramObject null
*)
-> int 
override ExecuteNonQuery : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        ?paramObject : Object 
(* Defaults:
        let _paramObject = defaultArg paramObject null
*)
-> int 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_4">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteNonQuery(TDbTransaction, String, CommandType, Object)</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />