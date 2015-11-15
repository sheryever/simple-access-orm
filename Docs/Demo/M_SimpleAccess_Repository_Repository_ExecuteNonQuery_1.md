# Repository.ExecuteNonQuery Method (SqlTransaction, String, CommandType, Object)
 

Executes the non query operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int ExecuteNonQuery(
	SqlTransaction sqlTransaction,
	string sql,
	CommandType commandType,
	Object paramObject
)
```

**VB**<br />
``` VB
Public Function ExecuteNonQuery ( 
	sqlTransaction As SqlTransaction,
	sql As String,
	commandType As CommandType,
	paramObject As Object
) As Integer
```

**C++**<br />
``` C++
public:
virtual int ExecuteNonQuery(
	SqlTransaction^ sqlTransaction, 
	String^ sql, 
	CommandType commandType, 
	Object^ paramObject
) sealed
```

**F#**<br />
``` F#
abstract ExecuteNonQuery : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        commandType : CommandType * 
        paramObject : Object -> int 
override ExecuteNonQuery : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        commandType : CommandType * 
        paramObject : Object -> int 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Int32<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_ExecuteNonQuery_1">IRepository.ExecuteNonQuery(SqlTransaction, String, CommandType, Object)</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_ExecuteNonQuery">ExecuteNonQuery Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />