# DataReaderToObjectExtensions.DataReaderToObject Method (IDataReader, Object, String, Dictionary(String, PropertyInfo), Dictionary(String, PropertyInfo))
 

Populates the properties of an object from a single DataReader row using Reflection by matching the DataReader fields to a public property on the object passed in. Unmatched properties are left unchanged. You need to pass in a data reader located on the active row you want to serialize. This routine works best for matching pure data entities and should be used only in low volume environments where retrieval speed is not critical due to its use of Reflection.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public static void DataReaderToObject(
	this IDataReader reader,
	Object instance,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> piList = null,
	Dictionary<string, PropertyInfo> piListBasedOnDbColumn = null
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub DataReaderToObject ( 
	reader As IDataReader,
	instance As Object,
	Optional fieldsToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing,
	Optional piListBasedOnDbColumn As Dictionary(Of String, PropertyInfo) = Nothing
)
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static void DataReaderToObject(
	IDataReader^ reader, 
	Object^ instance, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piListBasedOnDbColumn = nullptr
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member DataReaderToObject : 
        reader : IDataReader * 
        instance : Object * 
        ?fieldsToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> * 
        ?piListBasedOnDbColumn : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _piList = defaultArg piList null
        let _piListBasedOnDbColumn = defaultArg piListBasedOnDbColumn null
*)
-> unit 

```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: System.Data.IDataReader<br />Instance of the DataReader to read data from. Should be located on the correct record (Read() should have been called on it before calling this method)</dd><dt>instance</dt><dd>Type: System.Object<br />Instance of the object to populate properties on</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />Optional - A comma delimited list of object properties that should not be updated</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />Optional - Cached PropertyInfo dictionary that holds property info data for this object</dd><dt>piListBasedOnDbColumn (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />List of PropertyInfo object having <a href="T_SimpleAccess_DbColumnAttribute">DbColumnAttribute</a> in it's custom attributes</dd></dl>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type IDataReader. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_SimpleAccess_Core_DataReaderToObjectExtensions">DataReaderToObjectExtensions Class</a><br /><a href="Overload_SimpleAccess_Core_DataReaderToObjectExtensions_DataReaderToObject">DataReaderToObject Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />