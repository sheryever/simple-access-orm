# DataReaderToObjectExtensions.DataReaderToObject Method 
 


## Overload List
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_DataReaderToObjectExtensions_DataReaderToObject__1">DataReaderToObject(TType)(IDataReader, String, Dictionary(String, PropertyInfo), Dictionary(String, PropertyInfo))</a></td><td>
Created the object of TType and populates the properties of that object from a single DataReader row using Reflection by matching the DataReader fields to a public property of the object. Unmatched properties are left unchanged. You need to pass in a data reader located on the active row you want to serialize.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_DataReaderToObjectExtensions_DataReaderToObject">DataReaderToObject(IDataReader, Object, String, Dictionary(String, PropertyInfo), Dictionary(String, PropertyInfo))</a></td><td>
Populates the properties of an object from a single DataReader row using Reflection by matching the DataReader fields to a public property on the object passed in. Unmatched properties are left unchanged. You need to pass in a data reader located on the active row you want to serialize. This routine works best for matching pure data entities and should be used only in low volume environments where retrieval speed is not critical due to its use of Reflection.</td></tr></table>&nbsp;
<a href="#datareadertoobjectextensions.datareadertoobject-method">Back to Top</a>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_DataReaderToObjectExtensions">DataReaderToObjectExtensions Class</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />