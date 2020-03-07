# ISqlSimpleAccess.BuildSqlParameters Method 
 

Build SqlParameter Array from dynamic object.

**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
SqlParameter[] BuildSqlParameters(
	Object paramObject
)
```

**VB**<br />
``` VB
Function BuildSqlParameters ( 
	paramObject As Object
) As SqlParameter()
```

**C++**<br />
``` C++
array<SqlParameter^>^ BuildSqlParameters(
	Object^ paramObject
)
```

**F#**<br />
``` F#
abstract BuildSqlParameters : 
        paramObject : Object -> SqlParameter[] 

```


#### Parameters
&nbsp;<dl><dt>paramObject</dt><dd>Type: System.Object<br />The dynamic object as parameters.</dd></dl>

#### Return Value
Type: SqlParameter[]<br />SqlParameter[] object and if paramObject is null then return null

## See Also


#### Reference
<a href="T_SimpleAccess_SqlServer_ISqlSimpleAccess">ISqlSimpleAccess Interface</a><br /><a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />