# SqlSimpleAccess.ExecuteScalar(*T*) Method (SqlTransaction, String, CommandType, SqlParameter[])
 

Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public T ExecuteScalar<T>(
	SqlTransaction sqlTransaction,
	string commandText,
	CommandType commandType,
	params SqlParameter[] sqlParameters
)

```

**VB**<br />
``` VB
Public Function ExecuteScalar(Of T) ( 
	sqlTransaction As SqlTransaction,
	commandText As String,
	commandType As CommandType,
	ParamArray sqlParameters As SqlParameter()
) As T
```

**C++**<br />
``` C++
public:
generic<typename T>
virtual T ExecuteScalar(
	SqlTransaction^ sqlTransaction, 
	String^ commandText, 
	CommandType commandType, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteScalar : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        sqlParameters : SqlParameter[] -> 'T 
override ExecuteScalar : 
        sqlTransaction : SqlTransaction * 
        commandText : string * 
        commandType : CommandType * 
        sqlParameters : SqlParameter[] -> 'T 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Parmeters rquired to execute CommandText.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: *T*<br />The {TEntity} value

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_5">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).ExecuteScalar(T)(TDbTransaction, String, CommandType, TDataParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar">ExecuteScalar Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />