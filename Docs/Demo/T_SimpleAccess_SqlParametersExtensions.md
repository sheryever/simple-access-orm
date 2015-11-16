# SqlParametersExtensions Class
 

Defines the extra extensions method for SqlParameter.


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.SqlParametersExtensions<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess">SimpleAccess</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public static class SqlParametersExtensions
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public NotInheritable Class SqlParametersExtensions
```

**C++**<br />
``` C++
[ExtensionAttribute]
public ref class SqlParametersExtensions abstract sealed
```

**F#**<br />
``` F#
[<AbstractClassAttribute>]
[<SealedAttribute>]
[<ExtensionAttribute>]
type SqlParametersExtensions =  class end
```

The SqlParametersExtensions type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_CreateSqlParametersFromDynamic">CreateSqlParametersFromDynamic(List(SqlParameter), Object)</a></td><td>
Takes the dynamic object and creates the Sql Parameters from its properties</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_CreateSqlParametersFromDynamic_1">CreateSqlParametersFromDynamic(SqlParameter[], Object)</a></td><td>
Takes the dynamic object and creates the Sql Parameters from its properties</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToDataParam">ToDataParam(String, String)</a></td><td>
Create and returns a SqlParameter of attached string. The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToDataParam_1">ToDataParam(String, String, Int32)</a></td><td>
Create and returns a SqlParameter of attached struct type. The method also avoid the Sql Injection by replacing single qoute "'" character with tow single qoutes "''" characters</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToDataParam__1">ToDataParam(T)(Nullable(T), String)</a></td><td>
Create and returns a SqlParameter of attached nullable struct type.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToDataParam__1_2">ToDataParam(T)(T, String)</a></td><td>
Create and returns a SqlParameter of attached struct type.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToDataParam__1_1">ToDataParam(T)(Nullable(T), String, SqlDbType)</a></td><td>
Create and returns a SqlParameter of attached nullable struct type.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToDataParam__1_3">ToDataParam(T)(T, String, SqlDbType)</a></td><td>
Create and returns a SqlParameter of attached struct type.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_SqlParametersExtensions_ToSafeDataParam">ToSafeDataParam</a></td><td>
Create and returns a SqlParameter of attached struct type. The method allow to pass "'" character to database</td></tr></table>&nbsp;
<a href="#sqlparametersextensions-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess">SimpleAccess Namespace</a><br />