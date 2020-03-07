# SqlParametersExtensions.ToDataParam(*T*) Method (*T*, String, SqlDbType)
 

Create and returns a SqlParameter of attached struct type.

**Namespace:**&nbsp;<a href="N_SimpleAccess">SimpleAccess</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public static SqlParameter ToDataParam<T>(
	this T value,
	string paramName,
	SqlDbType sqlDbType
)
where T : struct, new()

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function ToDataParam(Of T As {Structure, New}) ( 
	value As T,
	paramName As String,
	sqlDbType As SqlDbType
) As SqlParameter
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
generic<typename T>
where T : value class, gcnew()
static SqlParameter^ ToDataParam(
	T value, 
	String^ paramName, 
	SqlDbType sqlDbType
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member ToDataParam : 
        value : 'T * 
        paramName : string * 
        sqlDbType : SqlDbType -> SqlParameter  when 'T : struct, new()

```


#### Parameters
&nbsp;<dl><dt>value</dt><dd>Type: *T*<br />The value of attached variable.</dd><dt>paramName</dt><dd>Type: System.String<br />SqlParameter Name</dd><dt>sqlDbType</dt><dd>Type: System.Data.SqlDbType<br />The SqlDbType of the SqlParameter</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Attached by variable type.</dd></dl>

#### Return Value
Type: SqlParameter<br />\[Missing <returns> documentation for "M:SimpleAccess.SqlParametersExtensions.ToDataParam``1(``0,System.String,System.Data.SqlDbType)"\]

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type . When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_SimpleAccess_SqlParametersExtensions">SqlParametersExtensions Class</a><br /><a href="Overload_SimpleAccess_SqlParametersExtensions_ToDataParam">ToDataParam Overload</a><br /><a href="N_SimpleAccess">SimpleAccess Namespace</a><br />