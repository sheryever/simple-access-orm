# Repository.EndTransaction Method 
 

Ends a transaction.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public void EndTransaction(
	SqlTransaction sqlTransaction,
	bool transactionSucceed = true,
	bool closeConnection = true
)
```

**VB**<br />
``` VB
Public Sub EndTransaction ( 
	sqlTransaction As SqlTransaction,
	Optional transactionSucceed As Boolean = true,
	Optional closeConnection As Boolean = true
)
```

**C++**<br />
``` C++
public:
virtual void EndTransaction(
	SqlTransaction^ sqlTransaction, 
	bool transactionSucceed = true, 
	bool closeConnection = true
) sealed
```

**F#**<br />
``` F#
abstract EndTransaction : 
        sqlTransaction : SqlTransaction * 
        ?transactionSucceed : bool * 
        ?closeConnection : bool 
(* Defaults:
        let _transactionSucceed = defaultArg transactionSucceed true
        let _closeConnection = defaultArg closeConnection true
*)
-> unit 
override EndTransaction : 
        sqlTransaction : SqlTransaction * 
        ?transactionSucceed : bool * 
        ?closeConnection : bool 
(* Defaults:
        let _transactionSucceed = defaultArg transactionSucceed true
        let _closeConnection = defaultArg closeConnection true
*)
-> unit 
```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>transactionSucceed (Optional)</dt><dd>Type: System.Boolean<br />(optional) the transaction succeed.</dd><dt>closeConnection (Optional)</dt><dd>Type: System.Boolean<br />(optional) the close connection.</dd></dl>

#### Implements
<a href="M_SimpleAccess_Repository_IRepository_EndTransaction">IRepository.EndTransaction(SqlTransaction, Boolean, Boolean)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_Repository">Repository Class</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />