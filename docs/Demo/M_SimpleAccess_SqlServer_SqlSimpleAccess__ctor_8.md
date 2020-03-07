# SqlSimpleAccess Constructor (String, CommandType)
 

Constructor.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public SqlSimpleAccess(
	string connection,
	CommandType defaultCommandType
)
```

**VB**<br />
``` VB
Public Sub New ( 
	connection As String,
	defaultCommandType As CommandType
)
```

**C++**<br />
``` C++
public:
SqlSimpleAccess(
	String^ connection, 
	CommandType defaultCommandType
)
```

**F#**<br />
``` F#
new : 
        connection : string * 
        defaultCommandType : CommandType -> SqlSimpleAccess
```


#### Parameters
&nbsp;<dl><dt>connection</dt><dd>Type: System.String<br />The ConnectionString Name from the config file or a complete ConnectionString .</dd><dt>defaultCommandType</dt><dd>Type: System.Data.CommandType<br />The default command type for all queries</dd></dl>

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess__ctor">SqlSimpleAccess Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />