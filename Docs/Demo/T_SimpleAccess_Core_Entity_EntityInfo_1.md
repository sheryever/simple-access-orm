# EntityInfo(*TParameterBuilder*) Class
 

Represents the SimpleAccess Entity information. The EntityInfo is create and cache the stored procedure name, queris and parameters


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.Core.Entity.EntityInfo(TParameterBuilder)<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess_Core_Entity">SimpleAccess.Core.Entity</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public class EntityInfo<TParameterBuilder>
where TParameterBuilder : new(), IParameterBuilder

```

**VB**<br />
``` VB
Public Class EntityInfo(Of TParameterBuilder As {New, IParameterBuilder})
```

**C++**<br />
``` C++
generic<typename TParameterBuilder>
where TParameterBuilder : gcnew(), IParameterBuilder
public ref class EntityInfo
```

**F#**<br />
``` F#
type EntityInfo<'TParameterBuilder when 'TParameterBuilder : new() and IParameterBuilder> =  class end
```


#### Type Parameters
&nbsp;<dl><dt>TParameterBuilder</dt><dd>\[Missing <typeparam name="TParameterBuilder"/> documentation for "T:SimpleAccess.Core.Entity.EntityInfo`1"\]</dd></dl>&nbsp;
The EntityInfo(TParameterBuilder) type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_Entity_EntityInfo_1__ctor">EntityInfo(TParameterBuilder)</a></td><td>
Initialize the new object</td></tr></table>&nbsp;
<a href="#entityinfo(*tparameterbuilder*)-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_DbObjectName">DbObjectName</a></td><td>
Table/View Name of the Entity extracted from the <a href="T_SimpleAccess_EntityAttribute">EntityAttribute</a> if the Entity is marked with it, otherwise the same name of Entity</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_DeleteStatment">DeleteStatment</a></td><td /></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_EntityType">EntityType</a></td><td>
The Type of the Entity.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_InsertParameters">InsertParameters</a></td><td>
Get the Insert statement or StoredProcedure Paramenters based on TDataParameters in ISimple</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_InsertStatement">InsertStatement</a></td><td /></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_SelectAllStatement">SelectAllStatement</a></td><td /></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_StoredProcedureNameKeyWord">StoredProcedureNameKeyWord</a></td><td>
Stored procedure prefix of the Entity extracted from the <a href="T_SimpleAccess_StoredProcedureNameKeyWordAttribute">StoredProcedureNameKeyWordAttribute</a> if the Entity is marked with it, otherwise the same name of Entity</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_UpdateParameters">UpdateParameters</a></td><td></td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_Entity_EntityInfo_1_UpdateSatetment">UpdateSatetment</a></td><td /></tr></table>&nbsp;
<a href="#entityinfo(*tparameterbuilder*)-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_Entity_EntityInfo_1_ClearSpParameters">ClearSpParameters</a></td><td>
Clear all DbParamters</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_Entity_EntityInfo_1_CreateSqlParametersFromProperties">CreateSqlParametersFromProperties</a></td><td>
Create paramters from object properties</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Equals</td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from Object.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>Finalize</td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetHashCode</td><td>
Serves as the default hash function.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetType</td><td>
Gets the Type of the current instance.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_Entity_EntityInfo_1_LoadOutParametersProperties">LoadOutParametersProperties</a></td><td>
Load all the properties from DbParameters which were marked as ParameterDirection.Out</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>MemberwiseClone</td><td>
Creates a shallow copy of the current Object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>ToString</td><td>
Returns a string that represents the current object.
 (Inherited from Object.)</td></tr></table>&nbsp;
<a href="#entityinfo(*tparameterbuilder*)-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_Core_Entity">SimpleAccess.Core.Entity Namespace</a><br />