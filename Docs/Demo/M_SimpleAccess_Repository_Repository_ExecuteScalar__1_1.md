# Repository.ExecuteScalar(*T*) Method (SqlTransaction, String, CommandType, Object)
 

Executes the scalar operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public T ExecuteScalar<T>(
	SqlTransaction sqlTransaction,
	string sql,
	CommandType commandType,
	Object paramObject
)

```

**VB**<br />
``` VB
Public Function ExecuteScalar(Of T) ( 
	sqlTransaction As SqlTransaction,
	sql As String,
	commandType As CommandType,
	paramObject As Object
) As T
```

**C++**<br />
``` C++
public:
generic<typename T>
virtual T ExecuteScalar(
	SqlTransaction^ sqlTransaction, 
	String^ sql, 
	CommandType commandType, 
	Object^ paramObject
) sealed
```

**F#**<br />
``` F#
abstract ExecuteScalar : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        commandType : CommandType * 
        paramObject : Object -> 'T 
override ExecuteScalar : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        commandType : CommandType * 
        paramObject : Object -> 'T 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: *T*<br />.

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_ExecuteScalar__1_1">IRepository.ExecuteScalar(T)(SqlTransaction, String, CommandType, Object)</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="Overload_SimpleAccess_Repository_Repository_ExecuteScalar">ExecuteScalar Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />