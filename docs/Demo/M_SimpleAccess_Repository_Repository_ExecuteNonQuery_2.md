# Repository.ExecuteNonQuery Method (String, CommandType, SqlParameter[])
 

Executes the non query operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int ExecuteNonQuery(
	string sql,
	CommandType commandType,
	params SqlParameter[] sqlParameters
)
```

**VB**<br />
``` VB
Public Function ExecuteNonQuery ( 
	sql As String,
	commandType As CommandType,
	ParamArray sqlParameters As SqlParameter()
) As Integer
```

**C++**<br />
``` C++
public:
virtual int ExecuteNonQuery(
	String^ sql, 
	CommandType commandType, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteNonQuery : 
        sql : string * 
        commandType : CommandType * 
        sqlParameters : SqlParameter[] -> int 
override ExecuteNonQuery : 
        sql : string * 
        commandType : CommandType * 
        sqlParameters : SqlParameter[] -> int 
```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Options for controlling the SQL.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_ExecuteNonQuery_2">IRepository.ExecuteNonQuery(String, CommandType, SqlParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />