# SqlSimpleAccess.GetNewConnection Method 
 

Gets the new connection.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public SqlConnection GetNewConnection()
```

**VB**<br />
``` VB
Public Function GetNewConnection As SqlConnection
```

**C++**<br />
``` C++
public:
virtual SqlConnection^ GetNewConnection() sealed
```

**F#**<br />
``` F#
abstract GetNewConnection : unit -> SqlConnection 
override GetNewConnection : unit -> SqlConnection 
```


#### Return Value
Type: SqlConnection<br />The new connection.

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_GetNewConnection">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).GetNewConnection()</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />