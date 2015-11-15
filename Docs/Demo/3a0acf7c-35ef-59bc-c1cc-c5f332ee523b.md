# ConnectionExtension.OpenSafely Method 
 

A SqlConnection extension method that opens a safely.

**Namespace:**&nbsp;<a href="a16105b5-9ef0-1333-33d4-5a00c99c3614">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public static void OpenSafely(
	this SqlConnection con
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub OpenSafely ( 
	con As SqlConnection
)
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static void OpenSafely(
	SqlConnection^ con
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member OpenSafely : 
        con : SqlConnection -> unit 

```


#### Parameters
&nbsp;<dl><dt>con</dt><dd>Type: System.Data.SqlClient.SqlConnection<br />The con to act on.</dd></dl>

#### Return Value
Type: <br />.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type SqlConnection. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="ae6cdd9e-c590-250c-c081-d18069807f18">ConnectionExtension Class</a><br /><a href="a16105b5-9ef0-1333-33d4-5a00c99c3614">SimpleAccess.Core Namespace</a><br />