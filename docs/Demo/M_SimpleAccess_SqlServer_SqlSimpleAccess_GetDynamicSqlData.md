# SqlSimpleAccess.GetDynamicSqlData Method 
 

Gets a dynamic SQL data.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public IList<Object> GetDynamicSqlData(
	SqlDataReader reader
)
```

**VB**<br />
``` VB
Public Function GetDynamicSqlData ( 
	reader As SqlDataReader
) As IList(Of Object)
```

**C++**<br />
``` C++
public:
virtual IList<Object^>^ GetDynamicSqlData(
	SqlDataReader^ reader
) sealed
```

**F#**<br />
``` F#
abstract GetDynamicSqlData : 
        reader : SqlDataReader -> IList<Object> 
override GetDynamicSqlData : 
        reader : SqlDataReader -> IList<Object> 
```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: System.Data.SqlClient.SqlDataReader<br />The reader.</dd></dl>

#### Return Value
Type: IList(Object)<br />The dynamic SQL data.

#### Implements
<a href="M_SimpleAccess_SqlServer_ISqlSimpleAccess_GetDynamicSqlData">ISqlSimpleAccess.GetDynamicSqlData(SqlDataReader)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />