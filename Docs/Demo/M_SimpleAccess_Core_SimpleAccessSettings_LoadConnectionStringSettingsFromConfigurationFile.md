# SimpleAccessSettings.LoadConnectionStringSettingsFromConfigurationFile Method 
 

Load and returns the ConnectionStringSettings from the default config file based on provided contection string name.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public static ConnectionStringSettings LoadConnectionStringSettingsFromConfigurationFile(
	string connectionStringName,
	bool force = false
)
```

**VB**<br />
``` VB
Public Shared Function LoadConnectionStringSettingsFromConfigurationFile ( 
	connectionStringName As String,
	Optional force As Boolean = false
) As ConnectionStringSettings
```

**C++**<br />
``` C++
public:
static ConnectionStringSettings^ LoadConnectionStringSettingsFromConfigurationFile(
	String^ connectionStringName, 
	bool force = false
)
```

**F#**<br />
``` F#
static member LoadConnectionStringSettingsFromConfigurationFile : 
        connectionStringName : string * 
        ?force : bool 
(* Defaults:
        let _force = defaultArg force false
*)
-> ConnectionStringSettings 

```


#### Parameters
&nbsp;<dl><dt>connectionStringName</dt><dd>Type: System.String<br />The connection string name</dd><dt>force (Optional)</dt><dd>Type: System.Boolean<br />The ConnectionStringSettings required other wise thorw Exception</dd></dl>

#### Return Value
Type: ConnectionStringSettings<br />\[Missing <returns> documentation for "M:SimpleAccess.Core.SimpleAccessSettings.LoadConnectionStringSettingsFromConfigurationFile(System.String,System.Boolean)"\]

## See Also


#### Reference
<a href="T_SimpleAccess_Core_SimpleAccessSettings">SimpleAccessSettings Class</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />