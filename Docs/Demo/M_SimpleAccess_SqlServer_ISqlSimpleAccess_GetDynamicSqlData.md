# ISqlSimpleAccess.GetDynamicSqlData Method 
 

Gets a dynamic SQL data.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
IList<Object> GetDynamicSqlData(
	SqlDataReader reader
)
```

**VB**<br />
``` VB
Function GetDynamicSqlData ( 
	reader As SqlDataReader
) As IList(Of Object)
```

**C++**<br />
``` C++
IList<Object^>^ GetDynamicSqlData(
	SqlDataReader^ reader
)
```

**F#**<br />
``` F#
abstract GetDynamicSqlData : 
        reader : SqlDataReader -> IList<Object> 

```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: System.Data.SqlClient.SqlDataReader<br />The reader.</dd></dl>

#### Return Value
Type: IList(Object)<br />The dynamic SQL data.

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_ISqlSimpleAccess">ISqlSimpleAccess Interface</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />