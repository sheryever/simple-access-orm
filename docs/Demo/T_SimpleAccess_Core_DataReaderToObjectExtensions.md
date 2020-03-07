# DataReaderToObjectExtensions Class
 

Extension to load objects from DataReaders


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.Core.DataReaderToObjectExtensions<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public static class DataReaderToObjectExtensions
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public NotInheritable Class DataReaderToObjectExtensions
```

**C++**<br />
``` C++
[ExtensionAttribute]
public ref class DataReaderToObjectExtensions abstract sealed
```

**F#**<br />
``` F#
[<AbstractClassAttribute>]
[<SealedAttribute>]
[<ExtensionAttribute>]
type DataReaderToObjectExtensions =  class end
```

The DataReaderToObjectExtensions type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_DataReaderToObjectExtensions_DataReaderToObject">DataReaderToObject(IDataReader, Object, String, Dictionary(String, PropertyInfo), Dictionary(String, PropertyInfo))</a></td><td>
Populates the properties of an object from a single DataReader row using Reflection by matching the DataReader fields to a public property on the object passed in. Unmatched properties are left unchanged. You need to pass in a data reader located on the active row you want to serialize. This routine works best for matching pure data entities and should be used only in low volume environments where retrieval speed is not critical due to its use of Reflection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_DataReaderToObjectExtensions_DataReaderToObject__1">DataReaderToObject(TType)(IDataReader, String, Dictionary(String, PropertyInfo), Dictionary(String, PropertyInfo))</a></td><td>
Created the object of TType and populates the properties of that object from a single DataReader row using Reflection by matching the DataReader fields to a public property of the object. Unmatched properties are left unchanged. You need to pass in a data reader located on the active row you want to serialize.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_DataReaderToObjectExtensions_DataReaderToObjectList__1">DataReaderToObjectList(TType)</a></td><td>
Creates a list of a given type from all the rows in a DataReader. Note this method uses Reflection so this isn't a high performance operation, but it can be useful for generic data reader to entity conversions on the fly and with anonymous types.</td></tr></table>&nbsp;
<a href="#datareadertoobjectextensions-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />