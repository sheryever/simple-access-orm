# SqlRepository Class
 

Implements SqlRepository base SqlSimpleAccess with command type stored procedures.


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.SqlServer.Repository.SqlRepository<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public class SqlRepository : ISqlRepository, 
	IDisposable
```

**VB**<br />
``` VB
Public Class SqlRepository
	Implements ISqlRepository, IDisposable
```

**C++**<br />
``` C++
public ref class SqlRepository : ISqlRepository, 
	IDisposable
```

**F#**<br />
``` F#
type SqlRepository =  
    class
        interface ISqlRepository
        interface IDisposable
    end
```

The SqlRepository type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository__ctor">SqlRepository()</a></td><td>
Default constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository__ctor_2">SqlRepository(String)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository__ctor_1">SqlRepository(ISqlSimpleAccess)</a></td><td>
Constructor.</td></tr></table>&nbsp;
<a href="#sqlrepository-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_SqlServer_Repository_SqlRepository_SimpleAccess">SimpleAccess</a></td><td>
The SQL connection.</td></tr></table>&nbsp;
<a href="#sqlrepository-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Delete__1">Delete(TEntity)(SqlParameter[])</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Delete__1_3">Delete(TEntity)(Int64)</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Delete__1_4">Delete(TEntity)(Object)</a></td><td>
Deletes the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Delete__1_1">Delete(TEntity)(SqlTransaction, SqlParameter[])</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Delete__1_2">Delete(TEntity)(SqlTransaction, Int64)</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Dispose">Dispose</a></td><td>
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Equals</td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from Object.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>Finalize</td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get">Get(String, SqlParameter, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get_1">Get(String, Int64, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get_2">Get(String, Object, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get__1">Get(TEntity)(SqlParameter, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get__1_1">Get(TEntity)(SqlTransaction, SqlParameter, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get__1_2">Get(TEntity)(Int64, SqlTransaction, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Get__1_3">Get(TEntity)(Object, SqlTransaction, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_GetAll__1">GetAll(TEntity)</a></td><td>
Enumerates get all in this collection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetHashCode</td><td>
Serves as the default hash function.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetType</td><td>
Gets the Type of the current instance.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Insert__1_1">Insert(TEntity)(SqlParameter[])</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Insert__1_3">Insert(TEntity)(Object)</a></td><td>
Inserts the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Insert__1">Insert(TEntity)(StoredProcedureParameters)</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Insert__1_2">Insert(TEntity)(SqlTransaction, StoredProcedureParameters)</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>MemberwiseClone</td><td>
Creates a shallow copy of the current Object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_SoftDelete__1">SoftDelete(TEntity)</a></td><td>
Soft delete.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>ToString</td><td>
Returns a string that represents the current object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Update__1_1">Update(TEntity)(SqlParameter[])</a></td><td>
Updates the given sqlParameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Update__1_3">Update(TEntity)(Object)</a></td><td>
Updates the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Update__1">Update(TEntity)(StoredProcedureParameters)</a></td><td>
Updates the given sqlParameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_Repository_SqlRepository_Update__1_2">Update(TEntity)(SqlTransaction, StoredProcedureParameters)</a></td><td>
Updates the given sqlParameters.</td></tr></table>&nbsp;
<a href="#sqlrepository-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_SqlServer_Repository">SimpleAccess.SqlServer.Repository Namespace</a><br />