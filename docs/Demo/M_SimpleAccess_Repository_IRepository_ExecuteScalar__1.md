# IRepository.ExecuteScalar(*T*) Method (SqlTransaction, String, CommandType, SqlParameter[])
 

Executes the scalar operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
T ExecuteScalar<T>(
	SqlTransaction sqlTransaction,
	string sql,
	CommandType commandType = CommandType.StoredProcedure,
	params SqlParameter[] sqlParameters
)

```

**VB**<br />
``` VB
Function ExecuteScalar(Of T) ( 
	sqlTransaction As SqlTransaction,
	sql As String,
	Optional commandType As CommandType = CommandType.StoredProcedure,
	ParamArray sqlParameters As SqlParameter()
) As T
```

**C++**<br />
``` C++
generic<typename T>
T ExecuteScalar(
	SqlTransaction^ sqlTransaction, 
	String^ sql, 
	CommandType commandType = CommandType::StoredProcedure, 
	... array<SqlParameter^>^ sqlParameters
)
```

**F#**<br />
``` F#
abstract ExecuteScalar : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        ?commandType : CommandType * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _commandType = defaultArg commandType CommandType.StoredProcedure
*)
-> 'T 

```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType (Optional)</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Options for controlling the SQL.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: *T*<br />The T value

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_ExecuteScalar">ExecuteScalar Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />