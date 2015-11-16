# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).Fill Method (String, DataSet)
 

Execute commant text against connection and add or refresh rows in DataSet

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
int Fill(
	string commandText,
	DataSet dataSet
)
```

**VB**<br />
``` VB
Function Fill ( 
	commandText As String,
	dataSet As DataSet
) As Integer
```

**C++**<br />
``` C++
int Fill(
	String^ commandText, 
	DataSet^ dataSet
)
```

**F#**<br />
``` F#
abstract Fill : 
        commandText : string * 
        dataSet : DataSet -> int 

```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>dataSet</dt><dd>Type: System.Data.DataSet<br />A DataSet to fill with records and, if necessary, schema</dd></dl>

#### Return Value
Type: Int32<br />\[Missing <returns> documentation for "M:SimpleAccess.Core.ISimpleAccess`6.Fill(System.String,System.Data.DataSet)"\]

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_Fill">Fill Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />