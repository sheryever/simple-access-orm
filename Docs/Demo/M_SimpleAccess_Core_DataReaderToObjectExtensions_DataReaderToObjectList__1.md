# DataReaderToObjectExtensions.DataReaderToObjectList(*TType*) Method 
 

Creates a list of a given type from all the rows in a DataReader. Note this method uses Reflection so this isn't a high performance operation, but it can be useful for generic data reader to entity conversions on the fly and with anonymous types.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public static List<TType> DataReaderToObjectList<TType>(
	this IDataReader reader,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> piList = null,
	Dictionary<string, PropertyInfo> piListBasedOnDbColumn = null
)
where TType : new()

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function DataReaderToObjectList(Of TType As New) ( 
	reader As IDataReader,
	Optional fieldsToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing,
	Optional piListBasedOnDbColumn As Dictionary(Of String, PropertyInfo) = Nothing
) As List(Of TType)
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
generic<typename TType>
where TType : gcnew()
static List<TType>^ DataReaderToObjectList(
	IDataReader^ reader, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piListBasedOnDbColumn = nullptr
)
```

**F#**<br />
``` F#
[<ExtensionAttribute>]
static member DataReaderToObjectList : 
        reader : IDataReader * 
        ?fieldsToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> * 
        ?piListBasedOnDbColumn : Dictionary<string, PropertyInfo> 
(* Defaults:
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _piList = defaultArg piList null
        let _piListBasedOnDbColumn = defaultArg piListBasedOnDbColumn null
*)
-> List<'TType>  when 'TType : new()

```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: System.Data.IDataReader<br />An open DataReader that's in position to read</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />Optional - comma delimited list of fields that you don't want to update</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />Optional - Cached PropertyInfo dictionary that holds property info data for this object. Can be used for caching hte PropertyInfo structure for multiple operations to speed up translation. If not passed automatically created.</dd><dt>piListBasedOnDbColumn (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />List of PropertyInfo object having <a href="T_SimpleAccess_DbColumnAttribute">DbColumnAttribute</a> in it's custom attributes</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TType</dt><dd /></dl>

#### Return Value
Type: List(*TType*)<br />\[Missing <returns> documentation for "M:SimpleAccess.Core.DataReaderToObjectExtensions.DataReaderToObjectList``1(System.Data.IDataReader,System.String,System.Collections.Generic.Dictionary{System.String,System.Reflection.PropertyInfo},System.Collections.Generic.Dictionary{System.String,System.Reflection.PropertyInfo})"\]

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type IDataReader. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_SimpleAccess_Core_DataReaderToObjectExtensions">DataReaderToObjectExtensions Class</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />