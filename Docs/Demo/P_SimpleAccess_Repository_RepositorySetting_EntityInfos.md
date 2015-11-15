# RepositorySetting.EntityInfos Property 
 

The Dictionary of EntityInfos for cache.

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public static Dictionary<int, EntityInfo> EntityInfos { get; set; }
```

**VB**<br />
``` VB
Public Shared Property EntityInfos As Dictionary(Of Integer, EntityInfo)
	Get
	Set
```

**C++**<br />
``` C++
public:
static property Dictionary<int, EntityInfo^>^ EntityInfos {
	Dictionary<int, EntityInfo^>^ get ();
	void set (Dictionary<int, EntityInfo^>^ value);
}
```

**F#**<br />
``` F#
static member EntityInfos : Dictionary<int, EntityInfo> with get, set

```


#### Property Value
Type: Dictionary(Int32, <a href="T_SimpleAccess_Entity_EntityInfo">EntityInfo</a>)

## See Also


#### Reference
<a href="T_SimpleAccess_Repository_RepositorySetting">RepositorySetting Class</a><br /><a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />