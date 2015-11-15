# SimpleAccessSettings Constructor (CommandType, ISimpleLogger)
 

Initialize the new object of SimpleAccessSettings with default properties.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public SimpleAccessSettings(
	CommandType defaultCommandType,
	ISimpleLogger defaultLogger
)
```

**VB**<br />
``` VB
Public Sub New ( 
	defaultCommandType As CommandType,
	defaultLogger As ISimpleLogger
)
```

**C++**<br />
``` C++
public:
SimpleAccessSettings(
	CommandType defaultCommandType, 
	ISimpleLogger^ defaultLogger
)
```

**F#**<br />
``` F#
new : 
        defaultCommandType : CommandType * 
        defaultLogger : ISimpleLogger -> SimpleAccessSettings
```


#### Parameters
&nbsp;<dl><dt>defaultCommandType</dt><dd>Type: System.Data.CommandType<br />The default CommandType of this new object</dd><dt>defaultLogger</dt><dd>Type: <a href="T_SimpleAccess_Core_Logger_ISimpleLogger">SimpleAccess.Core.Logger.ISimpleLogger</a><br />The default <a href="T_SimpleAccess_Core_Logger_ISimpleLogger">ISimpleLogger</a> implementaion for parent SimpleAccess object</dd></dl>

## See Also


#### Reference
<a href="T_SimpleAccess_Core_SimpleAccessSettings">SimpleAccessSettings Class</a><br /><a href="Overload_SimpleAccess_Core_SimpleAccessSettings__ctor">SimpleAccessSettings Overload</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />