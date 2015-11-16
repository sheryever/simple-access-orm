# Repository.ExecuteScalar(*T*) Method (String, CommandType, SqlParameter[])
 

Executes the scalar operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public T ExecuteScalar<T>(
	string sql,
	CommandType commandType,
	params SqlParameter[] sqlParameters
)

```

**VB**<br />
``` VB
Public Function ExecuteScalar(Of T) ( 
	sql As String,
	commandType As CommandType,
	ParamArray sqlParameters As SqlParameter()
) As T
```

**C++**<br />
``` C++
public:
generic<typename T>
virtual T ExecuteScalar(
	String^ sql, 
	CommandType commandType, 
	... array<SqlParameter^>^ sqlParameters
) sealed
```

**F#**<br />
``` F#
abstract ExecuteScalar : 
        sql : string * 
        commandType : CommandType * 
        sqlParameters : SqlParameter[] -> 'T 
override ExecuteScalar : 
        sql : string * 
        commandType : CommandType * 
        sqlParameters : SqlParameter[] -> 'T 
```


#### Parameters
&nbsp;<dl><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Options for controlling the SQL.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: *T*<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_ExecuteScalar__1_2">IRepository.ExecuteScalar(T)(String, CommandType, SqlParameter[])</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_ExecuteScalar">ExecuteScalar Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />