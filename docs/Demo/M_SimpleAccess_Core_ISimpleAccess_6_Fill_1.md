# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*).Fill Method (String, DataTable)
 

Execute commant text against connection and add or refresh rows in DataTable

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
int Fill(
	string commandText,
	DataTable dataTable
)
```

**VB**<br />
``` VB
Function Fill ( 
	commandText As String,
	dataTable As DataTable
) As Integer
```

**C++**<br />
``` C++
int Fill(
	String^ commandText, 
	DataTable^ dataTable
)
```

**F#**<br />
``` F#
abstract Fill : 
        commandText : string * 
        dataTable : DataTable -> int 

```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br /></dd><dt>dataTable</dt><dd>Type: System.Data.DataTable<br />A DataTable to fill with records and, if necessary, schema</dd></dl>

#### Return Value
Type: Int32<br />\[Missing <returns> documentation for "M:SimpleAccess.Core.ISimpleAccess`6.Fill(System.String,System.Data.DataTable)"\]

## See Also


#### Reference
<a href="T_SimpleAccess_Core_ISimpleAccess_6">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) Interface</a><br /><a href="Overload_SimpleAccess_Core_ISimpleAccess_6_Fill">Fill Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />