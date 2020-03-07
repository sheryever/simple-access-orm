# IRepository.EndTransaction Method 
 

Ends a transaction.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
void EndTransaction(
	SqlTransaction sqlTransaction,
	bool transactionSucceed = true,
	bool closeConnection = true
)
```

**VB**<br />
``` VB
Sub EndTransaction ( 
	sqlTransaction As SqlTransaction,
	Optional transactionSucceed As Boolean = true,
	Optional closeConnection As Boolean = true
)
```

**C++**<br />
``` C++
void EndTransaction(
	SqlTransaction^ sqlTransaction, 
	bool transactionSucceed = true, 
	bool closeConnection = true
)
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

```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>transactionSucceed (Optional)</dt><dd>Type: System.Boolean<br />(optional) the transaction succeed.</dd><dt>closeConnection (Optional)</dt><dd>Type: System.Boolean<br />(optional) the close connection.</dd></dl>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />