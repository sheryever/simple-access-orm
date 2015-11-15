# SimpleAccessSettings Class
 

Used for setting up the default setting of SimpleAccess.


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.Core.SimpleAccessSettings<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public class SimpleAccessSettings
```

**VB**<br />
``` VB
Public Class SimpleAccessSettings
```

**C++**<br />
``` C++
public ref class SimpleAccessSettings
```

**F#**<br />
``` F#
type SimpleAccessSettings =  class end
```

The SimpleAccessSettings type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_SimpleAccessSettings__ctor">SimpleAccessSettings()</a></td><td>
Initialize the new object of SimpleAccessSettings with default properties.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_SimpleAccessSettings__ctor_1">SimpleAccessSettings(CommandType)</a></td><td>
Initialize the new object of SimpleAccessSettings with default properties.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_SimpleAccessSettings__ctor_2">SimpleAccessSettings(CommandType, ISimpleLogger)</a></td><td>
Initialize the new object of SimpleAccessSettings with default properties.</td></tr></table>&nbsp;
<a href="#simpleaccesssettings-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_SimpleAccessSettings_DefaultCommandType">DefaultCommandType</a></td><td>
Default property of CommandType.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_SimpleAccessSettings_DefaultLogger">DefaultLogger</a></td><td>
Default <a href="T_SimpleAccess_Core_Logger_ISimpleLogger">ISimpleLogger</a>.</td></tr></table>&nbsp;
<a href="#simpleaccesssettings-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Equals</td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from Object.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>Finalize</td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetHashCode</td><td>
Serves as the default hash function.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_SimpleAccessSettings_GetProperConnectionString">GetProperConnectionString</a></td><td>
Check for the connection is a connectionString name from the config or a complete database connetion string and return the connnection string.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetType</td><td>
Gets the Type of the current instance.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_SimpleAccess_Core_SimpleAccessSettings_LoadConnectionStringSettingsFromConfigurationFile">LoadConnectionStringSettingsFromConfigurationFile</a></td><td>
Load and returns the ConnectionStringSettings from the default config file based on provided contection string name.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>MemberwiseClone</td><td>
Creates a shallow copy of the current Object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>ToString</td><td>
Returns a string that represents the current object.
 (Inherited from Object.)</td></tr></table>&nbsp;
<a href="#simpleaccesssettings-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />