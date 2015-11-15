# SqlSimpleAccess.Fill Method (String, DataSet)
 

Execute commant text against connection and add or refresh rows in DataSet

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public int Fill(
	string commandText,
	DataSet dataSet
)
```

**VB**<br />
``` VB
Public Function Fill ( 
	commandText As String,
	dataSet As DataSet
) As Integer
```

**C++**<br />
``` C++
public:
virtual int Fill(
	String^ commandText, 
	DataSet^ dataSet
) sealed
```

**F#**<br />
``` F#
abstract Fill : 
        commandText : string * 
        dataSet : DataSet -> int 
override Fill : 
        commandText : string * 
        dataSet : DataSet -> int 
```


#### Parameters
&nbsp;<dl><dt>commandText</dt><dd>Type: System.String<br />The SQL statement, table name or stored procedure to execute at the data source.</dd><dt>dataSet</dt><dd>Type: System.Data.DataSet<br />A DataSet to fill with records and, if necessary, schema</dd></dl>

#### Return Value
Type: Int32<br />\[Missing <returns> documentation for "M:SimpleAccess.SqlServer.SqlSimpleAccess.Fill(System.String,System.Data.DataSet)"\]

#### Implements
<a href="M_SimpleAccess_Core_ISimpleAccess_6_Fill">ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder).Fill(String, DataSet)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="Overload_SimpleAccess_SqlServer_SqlSimpleAccess_Fill">Fill Overload</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />