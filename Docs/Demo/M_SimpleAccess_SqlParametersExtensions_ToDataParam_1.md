# SqlParametersExtensions.ToDataParam Method (String, String, Int32)
 

Create and returns a SqlParameter of attached struct type. The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters

**Namespace:**&nbsp;<a href="N_SimpleAccess">SimpleAccess</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public static SqlParameter ToDataParam(
	this string value,
	string paramName,
	int size
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function ToDataParam ( 
	value As String,
	paramName As String,
	size As Integer
) As SqlParameter
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static SqlParameter^ ToDataParam(
	String^ value, 
	String^ paramName, 
	int size
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member ToDataParam : 
        value : string * 
        paramName : string * 
        size : int -> SqlParameter 

```


#### Parameters
&nbsp;<dl><dt>value</dt><dd>Type: System.String<br />The value of attached variable.</dd><dt>paramName</dt><dd>Type: System.String<br />SqlParameter Name</dd><dt>size</dt><dd>Type: System.Int32<br />The length of the string value in the SqlParameters</dd></dl>

#### Return Value
Type: SqlParameter<br />\[Missing <returns> documentation for "M:SimpleAccess.SqlParametersExtensions.ToDataParam(System.String,System.String,System.Int32)"\]

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type String. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_SimpleAccess_SqlParametersExtensions">SqlParametersExtensions Class</a><br /><a href="Overload_SimpleAccess_SqlParametersExtensions_ToDataParam">ToDataParam Overload</a><br /><a href="N_SimpleAccess">SimpleAccess Namespace</a><br />