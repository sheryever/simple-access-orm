# SqlParametersExtensions.CreateSqlParametersFromDynamic Method (SqlParameter[], Object)
 

Takes the dynamic object and creates the Sql Parameters from its properties

**Namespace:**&nbsp;<a href="N_SimpleAccess">SimpleAccess</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public static SqlParameter[] CreateSqlParametersFromDynamic(
	this SqlParameter[] sqlParameters,
	Object otherParameters
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function CreateSqlParametersFromDynamic ( 
	sqlParameters As SqlParameter(),
	otherParameters As Object
) As SqlParameter()
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static array<SqlParameter^>^ CreateSqlParametersFromDynamic(
	array<SqlParameter^>^ sqlParameters, 
	Object^ otherParameters
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member CreateSqlParametersFromDynamic : 
        sqlParameters : SqlParameter[] * 
        otherParameters : Object -> SqlParameter[] 

```


#### Parameters
&nbsp;<dl><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br /></dd><dt>otherParameters</dt><dd>Type: System.Object<br /></dd></dl>

#### Return Value
Type: SqlParameter[]<br />\[Missing <returns> documentation for "M:SimpleAccess.SqlParametersExtensions.CreateSqlParametersFromDynamic(System.Data.SqlClient.SqlParameter[],System.Object)"\]

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type . When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_SimpleAccess_SqlParametersExtensions">SqlParametersExtensions Class</a><br /><a href="Overload_SimpleAccess_SqlParametersExtensions_CreateSqlParametersFromDynamic">CreateSqlParametersFromDynamic Overload</a><br /><a href="N_SimpleAccess">SimpleAccess Namespace</a><br />