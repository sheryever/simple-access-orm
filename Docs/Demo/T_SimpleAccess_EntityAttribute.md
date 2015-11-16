# EntityAttribute Class
 

Specifies the database table/view name of the Entity.


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;System.Attribute<br />&nbsp;&nbsp;&nbsp;&nbsp;SimpleAccess.EntityAttribute<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess">SimpleAccess</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public class EntityAttribute : Attribute
```

**VB**<br />
``` VB
Public Class EntityAttribute
	Inherits Attribute
```

**C++**<br />
``` C++
public ref class EntityAttribute : public Attribute
```

**F#**<br />
``` F#
type EntityAttribute =  
    class
        inherit Attribute
    end
```

The EntityAttribute type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_EntityAttribute__ctor">EntityAttribute(String)</a></td><td>
Specifies the database table/view name of the Entity.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_EntityAttribute__ctor_1">EntityAttribute(String, DbObjectType)</a></td><td>
Specifies the database table/view name of the Entity.</td></tr></table>&nbsp;
<a href="#entityattribute-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_EntityAttribute_EntityName">EntityName</a></td><td>
Database table/view name.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_EntityAttribute_Type">Type</a></td><td>
Object type in the database (Table/View)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td>TypeId</td><td>
When implemented in a derived class, gets a unique identifier for this Attribute.
 (Inherited from Attribute.)</td></tr></table>&nbsp;
<a href="#entityattribute-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Equals</td><td>
Returns a value that indicates whether this instance is equal to a specified object.
 (Inherited from Attribute.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>Finalize</td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetHashCode</td><td>
Returns the hash code for this instance.
 (Inherited from Attribute.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetType</td><td>
Gets the Type of the current instance.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>IsDefaultAttribute</td><td>
When overridden in a derived class, indicates whether the value of this instance is the default value for the derived class.
 (Inherited from Attribute.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Match</td><td>
When overridden in a derived class, returns a value that indicates whether this instance equals a specified object.
 (Inherited from Attribute.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>MemberwiseClone</td><td>
Creates a shallow copy of the current Object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>ToString</td><td>
Returns a string that represents the current object.
 (Inherited from Object.)</td></tr></table>&nbsp;
<a href="#entityattribute-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess">SimpleAccess Namespace</a><br />