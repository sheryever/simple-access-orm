# IRepository.ExecuteReaderSingle Method (SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)
 

Executes the reader single operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
Object ExecuteReaderSingle(
	SqlTransaction sqlTransaction,
	string sql,
	CommandType commandType = CommandType.StoredProcedure,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> piList = null,
	Object paramObject = null
)
```

**VB**<br />
``` VB
Function ExecuteReaderSingle ( 
	sqlTransaction As SqlTransaction,
	sql As String,
	Optional commandType As CommandType = CommandType.StoredProcedure,
	Optional fieldsToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing,
	Optional paramObject As Object = Nothing
) As Object
```

**C++**<br />
``` C++
Object^ ExecuteReaderSingle(
	SqlTransaction^ sqlTransaction, 
	String^ sql, 
	CommandType commandType = CommandType::StoredProcedure, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr, 
	Object^ paramObject = nullptr
)
```

**F#**<br />
``` F#
abstract ExecuteReaderSingle : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        ?commandType : CommandType * 
        ?fieldsToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> * 
        ?paramObject : Object 
(* Defaults:
        let _commandType = defaultArg commandType CommandType.StoredProcedure
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _piList = defaultArg piList null
        let _paramObject = defaultArg paramObject null
*)
-> Object 

```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType (Optional)</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd><dt>paramObject (Optional)</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: Object<br />Result in a dynamic object.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_ExecuteReaderSingle">ExecuteReaderSingle Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />