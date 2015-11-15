# SqlSimpleAccess.BuildSqlParameters Method 
 

Build SqlParameter Array from dynamic object.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public SqlParameter[] BuildSqlParameters(
	Object paramObject
)
```

**VB**<br />
``` VB
Public Function BuildSqlParameters ( 
	paramObject As Object
) As SqlParameter()
```

**C++**<br />
``` C++
public:
virtual array<SqlParameter^>^ BuildSqlParameters(
	Object^ paramObject
) sealed
```

**F#**<br />
``` F#
abstract BuildSqlParameters : 
        paramObject : Object -> SqlParameter[] 
override BuildSqlParameters : 
        paramObject : Object -> SqlParameter[] 
```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: SqlParameter[]<br />SqlParameter[] object and if paramObject is null then return null

#### Implements
<a href="M_SimpleAccess_SqlServer_ISqlSimpleAccess_BuildSqlParameters">ISqlSimpleAccess.BuildSqlParameters(Object)</a><br />

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_SqlSimpleAccess">SqlSimpleAccess Class</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />