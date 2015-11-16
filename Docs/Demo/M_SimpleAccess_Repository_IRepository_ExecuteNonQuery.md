# IRepository.ExecuteNonQuery Method (SqlTransaction, String, CommandType, SqlParameter[])
 

Executes the non query operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
int ExecuteNonQuery(
	SqlTransaction sqlTransaction,
	string sql,
	CommandType commandType = CommandType.StoredProcedure,
	params SqlParameter[] sqlParameters
)
```

**VB**<br />
``` VB
Function ExecuteNonQuery ( 
	sqlTransaction As SqlTransaction,
	sql As String,
	Optional commandType As CommandType = CommandType.StoredProcedure,
	ParamArray sqlParameters As SqlParameter()
) As Integer
```

**C++**<br />
``` C++
int ExecuteNonQuery(
	SqlTransaction^ sqlTransaction, 
	String^ sql, 
	CommandType commandType = CommandType::StoredProcedure, 
	... array<SqlParameter^>^ sqlParameters
)
```

**F#**<br />
``` F#
abstract ExecuteNonQuery : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        ?commandType : CommandType * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _commandType = defaultArg commandType CommandType.StoredProcedure
*)
-> int 

```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType (Optional)</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Options for controlling the SQL.</dd></dl>

#### Return Value
Type: Int32<br />Number of rows affected (integer)

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />