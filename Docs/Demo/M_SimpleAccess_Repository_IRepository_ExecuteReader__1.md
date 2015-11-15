# IRepository.ExecuteReader(*TEntity*) Method (SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])
 

Executes the scalar operation.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
List<TEntity> ExecuteReader<TEntity>(
	SqlTransaction sqlTransaction,
	string sql,
	CommandType commandType = CommandType.StoredProcedure,
	string fieldsToSkip = null,
	Dictionary<string, PropertyInfo> piList = null,
	params SqlParameter[] sqlParameters
)
where TEntity : new()

```

**VB**<br />
``` VB
Function ExecuteReader(Of TEntity As New) ( 
	sqlTransaction As SqlTransaction,
	sql As String,
	Optional commandType As CommandType = CommandType.StoredProcedure,
	Optional fieldsToSkip As String = Nothing,
	Optional piList As Dictionary(Of String, PropertyInfo) = Nothing,
	ParamArray sqlParameters As SqlParameter()
) As List(Of TEntity)
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : gcnew()
List<TEntity>^ ExecuteReader(
	SqlTransaction^ sqlTransaction, 
	String^ sql, 
	CommandType commandType = CommandType::StoredProcedure, 
	String^ fieldsToSkip = nullptr, 
	Dictionary<String^, PropertyInfo^>^ piList = nullptr, 
	... array<SqlParameter^>^ sqlParameters
)
```

**F#**<br />
``` F#
abstract ExecuteReader : 
        sqlTransaction : SqlTransaction * 
        sql : string * 
        ?commandType : CommandType * 
        ?fieldsToSkip : string * 
        ?piList : Dictionary<string, PropertyInfo> * 
        sqlParameters : SqlParameter[] 
(* Defaults:
        let _commandType = defaultArg commandType CommandType.StoredProcedure
        let _fieldsToSkip = defaultArg fieldsToSkip null
        let _piList = defaultArg piList null
*)
-> List<'TEntity>  when 'TEntity : new()

```


#### Parameters
&nbsp;<dl><dt>sqlTransaction</dt><dd>Type: System.Data.SqlClient.SqlTransaction<br />The SQL transaction.</dd><dt>sql</dt><dd>Type: System.String<br />The SQL.</dd><dt>commandType (Optional)</dt><dd>Type: System.Data.CommandType<br />Type of the command.</dd><dt>fieldsToSkip (Optional)</dt><dd>Type: System.String<br />(optional) the fields to skip.</dd><dt>piList (Optional)</dt><dd>Type: System.Collections.Generic.Dictionary(String, PropertyInfo)<br />(optional) dictionary of property name and PropertyInfo object.</dd><dt>sqlParameters</dt><dd>Type: System.Data.SqlClient.SqlParameter[]<br />Options for controlling the SQL.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Generic type parameter.</dd></dl>

#### Return Value
Type: List(*TEntity*)<br />\[Missing <returns> documentation for "M:SimpleAccess.Repository.IRepository.ExecuteReader``1(System.Data.SqlClient.SqlTransaction,System.String,System.Data.CommandType,System.String,System.Collections.Generic.Dictionary{System.String,System.Reflection.PropertyInfo},System.Data.SqlClient.SqlParameter[])"\]

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td>Exception</td><td>Thrown when an exception error condition occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_IRepository">IRepository Interface</a><br /><a href="Overload_SimpleAccess_Repository_IRepository_ExecuteReader">ExecuteReader Overload</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />