# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).EndTransaction Method 
 

Close an open database transaction.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
void EndTransaction(
	TDbTransaction transaction,
	bool transactionSucceed = true,
	bool closeConnection = true
)
```

**VB**<br />
``` VB
Sub EndTransaction ( 
	transaction As TDbTransaction,
	Optional transactionSucceed As Boolean = true,
	Optional closeConnection As Boolean = true
)
```

**C++**<br />
``` C++
void EndTransaction(
	TDbTransaction transaction, 
	bool transactionSucceed = true, 
	bool closeConnection = true
)
```

**F#**<br />
``` F#
abstract EndTransaction : 
        transaction : 'TDbTransaction * 
        ?transactionSucceed : bool * 
        ?closeConnection : bool 
(* Defaults:
        let _transactionSucceed = defaultArg transactionSucceed true
        let _closeConnection = defaultArg closeConnection true
*)
-> unit 

```


#### Parameters
&nbsp;<dl><dt>transaction</dt><dd>Type: <a href="T_SimpleAccess_Core_ISimpleAccess_6">*TDbTransaction*</a><br />The SQL transaction.</dd><dt>transactionSucceed (Optional)</dt><dd>Type: System.Boolean<br />(optional) the transaction succeed.</dd><dt>closeConnection (Optional)</dt><dd>Type: System.Boolean<br />(optional) the close connection.</dd></dl>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />