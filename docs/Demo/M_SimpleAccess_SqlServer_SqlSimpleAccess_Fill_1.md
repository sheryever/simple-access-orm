# SqlSimpleAccess.Fill Method (String, DataTable)
 

Execute commant text against connection and add or refresh rows in DataTable

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Fill(
	string commandText,
	DataTable dataTable
)
```

**VB**<br />
``` VB
Public Function Fill ( 
	commandText As String,
	dataTable As DataTable
) As Integer
```

**C++**<br />
``` C++
public:
virtual int Fill(
	String^ commandText, 
	DataTable^ dataTable
) sealed
```

**F#**<br />
``` F#
abstract Fill : 
        commandText : string * 
        dataTable : DataTable -> int 
override Fill : 
        commandText : string * 
        dataTable : DataTable -> int 
```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br /></dd><dt>dataTable</dt><dd>Type: System.Data.DataTable<br />A DataTable to fill with records and, if necessary, schema</dd></dl>

#### Return Value
Type: Int32<br />\[Missing <returns> documentation for "M:SimpleAccess.SqlServer.SqlSimpleAccess.Fill(System.String,System.Data.DataTable)"\]

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_Fill_1">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).Fill(String, DataTable)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_Fill">Fill Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />