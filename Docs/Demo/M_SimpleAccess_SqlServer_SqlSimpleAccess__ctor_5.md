# SqlSimpleAccess Constructor (SqlConnection, CommandType)
 

Constructor.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public SqlSimpleAccess(
	SqlConnection sqlConnection,
	CommandType defaultCommandType
)
```

**VB**<br />
``` VB
Public Sub New ( 
	sqlConnection As SqlConnection,
	defaultCommandType As CommandType
)
```

**C++**<br />
``` C++
public:
SqlSimpleAccess(
	SqlConnection^ sqlConnection, 
	CommandType defaultCommandType
)
```

**F#**<br />
``` F#
new : 
        sqlConnection : SqlConnection * 
        defaultCommandType : CommandType -> SqlSimpleAccess
```


#### Parameters
&nbsp;<dl><dt>sqlConnection</dt><dd>Type: System.Data.SqlClient.SqlConnection<br />The SQL connection.</dd><dt>defaultCommandType</dt><dd>Type: System.Data.CommandType<br />The default command type for all queries</dd></dl>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess__ctor">SqlSimpleAccess Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />